/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-10-26 19:16:27
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Management;

namespace Jade.Helper
{
	public class PlugIn
	{
		//http://www.cnblogs.com/hehexiaoxia/p/4223927.html

		[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
		public static extern IntPtr FindWindowEx(IntPtr hwndParent, uint hwndChildAfter, string lpszClass, string lpszWindow);

		[DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

		[DllImport("user32.dll", EntryPoint = "SetForegroundWindow", SetLastError = true)]
		public static extern void SetForegroundWindow(IntPtr hwnd);

		[DllImport("user32.dll")]
		public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);

		public delegate bool CallBack(IntPtr hwnd, int lParam);

		/// <summary>
		/// 获得窗体句柄
		/// </summary>
		public static IntPtr GetWindowHandle()
		{

			IntPtr hWnd = FindWindow(null, "附加到进程");

			var btn = FindWindowEx(hWnd, "附加(&A)", true);
			//SendMessage(btn, 0x102, btn, null);

			return btn;
		}

		/// <summary>
		/// 查找窗体上控件句柄
		/// </summary>
		/// <param name="hwnd">父窗体句柄</param>
		/// <param name="lpszWindow">控件标题(Text)</param>
		/// <param name="bChild">设定是否在子窗体中查找</param>
		/// <returns>控件句柄，没找到返回IntPtr.Zero</returns>
		public static IntPtr FindWindowEx(IntPtr hwnd, string lpszWindow, bool bChild)
		{
			IntPtr iResult = IntPtr.Zero;
			// 首先在父窗体上查找控件
			iResult = FindWindowEx(hwnd, 0, null, lpszWindow);
			// 如果找到直接返回控件句柄
			if (iResult != IntPtr.Zero) return iResult;

			// 如果设定了不在子窗体中查找
			if (!bChild) return iResult;

			// 枚举子窗体，查找控件句柄
			int i = EnumChildWindows(hwnd, (h, l) =>
			{
				IntPtr f1 = FindWindowEx(h, 0, null, lpszWindow);
				if (f1 == IntPtr.Zero)
					return true;
				else
				{
					iResult = f1;
					return false;
				}
			},
			0);
			// 返回查找结果
			return iResult;
		}

	}
}


