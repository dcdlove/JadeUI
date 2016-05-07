/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-05-28 17:02:51
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
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Security.Permissions;

namespace Jade.UI.Shadow
{
    [PermissionSetAttribute(SecurityAction.LinkDemand, Name = "FullTrust")]//[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]//  
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]//This property lets you integrate dynamic HTML (DHTML) code with your client application code
    partial class FormShadow : Form
    {
        //控件层
		private FormMain bindform;

		public int backborder = 0;
		public Bitmap shadowimg { get; set; }
        //带参构造
        public FormShadow(FormMain frm)
        {
            //将控制层传值过来
			bindform = frm;
          

            InitializeComponent();
            //置顶窗体
			bindform.TopMost = TopMost = bindform.TopMost;
			bindform.BringToFront();
            //是否在任务栏显示
            ShowInTaskbar = false;
            //无边框模式
            FormBorderStyle = FormBorderStyle.None;
          
            //设置ICO
			Icon = bindform.Icon;
			ShowIcon = bindform.ShowIcon;
			////设置大小
			//Width = bindform.Width + backborder * 2;
			//Height = (bindform.Height + backborder * 2);
			////设置绘图层显示位置
			//Location = new Point(bindform.Location.X - backborder, bindform.Location.Y - backborder);
            //设置标题名
			Text = bindform.Text;
            //绘图层窗体移动
			bindform.LocationChanged += new EventHandler(Main_LocationChanged);
			bindform.SizeChanged += new EventHandler(Main_SizeChanged);
			bindform.VisibleChanged += new EventHandler(Main_VisibleChanged);
            //还原任务栏右键菜单
            //CommonClass.SetTaskMenu(Main);
			ChangeRectangle();
            //加载背景
            SetBits();
            //窗口鼠标穿透效果
            CanPenetrate();
			
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

        private void InitX()
        {
            //置顶窗体
            TopMost = bindform.TopMost;
			bindform.BringToFront();
            //是否在任务栏显示
            ShowInTaskbar = false;
            //无边框模式
            FormBorderStyle = FormBorderStyle.None;
            //设置绘图层显示位置
			this.Location = new Point(bindform.Location.X - backborder, bindform.Location.Y - backborder);
            //设置ICO
			Icon = bindform.Icon;
			ShowIcon = bindform.ShowIcon;
            //设置大小
			Width = bindform.Width + backborder * 2;
			Height = bindform.Height + backborder * 2;
            //设置标题名
			Text = bindform.Text;
            //绘图层窗体移动
			bindform.LocationChanged += new EventHandler(Main_LocationChanged);
			bindform.SizeChanged += new EventHandler(Main_SizeChanged);
			bindform.VisibleChanged += new EventHandler(Main_VisibleChanged);
            //还原任务栏右键菜单
			CommonClass.SetTaskMenu(bindform);
            //加载背景
            SetBits();
            //窗口鼠标穿透效果
            CanPenetrate();
        }


        #region 还原任务栏右键菜单
		protected override CreateParams CreateParams
		{
			get
			{
				if (IsDisposed) return new System.Windows.Forms.CreateParams();
				CreateParams cParms = base.CreateParams;
				cParms.ExStyle |= 0x00080000; // WS_EX_LAYERED
				return cParms;
			}
		}
        public class CommonClass
        {
            [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
            static extern int GetWindowLong(HandleRef hWnd, int nIndex);
            [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
            static extern IntPtr SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong);
            public const int WS_SYSMENU = 0x00080000;
            public const int WS_MINIMIZEBOX = 0x20000;
            public static void SetTaskMenu(Form form)
            {
                int windowLong = (GetWindowLong(new HandleRef(form, form.Handle), -16));
                SetWindowLong(new HandleRef(form, form.Handle), -16, windowLong | WS_SYSMENU | WS_MINIMIZEBOX);
            }
        }
        #endregion

        #region 减少闪烁
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
        #endregion

        #region 控件层相关事件
        //移动主窗体时
        void Main_LocationChanged(object sender, EventArgs e)
        {
			Location = new Point(bindform.Left - backborder, bindform.Top - backborder);
        }

        //主窗体大小改变时
        void Main_SizeChanged(object sender, EventArgs e)
        {

			ChangeRectangle();
            SetBits();
        }

        //主窗体显示或隐藏时
        void Main_VisibleChanged(object sender, EventArgs e)
        {
            this.Visible = bindform.Visible;
        }
        #endregion

        #region 使窗口有鼠标穿透功能
        /// <summary>
        /// 使窗口有鼠标穿透功能
        /// </summary>
        private void CanPenetrate()
        {
            int intExTemp = Win32.GetWindowLong(this.Handle, Win32.GWL_EXSTYLE);
            int oldGWLEx = Win32.SetWindowLong(this.Handle, Win32.GWL_EXSTYLE, Win32.WS_EX_TRANSPARENT | Win32.WS_EX_LAYERED);
        }
        #endregion

        #region 不规则无毛边方法
        public void SetBits()
        {
            //绘制绘图层背景
            Bitmap bitmap = new Bitmap(this.Width, this.Height);
            //Rectangle _BacklightLTRB = new Rectangle(20, 20, 20, 20);//窗体光泽重绘边界
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.HighQuality; //高质量
            g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
            //ImageDrawRect.DrawRect(g, Properties.Resources.main_light_bkg_top123, ClientRectangle, , 1, 1);
			RenderHelper.DrawImageWithNineRect(g, shadowimg, ClientRectangle, new Rectangle { Size = shadowimg.Size });
            
			if (!Bitmap.IsCanonicalPixelFormat(bitmap.PixelFormat) || !Bitmap.IsAlphaPixelFormat(bitmap.PixelFormat))
                throw new ApplicationException("图片必须是32位带Alhpa通道的图片。");
            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = Win32.GetDC(IntPtr.Zero);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = Win32.CreateCompatibleDC(screenDC);

            try
            {
                Win32.Point topLoc = new Win32.Point(Left, Top);
                Win32.Size bitMapSize = new Win32.Size(Width, Height);
                Win32.BLENDFUNCTION blendFunc = new Win32.BLENDFUNCTION();
                Win32.Point srcLoc = new Win32.Point(0, 0);

                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBits = Win32.SelectObject(memDc, hBitmap);

                blendFunc.BlendOp = Win32.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = Byte.Parse("255");
                blendFunc.AlphaFormat = Win32.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;

                Win32.UpdateLayeredWindow(Handle, screenDC, ref topLoc, ref bitMapSize, memDc, ref srcLoc, 0, ref blendFunc, Win32.ULW_ALPHA);
            }
            finally
            {
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBits);
                    Win32.DeleteObject(hBitmap);
                }
                Win32.ReleaseDC(IntPtr.Zero, screenDC);
                Win32.DeleteDC(memDc);
            }
        }
        #endregion
    }
}
