namespace ColorPicker
{
	partial class ClickedColorCopy
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxColorHexString = new System.Windows.Forms.TextBox();
			this.buttonCopyToClipboardColorHexString = new System.Windows.Forms.Button();
			this.buttonCopyToClipboardColorRGBString = new System.Windows.Forms.Button();
			this.textBoxColorRGBString = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.labelStatusText = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "HEX";
			// 
			// textBoxColorHexString
			// 
			this.textBoxColorHexString.BackColor = System.Drawing.SystemColors.Control;
			this.textBoxColorHexString.Location = new System.Drawing.Point(53, 27);
			this.textBoxColorHexString.Name = "textBoxColorHexString";
			this.textBoxColorHexString.ReadOnly = true;
			this.textBoxColorHexString.Size = new System.Drawing.Size(128, 20);
			this.textBoxColorHexString.TabIndex = 1;
			// 
			// buttonCopyToClipboardColorHexString
			// 
			this.buttonCopyToClipboardColorHexString.Location = new System.Drawing.Point(187, 25);
			this.buttonCopyToClipboardColorHexString.Name = "buttonCopyToClipboardColorHexString";
			this.buttonCopyToClipboardColorHexString.Size = new System.Drawing.Size(162, 23);
			this.buttonCopyToClipboardColorHexString.TabIndex = 2;
			this.buttonCopyToClipboardColorHexString.Text = "Copy &HEX value to clipboard";
			this.buttonCopyToClipboardColorHexString.UseVisualStyleBackColor = true;
			this.buttonCopyToClipboardColorHexString.Click += new System.EventHandler(this.buttonCopyToClipboardColorHexString_Click);
			// 
			// buttonCopyToClipboardColorRGBString
			// 
			this.buttonCopyToClipboardColorRGBString.Location = new System.Drawing.Point(187, 64);
			this.buttonCopyToClipboardColorRGBString.Name = "buttonCopyToClipboardColorRGBString";
			this.buttonCopyToClipboardColorRGBString.Size = new System.Drawing.Size(162, 23);
			this.buttonCopyToClipboardColorRGBString.TabIndex = 5;
			this.buttonCopyToClipboardColorRGBString.Text = "Copy &RGB value to clipboard";
			this.buttonCopyToClipboardColorRGBString.UseVisualStyleBackColor = true;
			this.buttonCopyToClipboardColorRGBString.Click += new System.EventHandler(this.buttonCopyToClipboardColorRGBString_Click);
			// 
			// textBoxColorRGBString
			// 
			this.textBoxColorRGBString.BackColor = System.Drawing.SystemColors.Control;
			this.textBoxColorRGBString.Location = new System.Drawing.Point(53, 66);
			this.textBoxColorRGBString.Name = "textBoxColorRGBString";
			this.textBoxColorRGBString.ReadOnly = true;
			this.textBoxColorRGBString.Size = new System.Drawing.Size(128, 20);
			this.textBoxColorRGBString.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 69);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(30, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "RGB";
			// 
			// labelStatusText
			// 
			this.labelStatusText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelStatusText.AutoSize = true;
			this.labelStatusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelStatusText.ForeColor = System.Drawing.Color.Green;
			this.labelStatusText.Location = new System.Drawing.Point(12, 116);
			this.labelStatusText.Name = "labelStatusText";
			this.labelStatusText.Size = new System.Drawing.Size(35, 13);
			this.labelStatusText.TabIndex = 6;
			this.labelStatusText.Text = "label3";
			// 
			// ClickedColorCopy
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(361, 138);
			this.Controls.Add(this.labelStatusText);
			this.Controls.Add(this.buttonCopyToClipboardColorRGBString);
			this.Controls.Add(this.textBoxColorRGBString);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonCopyToClipboardColorHexString);
			this.Controls.Add(this.textBoxColorHexString);
			this.Controls.Add(this.label1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ClickedColorCopy";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Color strings";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxColorHexString;
		private System.Windows.Forms.Button buttonCopyToClipboardColorHexString;
		private System.Windows.Forms.Button buttonCopyToClipboardColorRGBString;
		private System.Windows.Forms.TextBox textBoxColorRGBString;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelStatusText;
	}
}