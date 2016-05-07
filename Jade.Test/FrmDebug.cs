/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-10-27 10:04:53
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
using Jade.Helper;
using System.Runtime.InteropServices;
using Jade.UI;
namespace Jade.Test
{
	public partial class FrmDebug : JBaseForm
	{

		int pwnd = 0;
		int hwnd;   //窗口句柄
		int process;//进程句柄
		int pointer;
		int tindex = 0;

		private const uint LVM_FIRST = 0x1000;
		private const uint LVM_GETHEADER = LVM_FIRST + 31;
		private const uint LVM_GETITEMCOUNT = LVM_FIRST + 4;//获取列表行数
		private const uint LVM_GETITEMTEXT = LVM_FIRST + 45;//获取列表内的内容
		private const uint LVM_SETITEMSTATE = (LVM_FIRST + 43);//在Listview控件中设置当前选择项目
		private const uint LVM_GETITEMW = LVM_FIRST + 75;

		private const uint HDM_GETITEMCOUNT = 0x1200;//获取列表列数

		private const uint PROCESS_VM_OPERATION = 0x0008;//允许函数VirtualProtectEx使用此句柄修改进程的虚拟内存
		private const uint PROCESS_VM_READ = 0x0010;//允许函数访问权限
		private const uint PROCESS_VM_WRITE = 0x0020;//允许函数写入权限

		private const uint MEM_COMMIT = 0x1000;//为特定的页面区域分配内存中或磁盘的页面文件中的物理存储
		private const uint MEM_RELEASE = 0x8000;
		private const uint MEM_RESERVE = 0x2000;//保留进程的虚拟地址空间,而不分配任何物理存储

		private const uint PAGE_READWRITE = 4;

		private int LVIF_TEXT = 0x0001;

		[DllImport("user32.dll")]//查找窗口
		private static extern int FindWindow(
											string strClassName,    //窗口类名
											string strWindowName    //窗口标题
		);

		[DllImport("user32.dll")]//在窗口列表中寻找与指定条件相符的第一个子窗口
		private static extern int FindWindowEx(
											  int hwndParent, // handle to parent window
											int hwndChildAfter, // handle to child window
											  string className, //窗口类名            
											  string windowName // 窗口标题
		);
		[DllImport("user32.DLL")]
		private static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);
		[DllImport("user32.dll")]//找出某个窗口的创建者(线程或进程),返回创建者的标志符
		private static extern int GetWindowThreadProcessId(int hwnd, out int processId);
		[DllImport("kernel32.dll")]//打开一个已存在的进程对象,并返回进程的句柄
		private static extern int OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int processId);
		[DllImport("kernel32.dll")]//为指定的进程分配内存地址:成功则返回分配内存的首地址
		private static extern int VirtualAllocEx(int hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
		[DllImport("kernel32.dll")]//从指定内存中读取字节集数据
		private static extern bool ReadProcessMemory(
											int hProcess, //被读取者的进程句柄
											int lpBaseAddress,//开始读取的内存地址
											IntPtr lpBuffer, //数据存储变量
											int nSize, //要写入多少字节
											ref uint vNumberOfBytesRead//读取长度
		);
		[DllImport("kernel32.dll")]//将数据写入内存中
		private static extern bool WriteProcessMemory(
											int hProcess,//由OpenProcess返回的进程句柄
											int lpBaseAddress, //要写的内存首地址,再写入之前,此函数将先检查目标地址是否可用,并能容纳待写入的数据
											IntPtr lpBuffer, //指向要写的数据的指针
											int nSize, //要写入的字节数
											ref uint vNumberOfBytesRead
		);
		[DllImport("kernel32.dll")]
		private static extern bool CloseHandle(int handle);
		[DllImport("kernel32.dll")]//在其它进程中释放申请的虚拟内存空间
		private static extern bool VirtualFreeEx(
									int hProcess,//目标进程的句柄,该句柄必须拥有PROCESS_VM_OPERATION的权限
									int lpAddress,//指向要释放的虚拟内存空间首地址的指针
									uint dwSize,
									uint dwFreeType//释放类型
		);
		/// <summary>
		/// LVITEM结构体,是列表视图控件的一个重要的数据结构
		/// 占空间：4(int)x7=28个byte
		/// </summary>
		private struct LVITEM  //结构体
		{
			public int mask;//说明此结构中哪些成员是有效的
			public int iItem;//项目的索引值(可以视为行号)从0开始
			public int iSubItem; //子项的索引值(可以视为列号)从0开始
			public int state;//子项的状态
			public int stateMask; //状态有效的屏蔽位
			public IntPtr pszText;  //主项或子项的名称
			public int cchTextMax;//pszText所指向的缓冲区大小
		}

		[DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
		public static extern bool SetForegroundWindow(int handle);

		[DllImport("user32.dll", EntryPoint = "SetParent")]
		public static extern int SetParent(int hWndChild, int hWndNewParent);


		public FrmDebug()
		{
			InitializeComponent();
		}

		/// <summary>  
		/// LV列表总行数
		/// </summary>
		private int ListView_GetItemRows(int handle)
		{
			return SendMessage(handle, LVM_GETITEMCOUNT, 0, 0);
		}
		/// <summary>  
		/// LV列表总列数
		/// </summary>
		private int ListView_GetItemCols(int handle)
		{
			return SendMessage(handle, HDM_GETITEMCOUNT, 0, 0);
		}

		/// <summary>
		/// 设置LV列表选中项
		/// </summary>
		/// <param name="handle"></param>
		/// <param name="index">行索引（-1 取消选中)</param>
		/// <returns></returns>
		private int ListView_SetItemState(int handle, int index)
		{
			return SendMessage(handle, LVM_SETITEMSTATE, 0, index);
		}


		/// <summary>
		/// 从内存中读取指定的LV控件的文本内容
		/// </summary>
		/// <param name="rows">要读取的LV控件的行数</param>
		/// <param name="cols">要读取的LV控件的列数</param>
		/// <returns>取得的LV控件信息</returns>
		private string[,] GetListViewItmeValue(int rows, int cols)
		{
			string[,] tempStr = new string[rows, cols];//二维数组:保存LV控件的文本信息
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					byte[] vBuffer = new byte[256];//定义一个临时缓冲区
					LVITEM[] vItem = new LVITEM[1];
					vItem[0].mask = LVIF_TEXT;//说明pszText是有效的
					vItem[0].iItem = i;     //行号
					vItem[0].iSubItem = j;  //列号
					vItem[0].cchTextMax = vBuffer.Length;//所能存储的最大的文本为256字节
					vItem[0].pszText = (IntPtr)((int)pointer + Marshal.SizeOf(typeof(LVITEM)));
					uint vNumberOfBytesRead = 0;

					//把数据写到vItem中
					//pointer为申请到的内存的首地址
					//UnsafeAddrOfPinnedArrayElement:获取指定数组中指定索引处的元素的地址
					WriteProcessMemory(process, pointer, Marshal.UnsafeAddrOfPinnedArrayElement(vItem, 0), Marshal.SizeOf(typeof(LVITEM)), ref vNumberOfBytesRead);

					//发送LVM_GETITEMW消息给hwnd,将返回的结果写入pointer指向的内存空间
					SendMessage(hwnd, LVM_GETITEMW, i, pointer);

					//从pointer指向的内存地址开始读取数据,写入缓冲区vBuffer中
					ReadProcessMemory(process, ((int)pointer + Marshal.SizeOf(typeof(LVITEM))), Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0), vBuffer.Length, ref vNumberOfBytesRead);

					string vText = Encoding.Unicode.GetString(vBuffer, 0, (int)vNumberOfBytesRead); ;
					tempStr[i, j] = vText;
				}
			}
			VirtualFreeEx(process, pointer, 0, MEM_RELEASE);//在其它进程中释放申请的虚拟内存空间,MEM_RELEASE方式很彻底,完全回收
			CloseHandle(process);//关闭打开的进程对象
			return tempStr;
		}


		/// <summary>
		/// 获得窗体句柄
		/// </summary>
		public void Aoto()
		{
			//IntPtr windowHandle = PlugIn.FindWindow(null, "附加到进程");
			//IntPtr btnHandle = PlugIn.FindWindowEx(windowHandle, "附加(&A)", true);

			//jLable1.Value = windowHandle.ToString();
			//jLable2.Value = btnHandle.ToString();
		}

		private void FrmDebug_Load(object sender, EventArgs e)
		{
			RenderSkinCallback = (skin) =>
			{
				skin.HeaderHeight = 100;
				return skin;
			};



		}

		private void btnOk_Click(object sender, EventArgs e)
		{

			int headerhwnd; //listview控件的列头句柄
			int rows, cols;  //listview控件中的行列数
			int processId; //进程pid  

			hwnd = FindWindow("#32770", "附加到进程");

			pwnd = hwnd;
			//hwnd = FindWindowEx(hwnd, 0, "#32770", null);
			hwnd = FindWindowEx(hwnd, 0, "SysListView32", null);//
			headerhwnd = SendMessage(hwnd, LVM_GETHEADER, 0, 0);//listview的列头句柄

			cols = ListView_GetItemCols(headerhwnd);//列表列数
			rows = ListView_GetItemRows(hwnd);//总行数，即进程的数量



			GetWindowThreadProcessId(hwnd, out processId);
			jLable1.Value = processId.ToString();

			//打开并插入进程
			process = OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_READ | PROCESS_VM_WRITE, false, processId);
			//申请代码的内存区,返回申请到的虚拟内存首地址
			pointer = VirtualAllocEx(process, IntPtr.Zero, 4096, MEM_RESERVE | MEM_COMMIT, PAGE_READWRITE);
			string[,] tempStr;//二维数组
			string[] temp = new string[cols];

			tempStr = GetListViewItmeValue(rows, cols);//将要读取的其他程序中的ListView控件中的文本内容保存到二维数组中

			listView1.Items.Clear();//清空LV控件信息
			//输出数组中保存的其他程序的LV控件信息
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					temp[j] = tempStr[i, j];
				}

				ListViewItem lvi = new ListViewItem(temp);
				if (lvi.SubItems[2].Text.Contains("端口"))
				{
					SetForegroundWindow(pwnd);
					SendKeys.Send("{DOWN " + i + "}");
					//SendKeys.Send("~");
				}
				listView1.Items.Add(lvi);
			}

			for (int i = 0; i < listView1.Items.Count; i++)
			{
				//item.SubItems[0].ToString();
			}



		}

		private void btnSelect_Click(object sender, EventArgs e)
		{
			MsgBox.Alert("999");


		}


	}
}
