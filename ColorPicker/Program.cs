using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	static class Program
	{
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
			icon.Icon = new Icon(@"C:\Francois\Dev\VSprojects\CloudNote\CloudNote\app.ico");
			icon.Visible = true;
			icon.Click += delegate
			{
				new OverlayForm().ShowDialog();
			};

			while (icon.Visible)
				Application.DoEvents();
		}
		static NotifyIcon icon;
	}
}
