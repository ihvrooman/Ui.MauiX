using Components.Helpers;
using Ui.MauiX.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Ui.MauiX.Models
{
    public class ChatSettings : NotifyPropertyChanged
    {
        #region Fields
        private double _messageSpacing = 12.0;
        private double _messageFontSize = 14.0;
        private Thickness _messagePadding = new Thickness(12);
        private CornerRadius _messageCornerRadius = new CornerRadius(15);
        private Thickness _padding = new Thickness(10);
        private Color _backgroundColor = Color.White;
        private Color _userMessageColor = CustomColors.Blue;
        private Color _nonUserMessageColor = CustomColors.BackgroundColor;
        private Color _userMessageTextColor = Color.White;
        private Color _nonUserMessageTextColor = CustomColors.TextColor;
        #endregion

        #region Properties
        public double MessageSpacing { get => _messageSpacing; set => SetProperty(ref _messageSpacing, value); }

        public double MessageFontSize { get => _messageFontSize; set => SetProperty(ref _messageFontSize, value); }

        public Thickness MessagePadding { get => _messagePadding; set => SetProperty(ref _messagePadding, value); }

        public CornerRadius MessageCornerRadius { get => _messageCornerRadius; set => SetProperty(ref _messageCornerRadius, value); }

        public Thickness Padding { get => _padding; set => SetProperty(ref _padding, value); }

        public Color BackgroundColor { get => _backgroundColor; set => SetProperty(ref _backgroundColor, value); }

        public Color UserMessageColor { get => _userMessageColor; set => SetProperty(ref _userMessageColor, value); }

        public Color NonUserMessageColor { get => _nonUserMessageColor; set => SetProperty(ref _nonUserMessageColor, value); }

        public Color UserMessageTextColor { get => _userMessageTextColor; set => SetProperty(ref _userMessageTextColor, value); }

        public Color NonUserMessageTextColor { get => _nonUserMessageTextColor; set => SetProperty(ref _nonUserMessageTextColor, value); }
        #endregion
    }
}
