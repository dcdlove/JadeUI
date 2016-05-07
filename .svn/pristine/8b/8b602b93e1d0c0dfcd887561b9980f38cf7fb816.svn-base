/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-05-15 17:11:36
 * @version 1.0.0 
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Jade.UI
{
	[DefaultEvent("Click")]
	public class JButton : Control
	{
		private Color _NormaColor;
		private Color _HoverColor;
		private bool _IsShadow;
		

		public JButton()
			: base()
		{
			Size = new Size(80, 30);
			HoverColor = Color.FromArgb(56, 184, 0);
			NormaColor = Color.FromArgb(38, 181, 255);
			ForeColor = Color.White;
			SetStyles();
		}

		[Category("JadeControl"),
		Description("鼠标经过时背景颜色"),
		DefaultValue(typeof(Color), "#38B800")]
		public Color HoverColor
		{
			get { return _HoverColor; }
			set
			{
				_HoverColor = value;
				Invalidate();
			}
		}

		[Category("JadeControl"),
		Description("默认背景颜色"),
		DefaultValue(typeof(Color), "#42ADEC")]
		public Color NormaColor
		{
			get { return _NormaColor; }
			set
			{
				_NormaColor = value;
				BackColor = value;
				Invalidate();
			}
		}


		[Category("JadeControlEx"),
		Description("是否启用阴影")]
		public bool IsShadow
		{
			get { return _IsShadow; }
			set
			{
				if (value != _IsShadow)
				{
					Invalidate();
				}
				_IsShadow = value;
			}
		}

		[Category("JadeControl")]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		[Category("JadeControl")]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}
		[Category("JadeControl")]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}
		[Category("JadeControl")]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		[Category("JadeControl")]
		public override System.Drawing.Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			BackColor = Enabled ? HoverColor : SystemColors.ControlDark;
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			BackColor = Enabled ? NormaColor : SystemColors.ControlDark;

		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Color textColor = Enabled ? ForeColor : SystemColors.GrayText;
			using (SolidBrush sb = new SolidBrush(textColor))
			{
				StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
				e.Graphics.DrawString(Text, Font, sb, ClientRectangle, sf);
				
				if (IsShadow)
				{
					var rec = ClientRectangle;
					rec.Height -= 1;
					rec.Y = rec.Height;
					rec.Height = 1;
					e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, 0, 0, 0)), rec);//阴影
				}
			}
		}

		/// <summary>
		/// 开启双缓冲，减少闪烁
		/// </summary>
		protected void SetStyles()
		{
			SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.ResizeRedraw |
				ControlStyles.DoubleBuffer |
				ControlStyles.SupportsTransparentBackColor, true);
			//强制分配样式重新应用到控件上
			UpdateStyles();
		}

	}
}
