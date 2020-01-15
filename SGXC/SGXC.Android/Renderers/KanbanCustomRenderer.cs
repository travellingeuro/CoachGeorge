using Android.Widget;
using Syncfusion.SfKanban.Android;
using Syncfusion.SfKanban.XForms.Droid;
using System.Reflection;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: ExportRenderer(typeof(SGXC.Controls.CustomKanban), typeof(SGXC.Droid.KanbanCustomRenderer))]

namespace SGXC.Droid
{
    public class KanbanCustomRenderer : SfKanbanRenderer
    {
        public KanbanCustomRenderer() : base(Application.Context)
        {

        }
        //protected override void OnElementChanged(ElementChangedEventArgs<Syncfusion.SfKanban.XForms.SfKanban> e)
        //{
        //    base.OnElementChanged(e);
        //}

        //protected override void OnLayout(bool changed, int l, int t, int r, int b)
        //{
        //    base.OnLayout(changed, l, t, r, b);
        //    if (Control != null)
        //    {
        //        foreach (KanbanColumn column in Control.Columns)
        //        {
        //            if (column != null)
        //            {
        //                var value = column.GetType().GetProperty("HeaderLayout", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(column);
        //                if (value != null)
        //                {
        //                    Android.Widget.TextView countView = value.GetType().GetProperty("CountView", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(value) as Android.Widget.TextView;
        //                    countView.Visibility = Android.Views.ViewStates.Invisible;
        //                }
        //            }
        //        }
        //    }
        //}
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            if (Control != null)
            {
                foreach (KanbanColumn column in Control.ActualColumns)
                {
                    if (column != null)
                    {
                        var value = column.GetType().GetProperty("HeaderLayout", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(column);
                        if (value != null)
                        {
                            TextView countView = value.GetType().GetProperty("CountView", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(value) as Android.Widget.TextView;
                            countView.Visibility = Android.Views.ViewStates.Invisible;

                            //Si quiersa cambiar el tamaño del encabezado:
                            //TextView contentview = value.GetType().GetProperty("ContentView", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(value) as Android.Widget.TextView;
                            //contentview.TextSize = 14;
                        }

                    }
                }
            }
        }
    }
}