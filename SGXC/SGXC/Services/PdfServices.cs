using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System.IO;
using SQLite;
using System.Data;
using Syncfusion.DataSource;
using SGXC.Models.Helper;
using SGXC.Models;
using Xamarin.Essentials;
using System.Linq;

namespace SGXC.Services
{
    class PdfServices : IPdfServices
    {
        public SQLiteAsyncConnection Connection { get; set; }
        public ITimeServices TimeServices { get; set; }
        public IRunnerServices RunnerServices { get; set; }
        public IEventServices EventServices { get; set; }

        public Event CurrentEvent { get; set; }
        public List<EventWithRunners> EventWithRunners { get; set; }
        public string LocalPath { get; set; }

        //Contructor
        public PdfServices(ITimeServices timeServices, IRunnerServices runnerServices, IEventServices eventServices)
        {
            this.TimeServices = timeServices;
            this.RunnerServices = runnerServices;
            this.EventServices = eventServices;            
        }


        //PDF Creator
        public async Task CreatePDF(int eventId)
        {
            //call method to check if pdf is created
            var created = await checkifcreated(eventId);
            if (created)
            {
                OpenFileRequest();
            }
            else
            {
                CreateFileRequest();
            }          

        }

        private void CreateFileRequest()
        {
            PdfDocument document = new PdfDocument();
            document.PageSettings.Orientation = PdfPageOrientation.Portrait;
            document.PageSettings.Margins.All = 50;
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            //text
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            graphics.DrawString(AppSettings.TeamName, font, PdfBrushes.Black, point: new Syncfusion.Drawing.PointF(30, 0));
            //Image Maybe Try would work better
            //Stream streamImage = File.OpenRead(AppSettings.TeamLogo);
            //if (streamImage != null)
            //{
            //    PdfBitmap image = new PdfBitmap(streamImage);
            //    graphics.DrawImage(image, point: new Syncfusion.Drawing.PointF(20, 20), size: new Syncfusion.Drawing.SizeF(35, 35));
            //}
            //Event Name, location and date
            PdfFont standardFont = new PdfStandardFont(PdfFontFamily.Helvetica, 15);
            PdfTextElement textElement = new PdfTextElement(CurrentEvent.EventName, standardFont);
            PdfLayoutResult layoutResult = textElement.Draw(page, new Syncfusion.Drawing.RectangleF(0, 60, page.GetClientSize().Width, page.GetClientSize().Height));
            if (CurrentEvent.Location!=null)
            {
                textElement.Text = CurrentEvent.Location;
                layoutResult = textElement.Draw(page, new Syncfusion.Drawing.PointF(0, layoutResult.Bounds.Bottom + 5));
            }            
            textElement.Text = CurrentEvent.RaceDate.ToString("d MMM yyyy");
            layoutResult = textElement.Draw(page, new Syncfusion.Drawing.PointF(0, layoutResult.Bounds.Bottom + 5));
            //add a line
            PdfLine line = new PdfLine(new Syncfusion.Drawing.PointF(0, 0), new Syncfusion.Drawing.PointF(page.GetClientSize().Width, 0))
            {
                Pen = PdfPens.DarkGray
            };
            layoutResult = line.Draw(page, new Syncfusion.Drawing.PointF(0, layoutResult.Bounds.Bottom + 5));
            //Add Data            

            DataSource dataSource = new DataSource
            {
                Source = EventWithRunners
            };
            dataSource.GroupDescriptors.Add(new GroupDescriptor("Category")
            {
                PropertyName = "Category",
                KeySelector = (object obj1) =>
                {
                    var item = (obj1 as EventWithRunners);
                    return item.Category.ToString();
                }
            });

            var items = dataSource.Items;

            { 
                foreach (var item in dataSource.Groups)
                {
                    PdfGrid pdfGrid = new PdfGrid();
                    DataTable dataTable = new DataTable();
                    dataTable.TableName = item.Key.ToString();
                    textElement.Text = dataTable.TableName;
                    textElement.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 15);
                    layoutResult = textElement.Draw(page, new Syncfusion.Drawing.PointF(0, layoutResult.Bounds.Bottom + 20));

                    dataTable.Columns.Add("Name");
                    dataTable.Columns.Add("Distance");
                    dataTable.Columns.Add("Time");
                    var laps = item.Items as IEnumerable<EventWithRunners>;
                    foreach (var lap in laps.OrderBy(t => t.RanTime.Times))    //May consider order by time
                    {
                        dataTable.Rows.Add(new Object[] { lap.Runner.Name, lap.RanTime.Distance, lap.RanTime.Times.ToString(@"mm\:ss\.ff") });
                    }

                    pdfGrid.DataSource = dataTable;
                    pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent6);
                    layoutResult = pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(0, layoutResult.Bounds.Bottom));
                }

            }
          
            //Save Stream
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            document.Close(true);
            WriteToDevice(stream);
        }

        

        private async Task<bool> checkifcreated(int eventId)
        {
            CurrentEvent = await EventServices.GetEventById(eventId);
            EventWithRunners = await TimeServices.GetTimesByEventId(eventId);
            LocalPath = GetLocalPath();
            var content=Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            var created = content.Contains(LocalPath) ? true : false;
            return created;
        }

        private string GetLocalPath()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string localFilename = CurrentEvent.RaceDate.Month + CurrentEvent.RaceDate.Day + CurrentEvent.RaceDate.Year + CurrentEvent.EventName + ".pdf";
            string localPath = Path.Combine(documentsPath, localFilename);
            return localPath;
        }

        //PDF Save to disk
        public void WriteToDevice( MemoryStream stream)

        {
            byte[] docBytes = stream.ToArray();
            File.WriteAllBytes(LocalPath, docBytes);
            OpenFileRequest();
            
        }

        //Open pdf in device 
        private async void OpenFileRequest()
        {
            OpenFileRequest openFileRequest = new OpenFileRequest() { File = new ReadOnlyFile(LocalPath, "application/pdf") };
            await Launcher.OpenAsync(openFileRequest);
        }
    }
}
