using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SkiaSharp;
using Components.Helpers;
using Ui.MauiX.CustomControls;
using Ui.MauiX.Resources;
using Ui.MauiX.Converters;
using Xamarin.Forms;
using System.Collections.Specialized;

namespace Ui.MauiX.Models
{
    /// <summary>
    /// Settings used for displaying a <see cref="Graph"/>.
    /// </summary>
    public class GraphSettings : NotifyPropertyChanged
    {
        #region Fields
        private static SKColorToColorConverter _colorConverter = new SKColorToColorConverter();
        private float _xAxisMinimumBound = 0;
        private float _yAxisMinimumBound = 0;
        private float _xAxisMaximumBound = 50;
        private float _yAxisMaximumBound = 50;
        private float _xAxisReferenceSpacing = 5f;
        private float _yAxisReferenceSpacing = 5f;
        private string _xAxisLabel = string.Empty;
        private string _yAxisLabel = string.Empty;
        private bool _isXAxisLabelVisible = false;
        private bool _isYAxisLabelVisible = false;
        private bool _preserveAspectRatio = true;
        private string _title = string.Empty;
        private Thickness _graphAreaMargin = new Thickness(75, 65, 50, 75);
        private float _referenceIndicatorTextSize = 16f;
        private float _axisLabelTextSize = 18f;
        private float _titleTextSize = 24f;
        private SKColor _backgroundColor = SKColors.White;
        private double _seriesLegendTextSize = 14;
        private double _seriesLegendLineThickness = 3;
        private float _dataPointCircleRadius = 4f;
        private bool _showSeriesLegend;
        private ObservableCollection<DataSeriesRenderingInfo> _dataSeriesRenderingInfo = new ObservableCollection<DataSeriesRenderingInfo>()
        {
            new DataSeriesRenderingInfo(){Color= SKColors.Black },
            new DataSeriesRenderingInfo(){Color= (SKColor)_colorConverter.Convert(CustomColors.Red, null, "invert", null)},
            new DataSeriesRenderingInfo(){Color= (SKColor)_colorConverter.Convert(CustomColors.Blue, null, "invert", null)},
            new DataSeriesRenderingInfo(){Color = (SKColor)_colorConverter.Convert(CustomColors.Orange, null, "invert", null)},
            new DataSeriesRenderingInfo(){Color= (SKColor)_colorConverter.Convert(CustomColors.Green, null, "invert", null)},
            new DataSeriesRenderingInfo(){Color = (SKColor)_colorConverter.Convert(CustomColors.DarkBlue, null, "invert", null)},
        };
        private float _axisLineThickness = 3f;
        private float _referenceLineThickness = 0.95f;
        private float _seriesLineThickness = 0.95f;
        #endregion

        #region Properties
        /// <summary>
        /// The minimum value displayed on the x-axis.
        /// </summary>
        public float XAxisMinimumBound { get => _xAxisMinimumBound; set => SetProperty(ref _xAxisMinimumBound, value); }

        /// <summary>
        /// The minimum value displayed on the y-axis.
        /// </summary>
        public float YAxisMinimumBound { get => _yAxisMinimumBound; set => SetProperty(ref _yAxisMinimumBound, value); }

        /// <summary>
        /// The maximum value displayed on the x-axis.
        /// </summary>
        public float XAxisMaximumBound { get => _xAxisMaximumBound; set => SetProperty(ref _xAxisMaximumBound, value); }

        /// <summary>
        /// The maximum value displayed on the y-axis.
        /// </summary>
        public float YAxisMaximumBound { get => _yAxisMaximumBound; set => SetProperty(ref _yAxisMaximumBound, value); }

        /// <summary>
        /// Determines how far apart reference lines are spaced on the X axis.
        /// </summary>
        public float XAxisReferenceSpacing { get => _xAxisReferenceSpacing; set => SetProperty(ref _xAxisReferenceSpacing, value); }

        public float YAxisReferenceSpacing { get => _yAxisReferenceSpacing; set => SetProperty(ref _yAxisReferenceSpacing, value); }

        /// <summary>
        /// The label for the X-axis.
        /// </summary>
        public string XAxisLabel { get => _xAxisLabel; set => SetProperty(ref _xAxisLabel, value); }

        /// <summary>
        /// The label for the Y-axis.
        /// </summary>
        public string YAxisLabel { get => _yAxisLabel; set => SetProperty(ref _yAxisLabel, value); }

        /// <summary>
        /// Determines whether or not the label for the X-axis is visible.
        /// </summary>
        public bool IsXAxisLabelVisible { get => _isXAxisLabelVisible; set => SetProperty(ref _isXAxisLabelVisible, value); }

        /// <summary>
        /// Determines whether or not the label for the Y-axis is visible.
        /// </summary>
        public bool IsYAxisLabelVisible { get => _isYAxisLabelVisible; set => SetProperty(ref _isYAxisLabelVisible, value); }

        /// <summary>
        /// Determines whether or not the aspect ratio of the control is preserved.
        /// <para>Note: If this property is set to false, the <see cref="Graph"/> will be squared.</para>
        /// </summary>
        public bool PreserveAspectRatio { get => _preserveAspectRatio; set => SetProperty(ref _preserveAspectRatio, value); }

        /// <summary>
        /// The title to display above the graph area.
        /// </summary>
        public string Title { get => _title; set => SetProperty(ref _title, value); }

        /// <summary>
        /// The margin around the graph area.
        /// <para>Note: When modifying this property, be sure to leave enough space around the graph area for the axis labels and the graph title.</para>
        /// </summary>
        public Thickness GraphAreaMargin { get => _graphAreaMargin; set => SetProperty(ref _graphAreaMargin, value); }

        /// <summary>
        /// The text size of the reference indicators.
        /// </summary>
        public float ReferenceIndicatorTextSize { get => _referenceIndicatorTextSize; set => SetProperty(ref _referenceIndicatorTextSize, value); }

        /// <summary>
        /// The text size of the axis labels.
        /// </summary>
        public float AxisLabelTextSize { get => _axisLabelTextSize; set => SetProperty(ref _axisLabelTextSize, value); }

        /// <summary>
        /// The text size of the title.
        /// </summary>
        public float TitleTextSize { get => _titleTextSize; set => SetProperty(ref _titleTextSize, value); }

        /// <summary>
        /// The background color of the <see cref="Graph"/>.
        /// </summary>
        public SKColor BackgroundColor { get => _backgroundColor; set => SetProperty(ref _backgroundColor, value); }

        /// <summary>
        /// The text size of the series legend.
        /// </summary>
        public double SeriesLegendTextSize { get => _seriesLegendTextSize; set => SetProperty(ref _seriesLegendTextSize, value); }

        /// <summary>
        /// The thickness of the lines in the series legend.
        /// </summary>
        public double SeriesLegendLineThickness { get => _seriesLegendLineThickness; set => SetProperty(ref _seriesLegendLineThickness, value); }

        /// <summary>
        /// The radius of the circles drawn to represent the datapoints.
        /// </summary>
        public float DataPointCircleRadius { get => _dataPointCircleRadius; set => SetProperty(ref _dataPointCircleRadius, value); }

        /// <summary>
        /// Determines whether or not the series legend is displayed.
        /// </summary>
        public bool ShowSeriesLegend { get => _showSeriesLegend; set => SetProperty(ref _showSeriesLegend, value); }

        /// <summary>
        /// The data series rendering information.
        /// </summary>
        public ObservableCollection<DataSeriesRenderingInfo> DataSeriesRenderingInfo
        {
            get => _dataSeriesRenderingInfo;
            set
            {
                if (DataSeriesRenderingInfo != null)
                {
                    DataSeriesRenderingInfo.CollectionChanged -= DataSeriesRenderingInfo_CollectionChanged;
                }
                SetProperty(ref _dataSeriesRenderingInfo, value);
                if (DataSeriesRenderingInfo != null)
                {
                    DataSeriesRenderingInfo.CollectionChanged += DataSeriesRenderingInfo_CollectionChanged;
                }
            }
        }

        /// <summary>
        /// The thickness of the axes.
        /// </summary>
        public float AxisLineThickness { get => _axisLineThickness; set => SetProperty(ref _axisLineThickness, value); }

        /// <summary>
        /// The thickness of the reference lines.
        /// </summary>
        public float ReferenceLineThickness { get => _referenceLineThickness; set => SetProperty(ref _referenceLineThickness, value); }

        /// <summary>
        /// The thickness of the series lines.
        /// </summary>
        public float SeriesLineThickness { get => _seriesLineThickness; set => SetProperty(ref _seriesLineThickness, value); }
        #endregion

        #region Events
        public event EventHandler DataSeriesRenderingInfoCollectionChanged;
        #endregion

        #region Constructor
        public GraphSettings()
        {
            DataSeriesRenderingInfo.CollectionChanged += DataSeriesRenderingInfo_CollectionChanged;
        }
        #endregion

        #region Methods
        private void DataSeriesRenderingInfo_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            DataSeriesRenderingInfoCollectionChanged?.Invoke(sender, e);
        }
        #endregion
    }
}
