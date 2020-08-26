using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CoreGraphics;
using Foundation;
using SGXC.Controls;
using Syncfusion.SfKanban.iOS;
using Syncfusion.SfKanban.XForms.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(SGXC.Controls.CustomKanban), typeof(SGXC.iOS.Renderers.KanbanCustomRenderer))]
namespace SGXC.iOS.Renderers
{
    public class KanbanCustomRenderer : SfKanbanRenderer
    {
        protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<Syncfusion.SfKanban.XForms.SfKanban> e)
        {
            base.OnElementChanged(e);
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            if (this.Control != null)
            {
                foreach (KanbanColumn column in Control.Columns)
                {
                    if (column != null)
                    {
                        var value = column.GetType().GetProperty("HeaderLayout", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(column);
                        if (value != null)
                        {
                            UILabel label = value.GetType().GetField("defaultLabel", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(value) as UILabel;
                            label.Text = string.Empty;
                        }
                    }
                }
            }
        }
    }
}