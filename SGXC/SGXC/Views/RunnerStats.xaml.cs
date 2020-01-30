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
       
            e.LabelContent = String.Format("{0:c}", time);
            

        }
    }
}
