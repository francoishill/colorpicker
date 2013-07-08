using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorPicker
{
	public partial class ClickedColorCopy : Form
	{
		public ClickedColorCopy(Color color)
		{
			InitializeComponent();

			textBoxColorHexString.Text = HexConverter(color);
			textBoxColorRGBString.Text = RGBConverter(color);

			labelStatusText.Text = null;
		}

		private void SetClipboardTextAndShowStatusNotification(string newText, string statusNotificationOnSuccess)
		{
			try
			{
				var textToCopy = newText;
				Clipboard.SetText(textToCopy);
				if (Clipboard.GetText().Trim().Equals(textToCopy.Trim(), StringComparison.InvariantCultureIgnoreCase))
					ShowStatusNotification(statusNotificationOnSuccess);
				else
					MessageBox.Show("Unable to set clipboard text, please try again", "Clipboard copy failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception exc)
			{
				MessageBox.Show("Unable to set clipboard text: " + exc.Message, "Clipboard copy failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void buttonCopyToClipboardColorHexString_Click(object sender, EventArgs e)
		{
			SetClipboardTextAndShowStatusNotification(
				textBoxColorHexString.Text,
				"Successfully copied HEX value to clipboard");
		}

		private void buttonCopyToClipboardColorRGBString_Click(object sender, EventArgs e)
		{
			SetClipboardTextAndShowStatusNotification(
				textBoxColorRGBString.Text,
				"Successfully copied RGB value to clipboard");
		}

		bool busyShowingNotification = false;
		private Timer timer = null;
		private void ShowStatusNotification(string message, int duration = 2000)
		{
			if (busyShowingNotification) return;
			busyShowingNotification = true;

			try
			{
				if (timer != null)
				{
					timer.Stop();
					timer.Dispose();
					timer = null;
				}

				labelStatusText.Text = message;

				timer = new Timer();
				timer.Interval = duration;
				timer.Tick += delegate
				{
					labelStatusText.Text = null;
					timer.Stop();
					timer.Dispose();
					timer = null;
				};
				timer.Start();
			}
			finally
			{
				busyShowingNotification = false;
			}
		}

		private static String HexConverter(Color c)
		{
			return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
		}

		private static String RGBConverter(Color c)
		{
			return "RGB(" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
		}
	}
}
