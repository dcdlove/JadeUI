/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-05-15 17:05:39
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
using System.Drawing.Imaging;


namespace Jade.UI
{

	public class JPanel : Panel
	{
		public JPanel()
		{
			BackColor = Color.Transparent;
			BackgroundImageLayout = ImageLayout.None;
			SetStyle(
			  ControlStyles.UserPaint |
			  ControlStyles.AllPaintingInWmPaint |
			  ControlStyles.OptimizedDoubleBuffer |
			  ControlStyles.ResizeRedraw |
			  ControlStyles.DoubleBuffer, true);
			//强制分配样式重新应用到控件上
			UpdateStyles();
		}

		private bool IsFocus = false;


		private Color _HoverColor = Color.Gold;
		[Category("JadeControl")]
		[Description("高亮颜色")]
		public Color HoverColor
		{
			get { return _HoverColor; }
			set { _HoverColor = value; }
		}

		private Image _RegionImage;

		[Category("JadeControl")]
		[Description("勾画工作区域图片")]
		public Image RegionImage
		{
			get { return _RegionImage; }
			set
			{
				_RegionImage = value;
				if (_RegionImage != null)
				{
                    this.Width = _RegionImage.Width;
                    this.Height = _RegionImage.Height;
					Region = GetRegion(new Bitmap(_RegionImage));
					this.BackgroundImage = _RegionImage;
                    if (Parent!=null)
                        Parent.Invalidate();
					
				}
			}
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			IsFocus = true;
			base.OnMouseEnter(e);
			this.Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			IsFocus = false;
			base.OnMouseLeave(e);
			this.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (IsFocus)
			{
				var p = new Pen(new SolidBrush(Color.Red));
				e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, HoverColor)), ClientRectangle);
			}
		}

		/// <summary>
		/// 设置控件工作区域
		/// </summary>
		/// <param name="pts"></param>
		public virtual void SetRegion(Point[] pts)
		{
			var gp = new GraphicsPath();
			gp.AddPolygon(pts);
			Region = new Region(gp);
			Parent.Invalidate();
		}

		/// <summary>
		/// 根据图片得到一个图片非透明部分的区域
		/// </summary>
		/// <param name="bckImage"></param>
		/// <returns></returns>
		private unsafe Region GetRegion(Bitmap bckImage)
		{
			GraphicsPath path = new GraphicsPath();
			int w = bckImage.Width;
			int h = bckImage.Height;
			BitmapData bckdata = null;
			try
			{
				bckdata = bckImage.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
				uint* bckInt = (uint*)bckdata.Scan0;
				for (int j = 0; j < h; j++)
				{
					for (int i = 0; i < w; i++)
					{
						if ((*bckInt & 0xff000000) != 0)
						{
							path.AddRectangle(new Rectangle(i, j, 1, 1));
						}
						bckInt++;
					}
				}
				bckImage.UnlockBits(bckdata); bckdata = null;
			}
			catch
			{
				if (bckdata != null)
				{
					bckImage.UnlockBits(bckdata);
					bckdata = null;
				}
			}
			Region region = new System.Drawing.Region(path);
			path.Dispose(); path = null;
			return region;
		}

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (RegionImage != null)
            {
                Region = GetRegion(new Bitmap(RegionImage));
                if (Parent != null)
                    Parent.Invalidate();
            }
        }

	}
}
