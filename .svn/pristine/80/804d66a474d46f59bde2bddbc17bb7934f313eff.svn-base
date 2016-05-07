/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-08-08 08:37:06
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Jade.UI
{
    [DefaultEvent("ItemClick")]
    public partial class JListViewOld : UserControl
    {

		public JListViewOld()
        {
            InitializeComponent();
            SetStyle(
              ControlStyles.UserPaint |
              ControlStyles.AllPaintingInWmPaint |
              ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.ResizeRedraw |
              ControlStyles.DoubleBuffer, true);
            //强制分配样式重新应用到控件上
            UpdateStyles();
            this.BackColor = Color.FromArgb(40, 0, 0, 0);
            this.ForeColor = Color.White;
        }

        private int _ItemHeigth = 20;
        [Category("JadeControl")]
        [Description("选项高度")]
        public int ItemHeigth
        {
            get { return _ItemHeigth; }
            set
            {
                _ItemHeigth = value;
                Init();
            }
        }

        private Padding _ItemPadding = new Padding(4, 2, 0, 2);
        [Category("JadeControl")]
        [Description("选项间隔距离")]
        public Padding ItemPadding
        {
            get { return _ItemPadding; }
            set { _ItemPadding = value; Init(); }
        }
        private Color _HoverColor = Color.LawnGreen;

        [Category("JadeControl")]
        [Description("鼠标移入列表项文本显示颜色")]
        public Color HoverColor
        {
            get { return _HoverColor; }
            set { _HoverColor = value; }
        }

        private Color _ItemHoverColor = Color.FromArgb(200, ColorTranslator.FromHtml("#008cd6"));
        [Category("JadeControl")]
        [Description("鼠标移入列表项显示颜色")]
        public Color ItemHoverColor
        {
            get { return _ItemHoverColor; }
            set { _ItemHoverColor = value; }
        }

        DataTable _Data = new DataTable();
        [Category("JadeControl")]
        [Description("展示数据")]
        public DataTable Data
        {
            get { return _Data; }
            set
            {
                _Data = value;
                if (_Data == null)
                {
                    _Data = new DataTable();
                };
                FillData();
            }
        }

        int _PageIndex = 1;
        [Category("JadeControl")]
        [Description("当前展示页")]
        public int PageIndex
        {
            get { return _PageIndex; }
            private set { _PageIndex = value; }
        }

        int _PageSize = 10;
        [Category("JadeControl")]
        [Description("分页大小")]
        public int PageSize
        {
            get { return _PageSize; }
            set
            {
                _PageSize = value;

                Init();
            }
        }

        [Category("JadeControl")]
        [Description("总页数")]
        public int PageCount
        {
            get
            {
                var count = Data.Rows.Count / PageSize;
                if ((Data.Rows.Count % PageSize) > 0)
                {
                    count++;
                }
                return count;
            }
        }

        Image _ItemIcon;

        [Category("JadeControl")]
        [Description("列表项图标")]
        public Image ItemIcon
        {
            get { return _ItemIcon; }
            set { _ItemIcon = value; }
        }

        Image _MoneyIcon;

        [Category("JadeControl")]
        [Description("金币图标")]
        public Image MoneyIcon
        {
            get { return _MoneyIcon; }
            set { _MoneyIcon = value; }
        }

        private void Init()
        {
            Height = PageSize * (ItemHeigth + ItemPadding.Top);

            JPanel bar;
            Control[] arr = new Control[PageSize];
            for (int i = 0; i < PageSize; i++)
            {
                bar = new JPanel
                {
                    Name = "bar_" + i,
                    BackColor = Color.Transparent,
                    Width = Width,
                    Height = ItemHeigth,
                    Left = 0,
                    Top = (ItemHeigth + ItemPadding.Top) * i,
                    HoverColor = HoverColor,
                    Tag = null,
                };
                bar.Paint += new PaintEventHandler(bar_Paint);
                bar.Click += (s, e) =>
                {
                    if (ItemClick != null)
                    {
                        ItemClick(s, e);
                    }
                };
                arr[i] = bar;
            }
            Controls.Clear();
            Controls.AddRange(arr);

        }

        string DrawText = string.Empty;
        void bar_Paint(object sender, PaintEventArgs e)
        {
            var obj = sender as JPanel;
            if (obj.Tag == null) { return; }
            DataRow dr = obj.Tag as DataRow;

            if (ItemIcon != null && dr[0].ToString().ToLower() == "true")
            {
                e.Graphics.DrawImage(ItemIcon, 0 + ItemPadding.Left, (obj.Height - obj.Font.Height) / 2, ItemIcon.Width, ItemIcon.Height);
            }

            DrawText = dr[1].ToString();
            e.Graphics.DrawString(DrawText, Font, new SolidBrush(ForeColor), 30 + ItemPadding.Left, (obj.Height - obj.Font.Height) / 2);
            if (MoneyIcon != null)
            {
                e.Graphics.DrawImage(MoneyIcon, 130 + ItemPadding.Left, (obj.Height - obj.Font.Height) / 2, MoneyIcon.Width, MoneyIcon.Height);
            }
            DrawText = dr[2].ToString();
            e.Graphics.DrawString(DrawText, Font, new SolidBrush(ForeColor), 150 + ItemPadding.Left, (obj.Height - obj.Font.Height) / 2);
        }

        private void FillData()
        {
            for (int i = 0; i < PageSize; i++)
            {
                JPanel bar = Controls.Find("bar_" + i, false)[0] as JPanel;
                var ri = (PageSize * (PageIndex - 1)) + i;
                if (ri < Data.Rows.Count && ri >= 0)
                {
                    bar.Tag = Data.Rows[ri];
                }
                Invoke(new MethodInvoker(delegate()
                {
                    bar.Refresh();
                }));
            }
        }

        public void NextPage()
        {
            if ((PageIndex + 1) > PageCount)
            {
                return;
            }
            PageIndex++;
            FillData();

        }

        public void LastPage()
        {
            if ((PageIndex - 1) < 1)
            {
                return;
            }
            PageIndex--;
            FillData();
        }

        /// <summary>
        /// 剪切字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="font"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private string CutStringByWidth(string str, Font font, int width)
        {
            string temp = string.Empty;
            Graphics g = CreateGraphics();
            if (g.MeasureString(str, font).Width > width)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (g.MeasureString(temp + str[i], font).Width < width)
                    {
                        temp += str[i];
                    }
                }
                temp = temp.Remove(temp.Length - 4);
            }
            else
            {
                temp = str;
            }
            return temp;
        }

        [Category("JadeControl")]
        [Description("列表项单击事件")]
        public event EventHandler ItemClick;

    }
}
