using DDDD.Modules;
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

namespace DDDD
{
    public partial class NewGuestForm : Form
    {
        public NewGuestForm()
        {
            InitializeComponent();
            LoadRooms();

            textBox2.KeyPress += TextBox_KeyPress_OnlyDigits;
            textBox3.KeyPress += TextBox_KeyPress_OnlyDigits;

            //textBox2.Leave += TextBox_Leave_CheckLength;
            //textBox3.Leave += TextBox_Leave_CheckLength;
        }

        private void TextBox_KeyPress_OnlyDigits(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void TextBox_Leave_CheckLength(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null && textBox.Text.Length > 0)
            {
                if (textBox.Text.Length != 4)
                {
                    MessageBox.Show($"В поле {textBox.Name} должно быть ровно 4 цифры!",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox.Focus(); 
                }
            }
        }

        private void LoadRooms()
        {
            List<Rooms> rooms = HotelDatabase.GetRooms();

            if (comboBox1 != null)
            {
                comboBox1.DisplayMember = "Number";
                comboBox1.ValueMember = "Id";
                comboBox1.DataSource = rooms;
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
            string phone = $"89{rnd.Next(4, 99)}";
            textBox3.Text = phone;
        }

        private void AddGuestBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox6.Text) ||
                    string.IsNullOrWhiteSpace(textBox5.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля и выберите комнату!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (textBox2.Text.Length != 4 || textBox3.Text.Length != 4)
                {
                    MessageBox.Show("Поля 'Паспорт' и 'Телефон' должны содержать ровно 4 цифры!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Rooms selectedRoom = (Rooms)comboBox1.SelectedItem;

                Guests newGuest = new Guests
                {
                    Name = textBox6.Text.Trim(),
                    Surname = textBox5.Text.Trim(),
                    LastName = textBox4.Text.Trim(),
                    Phone = textBox3.Text.Trim(),
                    Passport = textBox2.Text.Trim(),
                    RoomID = selectedRoom.Id
                };

                Guests addedGuest = HotelDatabase.AddGuest(newGuest);

                MessageBox.Show("Гость успешно добавлен!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении гостя: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }
    }
}