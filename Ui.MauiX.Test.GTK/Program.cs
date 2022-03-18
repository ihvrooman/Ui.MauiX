using Ui.MauiX.CustomControls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;
using Ui.MauiX.Helpers;

namespace Ui.MauiX.Test.GTK
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            GLib.ExceptionManager.UnhandledException += ExceptionManager_UnhandledException;
            Gtk.Application.Init();
            Forms.Init();

            var app = new App();
            var window = new FormsWindow
            {
                Modal = true,
                AllowGrow = false,
                Decorated = false,
                Resizable = false,
                WindowPosition = Gtk.WindowPosition.CenterAlways,
                HeightRequest = 480,
                WidthRequest = 800
            };

            //Small screen size
            //HeightRequest = 480,
            //WidthRequest = 800,

            //Large screen size
            //HeightRequest = 600,
            //WidthRequest = 1024,

            window.LoadApplication(app);
            window.SetApplicationTitle("Ui.MauiX.Test.GTK");
            window.Show();

            Gtk.Application.Run();
        }

        private static async void ExceptionManager_UnhandledException(GLib.UnhandledExceptionArgs args)
        {
            await App.Current.MainPage.ShowErrorMessageAsync($"An unexpected error occurred.{Environment.NewLine}Details: {args.ExceptionObject}", "Unexpected Error");
        }
    }
}
