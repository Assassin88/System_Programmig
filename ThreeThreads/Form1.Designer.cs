namespace Threads2
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
            this.btnThread2 = new System.Windows.Forms.Button();
            this.btnThread3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnThread1 = new System.Windows.Forms.Button();
            this.tbText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnThread2
            // 
            this.btnThread2.Location = new System.Drawing.Point(219, 132);
            this.btnThread2.Name = "btnThread2";
            this.btnThread2.Size = new System.Drawing.Size(75, 23);
            this.btnThread2.TabIndex = 1;
            this.btnThread2.Text = "2";
            this.btnThread2.UseVisualStyleBackColor = true;
            this.btnThread2.Click += new System.EventHandler(this.btnThread2_Click);
            // 
            // btnThread3
            // 
            this.btnThread3.Location = new System.Drawing.Point(351, 74);
            this.btnThread3.Name = "btnThread3";
            this.btnThread3.Size = new System.Drawing.Size(75, 23);
            this.btnThread3.TabIndex = 2;
            this.btnThread3.Text = "3";
            this.btnThread3.UseVisualStyleBackColor = true;
            this.btnThread3.Click += new System.EventHandler(this.btnThread3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(104, 408);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnThread1
            // 
            this.btnThread1.Location = new System.Drawing.Point(93, 74);
            this.btnThread1.Name = "btnThread1";
            this.btnThread1.Size = new System.Drawing.Size(75, 23);
            this.btnThread1.TabIndex = 4;
            this.btnThread1.Text = "1";
            this.btnThread1.UseVisualStyleBackColor = true;
            this.btnThread1.Click += new System.EventHandler(this.btnThread1_Click);
            // 
            // tbText
            // 
            this.tbText.Location = new System.Drawing.Point(153, 215);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(216, 20);
            this.tbText.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 337);
            this.Controls.Add(this.tbText);
            this.Controls.Add(this.btnThread1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnThread3);
            this.Controls.Add(this.btnThread2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnThread2;
        private System.Windows.Forms.Button btnThread3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnThread1;
        private System.Windows.Forms.TextBox tbText;
    }
}

