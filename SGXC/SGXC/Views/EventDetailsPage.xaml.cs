using Syncfusion.SfKanban.XForms;
using System;
using Xamarin.Forms;

namespace SGXC.Views
{
    public partial class EventDetailsPage : ContentPage
    {
        public EventDetailsPage()
        {

            InitializeComponent();

            //if (Device.RuntimePlatform == Device.UWP || Device.RuntimePlatform == Device.iOS)
            //{
            //    kanban.HeaderTemplate = kanban.Resources["ExpandedTemplate"] as DataTemplate;                
            //}


        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {            
                Label label = sender as Label;
                KanbanColumn kanbanColumn = label.BindingContext as KanbanColumn;
                KanbanColumn currentColumn = null;
                foreach (var column in kanban.ActualColumns)
                {
                    if (kanbanColumn.Title.Equals(column.Title))
                    {
                        currentColumn = column;
                    }
                }
                if (kanbanColumn != null)
                {
                    if (kanbanColumn.IsExpanded)
                    {
                        kanbanColumn.IsExpanded = false;
                        currentColumn.IsExpanded = false;
                    }
                    else
                    {
                        kanbanColumn.IsExpanded = true;
                        currentColumn.IsExpanded = true;
                    }
                }            
        }
    }


}
