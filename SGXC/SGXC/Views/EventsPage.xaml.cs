using Syncfusion.DataSource.Extensions;
using Xamarin.Forms;

namespace SGXC.Views
{
    public partial class EventsPage : ContentPage
    {
        GroupResult expandedGroup;

        public EventsPage()
        {
            InitializeComponent();
        }

        private void Evenlist_GroupExpanding(object sender, Syncfusion.ListView.XForms.GroupExpandCollapseChangingEventArgs e)
        {
            if (e.Groups.Count > 0)
            {
                var group = e.Groups[0];
                if (expandedGroup == null || group.Key != expandedGroup.Key)
                {
                    foreach (var otherGroup in evenlist.DataSource.Groups)
                    {
                        if (group.Key != otherGroup.Key)
                        {
                            evenlist.CollapseGroup(otherGroup);
                        }
                    }
                    expandedGroup = group;
                    evenlist.ExpandGroup(expandedGroup);
                }
            }

        }

        private void Evenlist_Loaded(object sender, Syncfusion.ListView.XForms.ListViewLoadedEventArgs e)
        {
            evenlist.CollapseAll();
        }


    }
}
