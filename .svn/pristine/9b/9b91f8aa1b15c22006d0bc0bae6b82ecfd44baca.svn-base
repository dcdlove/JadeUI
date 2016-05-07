/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-06-19 10:34:11
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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Jade.UI
{
	public class JPictureBox : PictureBox
	{
		[Browsable(true)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				this.Invalidate();
			}
		}

		[Browsable(true)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
				this.Invalidate();
			}
		}

		[Browsable(true)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
				this.Invalidate();
			}
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
			pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			pe.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
			pe.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
			pe.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
			var ss = pe.Graphics.MeasureString(Text, Font);
			pe.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), (Width - ss.Width) / 2, (Height - ss.Height) / 2);
		}

	}
}
