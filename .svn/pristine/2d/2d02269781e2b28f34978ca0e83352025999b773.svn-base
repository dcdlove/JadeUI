/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-06-09 09:10:10
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
	public partial class JProgressBar : UserControl
	{
		public JProgressBar()
		{
			InitializeComponent();
			BackColor = Color.Transparent;
			lblColor.Font = Font;
			lblColor.ForeColor = ForeColor;
			lblColor.Width = 0;
		  
		}


		private Color barColor;
		[Category("JadeControl"),
		Description("进度条的颜色")]
		public Color BarColor
		{
			get { return barColor; }
			set { barColor = value; lblColor.BackColor = value; }
		}

		/// <summary>
		/// 设置当前显示进度
		/// </summary>
		/// <param name="val"></param>
		/// <param name="text"></param>
		public void SetValue(double val, string text = "")
		{
			this.BeginInvoke(new Action(() =>
			{
				lblColor.Width = (int)(val * Width);
				lblColor.Text = text;
			}));
		}

	}
}
