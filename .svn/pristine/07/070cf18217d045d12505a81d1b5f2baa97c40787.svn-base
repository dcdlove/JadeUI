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
    public class JDataGridView : DataGridView
    {
        public JDataGridView() 
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            //如果是Column
            if (e.RowIndex == -1)
            {
                drawColumnAndRow(e);
                e.Handled = true;
                //如果是Rowheader
            }
            else if (e.ColumnIndex < 0 && e.RowIndex >= 0)
            {
                drawColumnAndRow(e);
                e.Handled = true;
            }


        }

        /// <summary>
        /// Column和RowHeader绘制
        /// </summary>
        /// <param name="e"></param>
        void drawColumnAndRow(DataGridViewCellPaintingEventArgs e)
        {
            // 绘制背景色
            using (LinearGradientBrush backbrush = new LinearGradientBrush(e.CellBounds, ProfessionalColors.MenuItemPressedGradientBegin,ProfessionalColors.MenuItemPressedGradientMiddle, LinearGradientMode.Vertical))
            {

                Rectangle border = e.CellBounds;
                border.Width -= 1;
                //填充绘制效果
                e.Graphics.FillRectangle(backbrush, border);
                //绘制Column、Row的Text信息
                e.PaintContent(e.CellBounds);
                //绘制边框
                //ControlPaint.DrawBorder3D(e.Graphics, e.CellBounds, Border3DStyle.Etched);
                //e.PaintBackground(e.CellBounds, true);

            }
        }
    }
}
