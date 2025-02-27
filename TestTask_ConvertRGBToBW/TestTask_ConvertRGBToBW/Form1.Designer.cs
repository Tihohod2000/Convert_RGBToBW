namespace TestTask_ConvertRGBToBW
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureInput = new System.Windows.Forms.PictureBox();
            this.pictureOutput = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureInput
            // 
            this.pictureInput.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureInput.Location = new System.Drawing.Point(82, 50);
            this.pictureInput.Name = "pictureInput";
            this.pictureInput.Size = new System.Drawing.Size(521, 244);
            this.pictureInput.TabIndex = 0;
            this.pictureInput.TabStop = false;
            // 
            // pictureOutput
            // 
            this.pictureOutput.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureOutput.Location = new System.Drawing.Point(82, 381);
            this.pictureOutput.Name = "pictureOutput";
            this.pictureOutput.Size = new System.Drawing.Size(521, 269);
            this.pictureOutput.TabIndex = 1;
            this.pictureOutput.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(217, 300);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(247, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Сконвертировать изображение";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(217, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(247, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Загрузить изображение";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(82, 352);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(521, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 671);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureOutput);
            this.Controls.Add(this.pictureInput);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureOutput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureInput;
        private System.Windows.Forms.PictureBox pictureOutput;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

