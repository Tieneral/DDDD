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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DDDD
{
    public partial class NewGuestForm : Form
    {
        public NewGuestForm()
        {
            InitializeComponent();
            LoadRooms();
        }

        private void LoadRooms()
        {
            try
            {
                // список комнат
                List<Rooms> rooms = HotelDatabase.GetRooms();

                comboBox1.DisplayMember = "Number"; 
                comboBox1.ValueMember = "Id";       
                comboBox1.DataSource = rooms;

                comboBox1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки комнат: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PassRndBtn_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            string passport = rnd.Next(1000, 9999).ToString(); 
            textBox2.Text = passport; 
        }

        private void PhoneRndBtn_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            string phone = $"89{rnd.Next(10, 99)}"; 
            textBox3.Text = phone; 
        }

        private void AddGuestBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox6.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) || 
                string.IsNullOrWhiteSpace(textBox2.Text) || 
                comboBox1.SelectedValue == null)            
            {
                MessageBox.Show("Заполните все поля и выберите комнату!",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Добавляем гостя в БД
                string query = @"
                    INSERT INTO Guest (Name, Surname, Last_Name, Phone, Passport, RoomID)
                    VALUES (@name, @surname, @lastname, @phone, @passport, @roomid)";

                using (var connection = new SQLiteConnection(HotelDatabase.ConnectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", textBox6.Text.Trim());
                        command.Parameters.AddWithValue("@surname", textBox5.Text.Trim());
                        command.Parameters.AddWithValue("@lastname", textBox4.Text.Trim());
                        command.Parameters.AddWithValue("@phone", textBox3.Text.Trim());
                        command.Parameters.AddWithValue("@passport", textBox2.Text.Trim());
                        command.Parameters.AddWithValue("@roomid", comboBox1.SelectedValue);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Гость успешно добавлен!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении гостя: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
