using Gtk;
using Ui.MauiX.Test.GTK;
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
using Ui.MauiX.GTK.CustomRenderers;

[assembly: ExportRenderer(typeof(ScrollView), typeof(CustomScrollViewRenderer))]
namespace Ui.MauiX.Test.GTK
{
    public class CustomScrollViewRenderer : BaseCustomScrollViewRenderer
    {

    }
}
