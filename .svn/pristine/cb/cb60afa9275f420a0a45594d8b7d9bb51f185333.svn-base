/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-10-29 18:21:45
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
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using Jade.UI.Properties;
using System.IO;
using System.Runtime.Serialization;

namespace Jade.UI
{
	public partial class JBaseForm : Jade.UI.Shadow.FormMain
	{
		//BackForm backform;

		public JBaseForm()
		{
			InitializeComponent();
			SetStyles();

			if (!DesignMode)
			{
				//backform = new BackForm(this);
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			BackColor = Color.White;
			RenderSkin();
			BindEvent();
			ChangeSysControl();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			Jade.UI.RenderHelper.MoveWindow(this);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			ChangeSysControl();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var g = e.Graphics;
			var x = 4;

			using (SolidBrush sb = new SolidBrush(_Skin.HeaderBackColor))
			{
				if (_Skin.HeaderBackgroundImage != null && _Skin.BackgroundMode == BackgroundMode.Image)
					PaintImage(g, _Skin.HeaderBackgroundImage, 0, 0, Width, _Skin.HeaderHeight);
				else
					g.FillRectangle(sb, new Rectangle(0, 0, Width, _Skin.HeaderHeight));

				if (ShowIcon)
				{
					g.DrawIcon(Icon, new Rectangle(x, 4, 16, 16));
					x += 20;
				}
				//g.DrawString(Text, new Font("微软雅黑", 10.5F), SystemBrushes.ControlLightLight, x, 2);
				g.DrawString(Text, new Font("微软雅黑", 10.5F), new SolidBrush(_Skin.TitleColor), x, 2);
				//g.DrawLine(new Pen(ColorTranslator.FromHtml("#fff")), 0, _Skin.HeaderHeight, Width, _Skin.HeaderHeight);
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			//if (backform != null)
			//{
			//    backform.Close();
			//}
		}


		/// <summary>
		/// 绘制图片
		/// </summary>
		/// <param name="g"></param>
		/// <param name="img"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		protected void PaintImage(Graphics g, Image img, int x, int y, int width, int height)
		{
			TextureBrush tBrush = new TextureBrush(img);
			g.FillRectangle(tBrush, new Rectangle(x, y, width, height));
		}

		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				Invalidate();
			}
		}

		/// <summary>
		/// 皮肤渲染回调函数（适用自定义窗体界面的时候用）
		/// </summary>
		public Func<JFormSkin, JFormSkin> RenderSkinCallback { get; set; }
		private JFormSkin _Skin;

		/// <summary>
		/// 渲染主题
		/// </summary>
		public void RenderSkin()
		{
			_Skin = JSkinManage.JFormSkin.Clone();
			if (RenderSkinCallback != null)
			{
				RenderSkinCallback(_Skin);
			}

			Invalidate();
		}

		/// <summary>
		/// 开启双缓冲，减少闪烁
		/// </summary>
		protected void SetStyles()
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
		/// 重置系统按钮位置
		/// </summary>
		protected void ChangeSysControl()
		{
			if (btnSysSkin == null) return;
			btnSysSkin.Top = 0;
			btnSysMin.Top = 0;
			btnSysMax.Top = 0;
			btnSysMenu.Top = 0;
			btnSysSkin.Top = 0;

			var x = Width;
			if (btnSysClose.Visible)
			{
				x -= btnSysClose.Width;
				btnSysClose.Left = x;
			}

			if (btnSysMax.Visible)
			{
				x -= btnSysMax.Width;
				btnSysMax.Left = x;
			}
			if (btnSysMin.Visible)
			{
				x -= btnSysMin.Width;
				btnSysMin.Left = x;
			}

			if (btnSysMenu.Visible)
			{
				x -= btnSysMenu.Width;
				btnSysMenu.Left = x;
			}

			if (btnSysSkin.Visible)
			{
				x -= btnSysSkin.Width;
				btnSysSkin.Left = x;
			}
		}

		protected void BindEvent()
		{

			btnSysClose.Click += (s, e) =>
			{
				Close();
			};
			btnSysMax.Click += (s, e) =>
			{
				this.ShowInTaskbar = true;
				if (this.WindowState == FormWindowState.Maximized)
				{
					this.WindowState = FormWindowState.Normal;
				}
				else
				{
					this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
					this.WindowState = FormWindowState.Maximized;
				}
			};
			btnSysMin.Click += (s, e) =>
			{
				WindowState = FormWindowState.Minimized;
				ShowInTaskbar = true;
			};
			btnSysMenu.Click += (s, e) =>
			{

			};
			btnSysSkin.Click += (s, e) =>
			{
				var frmskin = new SkinSet(this);
				var loc = Location;
				loc.X += btnSysSkin.Left;
				loc.Y += btnSysSkin.Top + btnSysSkin.Height;
				//loc.Y += (Height - frmskin.Height) / 2;
				//loc.X += (Width-frmskin.Width)/2;
				new Popup(frmskin).Show(loc);
			};
		}



	}
}
