using Gdk;
using Gtk;
using Ui.MauiX.GTK.Extensions;
using System;
using System.ComponentModel;
using Xamarin.Forms.Platform.GTK.Extensions;
using Xamarin.Forms.Platform.GTK.Helpers;
using NativeLabel = Gtk.Label;

namespace Ui.MauiX.GTK.CustomRenderers
{
    /// <summary>
    /// A base implementation of a custom label renderer for the GTK platform.
    /// <para>Note: Developers can inherit from this class in their native GTK applications to use the custom label functionality.</para>
    /// </summary>
    public class BaseCustomLabelRenderer : Xamarin.Forms.Platform.GTK.ViewRenderer<Xamarin.Forms.Label, NativeLabel>
    {
        private Xamarin.Forms.SizeRequest _perfectSize;
        private bool _perfectSizeValid;
        private bool _allocated;

        public override Xamarin.Forms.SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            if (!_allocated && PlatformHelper.GetGTKPlatform() == GTKPlatform.Windows)
            {
                return default(Xamarin.Forms.SizeRequest);
            }

            if (!_perfectSizeValid)
            {
                _perfectSize = GetPerfectSize();
                _perfectSize.Minimum = new Xamarin.Forms.Size(Math.Min(10, _perfectSize.Request.Width), _perfectSize.Request.Height);
                _perfectSizeValid = true;
            }

            var widthFits = widthConstraint >= _perfectSize.Request.Width;
            var heightFits = heightConstraint >= _perfectSize.Request.Height;

            if (widthFits && heightFits)
                return _perfectSize;

            var result = GetPerfectSize((int)widthConstraint);
            var tinyWidth = Math.Min(10, result.Request.Width);
            result.Minimum = new Xamarin.Forms.Size(tinyWidth, result.Request.Height);

            if (widthFits || Element.LineBreakMode == Xamarin.Forms.LineBreakMode.NoWrap)
            {
                return new Xamarin.Forms.SizeRequest(
                    new Xamarin.Forms.Size(result.Request.Width, _perfectSize.Request.Height),
                    new Xamarin.Forms.Size(result.Minimum.Width, _perfectSize.Request.Height));
            }

            bool containerIsNotInfinitelyWide = !double.IsInfinity(widthConstraint);

            if (containerIsNotInfinitelyWide)
            {
                bool textCouldHaveWrapped = Element.LineBreakMode == Xamarin.Forms.LineBreakMode.WordWrap || Element.LineBreakMode == Xamarin.Forms.LineBreakMode.CharacterWrap;
                bool textExceedsContainer = result.Request.Width > widthConstraint;

                if (textExceedsContainer || textCouldHaveWrapped)
                {
                    var expandedWidth = Math.Max(tinyWidth, widthConstraint);
                    result.Request = new Xamarin.Forms.Size(expandedWidth, result.Request.Height);
                }
            }

            return result;
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.GTK.ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new NativeLabel());
                }

                UpdateText();
                UpdateColor();
                UpdateLineBreakMode();
                UpdateTextAlignment();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Xamarin.Forms.Label.TextColorProperty.PropertyName)
                UpdateColor();
            else if (e.PropertyName == Xamarin.Forms.Label.FontProperty.PropertyName)
                UpdateText();
            else if (e.PropertyName == Xamarin.Forms.Label.TextProperty.PropertyName)
                UpdateText();
            else if (e.PropertyName == Xamarin.Forms.Label.FontAttributesProperty.PropertyName)
                UpdateText();
            else if (e.PropertyName == Xamarin.Forms.Label.FormattedTextProperty.PropertyName)
                UpdateText();
            else if (e.PropertyName == Xamarin.Forms.Label.TextTransformProperty.PropertyName)
                UpdateText();
            else if (e.PropertyName == Xamarin.Forms.Label.HorizontalTextAlignmentProperty.PropertyName || e.PropertyName == Xamarin.Forms.Label.VerticalTextAlignmentProperty.PropertyName)
                UpdateTextAlignment();
            else if (e.PropertyName == Xamarin.Forms.Label.LineBreakModeProperty.PropertyName)
                UpdateLineBreakMode();
        }

        protected override void SetAccessibilityLabel()
        {
            var elemValue = (string)Element?.GetValue(Xamarin.Forms.AutomationProperties.NameProperty);

            if (string.IsNullOrWhiteSpace(elemValue)
                && Control?.Accessible.Description == Control?.Text)
                return;

            base.SetAccessibilityLabel();
        }

        protected override void OnSizeAllocated(Gdk.Rectangle allocation)
        {
            base.OnSizeAllocated(allocation);

            _allocated = true;

            Control.Layout.Width = Pango.Units.FromPixels((int)Element.Bounds.Width);
        }

        private void UpdateText()
        {
            _perfectSizeValid = false;

            string markupText = string.Empty;
            Xamarin.Forms.FormattedString formatted = Element.FormattedText;

            if (formatted != null)
            {
                Control.SetTextFromFormatted(formatted);
            }
            else
            {
                var span = new Xamarin.Forms.Span()
                {
                    FontAttributes = Element.FontAttributes,
                    FontFamily = Element.FontFamily,
                    FontSize = Element.FontSize,
                    Text = Element.UpdateFormsText(Element.Text ?? string.Empty, Element.TextTransform) ?? string.Empty
                };

                Control.SetTextFromSpan(span);
            }
        }

        private void UpdateColor()
        {
            if (Control == null)
                return;

            var textColor = Element.TextColor != Xamarin.Forms.Color.Default ? Element.TextColor : Xamarin.Forms.Color.Black;

            Control.ModifyFg(StateType.Normal, Extensions.ColorExtensions.ToGtkColor(textColor));
        }

        private void UpdateTextAlignment()
        {
            _perfectSizeValid = false;

            var hAlignmentValue = Element.HorizontalTextAlignment.ToAlignmentValue();
            var vAlignmentValue = Element.VerticalTextAlignment.ToAlignmentValue();

            Control.SetAlignment(hAlignmentValue, vAlignmentValue);
        }

        private void UpdateLineBreakMode()
        {
            _perfectSizeValid = false;

            switch (Element.LineBreakMode)
            {
                case Xamarin.Forms.LineBreakMode.NoWrap:
                    Control.LineWrap = false;
                    Control.Ellipsize = Pango.EllipsizeMode.None;
                    break;
                case Xamarin.Forms.LineBreakMode.WordWrap:
                    Control.LineWrap = true;
                    Control.LineWrapMode = Pango.WrapMode.Word;
                    Control.Ellipsize = Pango.EllipsizeMode.None;
                    break;
                case Xamarin.Forms.LineBreakMode.CharacterWrap:
                    Control.LineWrap = true;
                    Control.LineWrapMode = Pango.WrapMode.Char;
                    Control.Ellipsize = Pango.EllipsizeMode.None;
                    break;
                case Xamarin.Forms.LineBreakMode.HeadTruncation:
                    Control.LineWrap = false;
                    Control.LineWrapMode = Pango.WrapMode.Word;
                    Control.Ellipsize = Pango.EllipsizeMode.Start;
                    break;
                case Xamarin.Forms.LineBreakMode.TailTruncation:
                    Control.LineWrap = false;
                    Control.LineWrapMode = Pango.WrapMode.Word;
                    Control.Ellipsize = Pango.EllipsizeMode.End;
                    break;
                case Xamarin.Forms.LineBreakMode.MiddleTruncation:
                    Control.LineWrap = false;
                    Control.LineWrapMode = Pango.WrapMode.Word;
                    Control.Ellipsize = Pango.EllipsizeMode.Middle;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Xamarin.Forms.SizeRequest GetPerfectSize(int widthConstraint = -1)
        {
            int w, h;
            Control.Layout.Width = Pango.Units.FromPixels(widthConstraint);
            Control.Layout.GetPixelSize(out w, out h);

            return new Xamarin.Forms.SizeRequest(new Xamarin.Forms.Size(w, h));
        }
    }
}
