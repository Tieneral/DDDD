namespace DDDD
{
    partial class NewGuestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            AddGuestBtn = new Button();
            comboBox1 = new ComboBox();
            PassRndBtn = new Button();
            PhoneRndBtn = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(67, 19);
            label1.Name = "label1";
            label1.Size = new Size(130, 21);
            label1.TabIndex = 0;
            label1.Text = "Записать гостя";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(67, 58);
            label2.Name = "label2";
            label2.Size = new Size(41, 21);
            label2.TabIndex = 1;
            label2.Text = "Имя";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(31, 118);
            label3.Name = "label3";
            label3.Size = new Size(77, 21);
            label3.TabIndex = 2;
            label3.Text = "Отчество";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(33, 88);
            label4.Name = "label4";
            label4.Size = new Size(75, 21);
            label4.TabIndex = 3;
            label4.Text = "Фамилия";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(25, 192);
            label5.Name = "label5";
            label5.Size = new Size(77, 21);
            label5.TabIndex = 4;
            label5.Text = "Пасспорт";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(31, 156);
            label6.Name = "label6";
            label6.Size = new Size(71, 21);
            label6.TabIndex = 5;
            label6.Text = "Телефон";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.Location = new Point(31, 226);
            label7.Name = "label7";
            label7.Size = new Size(73, 42);
            label7.TabIndex = 6;
            label7.Text = "Номер \r\nкомнаты";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(114, 190);
            textBox2.MaxLength = 4;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 8;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(114, 154);
            textBox3.MaxLength = 4;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 9;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(114, 120);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(100, 23);
            textBox4.TabIndex = 10;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(114, 90);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(100, 23);
            textBox5.TabIndex = 11;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(114, 58);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(100, 23);
            textBox6.TabIndex = 12;
            // 
            // AddGuestBtn
            // 
            AddGuestBtn.Location = new Point(27, 277);
            AddGuestBtn.Name = "AddGuestBtn";
            AddGuestBtn.Size = new Size(187, 58);
            AddGuestBtn.TabIndex = 13;
            AddGuestBtn.Text = "Добавить клиента";
            AddGuestBtn.UseVisualStyleBackColor = true;
            AddGuestBtn.Click += AddGuestBtn_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(115, 226);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(99, 23);
            comboBox1.TabIndex = 14;
            // 
            // PassRndBtn
            // 
            PassRndBtn.Location = new Point(220, 193);
            PassRndBtn.Name = "PassRndBtn";
            PassRndBtn.Size = new Size(18, 23);
            PassRndBtn.TabIndex = 15;
            PassRndBtn.Text = "R";
            PassRndBtn.UseVisualStyleBackColor = true;
            PassRndBtn.Click += PassRndBtn_Click;
            // 
            // PhoneRndBtn
            // 
            PhoneRndBtn.Location = new Point(220, 157);
            PhoneRndBtn.Name = "PhoneRndBtn";
            PhoneRndBtn.Size = new Size(18, 23);
            PhoneRndBtn.TabIndex = 16;
            PhoneRndBtn.Text = "R";
            PhoneRndBtn.UseVisualStyleBackColor = true;
            PhoneRndBtn.Click += PhoneRndBtn_Click;
            // 
            // NewGuestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(248, 347);
            Controls.Add(PhoneRndBtn);
            Controls.Add(PassRndBtn);
            Controls.Add(comboBox1);
            Controls.Add(AddGuestBtn);
            Controls.Add(textBox6);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "NewGuestForm";
            Text = "NewGuestForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private TextBox textBox6;
        private Button AddGuestBtn;
        private ComboBox comboBox1;
        private Button PassRndBtn;
        private Button PhoneRndBtn;
    }
}