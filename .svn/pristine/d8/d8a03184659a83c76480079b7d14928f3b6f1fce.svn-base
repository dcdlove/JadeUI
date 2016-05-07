/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-08-03 13:38:41
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Globalization;

namespace Jade.UI
{


	public class Win32
	{
		#region Window Const

		public const int MF_REMOVE = 0x1000;

		public const int SC_RESTORE = 0xF120; //还原
		public const int SC_MOVE = 0xF010; //移动
		public const int SC_SIZE = 0xF000; //大小
		public const int SC_MINIMIZE = 0xF020; //最小化
		public const int SC_MAXIMIZE = 0xF030; //最大化
		public const int SC_CLOSE = 0xF060; //关闭 

		public const int WM_SYSCOMMAND = 0x0112;
		public const int WM_COMMAND = 0x0111;

		public const int GW_HWNDFIRST = 0;
		public const int GW_HWNDLAST = 1;
		public const int GW_HWNDNEXT = 2;
		public const int GW_HWNDPREV = 3;
		public const int GW_OWNER = 4;
		public const int GW_CHILD = 5;

		public const int WM_LBUTTONDOWN = 0x0201;
		public const int WM_LBUTTONUP = 0x0202;
		public const int WM_LBUTTONDBLCLK = 0x0203;
		public const int WM_WINDOWPOSCHANGING = 0x46;
		public const int WM_PAINT = 0xF;
		public const int WM_CREATE = 0x0001;
		public const int WM_ACTIVATE = 0x0006;
		public const int WM_NCCREATE = 0x0081;
		public const int WM_NCCALCSIZE = 0x0083;
		public const int WM_NCPAINT = 0x0085;
		public const int WM_NCACTIVATE = 0x0086;
		public const int WM_NCLBUTTONDOWN = 0x00A1;
		public const int WM_NCLBUTTONUP = 0x00A2;
		public const int WM_NCLBUTTONDBLCLK = 0x00A3;
		public const int WM_NCMOUSEMOVE = 0x00A0;

		public const int WM_PRINT = 0x317;
		public const int WM_DESTROY = 0x2;
		public const int WM_SHOWWINDOW = 0x18;
		public const int WM_SHARED_MENU = 0x1E2;
		public const int HC_ACTION = 0;
		public const int WH_CALLWNDPROC = 4;
		public const int GWL_WNDPROC = -4;
		public const int WM_NCHITTEST = 0x0084;

		public const int HTLEFT = 10;
		public const int HTRIGHT = 11;
		public const int HTTOP = 12;
		public const int HTTOPLEFT = 13;
		public const int HTTOPRIGHT = 14;
		public const int HTBOTTOM = 15;
		public const int HTBOTTOMLEFT = 0x10;
		public const int HTBOTTOMRIGHT = 17;
		public const int HTCAPTION = 2;

		public const int WS_SYSMENU = 0x80000;
		public const int WS_SIZEBOX = 0x40000;

		public const int WS_MAXIMIZEBOX = 0x10000;

		public const int WS_MINIMIZEBOX = 0x20000;

		public const int WM_FALSE = 0;
		public const int WM_TRUE = 1;

		public const int CS_DROPSHADOW = 0x20000;

		public const int GCW_ATOM = -32;
		public const int GCL_CBCLSEXTRA = -20;
		public const int GCL_CBWNDEXTRA = -18;
		public const int GCL_HBRBACKGROUND = -10;
		public const int GCL_HCURSOR = -12;
		public const int GCL_HICON = -14;
		public const int GCL_HMODULE = -16;
		public const int GCL_MENUNAME = -8;
		public const int GCL_STYLE = -26;
		public const int GCL_WNDPROC = -24;

		//-----------------------------------
		public const int WM_ERASEBKGND = 0x0014;

		public const int HTCLIENT = 1;
		public const int CS_DropSHADOW = 0x20000;
		public const int AW_BLEND = 0x00080000;
		public const int AW_CENTER = 0x00000010;
		public const int AW_ACTIVATE = 0x00020000;

		#endregion

		#region Public extern methods

		[DllImport("gdi32.dll")]
		public static extern int CreateRoundRectRgn(int x1, int y1, int x2, int y2, int x3, int y3);

		[DllImport("user32.dll")]
		public static extern int SetWindowRgn(IntPtr hwnd, int hRgn, Boolean bRedraw);

		[DllImport("gdi32.dll", EntryPoint = "DeleteObject", CharSet = CharSet.Ansi)]
		public static extern int DeleteObject(int hObject);

		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		/// <summary>
		/// 释放内存
		/// </summary>
		/// <param name="process"></param>
		/// <param name="minSize"></param>
		/// <param name="maxSize"></param>
		/// <returns></returns>
		[DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
		public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);


		#endregion

		#region 窗体边框阴影效果变量申明
		//[DllImport("user32.dll", CharSet = CharSet.Auto)]
		//public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
		//[DllImport("user32.dll", CharSet = CharSet.Auto)]
		//public static extern int GetClassLong(IntPtr hwnd, int nIndex);

		#endregion

		#region 窗口显示隐藏特殊效果。有两种类型的动画效果：滚动动画和滑动动画。
		[DllImport("user32.dll")]
		public static extern bool AnimateWindow(IntPtr hWnd, int dwTime, int dwFlags);
		#endregion
	}
}
