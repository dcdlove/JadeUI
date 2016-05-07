/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-10-23 23:54:18
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Jade.UI
{
	public partial class SkinSet : UserControl
	{
		private JBaseForm baseform;

		public SkinSet()
		{
			InitializeComponent();
			baseform = new JBaseForm();

		}

		public SkinSet(JBaseForm from)
		{

			InitializeComponent();
			baseform = from;

			btnFR.Value = JSkinManage.JFormSkin.FuzzyRadius;
			jLable1.Text = btnFR.Value.ToString();
		}

		private void SkinSet_Load(object sender, EventArgs e)
		{
			btnColor01.Click += btnColor_Click;
			btnColor02.Click += btnColor_Click;
			btnColor03.Click += btnColor_Click;
			btnColor04.Click += btnColor_Click;
			btnColor05.Click += btnColor_Click;
			btnColor06.Click += btnColor_Click;
			btnColor07.Click += btnColor_Click;
			btnColor08.Click += btnColor_Click;
			/**/

			pic01.Click += pic_Click;
			pic02.Click += pic_Click;
			pic03.Click += pic_Click;
			pic04.Click += pic_Click;
			pic05.Click += pic_Click;
			pic06.Click += pic_Click;
			pic07.Click += pic_Click;
			pic08.Click += pic_Click;
			RenderHelper.SetFormRoundRectRgn(btnClose, 20);
		}

		private void btnColor_Click(object sender, EventArgs e)
		{
			JSkinManage.JFormSkin.BackgroundMode = BackgroundMode.Color;
			JSkinManage.JFormSkin.HeaderBackColor = ((JButton)sender).NormaColor;
			baseform.RenderSkin();
			//Close();
		}

		private void pic_Click(object sender, EventArgs e)
		{
			JSkinManage.JFormSkin.BackgroundMode = BackgroundMode.Image;


			var tempimg = ((JPictureBox)sender).Image;
			JSkinManage.JFormSkin.HeaderBackgroundImage = JSkinManage.GetBackgroundEffectImage(tempimg, JSkinManage.JFormSkin.FuzzyRadius);
			baseform.RenderSkin();
			//Close();
		}

		private void Close()
		{
			var frm = Parent as Popup;
			frm.Close();
		}


		private void btnFR_ScrollEnd(object sender, ScrollEventArgs e)
		{
			JSkinManage.JFormSkin.FuzzyRadius = e.Value;
			baseform.RenderSkin();
		}

		private void btnFR_Scroll(object sender, ScrollEventArgs e)
		{
			jLable1.Text = e.Value.ToString();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}












	}
}
