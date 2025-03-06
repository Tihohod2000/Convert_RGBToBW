using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Resources.ResXFileRef;
using static System.Windows.Forms.LinkLabel;

namespace TestTask_ConvertRGBToBW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string ImagePath;

        public Bitmap inputImage;

        public OpenFileDialog openFileDialog = new OpenFileDialog();
        public ImageRGBToBW converter;

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"; // фильтр для изображений
            openFileDialog.Title = "Выберите изображение";

            // Проверяем, выбрал ли пользователь файл
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImagePath = openFileDialog.FileName;

                inputImage = new Bitmap(ImagePath);;

                pictureInput.Image = Image.FromFile(ImagePath);
                pictureInput.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрал ли пользователь файл
            if (!string.IsNullOrEmpty(ImagePath))
            {

                try
                {
                     converter = new ImageRGBToBW(ImagePath);
                    /*converter.ConvertAndSave(inputImage);*/

                    converter.ProgressChanged += (s, progress) =>
                    {
                        // Подписка на событие InvokeRequired
                        if (progressBar1.InvokeRequired)
                        {
                            progressBar1.Invoke(new Action<int>((p) => progressBar1.Value = p), progress);
                        }
                        else
                        {
                            progressBar1.Value = progress;
                        }
                    };

                    // Подписка на событие ImageUpdated
                    converter.ImageUpdated += (s, updatedImage) =>
                    {
                        if (pictureOutput.InvokeRequired)
                        {
                            pictureOutput.Invoke(new Action(() =>
                            {
                                pictureOutput.Image = updatedImage;
                                pictureOutput.SizeMode = PictureBoxSizeMode.Zoom;
                            }));
                        }
                        else
                        {
                            pictureOutput.Image = updatedImage;
                            pictureOutput.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    };

                    await converter.ConvertAndSaveAsync(inputImage);
                    Console.WriteLine("Конец");

                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Ошибка: {ex.Message}");
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //MessageBox.Show("Готово!");
            }
            else
            {
                MessageBox.Show("Выберите изображение!!!");
            }
        }

        private void ExitApp(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (converter == null)
                {
                    throw new NotSupportedException("Изображение не было сконвертированно");
                }

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Title = "Выберите место для сохранения";
                    saveFileDialog.Filter = "Изображения (*.jpg;*.png)|*.jpg;*.png|Все файлы (*.*)|*.*";
                    saveFileDialog.FileName = "image.jpg"; // Имя по умолчанию

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;

                        converter.SaveImage(filePath);
                        MessageBox.Show($"Файл был успешно сохранён");

                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            



        }
    }
}
