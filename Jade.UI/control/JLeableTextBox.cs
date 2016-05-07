/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-08-08 08:35:53
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

namespace Jade.UI.control
{
    //http://www.cnblogs.com/weekzero/archive/2012/07/11/2585549.html
    public partial class JLeableTextBox : UserControl
    {
        public JLeableTextBox()
        {
            InitializeComponent();
        }

        //winform点击窗体上面任何地方都触发的事件是什么 http://q.cnblogs.com/q/50865/

        private void JLeableTextBox_Load(object sender, EventArgs ex)
        {
            txtBox.MouseCaptureChanged += (s, e) =>
            {
                var pt = txtBox.PointToClient(Control.MousePosition);
                if (txtBox.Capture == false)
                {
                    if (pt.X < 0 || pt.Y < 0 || pt.X > txtBox.Width || pt.Y > txtBox.Height)
                    {
                        txtBox.Hide();
                        lblText.Text = txtBox.Text;
                        lblText.Show();
                        Blur.Invoke(this, e);
                    }
                    else {
                        txtBox.Capture = true;
                    }
                }
            };
            lblText.Click += (s, e) =>
            {
                txtBox.Capture = true;
                lblText.Hide();
                txtBox.Show();
                txtBox.Focus();
            };
            txtBox.Hide();
            lblText.Show();
        }

		[DefaultValue("点击输入"), Category("JadeControl"),Browsable(true)]
        public override string Text
        {
            get
            {
                return txtBox.Text;
            }
            set
            {
                txtBox.Text = value;
                lblText.Text = value;
            }
        }

        public event EventHandler Blur;

    }
}
