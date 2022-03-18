using SkiaSharp;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ui.MauiX.Enums;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CircularProgressControl : ContentView
    {
        #region Fields
        private const float _radius = 50;
        private const float _diameter = 2 * _radius;
        private const float _completeDegrees = 359.9999f;
        private object _textLinesLock = new object();
        private ObservableCollection<string> _copyOfTextLines = new ObservableCollection<string>();
        private double _internalProgress;
        #endregion

        #region Properties
        #region Public
        /// <summary>
        /// The progress value.
        /// <para>Note: This value must be between 0.0 and 1.0.</para>
        /// </summary>
        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        public static readonly BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(double), typeof(CircularProgressControl), propertyChanged: ProgressProperty_Changed, defaultValue: 0.0);

        /// <summary>
        /// The background color of the control.
        /// </summary>
        public new SKColor BackgroundColor
        {
            get => (SKColor)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(SKColor), typeof(CircularProgressControl), propertyChanged: BackgroundColorProperty_Changed);

        /// <summary>
        /// The color used to paint the center of the circle.
        /// </summary>
        public SKColor CircleFillColor
        {
            get => (SKColor)GetValue(CircleFillColorProperty);
            set => SetValue(CircleFillColorProperty, value);
        }

        /// <summary>
        /// The circle fill color property.
        /// </summary>
        public static readonly BindableProperty CircleFillColorProperty = BindableProperty.Create(nameof(CircleFillColor), typeof(SKColor), typeof(CircularProgressControl), propertyChanged: CircleFillColorProperty_Changed, defaultValue: SKColors.White);

        /// <summary>
        /// The color of the progress ring.
        /// </summary>
        public SKColor Color
        {
            get => (SKColor)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(SKColor), typeof(CircularProgressControl), propertyChanged: ColorProperty_Changed, defaultValue: SKColors.SkyBlue);

        /// <summary>
        /// The color of the progress shadow.
        /// <para>Note: The progress shadow is a ring that is displayed underneath the progress ring.</para>
        /// </summary>
        public SKColor ProgressShadowColor
        {
            get => (SKColor)GetValue(ProgressShadowColorProperty);
            set => SetValue(ProgressShadowColorProperty, value);
        }

        public static readonly BindableProperty ProgressShadowColorProperty = BindableProperty.Create(nameof(ProgressShadowColor), typeof(SKColor), typeof(CircularProgressControl), propertyChanged: ProgressShadowColorProperty_Changed, defaultValue: SKColors.LightGray);

        /// <summary>
        /// The thickness of the progress ring and the progress shadow.
        /// </summary>
        public float Thickness
        {
            get => (float)GetValue(ThicknessProperty);
            set => SetValue(ThicknessProperty, value);
        }

        public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(float), typeof(CircularProgressControl), propertyChanged: ThicknessProperty_Changed, defaultValue: 4f);

        /// <summary>
        /// The lines of text to display in the center of the progress control.
        /// <para>Note: Each string in the collection will be placed on a new line.</para>
        /// </summary>
        public ObservableCollection<string> TextLines
        {
            get => (ObservableCollection<string>)GetValue(TextLinesProperty);
            set => SetValue(TextLinesProperty, value);
        }

        public static readonly BindableProperty TextLinesProperty = BindableProperty.Create(nameof(TextLines), typeof(ObservableCollection<string>), typeof(CircularProgressControl), propertyChanged: TextLinesProperty_Changed);

        /// <summary>
        /// The size of the text displayed in the center of the progress control.
        /// </summary>
        public float TextSize
        {
            get => (float)GetValue(TextSizeProperty);
            set => SetValue(TextSizeProperty, value);
        }

        public static readonly BindableProperty TextSizeProperty = BindableProperty.Create(nameof(TextSize), typeof(float), typeof(CircularProgressControl), propertyChanged: TextSizeProperty_Changed, defaultValue: 10.0f);

        /// <summary>
        /// The time over which the progress will be animated from one value to the next in milliseconds.
        /// </summary>
        public int AnimationTime
        {
            get => (int)GetValue(AnimationTimeProperty);
            set => SetValue(AnimationTimeProperty, value);
        }

        public static readonly BindableProperty AnimationTimeProperty = BindableProperty.Create(nameof(AnimationTime), typeof(int), typeof(CircularProgressControl), defaultValue: 250, validateValue: AnimationTimeProperty_ValidateValue);

        /// <summary>
        /// The <see cref="EasingType"/> used when animating the progress.
        /// </summary>
        public EasingType AnimationEasingType
        {
            get => (EasingType)GetValue(AnimationEasingTypeProperty);
            set => SetValue(AnimationEasingTypeProperty, value);
        }

        public static readonly BindableProperty AnimationEasingTypeProperty = BindableProperty.Create(nameof(AnimationEasingType), typeof(EasingType), typeof(CircularProgressControl), defaultValue: EasingType.Linear);
        #endregion

        #region Private/Internal
        private double InternalProgress
        {
            get => _internalProgress;
            set
            {
                _internalProgress = value;
                SKCanvas_View.InvalidateSurface();
            }
        }

        /// <summary>
        /// Represents the internal progress in degrees.
        /// <para>E.g. A progress of 0.5 would be 180 degrees.</para>
        /// </summary>
        private float InternalProgressDegrees
        {
            get
            {
                var progress = InternalProgress;
                if (progress < 0)
                {
                    progress = 0;
                }
                else if (progress > 1)
                {
                    progress = 1;
                }

                var degrees = (float)(progress * 360);
                if (degrees > _completeDegrees)
                {
                    degrees = _completeDegrees;
                }

                return degrees;
            }
        }

        private float InnerCircleRadius { get => _radius - 3 - Thickness / 2; }

        internal SKPaint TextPaint { get; set; }

        internal SKPaint ProgressShadowPaint { get; set; }

        internal SKPaint ProgressPaint { get; set; }

        internal SKPaint InnerCirclePaint { get; set; } = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            StrokeCap = SKStrokeCap.Round,
            Color = SKColors.Orange,
            StrokeWidth = 0.5f,
        };

        internal SKPaint InnerCircleFillPaint { get; set; }
        #endregion
        #endregion

        #region Constructor
        public CircularProgressControl()
        {
            InitializeComponent();
            ProgressPaint = new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                IsAntialias = true,
                StrokeCap = SKStrokeCap.Round,
                Color = Color,
                StrokeWidth = Thickness,
            };
            ProgressShadowPaint = new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                IsAntialias = true,
                StrokeCap = SKStrokeCap.Round,
                Color = ProgressShadowColor,
                StrokeWidth = Thickness,
            };
            TextPaint = new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                IsLinearText = true,
                IsAntialias = true,
                TextSize = TextSize,
                Color = SKColors.Black,
            };
            InnerCircleFillPaint = new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                Color = CircleFillColor,
                IsAntialias = true,
            };
        }
        #endregion

        #region Methods
        #region Property changed handlers
        private static void ProgressProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var circularProgressControl = (CircularProgressControl)bindable;
            circularProgressControl.BeginProgressAnimation();
        }

        private static void BackgroundColorProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
            {
                return;
            }

            var circularProgressControl = (CircularProgressControl)bindable;
            circularProgressControl.SKCanvas_View.InvalidateSurface();
        }

        private static void CircleFillColorProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
            {
                return;
            }

            var circularProgressControl = (CircularProgressControl)bindable;
            circularProgressControl.InnerCircleFillPaint.Color = (SKColor)newValue;
            circularProgressControl.SKCanvas_View.InvalidateSurface();
        }


        private static void ColorProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var circularProgressControl = (CircularProgressControl)bindable;
            circularProgressControl.ProgressPaint.Color = (SKColor)newValue;
            circularProgressControl.SKCanvas_View.InvalidateSurface();
        }

        private static void ProgressShadowColorProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var circularProgressControl = (CircularProgressControl)bindable;
            circularProgressControl.ProgressShadowPaint.Color = (SKColor)newValue;
            circularProgressControl.SKCanvas_View.InvalidateSurface();
        }

        private static void ThicknessProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var circularProgressControl = (CircularProgressControl)bindable;
            var thickness = (float)newValue;
            circularProgressControl.ProgressShadowPaint.StrokeWidth = thickness;
            circularProgressControl.ProgressPaint.StrokeWidth = thickness;
            circularProgressControl.SKCanvas_View.InvalidateSurface();
        }

        private static void TextLinesProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var circularProgressControl = (CircularProgressControl)bindable;
            var oldTextLines = (ObservableCollection<string>)oldValue;
            var newTextLines = (ObservableCollection<string>)newValue;

            if (oldTextLines != null)
            {
                oldTextLines.CollectionChanged -= circularProgressControl.TextLines_Changed;
            }

            if (newTextLines != null)
            {
                newTextLines.CollectionChanged += circularProgressControl.TextLines_Changed;
            }
            circularProgressControl.CopyTextLines();
            circularProgressControl.SKCanvas_View.InvalidateSurface();
        }

        private static void TextSizeProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var circularProgressControl = (CircularProgressControl)bindable;
            circularProgressControl.TextPaint.TextSize = (float)newValue;
            circularProgressControl.SKCanvas_View.InvalidateSurface();
        }
        #endregion

        private void SKCanvas_View_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            //Prepare canvas
            var canvas = e.Surface.Canvas;
            canvas.Clear(BackgroundColor);

            var width = e.Info.Width;
            var height = e.Info.Height;

            canvas.Translate(width / 2, height / 2);
            canvas.Scale(Math.Min(width, height) / 100);
            canvas.Save();
            var grayArcGap = 0; //7;
            canvas.RotateDegrees(grayArcGap / 2);

            //Draw progress shadow
            var grayPath = new SKPath();
            grayPath.MoveTo(0, -_radius);
            grayPath.ArcTo(new SKRect(-_radius, -_radius, _radius, _radius), -90, _completeDegrees - grayArcGap, false);
            canvas.DrawPath(grayPath, ProgressShadowPaint);

            //Draw progress ring
            var path = new SKPath();
            path.MoveTo(0, -_radius);
            path.ArcTo(new SKRect(-_radius, -_radius, _radius, _radius), -90, InternalProgressDegrees, false);
            canvas.DrawPath(path, ProgressPaint);
            canvas.Restore();

            //Draw inner circle fill
            canvas.DrawCircle(0, 0, InnerCircleRadius, InnerCircleFillPaint);

            //Draw inner circle
            var innerCirclePath = new SKPath();
            innerCirclePath.MoveTo(0, -InnerCircleRadius);
            innerCirclePath.ArcTo(new SKRect(-InnerCircleRadius, -InnerCircleRadius, InnerCircleRadius, InnerCircleRadius), -90, _completeDegrees, false);
            canvas.DrawPath(innerCirclePath, InnerCirclePaint);

            //Draw text
            lock (_textLinesLock)
            {
                if (_copyOfTextLines == null || _copyOfTextLines.Count < 1)
                {
                    return;
                }

                float y = -TextPaint.FontSpacing * _copyOfTextLines.Count / 2 + TextPaint.FontSpacing / 2;
                foreach (var line in _copyOfTextLines)
                {
                    var textPath = new SKPath();
                    textPath.MoveTo(-_radius, y);
                    textPath.LineTo(_radius, y);
                    canvas.DrawTextOnPath(line, textPath, 0, 0, TextPaint);
                    y += TextPaint.FontSpacing;
                }
            }
        }

        private static bool AnimationTimeProperty_ValidateValue(BindableObject bindable, object value)
        {
            if (value is int animationTime)
            {
                return animationTime >= 0;
            }

            return false;
        }

        private void TextLines_Changed(object sender, NotifyCollectionChangedEventArgs e)
        {
            CopyTextLines();
            SKCanvas_View.InvalidateSurface();
        }

        private void CopyTextLines()
        {
            try
            {
                lock (_textLinesLock)
                {
                    _copyOfTextLines.Clear();
                    foreach (var line in TextLines)
                    {
                        _copyOfTextLines.Add(line);
                    }
                }
            }
            catch
            {

            }

        }

        private void BeginProgressAnimation()
        {
            Task.Run(() =>
            {
                var animationTime = AnimationTime;
                var startingProgress = InternalProgress;
                var targetProgress = Progress;

                Application.Current.MainPage.AbortAnimation("ProgressAnimation");

                var animation = new Animation((p) => InternalProgress = p, startingProgress, targetProgress);
                var easing = Easing.Linear;
                switch (AnimationEasingType)
                {
                    case EasingType.SinOut:
                        easing = Easing.SinOut;
                        break;
                    case EasingType.SinIn:
                        easing = Easing.SinIn;
                        break;
                    case EasingType.SinInOut:
                        easing = Easing.SinInOut;
                        break;
                    case EasingType.CubicIn:
                        easing = Easing.CubicIn;
                        break;
                    case EasingType.CubicOut:
                        easing = Easing.CubicOut;
                        break;
                    case EasingType.CubicInOut:
                        easing = Easing.CubicInOut;
                        break;
                    case EasingType.BounceOut:
                        easing = Easing.BounceOut;
                        break;
                    case EasingType.BounceIn:
                        easing = Easing.BounceIn;
                        break;
                    case EasingType.SpringIn:
                        easing = Easing.SpringIn;
                        break;
                    case EasingType.SpringOut:
                        easing = Easing.SpringOut;
                        break;
                    default:
                        break;
                }
                animation.Commit(Application.Current.MainPage, "ProgressAnimation", length: (uint)animationTime, easing: easing);
            });
        }        
        #endregion
    }
}