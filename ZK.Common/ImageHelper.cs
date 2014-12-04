using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ZK.Common
{
    public class ImageHelper
    {
        public ImageHelper()
        {

        }
        /// <summary>
        /// 判断制定路径下是否存在新文件
        /// </summary>
        /// <param name="fullPath">源文件的完整路径</param>
        /// <returns>新文件的完整路径</returns>
        private static string ExistsOrCreateFile(string fullPath, string targetName)
        {
            string newPath = ZK.Common.CommonFunction.GetPathWithoutExtension(fullPath);

            return newPath + targetName;
        }
        /// <summary>
        /// 生成缩略图片
        /// </summary>
        /// <param name="filePath">源图片的完整路径</param>
        /// <param name="_width">缩略图的宽</param>
        /// <param name="_height">高</param>
        /// <returns></returns>
        public static string Image(string filePath, string targetName, int _width, int _height)
        {
            try
            {

                using (System.Drawing.Image sourceImage = System.Drawing.Image.FromFile(filePath))
                {
                    //原图宽度和高度    
                    int width = sourceImage.Width;
                    int height = sourceImage.Height;
                    int smallWidth;
                    int smallHeight;
                    //获取第一张绘制图的大小,(比较 原图的宽/缩略图的宽  和 原图的高/缩略图的高)    
                    if (((decimal)width) / height <= ((decimal)_width) / _height)
                    {
                        smallWidth = _width;
                        smallHeight = _width * height / width;
                    }
                    else
                    {
                        smallWidth = _height * width / height;
                        smallHeight = _height;
                    }
                    //判断缩略图在当前文件夹下是否同名称文件存在                  
                    //缩略图保存的绝对路径    
                    string smallImagePath = ExistsOrCreateFile(filePath, targetName);
                    //新建一个图板,以最小等比例压缩大小绘制原图    
                    using (System.Drawing.Image bitmap = new System.Drawing.Bitmap(smallWidth, smallHeight))
                    {

                        using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap))
                        {

                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            g.Clear(Color.Black);
                            g.DrawImage(
                            sourceImage,
                            new System.Drawing.Rectangle(0, 0, smallWidth, smallHeight),
                            new System.Drawing.Rectangle(0, 0, width, height),
                            System.Drawing.GraphicsUnit.Pixel
                            );
                            g.Dispose();
                            bitmap.Save(smallImagePath, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                    return System.IO.Path.GetFileName(smallImagePath);
                }
            }
            catch
            {
                return "图片格式不正确";
            }
        }

    }
}
