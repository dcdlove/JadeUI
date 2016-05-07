

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace Jade.Music
{
	public partial class JWave : UserControl
	{
		WaterWave ww = null;


		public JWave()
		{
			InitializeComponent();

		}





		private void waterTime_Tick(object sender, EventArgs e)
		{
			BackgroundImage = ww.GetFrame();
		}

		private void pbViewport_MouseMove(object sender, MouseEventArgs e)
		{
			if (ww != null)
			{
				ww.DropStone(e.Location);
			}
		}
		Random rd = new Random();
		Point p;

		private void trAoto_Tick(object sender, EventArgs e)
		{

			if (ww != null)
			{
				p = new Point(rd.Next(0, 130), rd.Next(0, 130));
				ww.DropStone(p);
			}
		}


		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{


				base.BackgroundImage = value;
				if (value != null)
				{
					if (!DesignMode)
					{
						ww = new WaterWave(new Bitmap(value),new Size());
						waterTime.Enabled = true;
					}
				}
			}
		}


	}

}
