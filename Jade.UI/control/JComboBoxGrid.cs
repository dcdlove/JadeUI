using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

namespace Jade.UI
{
    

    public partial class JComboBoxGrid : ComboBox
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        int columnPadding = 5;
        private float[] columnWidths = new float[0];  //项宽度
        private String[] columnNames = new String[0]; //项名称
        private int valueMemberColumnIndex = 0;       //valueMember属性列所在的索引

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.Text = "JComboBoxGrid";
        }
        public JComboBoxGrid()
        {
            InitializeComponent();
            InitItems();
        }

        public JComboBoxGrid(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            InitItems();
        }

        private void InitItems()
        {
            this.DrawMode = DrawMode.OwnerDrawVariable;//手动绘制所有元素
            //this.DropDownStyle = ComboBoxStyle.DropDownList;//下拉框样式设置为不能编辑 
            this.DropDownStyle = ComboBoxStyle.DropDown;//下拉框样式设置为不能编辑 
            //this.Items.Clear();//清空原有项  
        }
        //protected override void OnDataSourceChanged(EventArgs e)
        //{
        //    base.OnDataSourceChanged(e);
        //    //InitializeColumns();

        //}
        /// <summary>
        /// 初始化数据源各列的名称
        /// </summary>
        private void InitializeColumns()
        {
            PropertyDescriptorCollection propertyDescriptorCollection = DataManager.GetItemProperties();
            columnWidths = new float[propertyDescriptorCollection.Count];
            columnNames = new string[propertyDescriptorCollection.Count];

            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                string name = propertyDescriptorCollection[i].Name;
                columnNames[i] = name;
            }
        }
        protected override void OnValueMemberChanged(EventArgs e)
        {
            base.OnValueMemberChanged(e);
            InitializeValueMemberColumn();
        }
        /// <summary>
        /// 返回valuemember在显示列中的索引位置
        /// </summary>
        private void InitializeValueMemberColumn()
        {
            int colIndex = 0;
            foreach (String columnName in columnNames)
            {
                if (String.Compare(columnName, ValueMember, true, CultureInfo.CurrentUICulture) == 0)
                {
                    valueMemberColumnIndex = colIndex;
                    break;
                }
                colIndex++;
            }
        }
        /// <summary>
        /// 显示下拉框的时候出发    
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);
            this.DropDownWidth = (int)CalculateTotalWidth();//计算下拉框的总宽度  
        }

        private float CalculateTotalWidth()
        {
            columnPadding = 5;
            float totalWidth = 0;
            foreach (int width in columnWidths)
            {
                totalWidth += (width + columnPadding);
            }
            //总宽度加上垂直滚动条的宽度
            return totalWidth + SystemInformation.VerticalScrollBarWidth;
        }
        /// <summary>
        /// 获取各列的宽度和项的总宽度
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            base.OnMeasureItem(e);
            if (DesignMode)
            {
                return;
            }
            InitializeColumns();
            for (int i = 0; i < columnNames.Length; i++)
            {
                string item = Convert.ToString(FilterItemOnProperty(Items[e.Index], columnNames[i]));
                SizeF sizeF = e.Graphics.MeasureString(item, Font);//返回显示项字符串的大小
                columnWidths[i] = Math.Max(columnWidths[i], sizeF.Width);
            }
            float totalWidth = CalculateTotalWidth();//计算combobox下拉框项的宽度
            e.ItemWidth = (int)totalWidth;//设置下拉框项的宽度
        }
        /// <summary>
        /// 绘制下拉框的内容
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            if (DesignMode)
            {
                return;
            }
            Rectangle boundsRect = e.Bounds;//获取绘制项边界的矩形
            //e.DrawBackground(); 
            e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            if (e.State == DrawItemState.Selected)
            {
                //this code keeps the last item drawn from having a Bisque background. 
                e.Graphics.FillRectangle(Brushes.Bisque, e.Bounds);

            }
            //Rectangle boundsRect = e.Bounds;//获取绘制项边界的矩形
            int lastRight = 0;
            using (Pen linePen = new Pen(SystemColors.GrayText))
            {
                using (SolidBrush brush = new SolidBrush(ForeColor))
                {
                    if (columnNames.Length == 0)
                    {
                        e.Graphics.DrawString(Convert.ToString(Items[e.Index]), Font, brush, boundsRect);
                    }
                    else
                    {
                        //循环各列
                        for (int i = 0; i < columnNames.Length; i++)
                        {
                            string item = Convert.ToString(FilterItemOnProperty(Items[e.Index], columnNames[i]));
                            boundsRect.X = lastRight;//列的左边位置
                            boundsRect.Width = (int)columnWidths[i] + columnPadding;//列的宽度
                            lastRight = boundsRect.Right;

                            if (i == valueMemberColumnIndex)//如果是valuemember
                            {
                                using (Font font = new Font(Font, FontStyle.Bold))
                                {
                                    //绘制项的内容
                                    e.Graphics.DrawString(item, font, brush, boundsRect);
                                }
                            }
                            else
                            {
                                //绘制项的内容
                                e.Graphics.DrawString(item, Font, brush, boundsRect);
                            }

                            //绘制各项间的竖线
                            if (i < columnNames.Length - 1)
                            {
                                e.Graphics.DrawLine(linePen, boundsRect.Right, boundsRect.Top, boundsRect.Right, boundsRect.Bottom);
                            }
                        }
                    }
                }
            }
            e.DrawFocusRectangle();

        }

    }
}
