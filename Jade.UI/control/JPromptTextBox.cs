/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-05-16 08:42:31
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
    public delegate bool VerifyHandler(string value);

    public partial class JPromptTextBox : UserControl
    {
        public JPromptTextBox()
        {
            InitializeComponent();
            Verify += new VerifyHandler(JPromptTextBox_Verify);
            LayoutUI();
        }

        bool JPromptTextBox_Verify(string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        [Category("JadeControl")]
        [Description("验证事件")]
        public event VerifyHandler Verify;

        [Category("JadeControl")]
        [Description("文本值")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(false)]
        public override string Text
        {
            get { return txtBox.Text; }
            set { txtBox.Text = value; }
        }

        public bool _IsNull = false;
        [Category("JadeControl")]
        [Description("是否允许值为空")]
        public bool IsNull
        {
            get { return _IsNull; }
            set { _IsNull = value; }
        }

        public bool _IsPwssword = false;
        [Category("JadeControl")]
        [Description("是否密码输入框")]
        public bool IsPwssword
        {
            get { return _IsPwssword; }
            set
            {
                _IsPwssword = value;
                if (IsPwssword)
                {
                    txtBox.PasswordChar = '●';
                }
            }
        }

        [Category("JadeControl")]
        [Description("标题")]
        public string Title
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        [Category("JadeControl")]
        [Description("重置布局")]
        public bool UIReset
        {
            get { return true; }
            set
            {
                LayoutUI();
            }
        }

        [Category("JadeControl")]
        [Description("是否显示多行")]
        public bool Multiline
        {
            get { return txtBox.Multiline; }
            set
            {
                txtBox.Multiline = value;
                LayoutUI();
            }
        }

        [Category("JadeControl"), Description("空值提示文本")]
        public string EmptyText
        {
            get { return txtBox.EmptyText; }
            set
            {

                txtBox.EmptyText = value;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            LayoutUI();

        }



        private void LayoutUI()
        {
            //this.BackColor = SystemColors.ButtonFace;
            this.SuspendLayout();
            lblTitle.Location = new Point(0, 0);
            lblTitle.Size = new Size(80, Height);
            lblError.Size = new Size(15, Height);
            lblTitle.Font = new Font("微软雅黑", 10F);
            lblTitle.TextAlign = ContentAlignment.TopRight;

            txtBox.Size = new Size(Width - (lblTitle.Width + lblError.Width) - 6, Height);
            txtBox.Location = new Point(lblTitle.Width, 0);
            txtBox.Font = new Font("微软雅黑", 11.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            lblError.Location = new Point((Width - lblError.Width) - 2, 0);
            lblError.ImageAlign = ContentAlignment.MiddleCenter;

            if (Multiline)
            {
                lblTitle.TextAlign = ContentAlignment.TopRight;
                lblError.ImageAlign = ContentAlignment.TopCenter;
            }
            this.ResumeLayout(false);

        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        public bool Verification()
        {
            var result = Verify.Invoke(Text);
            if (result)
            {
                lblError.Image = Properties.Resources.tips_success;
            }
            else
            {
                lblError.Image = Properties.Resources.tips_warning;
            }
            if (string.IsNullOrEmpty(Text) && IsNull)
            {
                lblError.Image = null;
                result = true;
            }
            return result;
        }

        private void JTextBox_Load(object sender, EventArgs e)
        {
            txtBox.TextChanged += new EventHandler(txtBox_TextChanged);
        }

        void txtBox_TextChanged(object sender, EventArgs e)
        {

            Verification();
        }
    }
}
