/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-10-29 18:17:44
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
using System.Security.Permissions;
using System.Runtime.InteropServices;
using Jade.InteropServices;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;



namespace Jade.UI
{
	
	[PermissionSetAttribute(SecurityAction.LinkDemand, Name = "FullTrust")]//[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]//  
	[System.Runtime.InteropServices.ComVisibleAttribute(true)]//This property lets you integrate dynamic HTML (DHTML) code with your client application code
	public partial class BackForm : Form
	{
		private Form bindform;//绑定窗体
		private int backborder = 0;//背景边框
		private Image shadowimg;//阴影图

		public BackForm()
		{
			InitializeComponent();

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="target"></param>
		/// <param name="border"></param>
		public BackForm(Form frm)
		{
			InitializeComponent();

			//SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
			//UpdateStyles();
			
			bindform = frm;

			
			//置顶窗体
			TopMost = bindform.TopMost;
			bindform.BringToFront();
			//是否在任务栏显示
			ShowInTaskbar = false;
			//无边框模式
			FormBorderStyle = FormBorderStyle.None;

			//设置ICO
			Icon = bindform.Icon;
			ShowIcon = bindform.ShowIcon;
			
			//设置标题名
			Text = bindform.Text;

			AddOwnedForm(bindform);
			bindform.SizeChanged += (s, e) =>
			{
				ChangeRectangle();
				DrawBackground();
			};
			bindform.LocationChanged += (s, e) =>
			{
				ChangeRectangle();
			};
			bindform.VisibleChanged += (s, e) => 
			{
				Visible = bindform.Visible;
			};
			ChangeRectangle();
			DrawBackground();
			//鼠标穿透
			Win32.GetWindowLong(this.Handle, Win32.GWL_EXSTYLE);
			Win32.SetWindowLong(this.Handle, Win32.GWL_EXSTYLE, Win32.WS_EX_TRANSPARENT | Win32.WS_EX_LAYERED);
		}

	
		/// <summary>
		/// 绘制背景
		/// </summary>
		private void DrawBackground()
		{
			//绘制绘图层背景
			Bitmap bitmap = new Bitmap(Width,Height);
			Graphics g = Graphics.FromImage(bitmap);
			g.SmoothingMode = SmoothingMode.HighQuality; //高质量
			g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
			Jade.UI.RenderHelper.DrawImageWithNineRect(g, shadowimg, ClientRectangle, new Rectangle { Size = shadowimg.Size });

			SetBitmap(bitmap);
		}

		/// <summary>
		/// 调节背景区域
		/// </summary>
		/// <returns></returns>
		private void ChangeRectangle()
		{
			//设置阴影图片
			if ((Screen.PrimaryScreen.Bounds.Height - bindform.Height) > 200)
			{
				backborder = 200;//大尺寸
				shadowimg = Jade.UI.Properties.Resources.shadow_bkg; ;
			}
			else if ((Screen.PrimaryScreen.Bounds.Height - bindform.Height) > 50)
			{
				backborder = 50;//小尺寸
				shadowimg = Jade.UI.Properties.Resources.shadow_bg;
			}
			//设置大小
			Width = bindform.Width + backborder * 2;
			Height = bindform.Height + backborder * 2;
			//设置位置
			Top = bindform.Top - backborder;
			Left = bindform.Left - backborder;

			
		}

		/// <summary>
		/// 绘制位图
		/// </summary>
		/// <param name="bitmap">位图</param>
		/// <param name="opacity">透明度</param>
		/// <returns></returns>
		public void SetBitmap(Bitmap bitmap, byte opacity = 255)
		{
			//if (!Image.IsCanonicalPixelFormat(bitmap.PixelFormat) || !Image.IsAlphaPixelFormat(bitmap.PixelFormat))
			//{
			//    throw new ApplicationException("图片必须是32位带Alhpa通道的图片。");
			//}
			IntPtr hObj = IntPtr.Zero;
			IntPtr dC = Win32.GetDC(IntPtr.Zero);
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = Win32.CreateCompatibleDC(dC);
			try
			{
				Win32.Point point = new Win32.Point(base.Left, base.Top);
				Win32.Size size = new Win32.Size(bitmap.Width, bitmap.Height);
				Win32.BLENDFUNCTION bLENDFUNCTION = default(Win32.BLENDFUNCTION);
				Win32.Point point2 = new Win32.Point(0, 0);
				intPtr = bitmap.GetHbitmap(Color.FromArgb(0));
				hObj = Win32.SelectObject(intPtr2, intPtr);
				bLENDFUNCTION.BlendOp = 0;
				bLENDFUNCTION.SourceConstantAlpha = opacity;
				bLENDFUNCTION.AlphaFormat = 1;
				bLENDFUNCTION.BlendFlags = 0;
				Win32.UpdateLayeredWindow(base.Handle, dC, ref point, ref size, intPtr2, ref point2, 0, ref bLENDFUNCTION, 2);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Win32.SelectObject(intPtr2, hObj);
					Win32.DeleteObject(intPtr);
				}
				Win32.ReleaseDC(IntPtr.Zero, dC);
				Win32.DeleteDC(intPtr2);
			}
		}

		/// <summary>
		/// 改变图片透明度
		/// </summary>
		/// <param name="srcImage"></param>
		/// <param name="opacity"></param>
		/// <returns></returns>
		public  Bitmap ChangeOpacity(Bitmap srcImage, float opacity)
		{
			if (opacity == 1f)
			{
				return srcImage;
			}
			if (opacity == 0f)
			{
				return new Bitmap(srcImage.Width, srcImage.Height);
			}
			float[][] array = new float[5][];
			float[][] arg_44_0 = array;
			int arg_44_1 = 0;
			float[] array2 = new float[5];
			array2[0] = 1f;
			arg_44_0[arg_44_1] = array2;
			float[][] arg_5B_0 = array;
			int arg_5B_1 = 1;
			float[] array3 = new float[5];
			array3[1] = 1f;
			arg_5B_0[arg_5B_1] = array3;
			float[][] arg_72_0 = array;
			int arg_72_1 = 2;
			float[] array4 = new float[5];
			array4[2] = 1f;
			arg_72_0[arg_72_1] = array4;
			float[][] arg_85_0 = array;
			int arg_85_1 = 3;
			float[] array5 = new float[5];
			array5[3] = opacity;
			arg_85_0[arg_85_1] = array5;
			array[4] = new float[]
			{
				0f, 
				0f, 
				0f, 
				0f, 
				1f
			};
			float[][] newColorMatrix = array;
			ColorMatrix newColorMatrix2 = new ColorMatrix(newColorMatrix);
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.SetColorMatrix(newColorMatrix2, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
			Bitmap bitmap = new Bitmap(srcImage.Width, srcImage.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.DrawImage(srcImage, new Rectangle(0, 0, srcImage.Width, srcImage.Height), 0, 0, srcImage.Width, srcImage.Height, GraphicsUnit.Pixel, imageAttributes);
			return bitmap;
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= 524288;
				return createParams;
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);
			UpdateStyles();
			base.OnHandleCreated(e);
		}

		
	}


}
#region WIN32

namespace Jade.InteropServices
{
	public struct BLENDFUNCTION
	{
		public byte AlphaFormat;
		public byte BlendFlags;
		public byte BlendOp;
		public byte SourceConstantAlpha;
	}

	public static class Win32
	{
		[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern int UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pptSrc, Int32 crKey, ref  BLENDFUNCTION pblend, Int32 dwFlags);
		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
		[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetDC(IntPtr hWnd);
		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern int DeleteDC(IntPtr hDC);
		[DllImport("gdi32.dll", ExactSpelling = true)]
		public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObj);
		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern int DeleteObject(IntPtr hObj);
		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
		[DllImport("user32", EntryPoint = "GetWindowLong")]
		public static extern int GetWindowLong(IntPtr hwnd, int nIndex);
		[DllImport("user32.dll")]
		public static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);
		[DllImport("user32.dll")]
		public static extern int SetWindowRgn(IntPtr hwnd, int hRgn, Boolean bRedraw);
		[DllImport("gdi32.dll")]
		public static extern int CreateRoundRectRgn(int x1, int y1, int x2, int y2, int x3, int y3);
		[DllImport("gdi32.dll", EntryPoint = "DeleteObject", CharSet = CharSet.Ansi)]
		public static extern int DeleteObject(int hObject);
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		public const int GWL_EXSTYLE = -20;
		public const int WS_EX_TRANSPARENT = 0x00000020;
		public const int WS_EX_LAYERED = 0x00080000;
		public const int WM_NCLBUTTONDOWN = 161;
		public const int HTCAPTION = 2;

		public struct Size
		{
			public int cx;
			public int cy;
			public Size(int x, int y)
			{
				this.cx = x;
				this.cy = y;
			}
		}
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct BLENDFUNCTION
		{
			public byte BlendOp;
			public byte BlendFlags;
			public byte SourceConstantAlpha;
			public byte AlphaFormat;
		}
		public struct Point
		{
			public int x;
			public int y;
			public Point(int x, int y)
			{
				this.x = x;
				this.y = y;
			}
		}
	}


}


#endregion