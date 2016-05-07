/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-05-16 08:43:58
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Media;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Jade.UI
{
	public class JTextBox : TextBox
	{
		public JTextBox()
			: base()
		{
			AutoSize = false;
			Height = 26;
			Font = new Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			ForeColor = ColorTranslator.FromHtml("#333");
		}

		/// <summary>   
		/// 获得当前进程，以便重绘控件   
		/// </summary>   
		/// <param name="hWnd"></param>   
		/// <returns></returns>   
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		static extern IntPtr GetWindowDC(IntPtr hWnd);
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		/// <summary>   
		/// 是否启用热点效果   
		/// </summary>   
		private bool _HotTrack = true;

		/// <summary>   
		/// 边框颜色   
		/// </summary>   
		private Color _BorderColor = ColorTranslator.FromHtml("#b9b9b9");

		/// <summary>   
		/// 热点边框颜色   
		/// </summary>   
		private Color _HotColor = ColorTranslator.FromHtml("#0096ff");

		/// <summary>   
		/// 是否鼠标MouseOver状态   
		/// </summary>   
		private bool _IsMouseOver = false;

		/// <summary>
		/// 没有焦点的时候显示文本
		/// </summary>
		private string _EmptyText = string.Empty;
		private Color _EmptyTextColor = ColorTranslator.FromHtml("#999");

		#region 属性

		[Category("JadeControl"),
		 Description("没有焦点的时候显示文本"), DefaultValue("")]
		public string EmptyText
		{
			get { return _EmptyText; }
			set { _EmptyText = value; }
		}


		[Category("JadeControl"),
		 Description("没有焦点的时候显示文本颜色")]
		public Color EmptyTextColor
		{
			get { return _EmptyTextColor; }
			set { _EmptyTextColor = value; }
		}


		/// <summary>   
		/// 是否启用热点效果   
		/// </summary>   
		[Category("JadeControl"),
		 Description("获得或设置一个值，指示当鼠标经过控件时控件边框是否发生变化。只在控件的BorderStyle为FixedSingle时有效"),
		 DefaultValue(true)]
		public bool HotTrack
		{
			get
			{
				return this._HotTrack;
			}
			set
			{
				this._HotTrack = value;
				//在该值发生变化时重绘控件，下同   
				//在设计模式下，更改该属性时，如果不调用该语句，   
				//则不能立即看到设计试图中该控件相应的变化   
				this.Invalidate();
			}
		}
		/// <summary>   
		/// 边框颜色   
		/// </summary>   
		[Category("JadeControl"),
		Description("获得或设置控件的边框颜色"),
		DefaultValue(typeof(Color), "#A7A6AA")]
		public Color BorderColor
		{
			get
			{
				return this._BorderColor;
			}
			set
			{
				this._BorderColor = value;
				this.Invalidate();
			}
		}
		/// <summary>   
		/// 热点时边框颜色   
		/// </summary>   
		[Category("JadeControl"),
		Description("获得或设置当鼠标经过控件时控件的边框颜色。只在控件的BorderStyle为FixedSingle时有效"),
		DefaultValue(typeof(Color), "#335EA8")]
		public Color HotColor
		{
			get
			{
				return this._HotColor;
			}
			set
			{
				this._HotColor = value;
				this.Invalidate();
			}
		}
		#endregion 属性

		/// <summary>   
		/// 鼠标移动到该控件上时   
		/// </summary>   
		/// <param name="e"></param>   
		protected override void OnMouseMove(MouseEventArgs e)
		{
			//鼠标状态   
			this._IsMouseOver = true;
			//如果启用HotTrack，则开始重绘   
			//如果不加判断这里不加判断，则当不启用HotTrack，   
			//鼠标在控件上移动时，控件边框会不断重绘，   
			//导致控件边框闪烁。下同   
			//谁有更好的办法？Please tell me , Thanks。   
			if (this._HotTrack)
			{
				//重绘   
				this.Invalidate();
			}
			base.OnMouseMove(e);
		}

		/// <summary>   
		/// 当鼠标从该控件移开时   
		/// </summary>   
		/// <param name="e"></param>   
		protected override void OnMouseLeave(EventArgs e)
		{
			this._IsMouseOver = false;

			if (this._HotTrack)
			{
				//重绘   
				this.Invalidate();
			}
			base.OnMouseLeave(e);
		}

		/// <summary>   
		/// 当该控件获得焦点时   
		/// </summary>   
		/// <param name="e"></param>   
		protected override void OnGotFocus(EventArgs e)
		{

			if (this._HotTrack)
			{
				//重绘   
				this.Invalidate();
			}
			base.OnGotFocus(e);
		}
		/// <summary>   
		/// 当该控件失去焦点时   
		/// </summary>   
		/// <param name="e"></param>   
		protected override void OnLostFocus(EventArgs e)
		{
			if (this._HotTrack)
			{
				//重绘   
				this.Invalidate();
			}
			base.OnLostFocus(e);
		}

		Pen sp = new Pen(Color.FromArgb(50, 0, 0, 0), 2);

		/// <summary>   
		/// 获得操作系统消息   
		/// </summary>   
		/// <param name="m"></param>   
		protected override void WndProc(ref Message m)
		{


			base.WndProc(ref m);
			if (m.Msg == 0xf || m.Msg == 0x133)
			{
				if (m.Msg == 0xf) WmPaint(ref m);
				//if (m.Msg == 0xf) WmPaint(ref m);
				//拦截系统消息，获得当前控件进程以便重绘。   
				//一些控件（如TextBox、Button等）是由系统进程绘制，重载OnPaint方法将不起作用.   
				//所有这里并没有使用重载OnPaint方法绘制TextBox边框。   
				//   
				//MSDN:重写 OnPaint 将禁止修改所有控件的外观。   
				//那些由 Windows 完成其所有绘图的控件（例如 Textbox）从不调用它们的 OnPaint 方法，   
				//因此将永远不会使用自定义代码。请参见您要修改的特定控件的文档，   
				//查看 OnPaint 方法是否可用。如果某个控件未将 OnPaint 作为成员方法列出，   
				//则您无法通过重写此方法改变其外观。   
				//   
				//MSDN:要了解可用的 Message.Msg、Message.LParam 和 Message.WParam 值，   
				//请参考位于 MSDN Library 中的 Platform SDK 文档参考。可在 Platform SDK（“Core SDK”一节）   
				//下载中包含的 windows.h 头文件中找到实际常数值，该文件也可在 MSDN 上找到。   
				IntPtr hDC = GetWindowDC(m.HWnd);
				if (hDC.ToInt32() == 0)
				{
					return;
				}

				//只有在边框样式为FixedSingle时自定义边框样式才有效   
				if (this.BorderStyle == BorderStyle.FixedSingle)
				{
					//边框Width为1个像素   
					System.Drawing.Pen pen = new Pen(this._BorderColor, 1);
					if (this.Focused)
					{
						pen.Color = this._HotColor;
					}
					else
					{

						if (_IsMouseOver)
						{
							pen.Color = ColorTranslator.FromHtml("#a9a9a9");
						}
						else
						{
							pen.Color = this._BorderColor;
						}
					}

					//绘制边框   
					System.Drawing.Graphics g = Graphics.FromHdc(hDC);
					g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

					g.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
					//g.DrawRectangle(sp, 1, 1, this.Width - 3, this.Height - 3);
					g.DrawLine(sp, 1, 1, this.Width - 1, 1);

					pen.Dispose();
				}
				//返回结果   
				m.Result = IntPtr.Zero;
				//释放   
				ReleaseDC(m.HWnd, hDC);
			}

		}


		private void WmPaint(ref Message m)
		{
			Rectangle rectangle = new Rectangle(0, 0, Width, Height);
			using (Graphics graphics = Graphics.FromHwnd(base.Handle))
			{
				if (Text.Length == 0 && !string.IsNullOrEmpty(_EmptyText) && !Focused)
				{
					TextFormatFlags format = TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter;
					if (RightToLeft == RightToLeft.Yes)
					{
						format |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
					}
					TextRenderer.DrawText(graphics, _EmptyText, Font, base.ClientRectangle, _EmptyTextColor, format);

				}

			}

		}
	}
}
