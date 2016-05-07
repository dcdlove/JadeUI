/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-07-31 09:58:01
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Jade.UI
{
	/// <summary>
	/// 控件的状态。
	/// </summary>
	internal enum ControlState
	{
		/// <summary>
		///  正常。
		/// </summary>
		Normal,
		/// <summary>
		/// 鼠标进入。
		/// </summary>
		Hover,
		/// <summary>
		/// 鼠标按下。
		/// </summary>
		Pressed,
		/// <summary>
		/// 获得焦点。
		/// </summary>
		Focused,
	}


	[ToolboxBitmap(typeof(CheckBox))]
	public class JCheckBox : CheckBox
	{
		private Color _baseColor = Color.FromArgb(0, 150, 255);
		private ControlState _controlState;

		private static readonly ContentAlignment RightAlignment =
			ContentAlignment.TopRight |
			ContentAlignment.BottomRight |
			ContentAlignment.MiddleRight;
		private static readonly ContentAlignment LeftAligbment =
			ContentAlignment.TopLeft |
			ContentAlignment.BottomLeft |
			ContentAlignment.MiddleLeft;

		public JCheckBox()
			: base()
		{
			SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.ResizeRedraw |
				ControlStyles.SupportsTransparentBackColor, true);
		}

		[Category("JadeControl _"),
		Description("主题颜色"),
		DefaultValue(typeof(Color), "#38B800")]
		public Color BaseColor
		{
			get { return _baseColor; }
			set
			{
				_baseColor = value;
				base.Invalidate();
			}
		}

		protected virtual int DefaultCheckButtonWidth
		{
			get { return 12; }
		}

		internal ControlState ControlState
		{
			get { return _controlState; }
			set
			{
				if (_controlState != value)
				{
					_controlState = value;
					base.Invalidate();
				}
			}
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			ControlState = ControlState.Hover;
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			ControlState = ControlState.Normal;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left && e.Clicks == 1)
			{
				ControlState = ControlState.Pressed;
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (e.Button == MouseButtons.Left && e.Clicks == 1)
			{
				if (ClientRectangle.Contains(e.Location))
				{
					ControlState = ControlState.Hover;
				}
				else
				{
					ControlState = ControlState.Normal;
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			base.OnPaintBackground(e);

			Graphics g = e.Graphics;
			Rectangle checkButtonRect;
			Rectangle textRect;

			CalculateRect(out checkButtonRect, out textRect);
			g.SmoothingMode = SmoothingMode.AntiAlias;

			Color backColor = ControlPaint.Light(_baseColor);
			Color borderColor;
			Color innerBorderColor;
			Color checkColor;
			bool hover = false;

			if (Enabled)
			{
				switch (ControlState)
				{
					case ControlState.Hover:
						borderColor = _baseColor;
						innerBorderColor = _baseColor;
						checkColor = GetColor(_baseColor, 0, 35, 24, 9);
						hover = true;
						break;
					case ControlState.Pressed:
						borderColor = _baseColor;
						innerBorderColor = GetColor(_baseColor, 0, -13, -8, -3);
						checkColor = GetColor(_baseColor, 0, -35, -24, -9);
						hover = true;
						break;
					default:
						borderColor = _baseColor;
						innerBorderColor = Color.Empty;
						checkColor = _baseColor;
						break;
				}
			}
			else
			{
				borderColor = SystemColors.ControlDark;
				innerBorderColor = SystemColors.ControlDark;
				checkColor = SystemColors.ControlDark;
			}

			using (SolidBrush brush = new SolidBrush(Color.White))
			{
				g.FillRectangle(brush, checkButtonRect);
			}

			if (hover)
			{
				using (Pen pen = new Pen(innerBorderColor, 2F))
				{
					g.DrawRectangle(pen, checkButtonRect);
				}
			}

			switch (CheckState)
			{
				case CheckState.Checked:
					DrawCheckedFlag(
						g,
						checkButtonRect,
						checkColor);
					break;
				case CheckState.Indeterminate:
					checkButtonRect.Inflate(-1, -1);
					using (GraphicsPath path = new GraphicsPath())
					{
						path.AddEllipse(checkButtonRect);
						using (PathGradientBrush brush = new PathGradientBrush(path))
						{
							brush.CenterColor = checkColor;
							brush.SurroundColors = new Color[] { Color.White };
							Blend blend = new Blend();
							blend.Positions = new float[] { 0f, 0.4f, 1f };
							blend.Factors = new float[] { 0f, 0.3f, 1f };
							brush.Blend = blend;
							g.FillEllipse(brush, checkButtonRect);
						}
					}
					checkButtonRect.Inflate(1, 1);
					break;
			}

			using (Pen pen = new Pen(borderColor))
			{
				g.DrawRectangle(pen, checkButtonRect);
			}

			Color textColor = Enabled ? ForeColor : SystemColors.GrayText;
			TextRenderer.DrawText(
				g,
				Text,
				Font,
				textRect,
				textColor,
				GetTextFormatFlags(TextAlign, RightToLeft == RightToLeft.Yes));
		}

		private void CalculateRect(out Rectangle checkButtonRect, out Rectangle textRect)
		{
			checkButtonRect = new Rectangle(0, 0, DefaultCheckButtonWidth, DefaultCheckButtonWidth);
			textRect = Rectangle.Empty;
			bool bCheckAlignLeft = (int)(LeftAligbment & CheckAlign) != 0;
			bool bCheckAlignRight = (int)(RightAlignment & CheckAlign) != 0;
			bool bRightToLeft = RightToLeft == RightToLeft.Yes;

			if ((bCheckAlignLeft && !bRightToLeft) ||(bCheckAlignRight && bRightToLeft))
			{
				switch (CheckAlign)
				{
					case ContentAlignment.TopRight:
					case ContentAlignment.TopLeft:
						checkButtonRect.Y = 2;
						break;
					case ContentAlignment.MiddleRight:
					case ContentAlignment.MiddleLeft:
						checkButtonRect.Y = (Height - DefaultCheckButtonWidth) / 2;
						break;
					case ContentAlignment.BottomRight:
					case ContentAlignment.BottomLeft:
						checkButtonRect.Y = Height - DefaultCheckButtonWidth - 2;
						break;
				}

				checkButtonRect.X = 1;

				textRect = new Rectangle(
					checkButtonRect.Right + 2,
					0,
					Width - checkButtonRect.Right - 4,
					Height);
			}
			else if ((bCheckAlignRight && !bRightToLeft)|| (bCheckAlignLeft && bRightToLeft))
			{
				switch (CheckAlign)
				{
					case ContentAlignment.TopLeft:
					case ContentAlignment.TopRight:
						checkButtonRect.Y = 2;
						break;
					case ContentAlignment.MiddleLeft:
					case ContentAlignment.MiddleRight:
						checkButtonRect.Y = (Height - DefaultCheckButtonWidth) / 2;
						break;
					case ContentAlignment.BottomLeft:
					case ContentAlignment.BottomRight:
						checkButtonRect.Y = Height - DefaultCheckButtonWidth - 2;
						break;
				}

				checkButtonRect.X = Width - DefaultCheckButtonWidth - 1;

				textRect = new Rectangle(
					2, 0, Width - DefaultCheckButtonWidth - 6, Height);
			}
			else
			{
				switch (CheckAlign)
				{
					case ContentAlignment.TopCenter:
						checkButtonRect.Y = 2;
						textRect.Y = checkButtonRect.Bottom + 2;
						textRect.Height = Height - DefaultCheckButtonWidth - 6;
						break;
					case ContentAlignment.MiddleCenter:
						checkButtonRect.Y = (Height - DefaultCheckButtonWidth) / 2;
						textRect.Y = 0;
						textRect.Height = Height;
						break;
					case ContentAlignment.BottomCenter:
						checkButtonRect.Y = Height - DefaultCheckButtonWidth - 2;
						textRect.Y = 0;
						textRect.Height = Height - DefaultCheckButtonWidth - 6;
						break;
				}

				checkButtonRect.X = (Width - DefaultCheckButtonWidth) / 2;

				textRect.X = 2;
				textRect.Width = Width - 4;
			}
		}

		private void DrawCheckedFlag(Graphics graphics, Rectangle rect, Color color)
		{
			PointF[] points = new PointF[3];
			points[0] = new PointF(
				rect.X + rect.Width / 4.5f,
				rect.Y + rect.Height / 2.5f);
			points[1] = new PointF(
				rect.X + rect.Width / 2.5f,
				rect.Bottom - rect.Height / 3f);
			points[2] = new PointF(
				rect.Right - rect.Width / 4.0f,
				rect.Y + rect.Height / 4.5f);
			using (Pen pen = new Pen(color, 2F))
			{
				graphics.DrawLines(pen, points);
			}
		}

		private Color GetColor(Color colorBase, int a, int r, int g, int b)
		{
			int a0 = colorBase.A;
			int r0 = colorBase.R;
			int g0 = colorBase.G;
			int b0 = colorBase.B;

			if (a + a0 > 255) { a = 255; } else { a = Math.Max(a + a0, 0); }
			if (r + r0 > 255) { r = 255; } else { r = Math.Max(r + r0, 0); }
			if (g + g0 > 255) { g = 255; } else { g = Math.Max(g + g0, 0); }
			if (b + b0 > 255) { b = 255; } else { b = Math.Max(b + b0, 0); }

			return Color.FromArgb(a, r, g, b);
		}

		internal static TextFormatFlags GetTextFormatFlags(ContentAlignment alignment,bool rightToleft)
		{
			TextFormatFlags flags = TextFormatFlags.WordBreak | TextFormatFlags.SingleLine;
			if (rightToleft)
			{
				flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
			}

			switch (alignment)
			{
				case ContentAlignment.BottomCenter:
					flags |= TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
					break;
				case ContentAlignment.BottomLeft:
					flags |= TextFormatFlags.Bottom | TextFormatFlags.Left;
					break;
				case ContentAlignment.BottomRight:
					flags |= TextFormatFlags.Bottom | TextFormatFlags.Right;
					break;
				case ContentAlignment.MiddleCenter:
					flags |= TextFormatFlags.HorizontalCenter |
						TextFormatFlags.VerticalCenter;
					break;
				case ContentAlignment.MiddleLeft:
					flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
					break;
				case ContentAlignment.MiddleRight:
					flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
					break;
				case ContentAlignment.TopCenter:
					flags |= TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
					break;
				case ContentAlignment.TopLeft:
					flags |= TextFormatFlags.Top | TextFormatFlags.Left;
					break;
				case ContentAlignment.TopRight:
					flags |= TextFormatFlags.Top | TextFormatFlags.Right;
					break;
			}
			return flags;
		}
	}
}

