using System.Collections.Generic;
using Xamarin.Forms;

namespace Ui.MauiX.Helpers
{
    public class ToolbarItemComparer : IComparer<ToolbarItem>
	{
		public int Compare(ToolbarItem x, ToolbarItem y) => x.Priority.CompareTo(y.Priority);
	}
}
