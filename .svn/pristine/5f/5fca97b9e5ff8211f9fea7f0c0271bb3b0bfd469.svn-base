/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-08-08 08:35:39
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Jade.UI
{
    public class JListBox : ListBox
    {
        public JListBox()
        {
            this.BorderStyle = BorderStyle.None;
            this.BackColor = Color.BlueViolet;
            this.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = Color.White;
            this.ItemHeight = 20;
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);


        }

       

        public void Log(object msg)
        {
            this.Items.Insert(0, string.Format("{0:yyyy-MM-dd HH:mm:ss}  {1}", DateTime.Now, msg));
        }
    }
}
