using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DDDD.Modules;

namespace DDDD.Forms
{
    public partial class NewRoomForm : Form
    {
        public NewRoomForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введите номер комнаты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Введите этаж", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Введите тип комнаты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Введите статус комнаты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка, что номер и этаж являются числами
            if (!int.TryParse(textBox1.Text, out int roomNumber))
            {
                MessageBox.Show("Номер комнаты должен быть числом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBox2.Text, out int floor))
            {
                MessageBox.Show("Этаж должен быть числом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            AddRoomToDatabase();
        }

        private void AddRoomToDatabase()
        {
            try
            {
                using var conn = new SQLiteConnection($"Data Source={HotelDatabase.ConnectionString};Version=3;");
                conn.Open();

                string query = @"
                    INSERT INTO Room (Number, Layer, Category, Locked) 
                    VALUES (@number, @layer, @category, @locked);
                    SELECT last_insert_rowid();";

                using var cmd = new SQLiteCommand(query, conn);

                cmd.Parameters.AddWithValue("@number", textBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@layer", textBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@category", textBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@locked", textBox4.Text.Trim());

                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    int newRoomId = Convert.ToInt32(result);
                    this.Close();
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}