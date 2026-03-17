using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace DDDD.Forms
{
    public partial class DeleteRoomForm : Form
    {
        public DeleteRoomForm()
        {
            InitializeComponent();
            LoadRoomNumbers();
        }

        private void LoadRoomNumbers()
        {
            try
            {
                using var conn = new SQLiteConnection($"Data Source={HotelDatabase.ConnectionString};Version=3;");
                conn.Open();

                string query = "SELECT Number FROM Room ORDER BY Number";

                using var cmd = new SQLiteCommand(query, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    numericUpDown1.Items.Add(reader["Number"].ToString());
                }

                if (numericUpDown1.Items.Count > 0)
                    numericUpDown1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки номеров комнат: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.SelectedItem == null)
            {
                MessageBox.Show("Выберите номер комнаты для удаления",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string roomNumber = numericUpDown1.SelectedItem.ToString();

            // Подтверждение удаления
            DialogResult result = MessageBox.Show(
                $"Вы уверены, что хотите удалить комнату с номером {roomNumber}?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int? roomId = GetRoomIdByNumber(roomNumber);

                    if (roomId == null)
                    {
                        MessageBox.Show($"Комната с номером {roomNumber} не найдена",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (HasGuestsInRoom(roomId.Value))
                    {
                        result = MessageBox.Show(
                            $"В комнате {roomNumber} есть гости. Вы уверены, что хотите удалить комнату?\n" +
                            "При удалении комнаты связь с гостями будет потеряна.",
                            "Подтверждение удаления",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (result == DialogResult.No)
                        {
                            return;
                        }

                        UpdateGuestsRoomIdToNull(roomId.Value);
                    }

                    DeleteRoomById(roomId.Value);

                    MessageBox.Show($"Комната с номером {roomNumber} успешно удалена",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении комнаты: {ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private int? GetRoomIdByNumber(string roomNumber)
        {
            using var conn = new SQLiteConnection($"Data Source={HotelDatabase.ConnectionString};Version=3;");
            conn.Open();

            string query = "SELECT Id FROM Room WHERE Number = @number";

            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@number", roomNumber);

            object result = cmd.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }

            return null;
        }

        private bool HasGuestsInRoom(int roomId)
        {
            using var conn = new SQLiteConnection($"Data Source={HotelDatabase.ConnectionString};Version=3;");
            conn.Open();

            string query = "SELECT COUNT(*) FROM Guest WHERE RoomID = @roomid";

            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@roomid", roomId);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }

        private void UpdateGuestsRoomIdToNull(int roomId)
        {
            using var conn = new SQLiteConnection($"Data Source={HotelDatabase.ConnectionString};Version=3;");
            conn.Open();

            string query = "UPDATE Guest SET RoomID = NULL WHERE RoomID = @roomid";

            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@roomid", roomId);

            cmd.ExecuteNonQuery();
        }

        private void DeleteRoomById(int roomId)
        {
            using var conn = new SQLiteConnection($"Data Source={HotelDatabase.ConnectionString};Version=3;");
            conn.Open();

            string query = "DELETE FROM Room WHERE Id = @id";

            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", roomId);

            cmd.ExecuteNonQuery();
        }
    }
}