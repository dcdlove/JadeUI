/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-10-29 08:26:21
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Jade.UI;
using Jade.Music;
namespace Jade.Test
{
	//http://www.cnblogs.com/feiyangqingyun/archive/2010/12/08/1900680.html 导出数据

	//http://sucai.flashline.cn/flash3/html5/trendsetter/ 个人主页
	public partial class FrmMusic : JBaseForm
	{
		public FrmMusic()
		{
			InitializeComponent();
		}
		WaterWave ww = null;
		private void FrmMusic_Load(object sender, EventArgs e)
		{
			jPictureBox1.MouseMove += (s, err) =>
			{
				if (ww != null)
				{
					ww.DropStone(err.Location);
				}
			};
			ww = new WaterWave(new Bitmap(Jade.Test.Properties.Resources._7610ad096b63f6245eb47a088144ebf81b4ca3db), jPictureBox1.Size);
			var tri = new Timer { Interval = 15 };
			tri.Tick += (s, err) =>
			{
				jPictureBox1.Image = ww.GetFrame();
			};
			tri.Start();
		}
	}
}
