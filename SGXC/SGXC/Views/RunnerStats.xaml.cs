using System;
using Xamarin.Forms;

namespace SGXC.Views
{
    public partial class RunnerStats : ContentPage
    {
        public RunnerStats()
        {
            InitializeComponent();
            
        }

        private void NumericalAxis_LabelCreated(object sender, Syncfusion.SfChart.XForms.ChartAxisLabelEventArgs e)
        {
            double Value = Convert.ToDouble(e.LabelContent);

            TimeSpan time = TimeSpan.FromMilliseconds(Value);
       
            e.LabelContent = string.Format("{0:mm\\:ss}", time);
            

        }

        private void chart1_DataMarkerLabelCreated(object sender, Syncfusion.SfChart.XForms.ChartDataMarkerLabelCreatedEventArgs e)
        {
            double InputValue = Convert.ToDouble(e.DataMarkerLabel.Label);
            TimeSpan labeltime = TimeSpan.FromMilliseconds(InputValue);
            e.DataMarkerLabel.Label = string.Format("{0:mm\\:ss}", labeltime);
        }
    }
}
