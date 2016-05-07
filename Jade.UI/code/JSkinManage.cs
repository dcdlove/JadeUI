/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-10-28 16:59:20
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;

namespace Jade.UI
{
	public class JSkinManage
	{
		[DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = true)]
		private static extern void CopyMemory(IntPtr Dest, IntPtr src, int Length);


		static JSkinManage()
		{

		}

		private static JFormSkin _JFormSkin;

		/// <summary>
		/// 窗体主题对象
		/// </summary>
		public static JFormSkin JFormSkin
		{
			get
			{
				if (_JFormSkin == null) _JFormSkin = new JFormSkin();
				return _JFormSkin;
			}
		}

		/// <summary>
		/// 对图片进行高斯模糊处理
		/// </summary>
		/// <author>jaly</author>
		/// <date>2015-10-28 17:06:38</date>
		/// <param name="img">图片</param>
		/// <param name="radius">半径</param>
		/// <returns></returns>
		public static Bitmap GetBackgroundEffectImage(Image img, int radius)
		{
			bool expandEdge = false;
			Bitmap bmp = null;
			IntPtr ImageCopyPointer, ImagePointer;
			int DataLength;
			try
			{
				bmp = new Bitmap(img);
				BitmapData BmpData = new BitmapData();
				bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat, BmpData);    // 用原始格式LockBits,得到图像在内存中真正地址，这个地址在图像的大小，色深等未发生变化时，每次Lock返回的Scan0值都是相同的。
				ImagePointer = BmpData.Scan0;                            //  记录图像在内存中的真正地址
				DataLength = BmpData.Stride * BmpData.Height;           //  记录整幅图像占用的内存大小
				ImageCopyPointer = Marshal.AllocHGlobal(DataLength);    //  直接用内存数据来做备份，AllocHGlobal在内部调用的是LocalAlloc函数
				CopyMemory(ImageCopyPointer, ImagePointer, DataLength); //  这里当然也可以用Bitmap的Clone方式来处理，但是我总认为直接处理内存数据比用对象的方式速度快。
				bmp.UnlockBits(BmpData);
				CopyMemory(ImagePointer, ImageCopyPointer, DataLength);             // 需要恢复原始的图像数据，不然模糊就会叠加了。
				Rectangle Rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
				Stopwatch Sw = new Stopwatch();
				Sw.Start();
				bmp.GaussianBlur(ref Rect, radius, expandEdge);

				Sw.Stop();

			}
			catch (Exception d)
			{
				Debug.Write(d);
			}
			return bmp;
		}

		public static void CreateDirectory(string path)
		{
			var dirpath = Path.GetDirectoryName(path);
			if (!Directory.Exists(dirpath))
				Directory.CreateDirectory(dirpath);

		}

		
	}
}
