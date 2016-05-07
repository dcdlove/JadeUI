/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-10-12 19:11:45
 * @version 1.0.0 
 */

using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Jade.Music
{
    public sealed unsafe class WaterWave
    {
        private Bitmap _orgImage = null;
        private Bitmap _newImage = null;
        private BitmapData _orgData = null;
        private byte* _pOrgBase;
        private int _width;
        private int[,] _buf1, _buf2;
        private int _rippleCount = int.MaxValue;
        public WaterWave(Bitmap bitmap,Size size)
        {
            _orgImage = (Bitmap)bitmap.Clone();
            _newImage = (Bitmap)bitmap.Clone();
			Width = size.Width;
			Height = size.Height;
            _orgData = _orgImage.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            _pOrgBase = (byte*)_orgData.Scan0.ToPointer();
            _width = Width * 3;
            if (_width % 4 != 0)
                _width = 4 * (_width / 4 + 1);
            _buf1 = new int[Width, Height];
            _buf2 = new int[Width, Height];
        }
        /// <summary>  
        /// 图片宽度  
        /// </summary>  
        public int Width { get; private set; }
        /// <summary>  
        /// 图片高度  
        /// </summary>  
        public int Height { get; private set; }
        /// <summary>  
        /// 扩散  
        /// </summary>  
        private void RippleSpread()
        {
            for (int i = 1; i < Width - 1; i++)
                for (int j = 1; j < Height - 1; j++)
                {
                    _buf2[i, j] = ((_buf1[i - 1, j] + _buf1[i + 1, j] + _buf1[i, j - 1] + _buf1[i, j + 1]) >> 1) - _buf2[i, j];
                    _buf2[i, j] -= _buf2[i, j] >> 5;
                }
            int[,] t = _buf1;
            _buf1 = _buf2;
            _buf2 = t;
        }
        /// <summary>  
        /// 重绘  
        /// </summary>  
        private void RenderRipple()
        {
            _newImage = new Bitmap(Width, Height);
            BitmapData newData = _newImage.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            byte* pBase = (byte*)newData.Scan0.ToPointer();
            int dx, dy;
            for (int i = 1; i < Width - 1; i++)
                for (int j = 1; j < Height - 1; j++)
                {
                    dx = _buf1[i, j - 1] - _buf1[i, j + 1];
                    dy = _buf1[i - 1, j] - _buf1[i + 1, j];
                    if (i + dx < 0 || i + dx >= Width || j + dy < 0 || j + dy >= Height)
                        continue;
                    //fmap.SetColor(i, j, FastOriginal.GetColor(i + dx, j + dy));  
                    *(pBase + j * _width + i * 3) = *(_pOrgBase + (j + dy) * _width + (i + dx) * 3);
                    *(pBase + j * _width + i * 3 + 1) = *(_pOrgBase + (j + dy) * _width + (i + dx) * 3 + 1);
                    *(pBase + j * _width + i * 3 + 2) = *(_pOrgBase + (j + dy) * _width + (i + dx) * 3 + 2);
                }
            _newImage.UnlockBits(newData);
        }
        /// <summary>  
        /// 增加波源  
        /// </summary>  
        /// <param name="point"></param>  
        /// <param name="deep"></param>  
        public void DropStone(Point point)
        {
			_buf1[point.X, point.Y] -= 100;
			_rippleCount = 90;
          
        }
        /// <summary>  
        /// 获取当前一帧图像  
        /// </summary>  
        /// <returns></returns>  
        public Bitmap GetFrame()
        {
            if (_rippleCount > 0)
            {
                RippleSpread();
                RenderRipple();
                _rippleCount--;
            }
            return _newImage;
        }
        #region IDisposable Members
        public void Dispose()
        {
            if (_orgData != null)
                _orgImage.UnlockBits(_orgData);
            _orgData = null;
        }
        #endregion 
    }
}
