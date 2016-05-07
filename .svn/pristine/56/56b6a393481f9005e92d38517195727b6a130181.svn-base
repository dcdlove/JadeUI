/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-09-17 17:08:07
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Diagnostics;
using System.Drawing.Drawing2D;


namespace Jade.UI
{
	[Designer(typeof(ScrollbarControlDesigner))]
	public class JScrollbar : UserControl
	{
		protected Color _ChannelColor = Color.Empty;
		protected Image _UpArrowImage = null;
		protected Image _DownArrowImage = null;
		protected Image _ThumbImage = null;
		private Image _ThumbHoverImage = null;

		protected int _Minimum = 0;
		protected int _Maximum = 0;
		protected int _Value = 0;
		protected int _ThumbTop = 0;
		private bool _IsThumbDown = false;
		private bool _IsThumbDragging = false;
		private bool _IsHover = false;

		public new event EventHandler Scroll = null;

		WebBrowser wb;
		ScrollableControl cc;
		public JScrollbar()
		{

			InitializeComponent();
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);

			_ChannelColor = ColorTranslator.FromHtml("#F5F5F5");
			_ThumbImage = Jade.UI.Properties.Resources.scrollbar_normal;
			_ThumbHoverImage = Jade.UI.Properties.Resources.scrollbar_hover;
			Width = 15;
			base.MinimumSize = new Size(Width, _ThumbImage.Height);
		}

		#region 属性

		[EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("JadeControl - Behavior"), Description("最小值Minimum")]
		public int Minimum
		{
			get { return _Minimum; }
			set
			{
				_Minimum = value;
				Invalidate();
			}
		}

		[EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("JadeControl - Behavior"), Description("最大值Maximum")]
		public int Maximum
		{
			get { return _Maximum; }
			set
			{
				_Maximum = value;
				Invalidate();
			}
		}

		[EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("JadeControl - Behavior"), Description("当前值Value")]
		public int Value
		{
			get { return _Value; }
			set
			{
				_Value = value;
				Invalidate();
			}
		}

		[EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("JadeControl -Skin"), Description("通道 颜色 Channel Color")]
		public Color ChannelColor
		{
			get { return _ChannelColor; }
			set { _ChannelColor = value; }
		}

		[EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("JadeControl -Skin"), Description("向上箭头图形 Up Arrow Graphic")]
		public Image UpArrowImage
		{
			get { return _UpArrowImage; }
			set { _UpArrowImage = value; }
		}

		[EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("JadeControl -Skin"), Description("向下箭头图形 Up Down Graphic")]
		public Image DownArrowImage
		{
			get { return _DownArrowImage; }
			set { _DownArrowImage = value; }
		}

		[EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("JadeControl -Skin"), Description("中间按钮图片")]
		public Image ThumbImage
		{
			get { return _ThumbImage; }
			set { _ThumbImage = value; }
		}

		protected Image ThumbHoverImage
		{
			get { return _ThumbHoverImage; }
			set { _ThumbHoverImage = value; }
		}

		private int ThumbHeight { get; set; }
		#endregion

		/// <summary>
		/// 关联控件
		/// </summary>
		/// <param name="ctl"></param>
		public void BindControl(WebBrowser ctl)
		{
			wb = ctl;
			Minimum = 0;
			Maximum = ctl.Document.Body.ScrollRectangle.Height - ctl.Height;
			Height = ctl.Height;
			ThumbHeight = Height - ThumbImage.Height;
		}


		public void BindControl(ScrollableControl ctl)
		{
			cc = ctl;

			Minimum = 0;
			//Height = cc.Height;
			//Top = 0;
			//Left = cc.Width - Width - 1 + 10;
			BringToFront();
			Invalidate();

		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// JScrollbar
			// 
			this.Name = "JScrollbar";
			this.MouseDown += new MouseEventHandler(JScrollbar_MouseDown);
			this.MouseMove += new MouseEventHandler(JScrollbar_MouseMove);
			this.MouseUp += new MouseEventHandler(JScrollbar_MouseUp);
			this.MouseEnter += new EventHandler(JScrollbar_MouseEnter);
			this.MouseLeave += new EventHandler(JScrollbar_MouseLeave);
			//this.MouseWheel += new MouseEventHandler(JScrollbar_MouseWheel);

			Scroll += new EventHandler(JScrollbar_Scroll);
			this.ResumeLayout(false);
			//RenderHelper.SetFormRoundRectRgn(this, 5);
		}

		void JScrollbar_MouseLeave(object sender, EventArgs e)
		{
			_IsHover = false;
			Invalidate();
		}

		void JScrollbar_MouseEnter(object sender, EventArgs e)
		{
			_IsHover = true;
			Focus();
			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{

			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			Brush _brush = new SolidBrush(_ChannelColor);
			FillRoundRectangle(e.Graphics, _brush, new Rectangle(0, 0, 12, Height), 6);
			//if (_IsHover) _brush = new SolidBrush(_ChannelColor);
			//e.Graphics.FillRectangle(_brush, );

			if (UpArrowImage != null)
			{
				//e.Graphics.DrawImage(UpArrowImage, new Rectangle(new Point(0, 0), UpArrowImage.Size));
			}

			//draw middle
			if (_IsHover)
				e.Graphics.DrawImage(ThumbHoverImage, new Rectangle(0, _ThumbTop, ThumbImage.Width, ThumbImage.Height));
			else
				e.Graphics.DrawImage(ThumbImage, new Rectangle(0, _ThumbTop, ThumbImage.Width, ThumbImage.Height));


			if (DownArrowImage != null)
			{
				//e.Graphics.DrawImage(DownArrowImage, new Rectangle(new Point(0, (Height - DownArrowImage.Height)), DownArrowImage.Size));
			}

		}

		void JScrollbar_MouseDown(object sender, MouseEventArgs e)
		{
			Point ptPoint = this.PointToClient(Cursor.Position);
			Rectangle thumbrect = new Rectangle(new Point(0, _ThumbTop), ThumbImage.Size);
			if (thumbrect.Contains(ptPoint))
			{
				_IsThumbDown = true;
			}

		}

		void JScrollbar_MouseMove(object sender, MouseEventArgs e)
		{
			if (_IsThumbDown == true)
			{
				_IsThumbDragging = true;
			}

			if (_IsThumbDragging)
			{
				if (cc != null)
				{
					Maximum = cc.VerticalScroll.Maximum - cc.Height;
					ThumbHeight = Height - ThumbImage.Height;
				}

				var num = e.Y;
				if (num < 0)
				{
					num = 0;
				}
				else if (num > ThumbHeight)
				{
					num = ThumbHeight;
				}
				_ThumbTop = num;
				var bl = (_ThumbTop * 1.0) / ThumbHeight;
				Value = (int)(bl * Maximum);
				//Debug.WriteLine("当前值：" + moValue.ToString() + "moThumbTop:" + moThumbTop);

				Application.DoEvents();

				Invalidate();

			}



			if (Scroll != null)
				Scroll(this, new EventArgs());
		}

		void JScrollbar_MouseUp(object sender, MouseEventArgs e)
		{
			_IsThumbDown = false;
			_IsThumbDragging = false;
		}

		void JScrollbar_MouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta < 0)
			{
				Value += (int)(Maximum * 0.1);
				_ThumbTop += (int)(ThumbHeight * 0.1);
			}
			else
			{
				Value -= (int)(Maximum * 0.1);
				_ThumbTop -= (int)(ThumbHeight * 0.1);
			}
			if (Scroll != null)
				Scroll(this, new EventArgs());
		}

		void JScrollbar_Scroll(object sender, EventArgs e)
		{
			if (wb != null)
			{
				if (wb.Document != null)
				{
					wb.Document.Window.ScrollTo(new Point(0, Value));
				}
			}

			if (cc != null)
			{
				cc.AutoScrollPosition = new Point(0, Value);
			}
		}

		public void FillRoundRectangle(Graphics g, Brush brush, Rectangle rect, int cornerRadius)
		{
			using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
			{
				g.FillPath(brush, path);
			}
		}

		public GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
		{
			GraphicsPath roundedRect = new GraphicsPath();
			roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
			roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
			roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
			roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
			roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
			roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
			roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
			roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
			roundedRect.CloseFigure();
			return roundedRect;
		}


	}

	internal class ScrollbarControlDesigner : System.Windows.Forms.Design.ControlDesigner
	{
		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules selectionRules = base.SelectionRules;
				PropertyDescriptor propDescriptor = TypeDescriptor.GetProperties(this.Component)["AutoSize"];
				if (propDescriptor != null)
				{
					bool autoSize = (bool)propDescriptor.GetValue(this.Component);
					if (autoSize)
					{
						selectionRules = SelectionRules.Visible | SelectionRules.Moveable | SelectionRules.BottomSizeable | SelectionRules.TopSizeable;
					}
					else
					{
						selectionRules = SelectionRules.Visible | SelectionRules.AllSizeable | SelectionRules.Moveable;
					}
				}
				return selectionRules;
			}
		}
	}

}