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
    public class JTest : ComboBox
    {

        public JTest()
        {
            //指定绘制模式，这项必须指定为,OwnerDrawFixed,OwnerDrawVariable
            //Normal 由操作系统绘制，并且元素大小都相等。 
            //OwnerDrawFixed 手动绘制的，并且元素大小都相等。 
            //OwnerDrawVariable 手动绘制，元素大小可能不相等。 
            DrawMode = DrawMode.OwnerDrawFixed;
            DisplayMember = "Text";
            ValueMember = "Value";
        }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            //获取要在其上绘制项的图形表面
            Graphics g = e.Graphics;
            //获取表示所绘制项的边界的矩形
            System.Drawing.Rectangle rect = e.Bounds;
            //定义要绘制到控件中的图标图像
            Image ico = Jade.UI.Properties.Resources.tips_success;
            //定义字体对象
            System.Drawing.Font font = new System.Drawing.Font(new FontFamily("宋体"), 12);
            if (e.Index >= 0)
            {
                //获得当前Item的文本
                string tempString = Items[e.Index].ToString();
                //如果当前项是没有状态的普通项
                if (e.State == DrawItemState.None)
                {
                    //在当前项图形表面上划一个矩形
                    g.FillRectangle(new SolidBrush(Color.FromArgb(200, 230, 255)), rect);
                    //在当前项图形表面上划上图标
                    g.DrawImage(ico, new Point(rect.Left, rect.Top));
                    //在当前项图形表面上划上当前Item的文本
                    g.DrawString(tempString, font, new SolidBrush(Color.Black), rect.Left + ico.Size.Width, rect.Top);
                    //将绘制聚焦框
                    e.DrawFocusRectangle();
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.LightBlue), rect);
                    g.DrawImage(ico, new Point(rect.Left, rect.Top));
                    g.DrawString(tempString, font, new SolidBrush(Color.Black), rect.Left + ico.Size.Width, rect.Top);
                    e.DrawFocusRectangle();
                }
            }
        }
    }
}
