using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Infrastructure.Image
{
    /// <summary>
    /// 生成ImageLogo
    /// </summary>
    public class ImageLogo
    {
        #region  字段
        /// <summary>
        /// 图片宽度
        /// </summary>
        private int Width{get;set;}
        /// <summary>
        /// 图片高度
        /// </summary>
        private int Height { get; set; }
        /// <summary>
        /// 字体格式
        /// </summary>
        private string FontFamily { get; set; }
        /// <summary>
        /// 字体大小
        /// </summary>
        private int FontSize { get; set; }
        /// <summary>
        /// 若文字太大，是否根据背景图来调整文字大小，默认为适应
        /// </summary>
        private bool Adaptable { get; set; }
        /// <summary>
        /// 字体样式
        /// </summary>
        private FontStyle FontStyle { get; set; }
        /// <summary>
        /// 水印文字是否使用阴影
        /// </summary>
        private bool Shadow { get; set; }
        /// <summary>
        /// 背景图片
        /// </summary>
        private string BackgroundImage { get; set; }
        /// <summary>
        /// 背景颜色
        /// </summary>
        private Color BgColor { get; set; }
        /// <summary>
        /// 文字左边距
        /// </summary>
        private int Left { get; set; }
        /// <summary>
        /// 保存的物理路径
        /// </summary>
        private string SavePhysicalPath { get; set; }
        /// <summary>
        /// 图片显示的文字
        /// </summary>
        private string Text { get; set; }
        /// <summary>
        /// 文字上边距
        /// </summary>
        private int Top { get; set; }
        /// <summary>
        /// 透明度
        /// </summary>
        private int Alpha { get; set; }
        private int Red { get; set; }
        private int Green { get; set; }
        private int Blue { get; set; }
        /// <summary>
        /// 输出图片质量，质量范围0-100,类型为long
        /// </summary>
        private long Quality { get; set; }
        private Color FontColor { get; set; }

        #endregion


        

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="savePhysicalPath">物理路径</param>
        public ImageLogo(string text,string savePhysicalPath)
        {
            this.Shadow = false;
            this.Alpha = 255;
            this.Quality = 100;
            this.FontSize = 48;//字号默认为48
            this.Adaptable = true;
            this.FontColor = Color.White;//字体默认为白色
            this.Text = text;
            this.SavePhysicalPath = savePhysicalPath;
            this.Width = 100;
            this.Height = 100;
            this.BgColor = Color.FromArgb(255, 176, 224, 230);//默认使用蓝色背景
        }
        /// <summary>
        /// 创建图片logo
        /// </summary>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <returns></returns>
        public bool Create(int width,int height)
        {
            this.Width = width;
            this.Height = height;
            return this.Create();
        }
        /// <summary>
        /// 创建图片logo
        /// </summary>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns></returns>
        public bool Create(int width, int height, int fontSize)
        {
            this.Width = width;
            this.Height = height;
            this.FontSize = fontSize;
            return this.Create();
        }
        /// <summary>
        /// 创建图片logo
        /// </summary>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontcolor">字体颜色</param>
        /// <returns></returns>
        public bool Create(int width, int height, int fontSize,Color fontcolor)
        {
            this.Width = width;
            this.Height = height;
            this.FontSize = fontSize;
            this.FontColor = fontcolor;
            return this.Create();
        }
        /// <summary>
        /// 创建图片logo
        /// </summary>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontcolor">字体颜色</param>
        /// <param name="alpha">透明度</param>
        /// <param name="red">基本颜色red</param>
        /// <param name="freen">基本颜色freen</param>
        /// <param name="blue">基本颜色blue</param>
        /// <returns></returns>
        public bool Create(int width, int height, int fontSize, Color fontcolor, int alpha, int red,int freen,int blue)
        {
            this.Width = width;
            this.Height = height;
            this.FontSize = fontSize;
            this.FontColor = fontcolor;
            this.Alpha = alpha;
            this.Red = red;
            this.Green = freen;
            this.Blue = blue;
            return this.Create();
        }
        /// <summary>
        /// 创建图片logo
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool Create()
        {
            
            Bitmap bitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format64bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            try
            {
                g.Clear(this.BgColor);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;

                Font f = new Font(FontFamily, FontSize, FontStyle);
                SizeF size = g.MeasureString(Text, f);
                // 调整文字大小直到能适应图片尺寸
                while (Adaptable == true && size.Width > bitmap.Width)
                {
                    FontSize--;
                    f = new Font(FontFamily, FontSize, FontStyle);
                    size = g.MeasureString(Text, f);
                }

                Brush b = new SolidBrush(FontColor);
                StringFormat StrFormat = new StringFormat();
                StrFormat.Alignment = StringAlignment.Near;
                if (this.Shadow)
                {
                    //不会执行
                    Brush b2 = new SolidBrush(Color.FromArgb(90, 0, 0, 0));
                    g.DrawString(Text, f, b2, Left + 2, Top + 1);
                }
                else
                {
                    //自动计算边距
                    Left = (int)(Width - size.Width) / 2;
                    Top = (int)(Height - size.Height) / 2;
                    g.DrawString(Text, f, b, new PointF(Left, Top), StrFormat);
                }

                bitmap.Save(this.SavePhysicalPath, ImageFormat.Jpeg);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                bitmap.Dispose();
                g.Dispose();
            }
        }
    }
}
