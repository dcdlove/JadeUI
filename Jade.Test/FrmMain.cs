/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-08-25 11:35:09
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
using Jade.Helper;
namespace Jade.Test
{
	public partial class FrmMain : JBaseForm
	{
		DataTable dataTable = new DataTable("Student");
		JImageListPopup prop;
		public FrmMain()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

			this.RenderSkinCallback = (skin) =>
			{
				skin.HeaderHeight = 100;
				skin.BackColor = Color.FromArgb(52, 73, 94);
				return skin;
			};
			jTabControl1.Hide();


			//
			dataTable.Columns.Add("Number", typeof(String));
			dataTable.Columns.Add("Name", typeof(String));
			dataTable.Columns.Add("RealName", typeof(String));
			dataTable.Columns.Add("UserName", typeof(String));
			dataTable.Columns.Add("Address", typeof(String));
			dataTable.Rows.Add(new String[] { "1", "James", "张三", "james.zhang", "长沙" });
			dataTable.Rows.Add(new String[] { "2", "Mary", "李四", "mary.xu", "山东" });
			dataTable.Rows.Add(new String[] { "3", "Jack", "王五", "jack.li", "台湾" });
			dataTable.Rows.Add(new String[] { "4", "joy", "赵六", "joy.zhou", "济南" });
			dataTable.Rows.Add(new String[] { "5", "jay", "钱七", "jay.ji", "美国" });
			dataTable.Rows.Add(new String[] { "6", "stephen", "康忠鑫", "Stephen.Kang", "深圳" });
			jComboBoxGrid1.DataSource = dataTable;
			jComboBoxGrid1.DisplayMember = "Name";
			jComboBoxGrid1.ValueMember = "Number";

			//
			jScrollbar1.BindControl(jPanel1);
			jLable2.Text = Jade.Test.Properties.Resources.Csharp;

			//
			jLeableTextBox1.Text = "单击输入";

			//
			#region listview
			ImageList imglist = new ImageList();
			List<string> keylist = new List<string>();
			imglist.ImageSize = new Size(48, 48);
			imglist.ColorDepth = ColorDepth.Depth32Bit;
			imglist.Images.Add("shopping_bag_blue", Jade.Test.Properties.Resources.shopping_bag_blue);
			keylist.Add("blue");
			imglist.Images.Add("shopping_bag_orange", Jade.Test.Properties.Resources.shopping_bag_orange);
			keylist.Add("orange");
			imglist.Images.Add("shopping_bag_purple", Jade.Test.Properties.Resources.shopping_bag_purple);
			keylist.Add("purple");

			//jlistView1.LargeImageList = imglist;
			//for (int i = 0; i < 3; i++)
			//{
			//    ListViewItem lvi = new ListViewItem();
			//    var rd = new Random().Next(0, 2);

			//    lvi.ImageIndex = rd;
			//    lvi.Text = keylist[rd];
			//    lvi.ToolTipText = keylist[rd];
			//    jlistView1.Items.Add(lvi);

			//}
			//jScrollbar2.BindControl(jlistView1);
			#endregion

			#region propimagelist

			// Creation of the First ImageList
			var imageList = new ImageList();
			imageList.ImageSize = new Size(24, 24);
			imageList.ColorDepth = ColorDepth.Depth32Bit;
			imageList.Images.AddStrip(new Bitmap(GetType(), "emoticons.bmp"));
			imageList.TransparentColor = Color.FromArgb(255, 0, 255);

			prop = new JImageListPopup();
			prop.EnableDragDrop = true;
			prop.Init(imageList, 8, 8, 5, 4);
			prop.ItemClick += new ImageListPopupEventHandler(prop_ItemClick);



			#endregion

			for (int i = 0; i < 95; i++)
			{
				//jIconListView1.AddItem(new JIconListViewItem("中兴" + i, Jade.Test.Properties.Resources.shopping_bag_blue));
				//jIconListView1.AddItem(new JIconListViewItem("华为" + i, Jade.Test.Properties.Resources.shopping_bag_orange));
				jIconListView1.AddItem(new JIconListViewItem("APP" + i, Jade.Test.Properties.Resources.shopping_bag_purple));

			}
			jIconListView1.DoubleClickItem += (s, a) =>
			{
				MsgBox.Alert(a.SelectItem.Text);
			};

		}



		private void jButton1_Click(object sender, EventArgs e)
		{
			//Point pt = PointToScreen(new Point(jButton1.Left, jButton1.Bottom));
			//prop.Show(pt.X, pt.Y);

			MsgBox.Alert("发送成功！");

		}
		void prop_ItemClick(object sender, ImageListPopupEventArgs ilpea)
		{
			if (sender.Equals(prop))
			{
				MsgBox.Alert(ilpea.SelectedItem);
			}
		}



		private void ball1_Click(object sender, EventArgs e)
		{
			ball1.Motion();
		}







		private void jButton9_Click(object sender, EventArgs e)
		{
			MsgBox.Alert("普通提示");
		}

		private void jButton12_Click(object sender, EventArgs e)
		{

			//SendKeys.Send("%{F4}");//
			//SendKeys.Send("^{ESC}");
			//SendKeys.Send("^{ESC}");
			//SendKeys.Send("^+{ESC}");
			//SendKeys.Send("^%P");


			MsgBox.Info("信息提示");
		}

		private void jButton11_Click(object sender, EventArgs e)
		{
			MsgBox.Error("报错提示");
		}

		private void jButton10_Click(object sender, EventArgs e)
		{
			MsgBox.Warn("警告提示");
		}



		private void btnInput_Click(object sender, EventArgs e)
		{
			var rest = MsgBox.Input("请输入你的姓名");
			MsgBox.Alert(rest);

		}

		private void jButton2_Click(object sender, EventArgs e)
		{

			MsgBox.Confirm("确定退出吗", "操作确认", (dr) =>
			{
				MsgBox.Alert(dr.ToString());
			});
		}

		private void jToobarItem1_Click(object sender, EventArgs e)
		{
			jTabControl1.Hide();
			jToobarItem1.Checked = true;
			jToobarItem2.Checked = false;

		}

		private void jToobarItem2_Click(object sender, EventArgs e)
		{
			jTabControl1.Show();
			jToobarItem2.Checked = true;
			jToobarItem1.Checked = false;
		}

		private void btn3_Click(object sender, EventArgs e)
		{
			BeginInvoke(new Action(() =>
			{
				for (int i = 1; i <= 100; i++)
				{
					System.Threading.Thread.Sleep(20);
					jProgressBar1.SetValue(i * 0.01);
					Application.DoEvents();

				}
			}));
		}

		private void jButton1_Click_1(object sender, EventArgs e)
		{
			new FrmMusic().ShowDialog();
		}

		private void ball1_Click_1(object sender, EventArgs e)
		{
			ball1.Motion();
		}




	}
}
