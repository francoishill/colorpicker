using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorPicker
{
	static class Program
	{
		private static NotifyIcon icon;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run(new Form1());
			icon = new NotifyIcon();
			icon.Icon = new Icon(
				Assembly.GetEntryAssembly().GetManifestResourceStream("ColorPicker.app.ico"));
			icon.Visible = true;
			icon.ContextMenu = GetNotifyiconContextmenu();
			icon.MouseClick += (sn, ev) =>
			{
				if (ev.Button == MouseButtons.Left)
					new OverlayForm().ShowDialog();
			};

			while (icon.Visible)
				Application.DoEvents();
		}

		private static ContextMenu GetNotifyiconContextmenu()
		{
			var menu = new ContextMenu();
			menu.MenuItems.Add(new MenuItem("E&xit", delegate
			{
				icon.Visible = false;
				Environment.Exit(0);
			}));
			return menu;
		}
	}
}
