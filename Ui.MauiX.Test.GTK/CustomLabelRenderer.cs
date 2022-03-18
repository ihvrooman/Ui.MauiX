using Ui.MauiX.GTK.CustomRenderers;
using Ui.MauiX.Test.GTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;
using Xamarin.Forms.Platform.GTK.Extensions;
using Xamarin.Forms.Platform.GTK.Helpers;
using NativeLabel = Gtk.Label;

[assembly: ExportRenderer(typeof(Label), typeof(CustomLabelRenderer))]
namespace Ui.MauiX.Test.GTK
{
	public class CustomLabelRenderer : BaseCustomLabelRenderer
    {

    }
}
