﻿/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-05-15 18:03:37
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Security.Permissions;

namespace Jade.UI.Shadow
{
	//控件层
	[PermissionSetAttribute(SecurityAction.LinkDemand, Name = "FullTrust")]//[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]//  
	[System.Runtime.InteropServices.ComVisibleAttribute(true)]//This property lets you integrate dynamic HTML (DHTML) code with your client application code
	public partial class FormMain : Form
	{
		//绘制层
		private FormShadow skin;
		public FormMain()
		{
			InitializeComponent();

			SetStyles();
		}

		/// <summary>
		/// 减少闪烁
		/// </summary>
		private void SetStyles()
		{
			SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.ResizeRedraw |
				ControlStyles.DoubleBuffer, true);
			//强制分配样式重新应用到控件上
			UpdateStyles();
			base.AutoScaleMode = AutoScaleMode.None;
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("关闭窗体阴影效果")]
		public void CloseShadow()
		{
			if (skin != null)
			{
				skin.Close();
			}
		}

		//不显示FormBorderStyle属性
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new FormBorderStyle FormBorderStyle
		{
			get { return base.FormBorderStyle; }
			set { base.FormBorderStyle = FormBorderStyle.None; }
		}


		//Show或Hide被调用时
		protected override void OnVisibleChanged(EventArgs e)
		{
			if (Visible)
			{

				//判断不是在设计器中
				if (!DesignMode && skin == null)
				{
					skin = new FormShadow(this);
					skin.Show(this);
				}
				base.OnVisibleChanged(e);
			}
			else
			{
				base.OnVisibleChanged(e);

			}
		}

		//窗体关闭时
		protected override void OnClosing(CancelEventArgs e)
		{

			//先关闭阴影窗体
			CloseShadow();
			base.OnClosing(e);
		}

		//控件首次创建时被调用
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			
		}

		
		//改变窗体大小时
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			
		}

		const int HTLEFT = 10;
		const int HTRIGHT = 11;
		const int HTTOP = 12;
		const int HTTOPLEFT = 13;
		const int HTTOPRIGHT = 14;
		const int HTBOTTOM = 15;
		const int HTBOTTOMLEFT = 0x10;
		const int HTBOTTOMRIGHT = 17;

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case 0x0084:
					base.WndProc(ref m);
					//取消运行调整窗体大小
					break;
					Point vPoint = new Point((int)m.LParam & 0xFFFF,
						(int)m.LParam >> 16 & 0xFFFF);
					vPoint = PointToClient(vPoint);
					if (vPoint.X <= 5)
						if (vPoint.Y <= 5)
							m.Result = (IntPtr)HTTOPLEFT;
						else if (vPoint.Y >= ClientSize.Height - 5)
							m.Result = (IntPtr)HTBOTTOMLEFT;
						else m.Result = (IntPtr)HTLEFT;
					else if (vPoint.X >= ClientSize.Width - 5)
						if (vPoint.Y <= 5)
							m.Result = (IntPtr)HTTOPRIGHT;
						else if (vPoint.Y >= ClientSize.Height - 5)
							m.Result = (IntPtr)HTBOTTOMRIGHT;
						else m.Result = (IntPtr)HTRIGHT;
					else if (vPoint.Y <= 5)
						m.Result = (IntPtr)HTTOP;
					else if (vPoint.Y >= ClientSize.Height - 5)
						m.Result = (IntPtr)HTBOTTOM;
					break;
				case 0x0201://鼠标左键按下的消息 
					m.Msg = 0x00A1;//更改消息为非客户区按下鼠标 
					m.LParam = IntPtr.Zero;//默认值 
					m.WParam = new IntPtr(2);//鼠标放在标题栏内 
					base.WndProc(ref m);
					break;
				default:
					base.WndProc(ref m);
					break;
			}
		}
		

		
		//protected override CreateParams CreateParams {
		//    get {
		//        const int WS_MINIMIZEBOX = 0x00020000;  // Winuser.h中定义
		//        CreateParams cp = base.CreateParams;
		//        cp.Style = cp.Style | WS_MINIMIZEBOX;   // 允许最小化操作
		//        return cp;
		//    }
		//}
		
	}
}
