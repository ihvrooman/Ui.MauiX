using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Ui.MauiX.Test.ViewModels
{
    public class CircularProgressViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<string> _textLines = new ObservableCollection<string>();
        private double _progress;
        #endregion

        #region Properties
        public ObservableCollection<string> TextLines { get => _textLines; set { SetProperty(ref _textLines, value); } }
        public double Progress { get => _progress; set { SetProperty(ref _progress, value); } }
        #endregion

        #region Constructor
        public CircularProgressViewModel()
        {
            TextLines.Add("00.0 °C");
            TextLines.Add("000.0 RPM");
            TextLines.Add("000.0 PSI");
            Task.Run(async () =>
            {
                await Task.Delay(5000);
                TextLines[1] = "525.2 RPM";
                await Task.Delay(2000);
                TextLines[0] = "23.1 C";
                while (true)
                {
                    TextLines[2] = DateTime.Now.ToString("fffffff");
                    await Task.Delay(1);
                }
            });

            Task.Run(async () =>
            {
                while (true)
                {
                    Progress = 0;
                    await Task.Delay(1000);
                    for (int i = 0; i < 10; i++)
                    {
                        Progress += 0.1;
                        await Task.Delay(250);
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        Progress -= 0.25;
                        await Task.Delay(1000);
                    }

                    Progress = 1;
                    await Task.Delay(1000);
                }
            });
        }
        #endregion
    }
}
