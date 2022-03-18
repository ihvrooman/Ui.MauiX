using SkiaSharp;
using Components.Helpers;
using Ui.Helpers;
using Ui.MauiX.Models;
using Ui.MauiX.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ui.MauiX.Test.ViewModels
{
    public class GraphViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<ObservableCollection<SKPoint>> _dataSeries = new ObservableCollection<ObservableCollection<SKPoint>>();
        #endregion

        #region Properties
        public ObservableCollection<ObservableCollection<SKPoint>> DataSeries { get => _dataSeries; set { SetProperty(ref _dataSeries, value); } }
        public GraphSettings GraphSettings { get; set; } = new GraphSettings()
        {

            XAxisLabel = "Wavelength (nm)",
            XAxisMaximumBound = 55,
            ShowSeriesLegend = true,
            SeriesLineThickness = 2,
            Title = "UV Analysis Results",
            XAxisMinimumBound = -55,
            DataPointCircleRadius = 3,
            XAxisReferenceSpacing = 10,
            YAxisLabel = "Absorbance",
            YAxisMaximumBound = 55,
            IsYAxisLabelVisible=true,
            IsXAxisLabelVisible = true,
            YAxisMinimumBound = -55,
            YAxisReferenceSpacing = 10,
        };
        #endregion

        #region Constructor
        public GraphViewModel()
        {
            Task.Run(async () =>
            {
                await Task.Delay(50);
                DataSeries.Add(new ObservableCollection<SKPoint>());
                GraphSettings.DataSeriesRenderingInfo[0].Name = "Series 1";
                GraphSettings.DataSeriesRenderingInfo[0].Color = SKColor.Parse(CustomColors.Green.ToHex());
                for (int x = -50; x <= 500; x += 5)
                {
                    float y = (float)Math.Sin(x) * 4;
                    y = y + 10;
                    DataSeries[0].Add(new SKPoint(x, y));
                    await Task.Delay(300);
                }
            });
            Task.Run(async () =>
            {
                await Task.Delay(600);
                DataSeries.Add(new ObservableCollection<SKPoint>());
                GraphSettings.DataSeriesRenderingInfo[1].Name = "Series 2";
                for (int x = -50; x <= 500; x += 5)
                {
                    float y = (float)Math.Cos(x) * 4;
                    DataSeries[1].Add(new SKPoint(x, y));
                    await Task.Delay(250);
                }
            });
            Task.Run(async () =>
            {
                await Task.Delay(700);
                DataSeries.Add(new ObservableCollection<SKPoint>());
                GraphSettings.DataSeriesRenderingInfo[2].Name = "Series 3";
                for (int x = -50; x <= 500; x += 5)
                {
                    float y = (float)Math.Tan(x) * 4;
                    y = y - 5;
                    DataSeries[2].Add(new SKPoint(x, y));
                    await Task.Delay(250);
                }
            });

            Task.Run(async () =>
            {
                await Task.Delay(5000);
                DataSeries.Add(new ObservableCollection<SKPoint>());
                GraphSettings.DataSeriesRenderingInfo[3].Name = "Series 4";
                for (int x = -50; x <= 500; x += 5)
                {
                    float y = (float)Math.Tan(x) * 4;
                    y = y + 50;
                    DataSeries[3].Add(new SKPoint(x, y));
                    await Task.Delay(150);
                }
            });
        }
        #endregion
    }
}
