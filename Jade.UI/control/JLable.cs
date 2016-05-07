/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-05-29 09:09:18
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Media;
using System.IO;

namespace Jade.UI
{
	public class JLable : Label
	{
		public JLable()
			: base()
		{
			BackColor = Color.Transparent;
			Font = new System.Drawing.Font("微软雅黑", 10F);
			//TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		}
		private string _Title;
		[Category("JadeControl")]
		[Description("标题")]
		public string Title
		{
			get { return _Title; }
			set
			{
				_Title = value;
				base.Text = _Title + _Value;
				Invalidate();
			}
		}

		private string _Value;
		[Category("JadeControl")]
		[Description("值")]
		public string Value
		{
			get { return _Value; }
			set
			{
				_Value = value;
				base.Text = _Title + _Value;
				Invalidate();
			}
		}

		[Browsable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				_Value = value;
				base.Text = _Title + value;
				Invalidate();
			}
		}
	}
}
