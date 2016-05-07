/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-10-28 15:58:25
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace Jade.UI
{


	[Serializable]
	public class JSkin : IDisposable
	{
		public JSkin()
		{
			ForeColor = Color.Black;
			BackColor = Color.White;
			BorderColor = Color.FromArgb(0, 150, 255);
			GID = Guid.NewGuid();
		}


		


		////////////////////////////////////////////////////////////////

		/// <summary>
		/// 背景图片
		/// </summary>
		public Image BackgroundImage { get; set; }


		/// <summary>
		/// 背景颜色
		/// </summary>
		public Color BackColor { get; set; }
		 


		/// <summary>
		/// 边框颜色
		/// </summary>
		public Color BorderColor { get; set; }
		



		/// <summary>
		/// 前景色（文本）
		/// </summary>
		public Color ForeColor { get; set; }
		

		public Guid GID { get; set; }


		public void Dispose()
		{

		}
		#region 颜色进制转换

		///// <summary>
		///// 16进制转成RGB
		///// </summary>
		///// <param name="str"></param>
		///// <returns></returns>
		//public Color FromHtml(string str)
		//{
		//    return ColorTranslator.FromHtml(str);
		//}
		///// <summary>
		///// RGB转成16进制
		///// </summary>
		///// <param name="color"></param>
		///// <returns></returns>
		//public string ToHtml(Color color)
		//{
		//    return ColorTranslator.ToHtml(color);
		//}


		#endregion

	}

	/// <summary>
	/// 背景模式
	/// </summary>
	public enum BackgroundMode
	{
		/// <summary>
		/// 颜色
		/// </summary>
		Color = 0,
		/// <summary>
		/// 图片
		/// </summary>
		Image = 1,
		/// <summary>
		/// 混合模式
		/// </summary>
		Mixture = 2

	}

	[Serializable]
	public class JFormSkin : JSkin
	{

		public JFormSkin()
			: base()
		{
			TitleColor = Color.White;
			HeaderHeight = 28;
			HeaderBackColor = Color.FromArgb(52, 73, 94);
			BackgroundMode = BackgroundMode.Color;
		}

		/// <summary>
		/// 模糊半径
		/// </summary>
		public int FuzzyRadius { get; set; }

	
		/// <summary>
		/// 标题颜色
		/// </summary>
		public Color TitleColor { get; set; }
	

		/// <summary>
		/// 背景模式
		/// </summary>
		public BackgroundMode BackgroundMode { get; set; }

		
		/// <summary>
		/// 头部背景颜色
		/// </summary>
		public Color HeaderBackColor { get; set; }
		

		/// <summary>
		/// 头部背景图片
		/// </summary>
		public Image HeaderBackgroundImage { get; set; }

	
		/// <summary>
		/// 头部高度
		/// </summary>
		public int HeaderHeight { get; set; }


		/// <summary>
		/// 克隆对象
		/// </summary>
		/// <returns></returns>
		public JFormSkin Clone()
		{
			var newobj = new JFormSkin();
			newobj.BackColor = this.BackColor;
			newobj.BackgroundImage = this.BackgroundImage;
			newobj.BackgroundMode = this.BackgroundMode;
			newobj.BorderColor = this.BorderColor;
			newobj.ForeColor = this.ForeColor;
			newobj.FuzzyRadius = this.FuzzyRadius;
			newobj.GID = this.GID;
			newobj.HeaderBackColor = this.HeaderBackColor;
			newobj.HeaderBackgroundImage = this.HeaderBackgroundImage;
			newobj.HeaderHeight = this.HeaderHeight;
			newobj.TitleColor = this.TitleColor;
			return newobj;
			//var zj = SerializeObject(this);
		
			//return (JFormSkin)DeserializeObject(zj);
			
		}

		///// <summary>
		///// 加载图片
		///// </summary>
		///// <param name="path"></param>
		///// <returns></returns>
		//public Image LoadImage(string path)
		//{
		//    if (File.Exists(path))
		//    {
		//        using (FileStream fs = new FileStream(path, FileMode.Open))
		//        {
		//            return Image.FromStream(fs);
		//        }
		//    }
		//    return null;
		//}



		///// <summary>
		///// 把对象序列化并返回相应的字节
		///// </summary>
		///// <param name="pObj">需要序列化的对象</param>
		///// <returns>byte[]</returns>
		//public byte[] SerializeObject(object pObj)
		//{
		//    if (pObj == null)
		//        return null;
		//    System.IO.MemoryStream _memory = new System.IO.MemoryStream();
		//    BinaryFormatter formatter = new BinaryFormatter();
		//    formatter.Serialize(_memory, pObj);
		//    _memory.Position = 0;
		//    byte[] read = new byte[_memory.Length];
		//    _memory.Read(read, 0, read.Length);
		//    _memory.Close();
		//    return read;
		//}

		///// <summary>
		///// 把字节反序列化成相应的对象
		///// </summary>
		///// <param name="pBytes">字节流</param>
		///// <returns>object</returns>
		//public object DeserializeObject(byte[] pBytes)
		//{
		//    object _newOjb = null;
		//    if (pBytes == null)
		//        return _newOjb;
		//    System.IO.MemoryStream _memory = new System.IO.MemoryStream(pBytes);
		//    _memory.Position = 0;
		//    BinaryFormatter formatter = new BinaryFormatter();
		//    _newOjb = formatter.Deserialize(_memory);
		//    _memory.Close();
		//    return _newOjb;
		//}


	}

}
