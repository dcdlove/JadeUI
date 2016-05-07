/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-05-29 09:09:08
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
namespace Jade.UI
{
    [ToolboxBitmap(typeof(TabControl))]
    public class JTabControl : TabControl
    {
        private SolidBrush onsb = new SolidBrush(Color.FromArgb(0, 150, 255));
        private SolidBrush bsb = new SolidBrush(Color.FromArgb(232, 232, 231));
        private SolidBrush tib = new SolidBrush(Color.FromArgb(255, 255, 254));
        public JTabControl()
        {
            SetStyles();
            SizeMode = TabSizeMode.FillToRight;  // 大小模式为固定  
            ItemSize = new Size(60, 30);   // 设定每个标签的尺寸
            Padding = new Point(0);
            Margin = new Padding(0);

        }

        ~JTabControl()
        {
            onsb.Dispose();
            bsb.Dispose();
            tib.Dispose();
        }




        private void SetStyles()
        {
            base.SetStyle(
            ControlStyles.UserPaint |                      // 控件将自行绘制，而不是通过操作系统来绘制  
            ControlStyles.OptimizedDoubleBuffer |          // 该控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁  
            ControlStyles.AllPaintingInWmPaint |           // 控件将忽略 WM_ERASEBKGND 窗口消息以减少闪烁  
            ControlStyles.ResizeRedraw |                   // 在调整控件大小时重绘控件  
            ControlStyles.SupportsTransparentBackColor,    // 控件接受 alpha 组件小于 255 的 BackColor 以模拟透明  
            true);                                         // 设置以上值为 true  
            base.UpdateStyles();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            var cr = ClientRectangle;
            e.Graphics.FillRectangle(tib, cr);//背景白色
            cr.Inflate(-1, -1);
            for (int i = 0; i < this.TabCount; i++)
            {
                var rec = this.GetTabRect(i);
                rec.Inflate(0, 0);
             
                e.Graphics.FillRectangle(bsb, rec.X, rec.Height - 1, rec.Width, 1);
                if (i == this.SelectedIndex)
                {
                    e.Graphics.FillRectangle(onsb, rec.X, rec.Height - 2, rec.Width, 2);
                }
               
                Rectangle bounds = this.GetTabRect(i);
                PointF textPoint = new PointF();
                SizeF textSize = TextRenderer.MeasureText(this.TabPages[i].Text, this.Font);

                // 注意要加上每个标签的左偏移量X  
                textPoint.X = bounds.X + (bounds.Width - textSize.Width) / 2;
                textPoint.Y = bounds.Y + (bounds.Height - textSize.Height) / 2;

                // 绘制高光  
                e.Graphics.DrawString(
                    this.TabPages[i].Text,
                    this.Font,
                    SystemBrushes.ControlLightLight,    // 高光颜色  
                    textPoint.X,
                    textPoint.Y);

                // 绘制正常文字  
                textPoint.Y--;
                e.Graphics.DrawString(
                    this.TabPages[i].Text,
                    this.Font,
                    SystemBrushes.ControlText,    // 正常颜色  
                    textPoint.X,
                    textPoint.Y);



            }
        }



    }
}
