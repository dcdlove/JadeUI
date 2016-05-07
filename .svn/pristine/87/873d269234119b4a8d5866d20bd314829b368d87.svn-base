/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-08-08 14:08:02
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jade.UI
{
    public partial class JApp : UserControl
    {

        public JApp()
        {
            InitializeComponent();
            SetStyles();
            Init();
        }



        private Image _AppIcon;

        public Image AppIcon
        {
            get { return _AppIcon; }
            set
            {

                _AppIcon = value;
                if (_AppIcon == null)
                {
                    _AppIcon = Jade.UI.Properties.Resources.msgbox_question;
                }

            }
        }

        private string _AppName;

        public string AppName
        {
            get { return _AppName; }
            set { _AppName = value; }
        }

        private string _AppPath;

        public string AppPath
        {
            get { return _AppPath; }
            set { _AppPath = value; }
        }
        public JApp(string name, Image icon, string path)
        {
            InitializeComponent();
            AppIcon = icon;
            AppName = name;
            AppPath = path;
            Init();
        }

        public void Init()
        {


            MouseEnter += new EventHandler(JApp_MouseEnter);
            MouseLeave += new EventHandler(JApp_MouseLeave);
            rec = new RectangleF() { Height = 26, Width = Width - 4, X = 4, Y = Height - 26 };
            DoubleClick += new EventHandler(JApp_Click);
            RenderHelper.SetFormRoundRectRgn(this, 5);
        }

        void JApp_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(AppPath);
            }
            catch (Exception ex)
            {

                MsgBox.Warn(ex.ToString());
            }

        }

        void JApp_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.Transparent;

        }


        void JApp_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(15, 0, 0, 0);



        }


        RectangleF rec;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!IsScrolling())
            {
                if (AppIcon != null)
                {
                    e.Graphics.DrawImage(AppIcon, new Rectangle(new Point(26, 10), new Size(48, 48)));
                }
                e.Graphics.DrawString(AppName, Font, new SolidBrush(ForeColor), rec, new StringFormat() { LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.LineLimit });
            }
        }

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

        public bool IsScrolling()
        {
            return false;
            var p = Parent as FlowLayoutPanel;
            if (p != null)
            {
                return p.VerticalScroll.Value.ToString() == p.Tag.ToString();
            }
            return false;
        }

    }
}
