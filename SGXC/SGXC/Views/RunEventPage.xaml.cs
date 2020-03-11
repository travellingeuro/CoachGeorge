using Syncfusion.DataSource.Extensions;
using Xamarin.Forms;

namespace SGXC.Views
{
    public partial class RunEventPage : ContentPage
    {
        GroupResult expandedGroup;


        public RunEventPage()

        {
            InitializeComponent();
            
        }

        private void Runlistview_GroupExpanding(object sender, Syncfusion.ListView.XForms.GroupExpandCollapseChangingEventArgs e)
        {
            if (e.Groups.Count > 0)
            {
                var group = e.Groups[0];
                if (expandedGroup == null || group.Key != expandedGroup.Key)
                {
                    foreach (var otherGroup in runlistview.DataSource.Groups)
                    {
                        if (group.Key != otherGroup.Key)
                        {
                            runlistview.CollapseGroup(otherGroup);
                        }
                    }
                    expandedGroup = group;
                    runlistview.ExpandGroup(expandedGroup);
                }
            }

        }

        private void Runlistview_Loaded(object sender, Syncfusion.ListView.XForms.ListViewLoadedEventArgs e)
        {
            runlistview.CollapseAll();

        }
    }
}
