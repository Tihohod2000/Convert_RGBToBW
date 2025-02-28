using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TestTask_ConvertRGBToBW
{
    public class ImageRGBToBW
    {
        public string Link { get; set; }
        public string NameOut { get; set; }

        private const byte Threshold = 128; // Порог

        public event EventHandler<int> ProgressChanged;

        public event EventHandler<Bitmap> ImageUpdated;

        public ImageRGBToBW(string link, string nameOut = "output.png")
        {
            Link = link;
            NameOut = nameOut;
        }

        public Bitmap Convert(Bitmap image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image), "Изображение не может быть null");

            PixelFormat format = image.PixelFormat;
            if (format != PixelFormat.Format24bppRgb && format != PixelFormat.Format32bppArgb)
                throw new NotSupportedException("Поддерживаются только 24-битные и 32-битные изображения.");

            Bitmap convertedImage = new Bitmap(image.Width, image.Height, format);

            BitmapData inputData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                                                  ImageLockMode.ReadOnly, format);
            BitmapData outputData = convertedImage.LockBits(new Rectangle(0, 0, convertedImage.Width, convertedImage.Height),
                                                            ImageLockMode.WriteOnly, format);

            int bytes = Math.Abs(inputData.Stride) * image.Height;
            byte[] pixelBuffer = new byte[bytes];

            Marshal.Copy(inputData.Scan0, pixelBuffer, 0, bytes);
            

            int pixelSize = (format == PixelFormat.Format24bppRgb) ? 3 : 4; // 3 байта (BGR) или 4 (BGRA)
            int totalPixels = pixelBuffer.Length / pixelSize;


            for (int i = 0; i < pixelBuffer.Length; i += pixelSize)
            {
                // Вычисляем яркость 
                byte grayValue = (byte)((pixelBuffer[i] + pixelBuffer[i + 1] + pixelBuffer[i + 2]) / 3);

                // Бинаризация
                byte binaryValue = (grayValue >= Threshold) ? (byte)255 : (byte)0;

                pixelBuffer[i] = binaryValue;     // Blue
                pixelBuffer[i + 1] = binaryValue; // Green
                pixelBuffer[i + 2] = binaryValue; // Red

                //  прогресс
                if (totalPixels > 0)
                {
                    int progress = (i * 100) / (totalPixels - 1); 
                    progress = Math.Max(0, Math.Min(100, progress)); 
                    OnProgressChanged(progress); // Вызываем событие
                }

            }

            image.UnlockBits(inputData);

            //100% прогресс
            OnProgressChanged(100);

            Marshal.Copy(pixelBuffer, 0, outputData.Scan0, bytes);
            convertedImage.UnlockBits(outputData);

            return convertedImage;
        }

        protected virtual void OnProgressChanged(int progress)
        {
            ProgressChanged?.Invoke(this, progress);
        }

        public async Task ConvertAndSaveAsync(Bitmap image)
        { 
            Bitmap outputImage = Convert(image);
            ImageUpdated?.Invoke(this, outputImage);
            /*outputImage.Save(NameOut, ImageFormat.Png); */// Сохраняем в PNG
            
        }

    }
}
