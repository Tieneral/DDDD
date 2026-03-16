using System;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace DDDD
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.AcceptButton = loginBtn;
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text.Trim();
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (CheckUserCredentials(login, password))
            {
                // Открываем главную форму
                Form1 mainForm = new Form1();
                mainForm.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль",
                    "Ошибка входа",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                textBox2.Clear();
                textBox2.Focus();
            }
        }

        private bool CheckUserCredentials(string login, string password)
        {
            try
            {
                using var conn = new SQLiteConnection($"Data Source={HotelDatabase.ConnectionString};Version=3;");
                conn.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE Login = @login AND UserPassword = @password";

                using var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", password);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при подключении к базе данных: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
    }
}