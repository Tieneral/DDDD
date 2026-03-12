namespace DDDD
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            dgvRooms = new DataGridView();
            menuStrip1 = new MenuStrip();
            добавитьГостяToolStripMenuItem = new ToolStripMenuItem();
            добавтьЗаписьToolStripMenuItem = new ToolStripMenuItem();
            dgvGuests = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRooms).BeginInit();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGuests).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dgvRooms);
            splitContainer1.Panel1.Controls.Add(menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dgvGuests);
            splitContainer1.Size = new Size(800, 555);
            splitContainer1.SplitterDistance = 277;
            splitContainer1.TabIndex = 0;
            // 
            // dgvRooms
            // 
            dgvRooms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRooms.Dock = DockStyle.Fill;
            dgvRooms.Location = new Point(0, 24);
            dgvRooms.Name = "dgvRooms";
            dgvRooms.Size = new Size(800, 253);
            dgvRooms.TabIndex = 1;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { добавитьГостяToolStripMenuItem, добавтьЗаписьToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // добавитьГостяToolStripMenuItem
            // 
            добавитьГостяToolStripMenuItem.Name = "добавитьГостяToolStripMenuItem";
            добавитьГостяToolStripMenuItem.Size = new Size(103, 20);
            добавитьГостяToolStripMenuItem.Text = "Добавить гостя";
            добавитьГостяToolStripMenuItem.Click += добавитьГостяToolStripMenuItem_Click;
            // 
            // добавтьЗаписьToolStripMenuItem
            // 
            добавтьЗаписьToolStripMenuItem.Name = "добавтьЗаписьToolStripMenuItem";
            добавтьЗаписьToolStripMenuItem.Size = new Size(111, 20);
            добавтьЗаписьToolStripMenuItem.Text = "Добавить запись";
            // 
            // dgvGuests
            // 
            dgvGuests.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGuests.Dock = DockStyle.Fill;
            dgvGuests.Location = new Point(0, 0);
            dgvGuests.Name = "dgvGuests";
            dgvGuests.Size = new Size(800, 274);
            dgvGuests.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 555);
            Controls.Add(splitContainer1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRooms).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGuests).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private DataGridView dgvRooms;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem добавитьГостяToolStripMenuItem;
        private ToolStripMenuItem добавтьЗаписьToolStripMenuItem;
        private DataGridView dgvGuests;
    }
}
