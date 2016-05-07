/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-06-09 09:56:46
 * @version 1.0.0 
 */

namespace Jade.UI
{
	partial class JBaseForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JBaseForm));
			this.btnSysMin = new Jade.UI.JButton();
			this.btnSysMax = new Jade.UI.JButton();
			this.btnSysClose = new Jade.UI.JButton();
			this.btnSysSkin = new Jade.UI.JButton();
			this.btnSysMenu = new Jade.UI.JButton();
			this.SuspendLayout();
			// 
			// btnSysMin
			// 
			this.btnSysMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSysMin.BackColor = System.Drawing.Color.Transparent;
			this.btnSysMin.ForeColor = System.Drawing.Color.White;
			this.btnSysMin.HoverColor = System.Drawing.Color.Transparent;
			this.btnSysMin.HoverImage = global::Jade.UI.Properties.Resources.btn_min2;
			this.btnSysMin.Image = global::Jade.UI.Properties.Resources.btn_min1;
			this.btnSysMin.ImageMode = true;
			this.btnSysMin.Location = new System.Drawing.Point(384, 0);
			this.btnSysMin.Margin = new System.Windows.Forms.Padding(0);
			this.btnSysMin.Name = "btnSysMin";
			this.btnSysMin.NormaColor = System.Drawing.Color.Transparent;
			this.btnSysMin.NormalImage = global::Jade.UI.Properties.Resources.btn_min1;
			this.btnSysMin.NowBackColor = System.Drawing.Color.Transparent;
			this.btnSysMin.Size = new System.Drawing.Size(27, 22);
			this.btnSysMin.TabIndex = 25;
			this.btnSysMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.btnSysMin.Click += new System.EventHandler(this.btnSysMin_Click);
			// 
			// btnSysMax
			// 
			this.btnSysMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSysMax.BackColor = System.Drawing.Color.Transparent;
			this.btnSysMax.ForeColor = System.Drawing.Color.White;
			this.btnSysMax.HoverColor = System.Drawing.Color.Transparent;
			this.btnSysMax.HoverImage = global::Jade.UI.Properties.Resources.btn_max2;
			this.btnSysMax.Image = global::Jade.UI.Properties.Resources.btn_max1;
			this.btnSysMax.ImageMode = true;
			this.btnSysMax.Location = new System.Drawing.Point(411, 0);
			this.btnSysMax.Margin = new System.Windows.Forms.Padding(0);
			this.btnSysMax.Name = "btnSysMax";
			this.btnSysMax.NormaColor = System.Drawing.Color.Transparent;
			this.btnSysMax.NormalImage = global::Jade.UI.Properties.Resources.btn_max1;
			this.btnSysMax.NowBackColor = System.Drawing.Color.Transparent;
			this.btnSysMax.Size = new System.Drawing.Size(27, 22);
			this.btnSysMax.TabIndex = 24;
			this.btnSysMax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.btnSysMax.Visible = false;
			this.btnSysMax.Click += new System.EventHandler(this.btnSysNarrow_Click);
			// 
			// btnSysClose
			// 
			this.btnSysClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSysClose.BackColor = System.Drawing.Color.Transparent;
			this.btnSysClose.ForeColor = System.Drawing.Color.White;
			this.btnSysClose.HoverColor = System.Drawing.Color.Transparent;
			this.btnSysClose.HoverImage = global::Jade.UI.Properties.Resources.btn_close2;
			this.btnSysClose.ImageMode = true;
			this.btnSysClose.Location = new System.Drawing.Point(438, 0);
			this.btnSysClose.Margin = new System.Windows.Forms.Padding(0);
			this.btnSysClose.Name = "btnSysClose";
			this.btnSysClose.NormaColor = System.Drawing.Color.Transparent;
			this.btnSysClose.NormalImage = global::Jade.UI.Properties.Resources.btn_close1;
			this.btnSysClose.NowBackColor = System.Drawing.Color.Transparent;
			this.btnSysClose.Size = new System.Drawing.Size(27, 22);
			this.btnSysClose.TabIndex = 23;
			this.btnSysClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.btnSysClose.Click += new System.EventHandler(this.btnSysClose_Click);
			// 
			// btnSysSkin
			// 
			this.btnSysSkin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSysSkin.BackColor = System.Drawing.Color.Transparent;
			this.btnSysSkin.ForeColor = System.Drawing.Color.White;
			this.btnSysSkin.HoverColor = System.Drawing.Color.Transparent;
			this.btnSysSkin.HoverImage = global::Jade.UI.Properties.Resources.btn_skin_2;
			this.btnSysSkin.Image = global::Jade.UI.Properties.Resources.btn_skin_1;
			this.btnSysSkin.ImageMode = true;
			this.btnSysSkin.Location = new System.Drawing.Point(330, -1);
			this.btnSysSkin.Margin = new System.Windows.Forms.Padding(0);
			this.btnSysSkin.Name = "btnSysSkin";
			this.btnSysSkin.NormaColor = System.Drawing.Color.Transparent;
			this.btnSysSkin.NormalImage = global::Jade.UI.Properties.Resources.btn_skin_1;
			this.btnSysSkin.NowBackColor = System.Drawing.Color.Transparent;
			this.btnSysSkin.Size = new System.Drawing.Size(27, 25);
			this.btnSysSkin.TabIndex = 26;
			this.btnSysSkin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.btnSysSkin.Visible = false;
			this.btnSysSkin.Click += new System.EventHandler(this.btnSysSkin_Click);
			// 
			// btnSysMenu
			// 
			this.btnSysMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSysMenu.BackColor = System.Drawing.Color.Transparent;
			this.btnSysMenu.ForeColor = System.Drawing.Color.White;
			this.btnSysMenu.HoverColor = System.Drawing.Color.Transparent;
			this.btnSysMenu.HoverImage = global::Jade.UI.Properties.Resources.btn_menu_2;
			this.btnSysMenu.Image = global::Jade.UI.Properties.Resources.btn_menu_1;
			this.btnSysMenu.ImageMode = true;
			this.btnSysMenu.Location = new System.Drawing.Point(357, 0);
			this.btnSysMenu.Margin = new System.Windows.Forms.Padding(0);
			this.btnSysMenu.Name = "btnSysMenu";
			this.btnSysMenu.NormaColor = System.Drawing.Color.Transparent;
			this.btnSysMenu.NormalImage = global::Jade.UI.Properties.Resources.btn_menu_1;
			this.btnSysMenu.NowBackColor = System.Drawing.Color.Transparent;
			this.btnSysMenu.Size = new System.Drawing.Size(27, 22);
			this.btnSysMenu.TabIndex = 27;
			this.btnSysMenu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.btnSysMenu.Visible = false;
			// 
			// JBaseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.WindowFrame;
			this.ClientSize = new System.Drawing.Size(476, 290);
			this.Controls.Add(this.btnSysMenu);
			this.Controls.Add(this.btnSysSkin);
			this.Controls.Add(this.btnSysMin);
			this.Controls.Add(this.btnSysMax);
			this.Controls.Add(this.btnSysClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "JBaseForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Jade UI";
			this.ResumeLayout(false);

		}

		#endregion

		public JButton btnSysClose;
		public JButton btnSysMax;
		public JButton btnSysMin;
        public JButton btnSysSkin;
        public JButton btnSysMenu;
	}
}