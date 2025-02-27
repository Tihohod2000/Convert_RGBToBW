using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace TestTask_ConvertRGBToBW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string selectedFilePath;

        public Bitmap inputImage;
        OpenFileDialog openFileDialog = new OpenFileDialog();

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"; // фильтр для изображений
            openFileDialog.Title = "Выберите изображение";

            // Проверяем, выбрал ли пользователь файл
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Получаем путь к выбранному файлу
                selectedFilePath = openFileDialog.FileName;

                //Загружаем изображение
                inputImage = new Bitmap(selectedFilePath);

                // Например, выводим путь в текстовое поле
                // MessageBox.Show("Вы выбрали файл: " + selectedFilePath);

                // Загрузка изображения в PictureBox
                pictureInput.Image = Image.FromFile(selectedFilePath);

                pictureInput.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрал ли пользователь файл
            if (!string.IsNullOrEmpty(selectedFilePath))
            {
                ImageRGBToBW converter = new ImageRGBToBW(selectedFilePath);
                /*converter.ConvertAndSave(inputImage);*/

                Bitmap bwImage = await converter.ConvertAndSaveAsync(inputImage, progressBar1);
                Console.WriteLine("Конец");

                pictureOutput.Image = bwImage;

                pictureOutput.SizeMode = PictureBoxSizeMode.Zoom;

                /*MessageBox.Show("Готово!");*/
            }
            else
            {
                MessageBox.Show("Выберите изображение!!!");
            }

            

            /*pictureOutput.Image = Image.FromFile(selectedFilePath);

            pictureOutput.SizeMode = PictureBoxSizeMode.Zoom;*/

        }
    }
}
