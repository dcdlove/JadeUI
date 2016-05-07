/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-10-26 19:29:39
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Jade.Helper
{
	public class WinIO
	{
		private const int KBC_KEY_CMD = 0x64;
		private const int KBC_KEY_DATA = 0x60;
		[DllImport("winio32.dll")]
		private static extern bool InitializeWinIo();
		[DllImport("winio32.dll")]
		private static extern bool GetPortVal(IntPtr wPortAddr, out int pdwPortVal, byte bSize);
		[DllImport("winio32.dll")]
		private static extern bool SetPortVal(uint wPortAddr, IntPtr dwPortVal, byte bSize);
		[DllImport("winio32.dll")]
		private static extern byte MapPhysToLin(byte pbPhysAddr, uint dwPhysSize, IntPtr PhysicalMemoryHandle);
		[DllImport("winio32.dll")]
		private static extern bool UnmapPhysicalMemory(IntPtr PhysicalMemoryHandle, byte pbLinAddr);
		[DllImport("winio32.dll")]
		private static extern bool GetPhysLong(IntPtr pbPhysAddr, byte pdwPhysVal);
		[DllImport("winio32.dll")]
		private static extern bool SetPhysLong(IntPtr pbPhysAddr, byte dwPhysVal);
		[DllImport("winio32.dll")]
		private static extern void ShutdownWinIo();
		[DllImport("user32.dll")]
		private static extern int MapVirtualKey(uint Ucode, uint uMapType);


		private WinIO()
		{
			IsInitialize = true;
		}
		/// <summary>
		/// 注册
		/// </summary>
		public static void Initialize()
		{
			if (InitializeWinIo())
			{
				KBCWait4IBE();
				IsInitialize = true;
			}
		}
		/// <summary>
		/// 释放
		/// </summary>
		public static void Shutdown()
		{
			if (IsInitialize)
				ShutdownWinIo();
			IsInitialize = false;
		}

		private static bool IsInitialize { get; set; }

		/// <summary>
		///  等待键盘缓冲区为空
		/// </summary>
		private static void KBCWait4IBE()
		{
			int dwVal = 0;
			do
			{
				bool flag = GetPortVal((IntPtr)0x64, out dwVal, 1);
			}
			while ((dwVal & 0x2) > 0);
		}
		/// <summary>
		/// 模拟键盘标按下
		/// </summary>
		/// <param name="vKeyCoad"></param>
		public static void KeyDown(Keys vKeyCoad)
		{
			if (!IsInitialize) return;

			int btScancode = 0;
			btScancode = MapVirtualKey((uint)vKeyCoad, 0);
			KBCWait4IBE();
			SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);
			KBCWait4IBE();
			SetPortVal(KBC_KEY_DATA, (IntPtr)0x60, 1);
			KBCWait4IBE();
			SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);
			KBCWait4IBE();
			SetPortVal(KBC_KEY_DATA, (IntPtr)btScancode, 1);
		}
		/// <summary>
		/// 模拟键盘弹出
		/// </summary>
		/// <param name="vKeyCoad"></param>
		public static void KeyUp(Keys vKeyCoad)
		{
			if (!IsInitialize) return;

			int btScancode = 0;
			btScancode = MapVirtualKey((uint)vKeyCoad, 0);
			KBCWait4IBE();
			SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);
			KBCWait4IBE();
			SetPortVal(KBC_KEY_DATA, (IntPtr)0x60, 1);
			KBCWait4IBE();
			SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);
			KBCWait4IBE();
			SetPortVal(KBC_KEY_DATA, (IntPtr)(btScancode | 0x80), 1);
		}


		//---------------------------------------
		//WinIO.Initialize(); // 注册
		//WinIO.KeyDown(Keys.A); // 按下A
		//WinIO.KeyUp(Keys.A); // 松开A
		//WinIO.Shutdown(); // 用完后注销

	}
}
