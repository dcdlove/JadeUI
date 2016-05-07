/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-08-26 09:22:44
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
namespace Jade.Lib
{
    /// <summary>
    /// 图形相关操作类
    /// </summary>
    public class Graphical
    {
        /// <summary>
        /// 屏幕截图
        /// </summary>
        /// <author>jaly</author>
        /// <date>2015-08-26 09:24:05</date>
        /// <returns></returns>
        public Bitmap ScreenShots()
        {
            Bitmap bitmap = new Bitmap(Screen.AllScreens[0].Bounds.Size.Width, Screen.AllScreens[0].Bounds.Size.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(0, 0, 0, 0, Screen.AllScreens[0].Bounds.Size);
            g.Dispose();
            return bitmap;
        }

        /// <summary>
        /// 二进制流转图片  
        /// </summary>
        /// <author>jaly</author>
        /// <date>2015-08-26 09:25:50</date>
        /// <param name="streamByte">二进制流</param>
        /// <returns></returns>
        public Image ReturnPhoto(byte[] streamByte)
        {
            MemoryStream ms = new MemoryStream(streamByte);
            Image img = Image.FromStream(ms);
            return img;
        }

        /// <summary>
        /// 图片转二进制  
        /// </summary>
        /// <author>jaly</author>
        /// <date>2015-08-26 09:27:16</date>
        /// <param name="imagepath">图片地址</param>
        /// <returns></returns>
        public byte[] GetPictureData(string imagepath)
        {
            //根据图片文件的路径使用文件流打开，并保存为byte[]   
            FileStream fs = new FileStream(imagepath, FileMode.Open);//可以是其他重载方法   
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            return byData;
        }

        /// <summary>
        /// 图片转二进制  
        /// </summary>
        /// <author>jaly</author>
        /// <date>2015-08-26 09:27:19</date>
        /// <param name="imgPhoto">图片对象</param>
        /// <returns></returns>
        public byte[] PhotoImageInsert(Image imgPhoto)
        {
            //将Image转换成流数据，并保存为byte[]   
            MemoryStream mstream = new MemoryStream();
            imgPhoto.Save(mstream, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] byData = new Byte[mstream.Length];
            mstream.Position = 0;
            mstream.Read(byData, 0, byData.Length);
            mstream.Close();
            return byData;
        }


        /// <summary>
        /// 生成缩略图  
        /// </summary>
        /// <author>jaly</author>
        /// <date>2015-08-26 09:53:15</date>
        /// <param name="sourceFile">原始图片文件</param>
        /// <param name="quality">质量压缩比</param>
        /// <param name="multiple">收缩倍数</param>
        /// <param name="outputFile">输出文件名</param>
        /// <returns></returns>
        public  bool GetThumImage(String sourceFile, long quality, int multiple, String outputFile)  
        {  
            try  
            {  
                long imageQuality = quality;  
                Bitmap sourceImage = new Bitmap(sourceFile);  
                ImageCodecInfo myImageCodecInfo = GetEncoderInfo("image/jpeg");  
                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;  
                EncoderParameters myEncoderParameters = new EncoderParameters(1);  
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, imageQuality);  
                myEncoderParameters.Param[0] = myEncoderParameter;  
                float xWidth = sourceImage.Width;  
                float yWidth = sourceImage.Height;  
                Bitmap newImage = new Bitmap((int)(xWidth / multiple), (int)(yWidth / multiple));  
                Graphics g = Graphics.FromImage(newImage);  
  
                g.DrawImage(sourceImage, 0, 0, xWidth / multiple, yWidth / multiple);  
                g.Dispose();  
                newImage.Save(outputFile, myImageCodecInfo, myEncoderParameters);  
                return true;  
            }  
            catch  
            {  
                return false;  
            }  
        }  
  
  

        /// <summary>  
        /// 获取图片编码信息  
        /// </summary>  
        private  ImageCodecInfo GetEncoderInfo(String mimeType)  
        {  
            int j;  
            ImageCodecInfo[] encoders;  
            encoders = ImageCodecInfo.GetImageEncoders();  
            for (j = 0; j < encoders.Length; ++j)  
            {  
                if (encoders[j].MimeType == mimeType)  
                    return encoders[j];  
            }  
            return null;  
        }  
  

    }
}
