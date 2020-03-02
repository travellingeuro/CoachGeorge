using Syncfusion.DataSource.Extensions;
using Xamarin.Forms;

namespace SGXC.Views
{
    public partial class AddRunnerToEvent : ContentPage
    {
        GroupResult expandedGroup;

        public AddRunnerToEvent()

        {
            InitializeComponent();
        }


        private void Runnerlist_GroupExpanding(object sender, Syncfusion.ListView.XForms.GroupExpandCollapseChangingEventArgs e)
        {
            if (e.Groups.Count > 0)
            {
                var group = e.Groups[0];
                if (expandedGroup == null || group.Key != expandedGroup.Key)
                {
                    foreach (var otherGroup in runnerlist.DataSource.Groups)
                    {
                        if (group.Key != otherGroup.Key)
                        {
                            runnerlist.CollapseGroup(otherGroup);
                        }
                    }
                    expandedGroup = group;
                    runnerlist.ExpandGroup(expandedGroup);
                }
            }

        }

        private void Runnerlist_Loaded(object sender, Syncfusion.ListView.XForms.ListViewLoadedEventArgs e)
        {
            runnerlist.ExpandAll();
        }
    }
}
