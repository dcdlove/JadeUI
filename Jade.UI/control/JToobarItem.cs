/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-08-09 16:36:24
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jade.UI
{
	[DefaultEvent("Click")]
	public partial class JToobarItem : UserControl
	{
		#region 属性

		private Image _IconImage;

		[Category("JadeControl"),
		Description("显示图标"),
		Browsable(true)]
		public Image IconImage
		{
			get
			{
				if (_IconImage == null)
				{
					return Jade.UI.Properties.Resources.msgbox_question;
				}
				return _IconImage;
			}
			set
			{
				_IconImage = value;
				this.Invalidate();

			}
		}



		private bool _Checked;
		[Category("JadeControl"),
		Description("是否选中"),
		Browsable(true)]
		public bool Checked
		{
			get { return _Checked; }
			set
			{
				_Checked = value;
				if (value)
					BackColor = Color.FromArgb(50, 255, 255, 255);
				else
					BackColor = Color.Transparent;

				this.Invalidate();
			}
		}

		private string _Title = "APP标题";

		[Category("JadeControl"),
		Description("标题"),
		Browsable(true)]
		public string Title
		{
			get { return _Title; }
			set { _Title = value; this.Invalidate(); }
		}


		#endregion

		RectangleF rec;
		public JToobarItem()
		{
			InitializeComponent();
			SetStyles();
			Init();
		}

		public JToobarItem(string text, Image icon)
		{
			InitializeComponent();
			IconImage = icon;
			Text = text;
			Init();
		}

		public void Init()
		{
			this.Size = new System.Drawing.Size(90, 100);
			MouseEnter += new EventHandler(JApp_MouseEnter);
			MouseLeave += new EventHandler(JApp_MouseLeave);
			rec = new RectangleF() { Height = 30, Width = Width, X = 0, Y = Height - 34 };
			DoubleClick += new EventHandler(JApp_Click);
			//RenderHelper.SetFormRoundRectRgn(this, 5);
		}

		void JApp_Click(object sender, EventArgs e)
		{
			try
			{

			}
			catch (Exception ex)
			{

				//MsgBox.Warn(ex.ToString());
			}

		}

		void JApp_MouseLeave(object sender, EventArgs e)
		{
			if (!Checked)
			{
				this.BackColor = Color.Transparent;
			}


		}

		void JApp_MouseEnter(object sender, EventArgs e)
		{
			this.BackColor = Color.FromArgb(50, 255, 255, 255);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.DrawImage(IconImage, new Rectangle(new Point(45 / 2, 15), new Size(45, 45)));
			// 绘制高光  

			e.Graphics.DrawString(Title, Font, SystemBrushes.ControlLightLight, rec, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });
			e.Graphics.DrawString(Title, Font, new SolidBrush(ForeColor), rec, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });
		}



		private void SetStyles()
		{
			SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.ResizeRedraw |
				ControlStyles.DoubleBuffer, true);
			//强制分配样式重新应用到控件上
			UpdateStyles();
			base.AutoScaleMode = AutoScaleMode.None;
		}


	}
}
