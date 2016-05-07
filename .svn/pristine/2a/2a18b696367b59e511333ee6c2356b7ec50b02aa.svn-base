/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-09-22 10:13:29
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Jade.UI
{
	public class JlistView : ListView
	{

		public JlistView()
			: base()
		{
			View = View.LargeIcon;
			if (View == View.LargeIcon)
			{
				base.OwnerDraw = true;
			}

		}



		protected override void OnDrawItem(DrawListViewItemEventArgs e)
		{
		
			base.OnDrawItem(e);
			Rectangle bounds = e.Bounds;
			var g = e.Graphics;
			e.DrawBackground();
			g.DrawRectangle(Pens.Brown, bounds);
			Image image = e.Item.ImageIndex == -1 ? null : e.Item.ImageList.Images[e.Item.ImageIndex];
			TextFormatFlags flags = TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter;

			if (image == null)
			{

				e.DrawText(flags);
				return;

			}
			Rectangle imageRect = new Rectangle((bounds.Width - image.Width) / 2, bounds.Y, bounds.Height, bounds.Height);
			g.DrawImage(image, imageRect);


			//Rectangle textRect = new Rectangle(0, bounds.Y, 48, bounds.Height);
			//

			////TextRenderer.DrawText(e.Graphics, e.Item.Text, e.Item.Font, textRect, e.Item.ForeColor, flags);
			//if (View != View.LargeIcon)
			//{
			//    e.DrawDefault = true;
			//}

		}

		protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
		{

			base.OnDrawSubItem(e);

			if (View != View.LargeIcon) return;


			if (e.ItemIndex == -1) return;

			Rectangle bounds = e.Bounds;
			ListViewItemStates itemState = e.ItemState;
			Graphics g = e.Graphics;
			TextFormatFlags flags = TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter;



			if ((itemState & ListViewItemStates.Selected) == ListViewItemStates.Selected)
			{



			}
			else
			{
				Rectangle imageRect = new Rectangle(bounds.X + 4, bounds.Y + 2, bounds.Height - 4, bounds.Height - 4);
				Image image = e.Item.ImageIndex == -1 ? null : e.Item.ImageList.Images[e.Item.ImageIndex];

				if (image == null)
				{

					e.DrawText(flags);
					return;

				}
				g.DrawImage(image, imageRect);
				Rectangle textRect = new Rectangle(imageRect.Right + 3, bounds.Y, bounds.Width - imageRect.Right - 3, bounds.Height);

				TextRenderer.DrawText(g, e.Item.Text, e.Item.Font, textRect, e.Item.ForeColor, flags);

				return;

			}

			e.DrawText(flags);

		}


	}
}
