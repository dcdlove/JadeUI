/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-10-24 12:10:43
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Jade.UI
{
	[DefaultEvent("Scroll")]
	public partial class JSliderBar : Control
	{
		public JSliderBar()
		{
			InitializeComponent();
			Scroll += new ScrollHandler(OnScroll);
			ScrollEnd += new ScrollHandler(OnScrollEnd);
			
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);

		}



		#region 字段&属性
		private int _MaxNum = 100;
		private int _MinNum = 0;
		private int _Value;
		private int _TrackHeight = 1;
		private Color _TrackColor = Color.LightGray;
		private Size _BarSize = new Size(14, 14);
		private Color _BarColor = Color.FromArgb(38, 181, 255);


		private Point _BarLocation;
		private Rectangle _BarRec;
		private int _StarBarLoc = 0;
		private double _Percent = 0;

		private bool _BeginMove = false;//开始移动
		private Point _MousePoint;

		public delegate void ScrollHandler(object sender, ScrollEventArgs e);

		[Category("JadeControl"),
		Description("最大值")]
		public int MaxNum
		{
			get { return _MaxNum; }
			set
			{
				if (value > 0)
				{
					_MaxNum = value;
				}

			}
		}

		[Category("JadeControl"),
		Description("最小值")]
		public int MinNum
		{
			get { return _MinNum; }
			set
			{
				if (value >= 0)
				{
					_MinNum = value;
				}

			}
		}

		[Category("JadeControl"),
		Description("滑块的位置")]
		public int Value
		{
			get { return _Value; }
			set
			{
				if (value > 0)
				{
					var bl = value / (_MaxNum + 0.0);
					_StarBarLoc = (int)Math.Truncate(bl * Width - _BarSize.Width);
					_BarLocation.X = _StarBarLoc;
					BarChang();
				}
				_Value = value;
			}
		}

		[Category("JadeControl-skin"),
		Description("滑块轨道背景颜色")]
		public Color TrackColor
		{
			get { return _TrackColor; }
			set { _TrackColor = value; }
		}

		[Category("JadeControl-skin"),
		Description("滑块颜色")]
		public Color BarColor
		{
			get { return _BarColor; }
			set { _BarColor = value; }
		}

		/// <summary>
		/// 获取滑块的区域
		/// </summary>
		/// <returns></returns>
		private Rectangle BarRec
		{
			get
			{

				return _BarRec;
			}
			set { _BarRec = value; }
		}

		private Point BarLocation
		{
			get
			{
				return _BarLocation;
			}
			set { _BarLocation = value; }
		}

		[Description("在拖动滑块时发生")]
		public event ScrollHandler Scroll;

		[Description("在拖动滑块结束时发生")]
		public event ScrollHandler ScrollEnd;


		#endregion

		#region 事件

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left && BarRec.Contains(new Point(e.X, e.Y)))
			{
				_MousePoint = new Point(e.X, e.Y);
				_StarBarLoc = _BarLocation.X;
				_BeginMove = true;

			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (_BeginMove)
			{
				var cx = (e.X - _MousePoint.X) + _StarBarLoc;

				if (cx >= 0 && cx <= (Width - _BarSize.Width))
				{
					_BarLocation.X = cx;
					_Percent = (cx + 0.0) / (Width - _BarSize.Width);
					_Value = (int)Math.Truncate(_Percent * _MaxNum);
					Scroll(this, new ScrollEventArgs(_Value));
					BarChang();

				}
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (e.Button == MouseButtons.Left)
			{
				_BeginMove = false;
				ScrollEnd(this, new ScrollEventArgs(_Value));
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			RenderPanel(e.Graphics, e.ClipRectangle);
			RenderBar(e.Graphics);

		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			if (Height > 25)
			{
				Height = 25;
			}
			if (Height < _BarSize.Height)
			{
				Height = _BarSize.Height;
			}
			BarChang();
		}
		#endregion

		/// <summary>
		/// 渲染面板
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rec"></param>
		private void RenderPanel(Graphics g, Rectangle rec)
		{
			using (SolidBrush sb = new SolidBrush(TrackColor))
			{
				rec.Height = _TrackHeight;
				rec.Y = (Height - _TrackHeight) / 2;
				g.FillRectangle(sb, rec);
				sb.Color = _BarColor;
				var r = rec;
				r.Width = BarLocation.X;
				g.FillRectangle(sb, r);
				//sb.Color = Color.Plum;
				//g.DrawString(Text, Font, sb, new PointF(100, 0));
			}
		}

		/// <summary>
		/// 渲染控制按钮
		/// </summary>
		/// <param name="g"></param>
		private void RenderBar(Graphics g)
		{
			using (SolidBrush sb = new SolidBrush(_BarColor))
			{
				g.SmoothingMode = SmoothingMode.HighQuality;

				g.FillEllipse(sb, BarRec);
				BarRec.Inflate(1, 1);
				g.DrawEllipse(new Pen(Color.FromArgb(255, 255, 255)), BarRec);


			}

		}

		private void BarChang()
		{
			_BarLocation.Y = (Height - _BarSize.Height) / 2;
			_BarRec.Size = _BarSize;
			_BarRec.Location = _BarLocation;
			Invalidate();
		}

		protected virtual void OnScroll(object sender, ScrollEventArgs e)
		{

		}
		protected virtual void OnScrollEnd(object sender, ScrollEventArgs e)
		{

		}
	}

	public class ScrollEventArgs : EventArgs
	{

		public ScrollEventArgs(int v)
		{
			Value = v;
		}
		public int Value { get; set; }
	}
}
