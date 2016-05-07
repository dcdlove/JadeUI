/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-06-10 08:41:05
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Jade.UI
{
	public class Ball : JButton
	{
		
		
		private double g = 2;
		private double vx = 0;//g*t
		private double vy = 0;



		Random rd = new Random((int)DateTime.Now.Ticks);

	

		Timer tir = new Timer { Interval = 30 };

		public Ball() 
		{
			
			tir.Tick += (s, e) =>
			{
				this.Invoke(new Action(() =>
				{
					Left += (int)vx;
					Top += (int)vy;
					vy += g;
					
					var h = (this.Parent.Height - Height);
					if (Top >= h)
					{
						Top = h;
						vy = -(vy * 0.9);
						tir.Stop();
						this.Dispose();
					}
				}));
			};
		}
		/// <summary>
		/// 掉落
		/// </summary>
		public void Motion()
		{
			vx = rd.NextDouble() * 10;
			vy = 0;
			//System.Threading.Thread.Sleep(30);
			tir.Start();
			GC.Collect();
		}
	}
}
