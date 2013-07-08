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
	public partial class OverlayForm : Form
	{
		private const int cMaxRectSideLength = 50;
		private const int cRectangleZoomFactor = 8;
		private const int cHandPointerTolerance = 2;//Remeber it is scaled with the cRectangleZoomFactor
		private bool alreadyHaveZoomRectangle = false;
		private bool alreadyHaveEncircledPixel = false;
		private Image originalImage = null;
		private Point? mouseDownPoint = null;
		private Point? lastEncircledPoint_ActualScale = null;

		public OverlayForm()
		{
			InitializeComponent();

		}

		private void OverlayForm_Load(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Maximized;

			var screenToCopy = Screen.PrimaryScreen;
			Bitmap bitmap = new Bitmap(screenToCopy.Bounds.Width, screenToCopy.Bounds.Height);
			Graphics graphics = Graphics.FromImage(bitmap as Image);
			graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
			DrawOnscreenBar(screenToCopy.Bounds, graphics);

			pictureBox1.Image = bitmap;
			pictureBox1.Refresh();

			originalImage = new Bitmap(pictureBox1.Image);
		}

		private void DrawOnscreenBar(Rectangle workBounds, Graphics graphics)
		{
			Pen boundRectColor = Pens.Green;
			graphics.DrawRectangle(boundRectColor, workBounds.Left, workBounds.Top, workBounds.Width, workBounds.Height);
			graphics.DrawRectangle(boundRectColor, workBounds.Left + 1, workBounds.Top + 1, workBounds.Width - 2, workBounds.Height - 2);
			graphics.DrawRectangle(boundRectColor, workBounds.Left + 2, workBounds.Top + 2, workBounds.Width - 4, workBounds.Height - 4);

			var barHeight = 25;
			var barWidth = 600;
			graphics.FillRectangle(
				new SolidBrush(boundRectColor.Color),
				workBounds.Left + workBounds.Width / 2 - barWidth / 2,
				workBounds.Bottom - barHeight,
				barWidth,
				barHeight);

			var helpText = "Right click to close overlay, hold left-click and drag to choose zoom rectangle. Maximum rectangle side length = " + cMaxRectSideLength;
			var helptextFont = (Font)this.Font.Clone();
			var stringSize = graphics.MeasureString(helpText, helptextFont);
			var stringLocation = new PointF(
				workBounds.Left + workBounds.Width / 2 - stringSize.Width / 2,
				workBounds.Bottom - barHeight / 2 - stringSize.Height / 2);
			graphics.DrawString(helpText, helptextFont, Brushes.White, stringLocation);
		}

		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
				this.Close();
			//else if (e.Button == System.Windows.Forms.MouseButtons.Left)
			//{
			//	if (alreadyHaveZoomRectangle
			//		&& alreadyHaveEncircledPixel)
			//	{
			//		//Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			//		//Graphics graphics = Graphics.FromImage(bitmap as Image);
			//		//graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
			//		//var color = bitmap.GetPixel(e.X, e.Y);
			//		if (lastEncircledPoint_ActualScale.HasValue
			//			&& IsMouseCloseToAlreadyEncircledPixel(e))
			//		{
			//			var point = lastEncircledPoint_ActualScale.Value;
			//			var color = (pictureBox1.Image as Bitmap).GetPixel(point.X, point.Y);
			//			UserMessages.ShowInfoMessage(HexConverter(color));
			//		}
			//	}
			//}
		}

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			mouseDownPoint = e.Location;
			if (alreadyHaveZoomRectangle
				&& !IsMouseCloseToAlreadyEncircledPixel(e))
				EncircleCurrentPixel(e);
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			if (!alreadyHaveEncircledPixel
				&& !MouseButtons.HasFlag(MouseButtons.Left)
					|| originalImage == null
					|| !mouseDownPoint.HasValue)
			{
				ResetImageOnMouseUp();
				return;
			}

			if (alreadyHaveEncircledPixel
				&& !MouseButtons.HasFlag(MouseButtons.Left))
			{
				if (IsMouseCloseToAlreadyEncircledPixel(e))
					this.Cursor = Cursors.Hand;
				else
					this.Cursor = Cursors.Default;
			}
			else if (alreadyHaveZoomRectangle)
			{
				EncircleCurrentPixel(e);
			}
			else
			{
				var rectWidth = e.X - mouseDownPoint.Value.X;
				var rectHeight = e.Y - mouseDownPoint.Value.Y;
				if (!IsRectangleDimensionsCorrect(rectWidth, rectHeight))
					return;

				if (rectWidth > cMaxRectSideLength)
					rectWidth = cMaxRectSideLength;
				if (rectHeight > cMaxRectSideLength)
					rectHeight = cMaxRectSideLength;
				pictureBox1.Image = new Bitmap(originalImage);
				Graphics graphics = Graphics.FromImage(pictureBox1.Image as Image);
				graphics.DrawRectangle(Pens.Red, new Rectangle(mouseDownPoint.Value, new Size(rectWidth, rectHeight)));
				pictureBox1.Refresh();
			}
		}

		private bool IsMouseCloseToAlreadyEncircledPixel(MouseEventArgs e)
		{
			var currentPoint = new Point(e.X / cRectangleZoomFactor, e.Y / cRectangleZoomFactor);
			return lastEncircledPoint_ActualScale.HasValue
				&& Math.Abs(currentPoint.X - lastEncircledPoint_ActualScale.Value.X) <= cHandPointerTolerance
				&& Math.Abs(currentPoint.Y - lastEncircledPoint_ActualScale.Value.Y) <= cHandPointerTolerance;
		}

		private void EncircleCurrentPixel(MouseEventArgs e)
		{
			var rectLeft = e.X / cRectangleZoomFactor - 1;
			if (rectLeft < 0)
				rectLeft = 0;

			var rectTop = e.Y / cRectangleZoomFactor - 1;
			if (rectTop < 0)
				rectTop = 0;

			var rectRight = e.X / cRectangleZoomFactor + 1;
			if (rectRight > pictureBox1.ClientSize.Width - 1)
				rectRight = pictureBox1.ClientSize.Width - 1;

			var rectBottom = e.Y / cRectangleZoomFactor + 1;
			if (rectBottom > pictureBox1.ClientSize.Height - 1)
				rectBottom = pictureBox1.ClientSize.Height - 1;

			Rectangle rect = new Rectangle(rectLeft, rectTop, rectRight - rectLeft, rectBottom - rectTop);
			pictureBox1.Image = new Bitmap(originalImage);
			Graphics graphics = Graphics.FromImage(pictureBox1.Image as Image);
			graphics.DrawRectangle(Pens.Red, rect);
			pictureBox1.Refresh();
			lastEncircledPoint_ActualScale = new Point(e.X / cRectangleZoomFactor, e.Y / cRectangleZoomFactor);
		}

		private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button != System.Windows.Forms.MouseButtons.Left)
				return;

			try
			{
				if (alreadyHaveZoomRectangle)
				{
					alreadyHaveEncircledPixel = true;

					//Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
					//Graphics graphics = Graphics.FromImage(bitmap as Image);
					//graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
					//var color = bitmap.GetPixel(e.X, e.Y);
					if (lastEncircledPoint_ActualScale.HasValue
						&& IsMouseCloseToAlreadyEncircledPixel(e))
					{
						var point = lastEncircledPoint_ActualScale.Value;
						var color = (pictureBox1.Image as Bitmap).GetPixel(point.X, point.Y);

						new ClickedColorCopy(color).ShowDialog();
					}
				}
				else
				{
					var rectWidth = e.X - mouseDownPoint.Value.X;
					var rectHeight = e.Y - mouseDownPoint.Value.Y;
					if (!IsRectangleDimensionsCorrect(rectWidth, rectHeight))
					{
						MessageBox.Show("Invalid rectangle size, width & height must be > 0", "Invalid rectable size", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}

					alreadyHaveZoomRectangle = true;

					if (rectWidth > cMaxRectSideLength)
						rectWidth = cMaxRectSideLength;
					if (rectHeight > cMaxRectSideLength)
						rectHeight = cMaxRectSideLength;

					this.pictureBox1.Image = ((Bitmap)originalImage).Clone(new Rectangle(mouseDownPoint.Value, new Size(rectWidth, rectHeight)), System.Drawing.Imaging.PixelFormat.DontCare);
					// new Bitmap(originalImage, new Size(rectWidth, rectHeight));
					pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
					this.pictureBox1.Refresh();
					this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;//If we allow resizing it complicates the drawing of the Encircled Pixel
					this.WindowState = FormWindowState.Normal;
					this.ClientSize = new Size(rectWidth * cRectangleZoomFactor, rectHeight * cRectangleZoomFactor);
					var workArea = Screen.FromPoint(new Point(this.Left, this.Top)).WorkingArea;
					this.Left = workArea.Left + workArea.Width / 2 - this.ClientSize.Width / 2;
					this.Top = workArea.Top + workArea.Height / 2 - this.ClientSize.Height / 2;

					originalImage = new Bitmap(pictureBox1.Image);
				}
			}
			finally
			{
				ResetImageOnMouseUp();
			}
		}

		private bool IsRectangleDimensionsCorrect(int width, int height)
		{
			if (width > 0 & height > 0)
				return true;
			else
				return false;
		}

		private void ResetImageOnMouseUp()
		{
			if (alreadyHaveZoomRectangle)
			{
			}
			else
			{
				mouseDownPoint = null;
				if (originalImage != null)
				{
					pictureBox1.Image = originalImage;
					pictureBox1.Refresh();
				}
			}
		}
	}
}
