using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;
using System.IO;


namespace EnrolmentPlatform.Project.Infrastructure.RQCode
{
    public class RQCodeHelper
    {
        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="QRString">二维码字符串</param>
        /// <param name="QRCodeEncodeMode">二维码编码(Byte、AlphaNumeric、Numeric)</param>
        /// <param name="QRCodeScale">二维码尺寸(Version为0时，1：26x26，每加1宽和高各加25</param>
        /// <param name="QRCodeVersion">二维码密集度0-40</param>
        /// <param name="QRCodeErrorCorrect">二维码纠错能力(L：7% M：15% Q：25% H：30%)</param>
        /// <param name="filePath">保存路径</param>
        /// <param name="hasLogo">是否有logo(logo尺寸50x50，QRCodeScale>=5，QRCodeErrorCorrect为H级)</param>
        /// <param name="logoFilePath">logo路径</param>
        /// <returns></returns>
        public static bool CreateQRCode(string QRString, string QRCodeEncodeMode, short QRCodeScale, int QRCodeVersion, string QRCodeErrorCorrect, string filePath, bool hasLogo, string logoFilePath)
        {
            bool result = true;

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

            switch (QRCodeEncodeMode)
            {
                case "Byte":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;
                case "AlphaNumeric":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                    break;
                case "Numeric":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                    break;
                default:
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;
            }

            qrCodeEncoder.QRCodeScale = QRCodeScale;
            qrCodeEncoder.QRCodeVersion = QRCodeVersion;

            switch (QRCodeErrorCorrect)
            {
                case "L":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                    break;
                case "M":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                    break;
                case "Q":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                    break;
                case "H":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                    break;
                default:
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                    break;
            }

            try
            {
                System.Drawing.Image image = qrCodeEncoder.Encode(QRString, System.Text.Encoding.UTF8);

                System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                fs.Close();

                //if (hasLogo)
                //{
                //    System.Drawing.Image copyImage = System.Drawing.Image.FromFile(logoFilePath);
                //    Graphics g = Graphics.FromImage(image);
                //    int x = image.Width / 2 - copyImage.Width / 2;
                //    int y = image.Height / 2 - copyImage.Height / 2;
                //    g.DrawImage(copyImage, new Rectangle(x, y, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel);
                //    g.Dispose();

                //    image.Save(filePath);
                //    copyImage.Dispose();
                //}
                image.Dispose();

            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="Content">内容文本</param>
        /// <param name="QRCodeEncodeMode">二维码编码方式</param>
        /// <param name="QRCodeErrorCorrect">纠错码等级</param>
        /// <param name="QRCodeVersion">二维码版本号 0-40</param>
        /// <param name="QRCodeScale">每个小方格的预设宽度（像素），正整数</param>
        /// <param name="size">图片尺寸（像素），0表示不设置</param>
        /// <param name="border">图片白边（像素），当size大于0时有效</param>
        /// <returns></returns>
        public static bool CreateQRCode(string QRString, string QRCodeEncodeMode, int QRCodeScale, int QRCodeVersion, string QRCodeErrorCorrect, string filePath, int size = 0, int border = 0)
        {
            bool result = true;
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            switch (QRCodeEncodeMode)
            {
                case "Byte":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;
                case "AlphaNumeric":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                    break;
                case "Numeric":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                    break;
                default:
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;
            }

            switch (QRCodeErrorCorrect)
            {
                case "L":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                    break;
                case "M":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                    break;
                case "Q":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                    break;
                case "H":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                    break;
                default:
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                    break;
            }
            qrCodeEncoder.QRCodeScale = QRCodeScale;
            qrCodeEncoder.QRCodeVersion = QRCodeVersion;
            System.Drawing.Image image = qrCodeEncoder.Encode(QRString);

            #region 根据设定的目标图片尺寸调整二维码QRCodeScale设置，并添加边框
            if (size > 0)
            {
                //当设定目标图片尺寸大于生成的尺寸时，逐步增大方格尺寸
                #region 当设定目标图片尺寸大于生成的尺寸时，逐步增大方格尺寸
                while (image.Width < size)
                {
                    qrCodeEncoder.QRCodeScale++;
                    System.Drawing.Image imageNew = qrCodeEncoder.Encode(QRString);
                    if (imageNew.Width < size)
                    {
                        image = new System.Drawing.Bitmap(imageNew);
                        imageNew.Dispose();
                        imageNew = null;
                    }
                    else
                    {
                        qrCodeEncoder.QRCodeScale--; //新尺寸未采用，恢复最终使用的尺寸
                        imageNew.Dispose();
                        imageNew = null;
                        break;
                    }
                }
                #endregion

                //当设定目标图片尺寸小于生成的尺寸时，逐步减小方格尺寸
                #region 当设定目标图片尺寸小于生成的尺寸时，逐步减小方格尺寸
                while (image.Width > size && qrCodeEncoder.QRCodeScale > 1)
                {
                    qrCodeEncoder.QRCodeScale--;
                    System.Drawing.Image imageNew = qrCodeEncoder.Encode(QRString);
                    image = new System.Drawing.Bitmap(imageNew);
                    imageNew.Dispose();
                    imageNew = null;
                    if (image.Width < size)
                    {
                        break;
                    }
                }
                #endregion

                //如果目标尺寸大于生成的图片尺寸，则为图片增加白边
                #region 如果目标尺寸大于生成的图片尺寸，则为图片增加白边
                if (image.Width <= size)
                {
                    //根据参数设置二维码图片白边的最小宽度
                    #region 根据参数设置二维码图片白边的最小宽度
                    if (border > 0)
                    {
                        while (image.Width <= size && size - image.Width < border * 2 && qrCodeEncoder.QRCodeScale > 1)
                        {
                            qrCodeEncoder.QRCodeScale--;
                            System.Drawing.Image imageNew = qrCodeEncoder.Encode(QRString);
                            image = new System.Drawing.Bitmap(imageNew);
                            imageNew.Dispose();
                            imageNew = null;
                        }
                    }
                    #endregion

                    //当目标图片尺寸大于二维码尺寸时，将二维码绘制在目标尺寸白色画布的中心位置
                    if (image.Width < size)
                    {
                        //新建空白绘图
                        System.Drawing.Bitmap panel = new System.Drawing.Bitmap(size, size);
                        System.Drawing.Graphics graphic0 = System.Drawing.Graphics.FromImage(panel);
                        int p_left = 0;
                        int p_top = 0;
                        if (image.Width <= size) //如果原图比目标形状宽
                        {
                            p_left = (size - image.Width) / 2;
                        }
                        if (image.Height <= size)
                        {
                            p_top = (size - image.Height) / 2;
                        }

                        //将生成的二维码图像粘贴至绘图的中心位置
                        graphic0.DrawImage(image, p_left, p_top, image.Width, image.Height);
                        image = new System.Drawing.Bitmap(panel);
                        panel.Dispose();
                        panel = null;
                        graphic0.Dispose();
                        graphic0 = null;
                    }
                }
                #endregion


            }
            #endregion

            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                fs.Close();

                //if (hasLogo)
                //{
                //    System.Drawing.Image copyImage = System.Drawing.Image.FromFile(logoFilePath);
                //    Graphics g = Graphics.FromImage(image);
                //    int x = image.Width / 2 - copyImage.Width / 2;
                //    int y = image.Height / 2 - copyImage.Height / 2;
                //    g.DrawImage(copyImage, new Rectangle(x, y, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel);
                //    g.Dispose();

                //    image.Save(filePath);
                //    copyImage.Dispose();
                //}
                image.Dispose();

            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
    }
}
