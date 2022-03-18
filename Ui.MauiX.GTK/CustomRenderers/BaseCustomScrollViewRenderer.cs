using Gtk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;
using Xamarin.Forms.Platform.GTK.Renderers;
using Xamarin.Forms.Platform.GTK.Extensions;
using System.ComponentModel;
using System.Diagnostics;
using Ui.MauiX.GTK.Extensions;

namespace Ui.MauiX.GTK.CustomRenderers
{
    /// <summary>
    /// A base implementation of a custom scroll view renderer for the GTK platform.
    /// <para>Note: Developers can inherit from this class in their native GTK applications to use the custom scroll view functionality.</para>
    /// </summary>
    public class BaseCustomScrollViewRenderer : ViewRenderer<ScrollView, ScrolledWindow>
    {
        #region Fields
        private double _dragStartX;
        private double _dragStartY;
        private DateTime _previousScrollTime;
        private double _xRate;
        private double _yRate;
        private bool _mouseDown;
        private VisualElement _currentView;
        private Viewport _viewPort;
        #endregion

        protected IScrollViewController Controller
        {
            get { return Element; }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ScrollView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                e.OldElement.ScrollToRequested -= OnScrollToRequested;
            }

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    // Use Gtk.ScrolledWindow that adds scrollbars to its child widget.
                    Control = new ScrolledWindow
                    {
                        CanFocus = true,
                        ShadowType = ShadowType.None,
                        BorderWidth = 0,
                        HscrollbarPolicy = ScrollBarVisibilityToGtk(Element.HorizontalScrollBarVisibility),
                        VscrollbarPolicy = ScrollBarVisibilityToGtk(Element.VerticalScrollBarVisibility)
                    };

                    _viewPort = new Viewport();
                    _viewPort.ShadowType = ShadowType.None;
                    _viewPort.BorderWidth = 0;

                    Control.Add(_viewPort);
                    SetNativeControl(Control);

                    Control.Hadjustment.ValueChanged += OnScrollEvent;
                    Control.Vadjustment.ValueChanged += OnScrollEvent;
                    Control.ButtonPressEvent += Control_ButtonPressEvent;
                    Control.ButtonReleaseEvent += Control_ButtonReleaseEvent;
                    Control.MotionNotifyEvent += Control_MotionNotifyEvent;
                }

                Element.ScrollToRequested += OnScrollToRequested;

                UpdateOrientation();
                LoadContent();
                UpdateContentSize();
                UpdateVerticalScrollBarVisibility();
                UpdateHorizontalScrollBarVisibility();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ScrollView.ContentSizeProperty.PropertyName)
                UpdateContentSize();
            else if (e.PropertyName == nameof(ScrollView.Content))
                LoadContent();
            else if (e.PropertyName == ScrollView.OrientationProperty.PropertyName)
                UpdateOrientation();
            else if (e.PropertyName == ScrollView.VerticalScrollBarVisibilityProperty.PropertyName)
                UpdateVerticalScrollBarVisibility();
            else if (e.PropertyName == ScrollView.HorizontalScrollBarVisibilityProperty.PropertyName)
                UpdateHorizontalScrollBarVisibility();
        }

        protected override void Dispose(bool disposing)
        {
            if (Control != null)
            {
                Control.ScrollEvent -= OnScrollEvent;
                Control.ButtonPressEvent -= Control_ButtonPressEvent;
                Control.ButtonReleaseEvent -= Control_ButtonReleaseEvent;
                Control.MotionNotifyEvent -= Control_MotionNotifyEvent;

                if (Control.Hadjustment != null)
                    Control.Hadjustment.ValueChanged -= OnScrollEvent;

                if (Control.Vadjustment != null)
                    Control.Vadjustment.ValueChanged -= OnScrollEvent;
            }
            if (_viewPort != null)
            {
                _viewPort.Destroy();
                _viewPort = null;
            }

            base.Dispose(disposing);
        }

        protected override void UpdateBackgroundColor()
        {
            if (Element.BackgroundColor.IsDefaultOrTransparent())
            {
                return;
            }

            var backgroundColor = Element.BackgroundColor;

            if (Control != null)
            {
                Control.ModifyBg(StateType.Normal, Extensions.ColorExtensions.ToGtkColor(backgroundColor));
            }

            if (_viewPort != null)
            {
                _viewPort.ModifyBg(StateType.Normal, Extensions.ColorExtensions.ToGtkColor(backgroundColor));
            }

            base.UpdateBackgroundColor();
        }

        private void OnScrollEvent(object o, EventArgs args)
        {
            Controller.SetScrolledPosition(Control.Hadjustment.Value, Control.Vadjustment.Value);
        }

        private void LoadContent()
        {
            if (_currentView != null)
            {
                _currentView.Cleanup();
            }

            _currentView = Element.Content;

            IVisualElementRenderer renderer = null;

            if (_currentView != null)
            {
                renderer = _currentView.GetOrCreateRenderer();
                renderer.Container.Unparent();
            }

            if (renderer != null)
            {
                var content = renderer.Container;
                _viewPort.Add(content);
            }
        }

        private void UpdateOrientation()
        {
            if (Control == null)
                return;

            switch (Element.Orientation)
            {
                case ScrollOrientation.Vertical:
                    Control.HscrollbarPolicy = PolicyType.Never;
                    Control.VscrollbarPolicy = ScrollBarVisibilityToGtk(Element.VerticalScrollBarVisibility);
                    break;
                case ScrollOrientation.Horizontal:
                    Control.HscrollbarPolicy = ScrollBarVisibilityToGtk(Element.HorizontalScrollBarVisibility);
                    Control.VscrollbarPolicy = PolicyType.Never;
                    break;
                case ScrollOrientation.Both:
                    Control.HscrollbarPolicy = ScrollBarVisibilityToGtk(Element.HorizontalScrollBarVisibility);
                    Control.VscrollbarPolicy = ScrollBarVisibilityToGtk(Element.VerticalScrollBarVisibility);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Element.Orientation));
            }
        }

        private void OnScrollToRequested(object sender, ScrollToRequestedEventArgs e)
        {
            double x = e.ScrollX, y = e.ScrollY;

            ScrollToMode mode = e.Mode;

            if (mode == ScrollToMode.Element)
            {
                Point pos = Controller.GetScrollPositionForElement((VisualElement)e.Element, e.Position);
                x = pos.X;
                y = pos.Y;
                mode = ScrollToMode.Position;
            }

            if (mode == ScrollToMode.Position)
            {
                Control.Hadjustment.Value = x;
                Control.Vadjustment.Value = y;
            }

            Controller.SendScrollFinished();
        }

        private void UpdateContentSize()
        {
            if (Control == null)
                return;

            var contentSize = Element.ContentSize;

            var height = Convert.ToInt32(contentSize.Height);
            var width = Convert.ToInt32(contentSize.Width);

            Control.SetSizeRequest(width, height);
        }

        void UpdateVerticalScrollBarVisibility()
        {
            Control.VscrollbarPolicy = ScrollBarVisibilityToGtk(Element.VerticalScrollBarVisibility);
        }

        void UpdateHorizontalScrollBarVisibility()
        {
            var orientation = Element.Orientation;
            if (orientation == ScrollOrientation.Horizontal || orientation == ScrollOrientation.Both)
                Control.HscrollbarPolicy = ScrollBarVisibilityToGtk(Element.HorizontalScrollBarVisibility);
        }

        PolicyType ScrollBarVisibilityToGtk(ScrollBarVisibility visibility)
        {
            switch (visibility)
            {
                case ScrollBarVisibility.Default:
                    return PolicyType.Automatic;
                case ScrollBarVisibility.Always:
                    return PolicyType.Always;
                case ScrollBarVisibility.Never:
                    return PolicyType.Never;
                default:
                    return PolicyType.Automatic;
            }
        }

        private void Control_ButtonPressEvent(object o, ButtonPressEventArgs args)
        {
            var buttonArgs = (Gdk.EventButton)args.Args[0];
            _dragStartX = buttonArgs.X;
            _dragStartY = buttonArgs.Y;
            _previousScrollTime = DateTime.Now;
            CancelScrollAnimations();
            _mouseDown = true;

            //Console.WriteLine("ButtonDown");
        }

        private void CancelScrollAnimations()
        {
            Xamarin.Forms.Application.Current.MainPage.AbortAnimation("CustomXScrollAnimation");
            Xamarin.Forms.Application.Current.MainPage.AbortAnimation("CustomYScrollAnimation");
        }

        private void Control_ButtonReleaseEvent(object o, ButtonReleaseEventArgs args)
        {
            _mouseDown = false;

            var xRateStart = _xRate;
            var yRateStart = _yRate;

            uint animationRate = 100;
            var xScrollAnimation = new Animation((rate) => AnimateXScroll(rate, animationRate), xRateStart, 0);
            xScrollAnimation.Commit(Xamarin.Forms.Application.Current.MainPage, "CustomXScrollAnimation", rate: animationRate, easing: Easing.CubicOut, length: 2000);

            var yScrollAnimation = new Animation((rate) => AnimateYScroll(rate, animationRate), yRateStart, 0);
            yScrollAnimation.Commit(Xamarin.Forms.Application.Current.MainPage, "CustomYScrollAnimation", rate: animationRate, easing: Easing.CubicOut, length: 2000);
        }

        private void AnimateXScroll(double xRate, uint millisecondDelay)
        {
            var xTravel = (double)xRate * millisecondDelay / 1000;
            ArtificiallyScroll(xTravel, 0, false);
        }

        private void AnimateYScroll(double yRate, uint millisecondDelay)
        {
            var yTravel = (double)yRate * millisecondDelay / 1000;
            ArtificiallyScroll(0, yTravel, false);
            //Debug.WriteLine($"yTravel: {yTravel} | yRate: {yRate}");
        }

        private void Control_MotionNotifyEvent(object o, MotionNotifyEventArgs args)
        {
            if (!_mouseDown)
            {
                return;
            }

            var motionArgs = (Gdk.EventMotion)args.Args[0];
            var dragEndX = motionArgs.X;
            var dragEndY = motionArgs.Y;

            var xAdjustment = _dragStartX - dragEndX;
            var yAdjustment = _dragStartY - dragEndY;

            ArtificiallyScroll(xAdjustment, yAdjustment);
        }

        private void CalculateScrollRates(double xDiff, double yDiff)
        {
            var timeDiffInMilliseconds = (DateTime.Now - _previousScrollTime).TotalMilliseconds;

            //Console.WriteLine($"TimeDiff: {timeDiffInMilliseconds} | xDiff: {xDiff} | yDiff: {yDiff}");

            _xRate = xDiff * 1000 / timeDiffInMilliseconds;
            _yRate = yDiff * 1000 / timeDiffInMilliseconds;

            var minRate = 25;

            if (_xRate < minRate && _xRate >= 0)
            {
                _xRate = minRate;
            }
            else if (_xRate < 0 && _xRate > -minRate)
            {
                _xRate = -minRate;
            }

            if (_yRate < minRate && _yRate >= 0)
            {
                _yRate = minRate;
            }
            else if (_yRate < 0 && _yRate > -minRate)
            {
                _yRate = -minRate;
            }

            //Console.WriteLine(Environment.NewLine + Environment.NewLine);
            //Console.WriteLine($"X Rate: {_xRate}");
            //Console.WriteLine($"Y Rate: {_yRate + Environment.NewLine + Environment.NewLine}");
        }

        private void ArtificiallyScroll(double scrollX, double scrollY, bool setScrollRates = true)
        {
            if (Control == null)
            {
                CancelScrollAnimations();
                return;
            }

            if (scrollX == 0 && scrollY == 0)
            {
                return;
            }

            if (setScrollRates)
            {
                CalculateScrollRates(scrollX, scrollY);
            }

            if (Control.HScrollbar.Visible && Control.Hadjustment != null)
            {
                if (scrollX > Control.Hadjustment.Upper - Control.Hadjustment.Value - Control.Hadjustment.PageSize)
                {
                    Control.Hadjustment.Value = Control.Hadjustment.Upper - Control.Hadjustment.PageSize;
                }
                else
                {
                    Control.Hadjustment.Value += scrollX;
                }
            }

            if (Control.VScrollbar.Visible && Control.Vadjustment != null)
            {
                if (scrollY > Control.Vadjustment.Upper - Control.Vadjustment.Value - Control.Vadjustment.PageSize)
                {
                    Control.Vadjustment.Value = Control.Vadjustment.Upper - Control.Vadjustment.PageSize;
                }
                else
                {
                    Control.Vadjustment.Value += scrollY;
                }
            }

            Controller.SendScrollFinished();
        }
    }
}
