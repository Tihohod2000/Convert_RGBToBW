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

        public Bitmap outputImage;

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

            Bitmap convertedImage = new Bitmap(image.Width, image.Height, PixelFormat.Format1bppIndexed);

            BitmapData inputData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                                                  ImageLockMode.ReadOnly, format);
            BitmapData outputData = convertedImage.LockBits(new Rectangle(0, 0, convertedImage.Width, convertedImage.Height),
                                                            ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);

            int width = image.Width;
            int height = image.Height;
            int stride = inputData.Stride;
            int pixelSize = (format == PixelFormat.Format24bppRgb) ? 3 : 4;

            int totalRows = height;
            int processedRows = 0;

            unsafe
            {
                byte* inputPtr = (byte*)inputData.Scan0;
                byte* outputPtr = (byte*)outputData.Scan0;

                Parallel.For(0, height, y =>
                {
                    byte* rowInput = inputPtr + y * stride;
                    byte* rowOutput = outputPtr + (y * outputData.Stride);

                    byte pixelByte = 0;
                    int bitIndex = 7;

                    for (int x = 0; x < width; x++)
                    {
                        byte brightness = (byte)((rowInput[x * pixelSize] + rowInput[x * pixelSize + 1] + rowInput[x * pixelSize + 2]) / 3);
                        if (brightness >= Threshold)
                            pixelByte |= (byte)(1 << bitIndex);

                        bitIndex--;

                        if (bitIndex < 0 || x == width - 1)
                        {
                            rowOutput[x / 8] = pixelByte;
                            pixelByte = 0;
                            bitIndex = 7;
                        }
                    }

                    // Обновление прогресса раз в 10 строк
                    if (Interlocked.Increment(ref processedRows) % 10 == 0)
                    {
                        int progress = (processedRows * 100) / totalRows;
                        ProgressChanged?.Invoke(this, progress);
                    }
                });
            }

            image.UnlockBits(inputData);
            convertedImage.UnlockBits(outputData);

            ProgressChanged?.Invoke(this, 100);

            return convertedImage;
        }

        protected virtual void OnProgressChanged(int progress)
        {
            ProgressChanged?.Invoke(this, progress);
        }

        public async Task ConvertAndSaveAsync(Bitmap image)
        { 
            outputImage = await Task.Run(() => Convert(image));
            ImageUpdated?.Invoke(this, outputImage);
            /*outputImage.Save(NameOut, ImageFormat.Png); */// Сохраняем в PNG
            
        }

        public void SaveImage(string NameOut)
        {
            if (outputImage != null)
            {
                outputImage.Save(NameOut, ImageFormat.Png);
            }
            else
            {
                throw new NotSupportedException("Изображение не было сконвертированно");
            }
        }

    }
}
