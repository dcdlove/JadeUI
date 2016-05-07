/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-06-03 15:30:31
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace Jade.UI
{
	public partial class JMsgBox : JBaseForm
	{
		public string InputStr = string.Empty;

		public JMsgBox()
		{
			InitializeComponent();

		}

		public JMsgBox(string content, string tittle, Image icon, bool isConfirm = false)
			: base()
		{
			InitializeComponent();
			Text = tittle;
			lblContent.Text = content;
			picIco.Image = icon;
			btnCancel.Visible = isConfirm;
			RenderSkinCallback = (skin) =>
			{
				
				return new JFormSkin();
			};
			
		}


		public JMsgBox(string tittle)
			: base()
		{
			InitializeComponent();
			Text = tittle;
			lblContent.Visible = false; ;
			picIco.Visible = false;
			btnCancel.Visible = false;
			txtInput.Visible = true;
			RenderSkinCallback = (skin) =>
			{
				return new JFormSkin();
			};
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.DrawLine(new Pen(Color.FromArgb(18, 0, 0, 0)), 0, Height - 40, Width, Height - 40);
			//e.Graphics.FillRegion(new SolidBrush(Color.FromArgb(5, 0, 0, 255)), new Region(new Rectangle(0, Height - 34, Width, Height - 34)));
		}



		private void btnDete_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			InputStr = txtInput.Text;
			if (string.IsNullOrEmpty(InputStr))
			{
				this.Close();
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.None;
			this.Close();
		}

		private void btn_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void MsgBox_Load(object sender, EventArgs e)
		{
			lblContent.MouseDown += (s, ex) =>
			{
				RenderHelper.MoveWindow(this);
			};

			picIco.MouseDown += (s, ex) =>
			{
				RenderHelper.MoveWindow(this);
			};


		}
	}

	public class MsgBox
	{
		public static DialogResult Alert(object msg, string tittle = "SUCCESS")
		{
			if (msg == null) msg = string.Empty;
			return new JMsgBox(msg.ToString(), tittle, Properties.Resources.msgbox_success).ShowDialog();
		}

		public static DialogResult Info(string msg, string tittle = "INFORMATION")
		{
			return new JMsgBox(msg, tittle, Properties.Resources.msgbox_info).ShowDialog();
		}

		public static DialogResult Error(string msg, string tittle = "ERROR")
		{
			return new JMsgBox(msg, tittle, Properties.Resources.msgbox_error).ShowDialog();
		}

		public static DialogResult Warn(string msg, string tittle = "WARNING")
		{
			return new JMsgBox(msg, tittle, Properties.Resources.msgbox_warning).ShowDialog();
		}

		public static void Confirm(string msg, string tittle = "请选择", Action<DialogResult> func = null)
		{
			var dr = new JMsgBox(msg, tittle, Properties.Resources.msgbox_question, true).ShowDialog();
			if (func != null)
			{
				func(dr);
			}
		}

		public static string Input(string title = "请输入内容")
		{
			var str = "";
			var mb = new JMsgBox(title);
			mb.ShowDialog();
			str = mb.InputStr;
			mb.Close();
			return str;
		}
	}
}
