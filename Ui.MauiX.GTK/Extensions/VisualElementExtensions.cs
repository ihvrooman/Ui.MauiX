using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

namespace Ui.MauiX.GTK.Extensions
{
	public static class VisualElementExtensions
	{
		internal static IEnumerable<Element> GetParentsPath(this VisualElement self)
		{
			Element current = self;

			while (!Application.IsApplicationOrNull(current.RealParent))
			{
				current = current.RealParent;
				yield return current;
			}
		}

		internal static void Cleanup(this VisualElement self)
		{
			if (self == null)
				throw new ArgumentNullException("self");

			IVisualElementRenderer renderer = Platform.GetRenderer(self);

			foreach (Element element in self.Descendants())
			{
				var visual = element as VisualElement;
				if (visual == null)
					continue;

				IVisualElementRenderer childRenderer = Platform.GetRenderer(visual);
				if (childRenderer != null)
				{
					((Gtk.Widget)childRenderer).Destroy();
					Platform.SetRenderer(visual, null);
				}
			}

			if (renderer != null)
			{
				((Gtk.Widget)renderer).Destroy();
				Platform.SetRenderer(self, null);
			}
		}
	}
}
