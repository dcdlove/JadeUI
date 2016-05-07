/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-10-31 17:01:40
 * @version 1.0.0 
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Jade.UI
{
	[DefaultEvent("Click")]
	public class JImageButton : Control
	{
		private Image _NormalImage;
		private Image _HoverImage;
		private Image _NowImage;
		Rectangle srcRect;

		public JImageButton()
			: base()
		{
			SetStyles();
			Size = new Size(80, 30);
			Font = new Font("微软雅黑", 9);
			ForeColor = SystemColors.ControlText;
			NormalImage = Properties.Resources.btn_normal;
			HoverImage = Properties.Resources.btn_hover;
		}

		[Category("JadeControl"),
		Description("默认显示图片")]
		public Image NormalImage
		{
			get { return _NormalImage; }
			set
			{
				_NormalImage = value;
				if (_NormalImage != null)
				{
					srcRect.Size = _NormalImage.Size;
				}
				_NowImage = value;
				Invalidate();
			}
		}

		[Category("JadeControl"),
		Description("鼠标经过时显示图片")]
		public Image HoverImage
		{
			get { return _HoverImage; }
			set 
			{ 
				_HoverImage = value; 
				Invalidate(); 
			}
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			_NowImage = HoverImage;
			Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			_NowImage = NormalImage;
			Invalidate();
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
		}

		[Browsable(false)]
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

		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (_NowImage != null)
			{
				RenderHelper.DrawImageWithNineRect(e.Graphics, _NowImage, ClientRectangle, srcRect);
			}

			Color textColor = Enabled ? ForeColor : SystemColors.GrayText;
			StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
			e.Graphics.DrawString(Text, Font, new SolidBrush(textColor), ClientRectangle, sf);

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
