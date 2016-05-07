/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-06-02 14:49:18
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
using System.Runtime.InteropServices;
using System.Drawing.Imaging;



namespace Jade.UI
{
	[ToolboxBitmap(typeof(ComboBox))]
	public class JComboBox : ComboBox
	{


		private IntPtr _editHandle;
		private ControlState _buttonState;
		private Color _baseColor = Color.FromArgb(255, 255, 255);
		private Color _borderColor = ColorTranslator.FromHtml("#b9b9b9");
		private Color _arrowColor = ColorTranslator.FromHtml("#b9b9b9");
		private Color _HotColor = ColorTranslator.FromHtml("#0096ff");
		private bool _bPainting;
		private bool _IsMouseOver = false;
		Pen sp = new Pen(Color.FromArgb(50, 0, 0, 0), 2);



		public JComboBox()
			: base()
		{
			Font = new Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			DisplayMember = "Text";
			ValueMember = "Value"; 
			
		}



		#region 属性

		[Category("JadeControl")]
		[Description("主题颜色"),
		DefaultValue(typeof(Color), "0, 150, 255")]
		public Color BaseColor
		{
			get { return _baseColor; }
			set
			{
				if (_baseColor != value)
				{
					_baseColor = value;
					base.Invalidate();
				}
			}
		}

		[Category("JadeControl")]
		[Description("边框颜色"),
		DefaultValue(typeof(Color), "0, 150, 255")]
		public Color BorderColor
		{
			get { return _borderColor; }
			set
			{
				if (_borderColor != value)
				{
					_borderColor = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>   
		/// 热点时边框颜色   
		/// </summary>   
		[Category("JadeControl"),
		Description("热点时边框颜色"),
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

		[Category("JadeControl")]
		[Description("箭头颜色"),
		DefaultValue(typeof(Color), "185, 185, 185")]
		public Color ArrowColor
		{
			get { return _arrowColor; }
			set
			{
				if (_arrowColor != value)
				{
					_arrowColor = value;
					base.Invalidate();
				}
			}
		}

		internal ControlState ButtonState
		{
			get { return _buttonState; }
			set
			{
				if (_buttonState != value)
				{
					_buttonState = value;
					Invalidate(ButtonRect);
				}
			}
		}

		internal Rectangle ButtonRect
		{
			get
			{
				return GetDropDownButtonRect();
			}
		}

		internal bool ButtonPressed
		{
			get
			{
				if (IsHandleCreated)
				{
					return GetComboBoxButtonPressed();
				}
				return false;
			}
		}

		internal IntPtr EditHandle
		{
			get { return _editHandle; }
		}

		internal Rectangle EditRect
		{
			get
			{
				if (DropDownStyle == ComboBoxStyle.DropDownList)
				{
					Rectangle rect = new Rectangle(
						3, 3, Width - ButtonRect.Width - 6, Height - 6);
					if (RightToLeft == RightToLeft.Yes)
					{
						rect.X += ButtonRect.Right;
					}
					return rect;
				}
				if (IsHandleCreated && EditHandle != IntPtr.Zero)
				{
					NativeMethods.RECT rcClient = new NativeMethods.RECT();
					NativeMethods.GetWindowRect(EditHandle, ref rcClient);
					return RectangleToClient(rcClient.Rect);
				}
				return Rectangle.Empty;
			}
		}

		#endregion

		#region 重写方法

		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			NativeMethods.ComboBoxInfo cbi = GetComboBoxInfo();
			_editHandle = cbi.hwndEdit;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			Point point = e.Location;
			if (ButtonRect.Contains(point))
			{
				ButtonState = ControlState.Hover;
			}
			else
			{
				ButtonState = ControlState.Normal;
			}
			_IsMouseOver = true;
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);

			Point point = PointToClient(Cursor.Position);
			if (ButtonRect.Contains(point))
			{
				ButtonState = ControlState.Hover;
			}
			_IsMouseOver = true;
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			ButtonState = ControlState.Normal;
			_IsMouseOver = false;
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			ButtonState = ControlState.Normal;
			_IsMouseOver = false;
		}

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case NativeMethods.WM_PAINT:
					WmPaint(ref m);
					break;
				default:
					base.WndProc(ref m);
					break;
			}
		}

		#endregion

		#region 系统消息

		private void WmPaint(ref Message m)
		{
			if (base.DropDownStyle == ComboBoxStyle.Simple)
			{
				base.WndProc(ref m);
				return;
			}

			if (base.DropDownStyle == ComboBoxStyle.DropDown)
			{
				if (!_bPainting)
				{
					NativeMethods.PAINTSTRUCT ps =
						new NativeMethods.PAINTSTRUCT();

					_bPainting = true;
					NativeMethods.BeginPaint(m.HWnd, ref ps);

					RenderComboBox(ref m);

					NativeMethods.EndPaint(m.HWnd, ref ps);
					_bPainting = false;
					m.Result = NativeMethods.TRUE;
				}
				else
				{
					base.WndProc(ref m);
				}
			}
			else
			{
				base.WndProc(ref m);
				RenderComboBox(ref m);
			}
		}

		#endregion

		#region 渲染方法

		private void RenderComboBox(ref Message m)
		{
			Rectangle rect = new Rectangle(Point.Empty, Size);
			Rectangle buttonRect = ButtonRect;
			ControlState state = ButtonPressed ?
				ControlState.Pressed : ButtonState;
			using (Graphics g = Graphics.FromHwnd(m.HWnd))
			{
				RenderComboBoxBackground(g, rect, buttonRect);
				RenderConboBoxDropDownButton(g, ButtonRect, state);
				RenderConboBoxBorder(g, rect);
			}
		}

		/// <summary>
		/// 绘制边框
		/// </summary>
		/// <param name="g"></param>
		/// <param name="buttonRect"></param>
		/// <param name="state"></param>
		private void RenderConboBoxBorder(Graphics g, Rectangle rect)
		{
			Color borderColor = _IsMouseOver ? _HotColor : _borderColor;

			if (!base.Enabled)
			{
				borderColor = SystemColors.ControlDarkDark;
			}
			using (Pen pen = new Pen(borderColor))
			{
				rect.Width--;
				rect.Height--;
				g.DrawRectangle(pen, rect);
				g.DrawLine(sp, 1, 1, rect.Width - 1, 1);
			}
		}

		private void RenderComboBoxBackground(Graphics g, Rectangle rect, Rectangle buttonRect)
		{
			Color backColor = base.Enabled ? base.BackColor : SystemColors.Control;
			using (SolidBrush brush = new SolidBrush(backColor))
			{
				buttonRect.Inflate(-1, -1);
				rect.Inflate(-1, -1);
				using (Region region = new Region(rect))
				{
					region.Exclude(buttonRect);
					region.Exclude(EditRect);
					g.FillRegion(brush, region);
				}
			}
		}

		private void RenderConboBoxDropDownButton(Graphics g, Rectangle buttonRect, ControlState state)
		{
			Color baseColor;
			Color backColor = Color.FromArgb(160, 250, 250, 250);
			Color borderColor = base.Enabled ?
				_borderColor : SystemColors.ControlDarkDark;
			Color arrowColor = base.Enabled ?
				_arrowColor : SystemColors.ControlDarkDark;
			Rectangle rect = buttonRect;

			if (base.Enabled)
			{
				switch (state)
				{
					case ControlState.Hover:
						baseColor = RenderHelper.GetColor(_baseColor, 0, -33, -22, -13);
						//baseColor = _baseColor;
						break;
					case ControlState.Pressed:
						baseColor = RenderHelper.GetColor(_baseColor, 0, -65, -47, -25);
						//baseColor = _baseColor;
						break;
					default:
						baseColor = _baseColor;
						break;
				}
			}
			else
			{
				baseColor = SystemColors.ControlDark;
			}

			rect.Inflate(-1, -1);

			RenderScrollBarArrowInternal(
				g,
				rect,
				baseColor,
				borderColor,
				backColor,
				arrowColor,
				RoundStyle.None,
				false,
				false,
				ArrowDirection.Down,
				LinearGradientMode.Vertical);
		}

		internal void RenderScrollBarArrowInternal(
		   Graphics g,
		   Rectangle rect,
		   Color baseColor,
		   Color borderColor,
		   Color innerBorderColor,
		   Color arrowColor,
		   RoundStyle roundStyle,
		   bool drawBorder,
		   bool drawGlass,
		   ArrowDirection arrowDirection,
		   LinearGradientMode mode)
		{
			RenderHelper.RenderBackgroundInternal(
			   g,
			   rect,
			   baseColor,
			   borderColor,
			   innerBorderColor,
			   roundStyle,
			   0,
			   .45F,
			   drawBorder,
			   drawGlass,
			   mode);

			using (SolidBrush brush = new SolidBrush(arrowColor))
			{
				RenderArrowInternal(
					g,
					rect,
					arrowDirection,
					brush);
			}
		}

		internal void RenderArrowInternal(
			Graphics g,
			Rectangle dropDownRect,
			ArrowDirection direction,
			Brush brush)
		{
			Point point = new Point(
				dropDownRect.Left + (dropDownRect.Width / 2),
				dropDownRect.Top + (dropDownRect.Height / 2));
			Point[] points = null;
			switch (direction)
			{
				case ArrowDirection.Left:
					points = new Point[] { 
                        new Point(point.X + 2, point.Y - 3), 
                        new Point(point.X + 2, point.Y + 3), 
                        new Point(point.X - 1, point.Y) };
					break;

				case ArrowDirection.Up:
					points = new Point[] { 
                        new Point(point.X - 3, point.Y + 2), 
                        new Point(point.X + 3, point.Y + 2), 
                        new Point(point.X, point.Y - 2) };
					break;

				case ArrowDirection.Right:
					points = new Point[] {
                        new Point(point.X - 2, point.Y - 3), 
                        new Point(point.X - 2, point.Y + 3), 
                        new Point(point.X + 1, point.Y) };
					break;

				default:
					points = new Point[] {
                        new Point(point.X - 2, point.Y - 1), 
                        new Point(point.X + 3, point.Y - 1), 
                        new Point(point.X, point.Y + 2) };
					break;
			}
			g.FillPolygon(brush, points);
		}

		#endregion

		#region Help Methods

		private NativeMethods.ComboBoxInfo GetComboBoxInfo()
		{
			NativeMethods.ComboBoxInfo cbi = new NativeMethods.ComboBoxInfo();
			cbi.cbSize = Marshal.SizeOf(cbi);
			NativeMethods.GetComboBoxInfo(base.Handle, ref cbi);
			return cbi;
		}

		private bool GetComboBoxButtonPressed()
		{
			NativeMethods.ComboBoxInfo cbi = GetComboBoxInfo();
			return cbi.stateButton ==
				NativeMethods.ComboBoxButtonState.STATE_SYSTEM_PRESSED;
		}

		private Rectangle GetDropDownButtonRect()
		{
			NativeMethods.ComboBoxInfo cbi = GetComboBoxInfo();

			return cbi.rcButton.Rect;
		}

		#endregion
	}



	internal class NativeMethods
	{
		#region Const

		public static readonly IntPtr FALSE = IntPtr.Zero;
		public static readonly IntPtr TRUE = new IntPtr(1);

		public const int WM_PAINT = 0x000F;

		#endregion

		#region ComboBoxButtonState

		public enum ComboBoxButtonState
		{
			STATE_SYSTEM_NONE = 0,
			STATE_SYSTEM_INVISIBLE = 0x00008000,
			STATE_SYSTEM_PRESSED = 0x00000008
		}

		#endregion

		#region RECT

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;

			public RECT(int left, int top, int right, int bottom)
			{
				Left = left;
				Top = top;
				Right = right;
				Bottom = bottom;
			}

			public RECT(Rectangle rect)
			{
				Left = rect.Left;
				Top = rect.Top;
				Right = rect.Right;
				Bottom = rect.Bottom;
			}

			public Rectangle Rect
			{
				get
				{
					return new Rectangle(
						Left,
						Top,
						Right - Left,
						Bottom - Top);
				}
			}

			public Size Size
			{
				get
				{
					return new Size(Right - Left, Bottom - Top);
				}
			}

			public static RECT FromXYWH(int x, int y, int width, int height)
			{
				return new RECT(x,
								y,
								x + width,
								y + height);
			}

			public static RECT FromRectangle(Rectangle rect)
			{
				return new RECT(rect.Left,
								 rect.Top,
								 rect.Right,
								 rect.Bottom);
			}
		}

		#endregion

		#region PAINTSTRUCT

		[StructLayout(LayoutKind.Sequential)]
		public struct PAINTSTRUCT
		{
			public IntPtr hdc;
			public int fErase;
			public RECT rcPaint;
			public int fRestore;
			public int fIncUpdate;
			public int Reserved1;
			public int Reserved2;
			public int Reserved3;
			public int Reserved4;
			public int Reserved5;
			public int Reserved6;
			public int Reserved7;
			public int Reserved8;
		}

		#endregion

		#region ComboBoxInfo Struct

		[StructLayout(LayoutKind.Sequential)]
		public struct ComboBoxInfo
		{
			public int cbSize;
			public RECT rcItem;
			public RECT rcButton;
			public ComboBoxButtonState stateButton;
			public IntPtr hwndCombo;
			public IntPtr hwndEdit;
			public IntPtr hwndList;
		}

		#endregion

		#region API Methods

		[DllImport("user32.dll")]
		public static extern bool GetComboBoxInfo(
			IntPtr hwndCombo, ref ComboBoxInfo info);

		[DllImport("user32.dll")]
		public static extern int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

		[DllImport("user32.dll")]
		public static extern IntPtr BeginPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

		[DllImport("user32.dll")]
		public static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

		#endregion
	}

	public sealed class ControlPaintEx
	{
		public static void DrawCheckedFlag(Graphics graphics, Rectangle rect, Color color)
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

		public static void DrawGlass(
			Graphics g, RectangleF glassRect, int alphaCenter, int alphaSurround)
		{
			DrawGlass(g, glassRect, Color.White, alphaCenter, alphaSurround);
		}

		public static void DrawGlass(
		   Graphics g,
			RectangleF glassRect,
			Color glassColor,
			int alphaCenter,
			int alphaSurround)
		{
			using (GraphicsPath path = new GraphicsPath())
			{
				path.AddEllipse(glassRect);
				using (PathGradientBrush brush = new PathGradientBrush(path))
				{
					brush.CenterColor = Color.FromArgb(alphaCenter, glassColor);
					brush.SurroundColors = new Color[] { 
                        Color.FromArgb(alphaSurround, glassColor) };
					brush.CenterPoint = new PointF(
						glassRect.X + glassRect.Width / 2,
						glassRect.Y + glassRect.Height / 2);
					g.FillPath(brush, path);
				}
			}
		}

		public static void DrawBackgroundImage(
			Graphics g,
			Image backgroundImage,
			Color backColor,
			ImageLayout backgroundImageLayout,
			Rectangle bounds,
			Rectangle clipRect)
		{
			DrawBackgroundImage(
				g,
				backgroundImage,
				backColor,
				backgroundImageLayout,
				bounds,
				clipRect,
				Point.Empty,
				RightToLeft.No);
		}

		public static void DrawBackgroundImage(
			Graphics g,
			Image backgroundImage,
			Color backColor,
			ImageLayout backgroundImageLayout,
			Rectangle bounds,
			Rectangle clipRect,
			Point scrollOffset)
		{
			DrawBackgroundImage(
				g,
				backgroundImage,
				backColor,
				backgroundImageLayout,
				bounds,
				clipRect,
				scrollOffset,
				RightToLeft.No);
		}

		public static void DrawBackgroundImage(
			Graphics g,
			Image backgroundImage,
			Color backColor,
			ImageLayout backgroundImageLayout,
			Rectangle bounds,
			Rectangle clipRect,
			Point scrollOffset,
			RightToLeft rightToLeft)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			if (backgroundImageLayout == ImageLayout.Tile)
			{
				using (TextureBrush brush = new TextureBrush(backgroundImage, WrapMode.Tile))
				{
					if (scrollOffset != Point.Empty)
					{
						Matrix transform = brush.Transform;
						transform.Translate((float)scrollOffset.X, (float)scrollOffset.Y);
						brush.Transform = transform;
					}
					g.FillRectangle(brush, clipRect);
					return;
				}
			}
			Rectangle rect = CalculateBackgroundImageRectangle(
				bounds,
				backgroundImage,
				backgroundImageLayout);
			if ((rightToLeft == RightToLeft.Yes) &&
				(backgroundImageLayout == ImageLayout.None))
			{
				rect.X += clipRect.Width - rect.Width;
			}
			using (SolidBrush brush2 = new SolidBrush(backColor))
			{
				g.FillRectangle(brush2, clipRect);
			}
			if (!clipRect.Contains(rect))
			{
				if ((backgroundImageLayout == ImageLayout.Stretch) ||
					(backgroundImageLayout == ImageLayout.Zoom))
				{
					rect.Intersect(clipRect);
					g.DrawImage(backgroundImage, rect);
				}
				else if (backgroundImageLayout == ImageLayout.None)
				{
					rect.Offset(clipRect.Location);
					Rectangle destRect = rect;
					destRect.Intersect(clipRect);
					Rectangle rectangle3 = new Rectangle(Point.Empty, destRect.Size);
					g.DrawImage(
						backgroundImage,
						destRect,
						rectangle3.X,
						rectangle3.Y,
						rectangle3.Width,
						rectangle3.Height,
						GraphicsUnit.Pixel);
				}
				else
				{
					Rectangle rectangle4 = rect;
					rectangle4.Intersect(clipRect);
					Rectangle rectangle5 = new Rectangle(
						new Point(rectangle4.X - rect.X, rectangle4.Y - rect.Y),
						rectangle4.Size);
					g.DrawImage(
						backgroundImage,
						rectangle4,
						rectangle5.X,
						rectangle5.Y,
						rectangle5.Width,
						rectangle5.Height,
						GraphicsUnit.Pixel);
				}
			}
			else
			{
				ImageAttributes imageAttr = new ImageAttributes();
				imageAttr.SetWrapMode(WrapMode.TileFlipXY);
				g.DrawImage(
					backgroundImage,
					rect,
					0,
					0,
					backgroundImage.Width,
					backgroundImage.Height,
					GraphicsUnit.Pixel,
					imageAttr);
				imageAttr.Dispose();
			}
		}

		internal static Rectangle CalculateBackgroundImageRectangle(
			Rectangle bounds,
			Image backgroundImage,
			ImageLayout imageLayout)
		{
			Rectangle rectangle = bounds;
			if (backgroundImage != null)
			{
				switch (imageLayout)
				{
					case ImageLayout.None:
						rectangle.Size = backgroundImage.Size;
						return rectangle;

					case ImageLayout.Tile:
						return rectangle;

					case ImageLayout.Center:
						{
							rectangle.Size = backgroundImage.Size;
							Size size = bounds.Size;
							if (size.Width > rectangle.Width)
							{
								rectangle.X = (size.Width - rectangle.Width) / 2;
							}
							if (size.Height > rectangle.Height)
							{
								rectangle.Y = (size.Height - rectangle.Height) / 2;
							}
							return rectangle;
						}
					case ImageLayout.Stretch:
						rectangle.Size = bounds.Size;
						return rectangle;

					case ImageLayout.Zoom:
						{
							Size size2 = backgroundImage.Size;
							float num = ((float)bounds.Width) / ((float)size2.Width);
							float num2 = ((float)bounds.Height) / ((float)size2.Height);
							if (num >= num2)
							{
								rectangle.Height = bounds.Height;
								rectangle.Width = (int)((size2.Width * num2) + 0.5);
								if (bounds.X >= 0)
								{
									rectangle.X = (bounds.Width - rectangle.Width) / 2;
								}
								return rectangle;
							}
							rectangle.Width = bounds.Width;
							rectangle.Height = (int)((size2.Height * num) + 0.5);
							if (bounds.Y >= 0)
							{
								rectangle.Y = (bounds.Height - rectangle.Height) / 2;
							}
							return rectangle;
						}
				}
			}
			return rectangle;
		}
	}

}

