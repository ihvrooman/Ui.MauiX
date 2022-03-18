using Ui.MauiX.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Chat : ContentView
    {
        #region Properties
        public ObservableCollection<ChatMessage> Messages { get => (ObservableCollection<ChatMessage>)GetValue(MessagesProperty); set => SetValue(MessagesProperty, value); }

        public static readonly BindableProperty MessagesProperty = BindableProperty.Create(nameof(Messages), typeof(ObservableCollection<ChatMessage>), typeof(Chat));

        public ChatSettings Settings { get => (ChatSettings)GetValue(SettingsProperty); set => SetValue(SettingsProperty, value); }

        public static readonly BindableProperty SettingsProperty = BindableProperty.Create(nameof(Settings), typeof(ChatSettings), typeof(Chat), new ChatSettings());
        #endregion

        #region Constructor
        public Chat()
        {
            InitializeComponent();
        }
        #endregion
    }
}