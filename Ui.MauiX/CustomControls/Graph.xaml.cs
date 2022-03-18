using SkiaSharp;
using Ui.MauiX.Models;
using Ui.MauiX.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Graph : CustomFrame
    {
        #region Fields
        SKPaint _paint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            Color = SKColors.Black,
            StrokeWidth = 5,
        };
        SKPaint _seriesLinePaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            StrokeWidth = 5,
        };
        SKPaint _dataPointPaint = new SKPaint()
        {
            Style = SKPaintStyle.Fill,
            IsAntialias = true,
        };
        SKPaint _textPaint = new SKPaint()
        {
            Style = SKPaintStyle.Fill,
            TextAlign = SKTextAlign.Center,
            IsLinearText = true,
            IsAntialias = true,
            TextSize = 14,
            Color = SKColors.Black,
        };
        private float _xScaleFactor = 1;
        private float _yScaleFactor = 1;
        private int _numberOfDecimalPlacesInXAxisReferenceSpacing;
        private int _numberOfDecimalPlacesInYAxisReferenceSpacing;
        #endregion

        #region Properties
        /// <summary>
        /// The collection of data series to be graphed.
        /// </summary>
        public IEnumerable<IEnumerable<SKPoint>> DataSeries
        {
            get => (IEnumerable<IEnumerable<SKPoint>>)GetValue(DataSeriesProperty);
            set => SetValue(DataSeriesProperty, value);
        }

        /// <summary>
        /// The data series property.
        /// </summary>
        public static readonly BindableProperty DataSeriesProperty = BindableProperty.Create(nameof(DataSeries), typeof(IEnumerable<IEnumerable<SKPoint>>), typeof(Graph), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var graph = (Graph)bindable;
            if (oldValue is INotifyCollectionChanged o)
            {
                o.CollectionChanged -= graph.DataSeriesCollection_Changed;
                foreach (var series in (IEnumerable<IEnumerable<SKPoint>>)oldValue)
                {
                    if (series is INotifyCollectionChanged s)
                    {
                        s.CollectionChanged -= graph.Collection_Changed;
                    }
                }
            }
            if (newValue is INotifyCollectionChanged n)
            {
                n.CollectionChanged += graph.DataSeriesCollection_Changed;
                foreach (var series in (IEnumerable<IEnumerable<SKPoint>>)newValue)
                {
                    if (series is INotifyCollectionChanged s)
                    {
                        s.CollectionChanged += graph.Collection_Changed;
                    }
                }
            }
            graph.SKCanvas_View.InvalidateSurface();
        });

        /// <summary>
        /// The <see cref="GraphSettings"/> used to display the <see cref="Graph"/>.
        /// </summary>
        public GraphSettings GraphSettings { get => (GraphSettings)GetValue(GraphSettingsProperty); set => SetValue(GraphSettingsProperty, value); }

        public static readonly BindableProperty GraphSettingsProperty = BindableProperty.Create(nameof(GraphSettings), typeof(GraphSettings), typeof(Graph), new GraphSettings(), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var graph = (Graph)bindable;
            var oldGraphSettings = (GraphSettings)oldValue;
            var newGraphSettings = (GraphSettings)newValue;

            oldGraphSettings.PropertyChanged -= graph.GraphSettings_PropertyChanged;
            oldGraphSettings.DataSeriesRenderingInfoCollectionChanged -= graph.GraphSettings_DataSeriesRenderingInfoCollectionChanged;

            newGraphSettings.PropertyChanged += graph.GraphSettings_PropertyChanged;
            newGraphSettings.DataSeriesRenderingInfoCollectionChanged += graph.GraphSettings_DataSeriesRenderingInfoCollectionChanged;

            graph.SKCanvas_View.InvalidateSurface();
        });
        #endregion

        #region Constructor
        public Graph()
        {
            InitializeComponent();
            GraphSettings.PropertyChanged += GraphSettings_PropertyChanged;
            GraphSettings.DataSeriesRenderingInfoCollectionChanged += GraphSettings_DataSeriesRenderingInfoCollectionChanged;
            SKCanvas_View.InvalidateSurface();
        }
        #endregion

        #region Methods
        #region Public
        /// <summary>
        /// Automatically sets the axis bounds and reference spacing to propertly show the data.
        /// </summary>
        public async Task AutoSetBoundsAndReferencePoints()
        {
            float yMax = 50;
            float yMin = -50;
            float xMax = 50;
            float xMin = -50;
            await Task.Run(() =>
            {
                try
                {
                    if (DataSeries != null)
                    {
                        var yCollection = new List<float>();
                        var xCollection = new List<float>();
                        DataSeries.ForEach(series => series.ForEach(datapoint =>
                        {
                            yCollection.Add(datapoint.Y);
                            xCollection.Add(datapoint.X);
                        }));
                        yMax = yCollection.Max();
                        yMin = yCollection.Min();
                        xMax = xCollection.Max();
                        xMin = xCollection.Min();
                        if (yMax == yMin)
                        {
                            yMax += 25;
                            yMin -= 25;
                        }
                        if (xMax == xMin)
                        {
                            xMax += 25;
                            xMin -= 25;
                        }
                    }
                }
                catch
                {
                    //Do nothing
                }
            });

            var targetNumberOfReferenceLines = 5;
            GraphSettings.XAxisReferenceSpacing = Math.Abs(xMax - xMin) / targetNumberOfReferenceLines;
            GraphSettings.XAxisReferenceSpacing = RoundAxisReferenceSpacing(GraphSettings.XAxisReferenceSpacing);
            GraphSettings.YAxisReferenceSpacing = Math.Abs(yMax - yMin) / targetNumberOfReferenceLines;
            GraphSettings.YAxisReferenceSpacing = RoundAxisReferenceSpacing(GraphSettings.YAxisReferenceSpacing);

            var multiplier = 3;
            var xMidpoint = (xMax + xMin) / 2;
            var yMidpoint = (yMax + yMin) / 2;
            GraphSettings.XAxisMinimumBound = (float)Math.Round(-multiplier * GraphSettings.XAxisReferenceSpacing + xMidpoint, _numberOfDecimalPlacesInXAxisReferenceSpacing);
            GraphSettings.XAxisMaximumBound = (float)Math.Round(multiplier * GraphSettings.XAxisReferenceSpacing + xMidpoint, _numberOfDecimalPlacesInXAxisReferenceSpacing);
            GraphSettings.YAxisMinimumBound = (float)Math.Round(-multiplier * GraphSettings.YAxisReferenceSpacing + yMidpoint, _numberOfDecimalPlacesInYAxisReferenceSpacing);
            GraphSettings.YAxisMaximumBound = (float)Math.Round(multiplier * GraphSettings.YAxisReferenceSpacing + yMidpoint, _numberOfDecimalPlacesInYAxisReferenceSpacing);
        }
        #endregion

        #region Private
        private void DataSeriesCollection_Changed(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (var dataSeries in e.OldItems)
                {
                    if (dataSeries is INotifyCollectionChanged s)
                    {
                        s.CollectionChanged -= Collection_Changed;
                    }
                }
            }

            if (e.NewItems != null)
            {
                foreach (var dataSeries in e.NewItems)
                {
                    if (dataSeries is INotifyCollectionChanged s)
                    {
                        s.CollectionChanged += Collection_Changed;
                    }
                }
            }
            SKCanvas_View.InvalidateSurface();
        }

        private void Collection_Changed(object sender, NotifyCollectionChangedEventArgs e)
        {
            SKCanvas_View.InvalidateSurface();
        }

        private void GraphSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GraphSettings.XAxisReferenceSpacing) || e.PropertyName == nameof(GraphSettings.YAxisReferenceSpacing))
            {
                _numberOfDecimalPlacesInXAxisReferenceSpacing = GetNumberOfDecimalPlaces(GraphSettings.XAxisReferenceSpacing);
                _numberOfDecimalPlacesInYAxisReferenceSpacing = GetNumberOfDecimalPlaces(GraphSettings.YAxisReferenceSpacing);
            }
            SKCanvas_View.InvalidateSurface();
        }

        private void GraphSettings_DataSeriesRenderingInfoCollectionChanged(object sender, EventArgs e)
        {
            SKCanvas_View.InvalidateSurface();
        }

        private void SKCanvas_View_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            /* Note: Since the Skia canvas calls the top left corner
             * the 0,0 point (rather than the bottom left corner),
             * this means that the skia canvas' Y axis and the graph's
             * Y axis are inverted relative to one another. For this
             * reason, the Y coordinate of all datapoints is inverted
             * prior to drawing the datapoint.
             */

            //Prepare canvas
            var canvas = e.Surface.Canvas;
            canvas.Clear(GraphSettings.BackgroundColor);

            //Gather device info and calculate size of graph area
            var deviceWidth = e.Info.Width;
            var deviceHeight = e.Info.Height;
            var halfDeviceWidth = deviceWidth / 2;
            var halfDeviceHeight = deviceHeight / 2;
            if (!GraphSettings.PreserveAspectRatio)
            {
                var minValue = Math.Min(deviceWidth, deviceHeight);
                deviceWidth = minValue;
                deviceHeight = minValue;
            }
            var graphAreaLeftMargin = (float)GraphSettings.GraphAreaMargin.Left;
            var graphAreaTopMargin = (float)GraphSettings.GraphAreaMargin.Top;
            var graphAreaRightMargin = (float)GraphSettings.GraphAreaMargin.Right;
            var graphAreaBottomMargin = (float)GraphSettings.GraphAreaMargin.Bottom;
            var halfGraphAreaLeftMargin = graphAreaLeftMargin / 2;
            var halfGraphAreaBottomMargin = graphAreaBottomMargin / 2;
            var quarterGraphAreaLeftMargin = halfGraphAreaLeftMargin / 2;
            var quarterGraphAreaBottomMargin = halfGraphAreaBottomMargin / 2;
            var graphAreaWidth = deviceWidth - graphAreaLeftMargin - graphAreaRightMargin;
            var graphAreaHeight = deviceHeight - graphAreaBottomMargin - graphAreaTopMargin;

            //Draw title
            _textPaint.TextSize = GraphSettings.TitleTextSize;
            if (!string.IsNullOrWhiteSpace(GraphSettings.Title))
            {
                DrawVerticallyCenteredText(canvas, GraphSettings.Title, halfDeviceWidth, graphAreaTopMargin / 2);
            }

            //Translate to start of graph area
            canvas.Save();
            canvas.Translate(graphAreaLeftMargin, graphAreaTopMargin);

            //Calculate scale factors
            var scaledHeight = GraphSettings.YAxisMaximumBound - GraphSettings.YAxisMinimumBound;
            var scaledWidth = GraphSettings.XAxisMaximumBound - GraphSettings.XAxisMinimumBound;
            _xScaleFactor = graphAreaWidth / scaledWidth;
            _yScaleFactor = graphAreaHeight / scaledHeight;

            //Move to point 0,0
            canvas.Translate(-GraphSettings.XAxisMinimumBound * _xScaleFactor, GraphSettings.YAxisMaximumBound * _yScaleFactor);

            _paint.StrokeWidth = GraphSettings.AxisLineThickness;

            //Draw Y axis if visible in given bounds
            if (GraphSettings.XAxisMinimumBound <= 0 && GraphSettings.XAxisMaximumBound >= 0)
            {
                canvas.DrawLine(0, -GraphSettings.YAxisMaximumBound * _yScaleFactor, 0, -GraphSettings.YAxisMinimumBound * _yScaleFactor, _paint);
            }

            //Draw X axis if visible in given bounds
            if (GraphSettings.YAxisMinimumBound <= 0 && GraphSettings.YAxisMaximumBound >= 0)
            {
                canvas.DrawLine(GraphSettings.XAxisMinimumBound * _xScaleFactor, 0, GraphSettings.XAxisMaximumBound * _xScaleFactor, 0, _paint);
            }

            _paint.StrokeWidth = GraphSettings.ReferenceLineThickness;
            _dataPointPaint.StrokeWidth = GraphSettings.SeriesLineThickness;
            _seriesLinePaint.StrokeWidth = GraphSettings.SeriesLineThickness;

            //Draw X axis reference lines
            if (GraphSettings.XAxisReferenceSpacing > 0)
            {
                if (GraphSettings.IsXAxisLabelVisible)
                {
                    var path = new SKPath();
                    var y = -GraphSettings.YAxisMinimumBound * _yScaleFactor + halfGraphAreaBottomMargin + quarterGraphAreaBottomMargin;
                    path.MoveTo(GraphSettings.XAxisMinimumBound * _xScaleFactor, y);
                    path.LineTo(GraphSettings.XAxisMaximumBound * _xScaleFactor, y);
                    _textPaint.TextSize = GraphSettings.AxisLabelTextSize;
                    canvas.DrawTextOnPath(GraphSettings.XAxisLabel, path, SKPoint.Empty, _textPaint);
                }

                float referenceXCoordinate = Math.Max(GraphSettings.XAxisReferenceSpacing, GraphSettings.XAxisMinimumBound);
                DrawXAxisReferenceNumber(canvas, 0, -GraphSettings.YAxisMinimumBound * _yScaleFactor + quarterGraphAreaBottomMargin);
                while (referenceXCoordinate <= GraphSettings.XAxisMaximumBound)
                {
                    //Console.WriteLine($"**** ReferenceXCoordinate: {referenceXCoordinate}");
                    canvas.DrawLine(referenceXCoordinate * _xScaleFactor, -GraphSettings.YAxisMaximumBound * _yScaleFactor, referenceXCoordinate * _xScaleFactor, -GraphSettings.YAxisMinimumBound * _yScaleFactor, _paint);
                    DrawXAxisReferenceNumber(canvas, referenceXCoordinate, -GraphSettings.YAxisMinimumBound * _yScaleFactor + quarterGraphAreaBottomMargin);
                    referenceXCoordinate = (float)Math.Round(referenceXCoordinate + GraphSettings.XAxisReferenceSpacing, _numberOfDecimalPlacesInXAxisReferenceSpacing);
                }
                referenceXCoordinate = Math.Min(-GraphSettings.XAxisReferenceSpacing, GraphSettings.XAxisMaximumBound);
                while (referenceXCoordinate >= GraphSettings.XAxisMinimumBound)
                {
                    //Console.WriteLine($"**** ReferenceXCoordinate: {referenceXCoordinate}");
                    canvas.DrawLine(referenceXCoordinate * _xScaleFactor, -GraphSettings.YAxisMaximumBound * _yScaleFactor, referenceXCoordinate * _xScaleFactor, -GraphSettings.YAxisMinimumBound * _yScaleFactor, _paint);
                    DrawXAxisReferenceNumber(canvas, referenceXCoordinate, -GraphSettings.YAxisMinimumBound * _yScaleFactor + quarterGraphAreaBottomMargin);
                    referenceXCoordinate = (float)Math.Round(referenceXCoordinate - GraphSettings.XAxisReferenceSpacing, _numberOfDecimalPlacesInXAxisReferenceSpacing);
                }
            }

            //Draw Y axis reference lines
            if (GraphSettings.YAxisReferenceSpacing > 0)
            {
                if (GraphSettings.IsYAxisLabelVisible)
                {
                    var path = new SKPath();
                    var x = GraphSettings.XAxisMinimumBound * _xScaleFactor - halfGraphAreaLeftMargin - (quarterGraphAreaLeftMargin / 2);
                    path.MoveTo(x, -GraphSettings.YAxisMinimumBound * _yScaleFactor);
                    path.LineTo(x, -GraphSettings.YAxisMaximumBound * _yScaleFactor);
                    _textPaint.TextSize = GraphSettings.AxisLabelTextSize;
                    canvas.DrawTextOnPath(GraphSettings.YAxisLabel, path, SKPoint.Empty, _textPaint);
                }

                float referenceYCoordinate = Math.Max(GraphSettings.YAxisReferenceSpacing, -GraphSettings.YAxisMaximumBound);
                DrawYAxisReferenceNumber(canvas, 0, GraphSettings.XAxisMinimumBound * _xScaleFactor - quarterGraphAreaLeftMargin);
                while (referenceYCoordinate <= -GraphSettings.YAxisMinimumBound)
                {
                    //Console.WriteLine($"**** ReferenceYCoordinate: {referenceYCoordinate}");
                    canvas.DrawLine(GraphSettings.XAxisMinimumBound * _xScaleFactor, referenceYCoordinate * _yScaleFactor, GraphSettings.XAxisMaximumBound * _xScaleFactor, referenceYCoordinate * _yScaleFactor, _paint);
                    DrawYAxisReferenceNumber(canvas, referenceYCoordinate, GraphSettings.XAxisMinimumBound * _xScaleFactor - quarterGraphAreaLeftMargin);
                    referenceYCoordinate = (float)Math.Round(referenceYCoordinate + GraphSettings.YAxisReferenceSpacing, _numberOfDecimalPlacesInYAxisReferenceSpacing);
                }
                referenceYCoordinate = Math.Min(-GraphSettings.YAxisReferenceSpacing, -GraphSettings.YAxisMinimumBound);
                while (referenceYCoordinate >= -GraphSettings.YAxisMaximumBound)
                {
                    //Console.WriteLine($"**** ReferenceYCoordinate: {referenceYCoordinate}");
                    canvas.DrawLine(GraphSettings.XAxisMinimumBound * _xScaleFactor, referenceYCoordinate * _yScaleFactor, GraphSettings.XAxisMaximumBound * _xScaleFactor, referenceYCoordinate * _yScaleFactor, _paint);
                    DrawYAxisReferenceNumber(canvas, referenceYCoordinate, GraphSettings.XAxisMinimumBound * _xScaleFactor - quarterGraphAreaLeftMargin);
                    referenceYCoordinate = (float)Math.Round(referenceYCoordinate - GraphSettings.YAxisReferenceSpacing, _numberOfDecimalPlacesInYAxisReferenceSpacing);
                }
            }

            //Draw datapoints
            if (DataSeries == null)
            {
                return;
            }

            SKPoint previousPoint = SKPoint.Empty;
            var isFirstDataPoint = true;
            var seriesIndex = 0;
            try
            {
                DataSeries.ForEach(series =>
                {
                    _dataPointPaint.Color = GraphSettings.DataSeriesRenderingInfo[seriesIndex].Color;
                    _seriesLinePaint.Color = _dataPointPaint.Color;
                    //Debug.WriteLine($"*** Series index: {seriesIndex} | Number of datapoints in series: {series.Count()} | Color: {_dataPointPaint.Color}");
                    isFirstDataPoint = true;
                    foreach (var dataPoint in series)
                    {
                        if (dataPoint.X > GraphSettings.XAxisMaximumBound || dataPoint.X < GraphSettings.XAxisMinimumBound || dataPoint.Y > GraphSettings.YAxisMaximumBound || dataPoint.Y < GraphSettings.YAxisMinimumBound)
                        {
                            AutoSetBoundsAndReferencePoints();
                        }
                        canvas.DrawCircle(dataPoint.X * _xScaleFactor, -dataPoint.Y * _yScaleFactor, GraphSettings.DataPointCircleRadius, _dataPointPaint);
                        if (!isFirstDataPoint)
                        {
                            canvas.DrawLine(previousPoint.X * _xScaleFactor, -previousPoint.Y * _yScaleFactor, dataPoint.X * _xScaleFactor, -dataPoint.Y * _yScaleFactor, _seriesLinePaint);
                        }
                        previousPoint = dataPoint;
                        isFirstDataPoint = false;
                    }
                    seriesIndex++;
                });
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("Collection was modified"))
                {
                    //Do nothing, re-draw will be triggered.
                }
            }
        }

        private void DrawVerticallyCenteredText(SKCanvas canvas, string text, float x, float y)
        {
            var textHeight = _textPaint.FontMetrics.Ascent + _textPaint.FontMetrics.Descent;
            canvas.DrawText(text, x, y - (textHeight / 2), _textPaint);
        }

        private void DrawXAxisReferenceNumber(SKCanvas canvas, float referenceNumber, float y)
        {
            _textPaint.TextSize = GraphSettings.ReferenceIndicatorTextSize;
            DrawVerticallyCenteredText(canvas, referenceNumber.ToString($"N{_numberOfDecimalPlacesInXAxisReferenceSpacing}"), referenceNumber * _xScaleFactor, y);
        }

        private void DrawYAxisReferenceNumber(SKCanvas canvas, float referenceNumber, float x)
        {
            _textPaint.TextSize = GraphSettings.ReferenceIndicatorTextSize;
            DrawVerticallyCenteredText(canvas, (-referenceNumber).ToString($"N{_numberOfDecimalPlacesInYAxisReferenceSpacing}"), x, referenceNumber * _yScaleFactor);
        }

        private static int GetNumberOfDecimalPlaces(float n)
        {
            var d = (decimal)n;
            d = Math.Abs(d); //make sure it is positive.
            d -= (int)d;     //remove the integer part of the number.
            var decimalPlaces = 0;
            while (d > 0)
            {
                decimalPlaces++;
                d *= 10;
                d -= (int)d;
            }
            return decimalPlaces;
        }

        private float RoundAxisReferenceSpacing(float axisReferenceSpacing)
        {
            double m = 1;
            float x = 10.0f;
            if (axisReferenceSpacing != 0)
            {
                while (axisReferenceSpacing >= 10.0f)
                {
                    m = m * x;
                    axisReferenceSpacing = axisReferenceSpacing / x;
                }
                while (axisReferenceSpacing < 1.0f)
                {
                    m = m / x;
                    axisReferenceSpacing = axisReferenceSpacing * x;
                }
            }

            var n = (float)Math.Floor(axisReferenceSpacing);
            axisReferenceSpacing = (float)Math.Round(axisReferenceSpacing, 1);
            if (axisReferenceSpacing >= n + 0.7f)
            {
                axisReferenceSpacing = n + 1.0f;
            }
            else if (axisReferenceSpacing >= n + 0.4f)
            {
                axisReferenceSpacing = n + 0.5f;
            }
            else
            {
                axisReferenceSpacing = n;
            }

            var result = (float)(axisReferenceSpacing * m);
            return result;
        }
        #endregion
        #endregion
    }
}