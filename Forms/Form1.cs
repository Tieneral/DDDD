using DDDD.Forms;
using DDDD.Modules;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace DDDD
{
    public partial class Form1 : Form
    {
        private DataTable roomsTable;
        private DataTable guestsTable;

        public Form1()
        {
            InitializeComponent();
            SetupDataGridViews();

            GetData();
        }

        private void SetupDataGridViews()
        {
            dgvRooms.AllowUserToAddRows = true;
            dgvRooms.AllowUserToDeleteRows = true;
            dgvRooms.ReadOnly = false;
            dgvRooms.EditMode = DataGridViewEditMode.EditOnEnter;

            dgvRooms.CellEndEdit += DgvRooms_CellEndEdit;
            dgvRooms.UserDeletedRow += DgvRooms_UserDeletedRow;

            dgvGuests.AllowUserToAddRows = false;
            dgvGuests.AllowUserToDeleteRows = false;
            dgvGuests.ReadOnly = true;
            dgvGuests.EditMode = DataGridViewEditMode.EditProgrammatically;

        }

        public void GetData()
        {
            try
            {
                roomsTable = GetRoomsDataTable();
                dgvRooms.DataSource = roomsTable;

                if (dgvRooms.Columns["Id"] != null)
                    dgvRooms.Columns["Id"].Visible = false;

                guestsTable = GetGuestsDataTable();
                dgvGuests.DataSource = guestsTable;

                if (dgvGuests.Columns["Id"] != null)
                    dgvGuests.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetRoomsDataTable()
        {
            var dataTable = new DataTable();

            using var conn = new SQLiteConnection($"Data Source={HotelDatabase.ConnectionString};Version=3;");
            conn.Open();

            string query = "SELECT Id, Number as 'Номер в фонде', Layer as 'Этаж', " +
                          "Category as 'Тип номера', Locked as 'Занят?' FROM Room";

            using var adapter = new SQLiteDataAdapter(query, conn);

            var builder = new SQLiteCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();
            adapter.InsertCommand = builder.GetInsertCommand();
            adapter.DeleteCommand = builder.GetDeleteCommand();

            adapter.Fill(dataTable);

            return dataTable;
        }

        private DataTable GetGuestsDataTable()
        {
            var dataTable = new DataTable();

            using var conn = new SQLiteConnection($"Data Source={HotelDatabase.ConnectionString};Version=3;");
            conn.Open();

            string query = @"
        SELECT 
            Guest.Id, 
            Guest.Name as 'Имя', 
            Guest.Surname as 'Фамилия', 
            Guest.Last_Name as 'Отчество', 
            Guest.Phone as 'Телефон', 
            Guest.Passport as 'Паспорт',
            Room.Number as 'Номер комнаты'
        FROM Guest
        LEFT JOIN Room ON Guest.RoomID = Room.Id";

            using var adapter = new SQLiteDataAdapter(query, conn);
            adapter.Fill(dataTable);

            return dataTable;
        }

        private void DgvRooms_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var dataTable = (DataTable)dgvRooms.DataSource;

                using var conn = new SQLiteConnection($"Data Source={HotelDatabase.ConnectionString};Version=3;");
                conn.Open();

                // Получаем ID изменяемой записи
                int rowId = Convert.ToInt32(dataTable.Rows[e.RowIndex]["Id"]);
                string columnName = dataTable.Columns[e.ColumnIndex].ColumnName;
                object newValue = dataTable.Rows[e.RowIndex][e.ColumnIndex];

                string dbColumnName = columnName switch
                {
                    "Номер в фонде" => "Number",
                    "Этаж" => "Layer",
                    "Тип номера" => "Category",
                    "Занят?" => "Locked",
                    _ => columnName
                };

                if (dbColumnName != "Id")
                {
                    string updateQuery = $"UPDATE Room SET {dbColumnName} = @value WHERE Id = @id";

                    using var cmd = new SQLiteCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@value", newValue ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@id", rowId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                GetData();
            }
        }

        private void DgvRooms_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.Cells["Id"].Value != null && e.Row.Cells["Id"].Value != DBNull.Value)
                {
                    int rowId = Convert.ToInt32(e.Row.Cells["Id"].Value);

                    using var conn = new SQLiteConnection($"Data Source={HotelDatabase.ConnectionString};Version=3;");
                    conn.Open();

                    string checkGuestsQuery = "SELECT COUNT(*) FROM Guest WHERE RoomID = @roomId";
                    using var checkCmd = new SQLiteCommand(checkGuestsQuery, conn);
                    checkCmd.Parameters.AddWithValue("@roomId", rowId);

                    int guestsCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (guestsCount > 0)
                    {
                        MessageBox.Show("Нельзя удалить номер, в котором есть гости.",
                            "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        GetData();
                        return;
                    }

                    string deleteQuery = "DELETE FROM Room WHERE Id = @id";
                    using var deleteCmd = new SQLiteCommand(deleteQuery, conn);
                    deleteCmd.Parameters.AddWithValue("@id", rowId);

                    deleteCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении записи: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                GetData();
            }
        }

        private void добавитьГостяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGuestForm newGuestForm = new NewGuestForm();
            newGuestForm.ShowDialog();

            GetData();
        }

        private void добавитьНомерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewRoomForm newRoomForm = new NewRoomForm();
            newRoomForm.ShowDialog();

            GetData();
        }

        private void удалитьНомерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteRoomForm deleteRoomForm = new DeleteRoomForm();

            if (deleteRoomForm.ShowDialog() == DialogResult.OK)
            {
                GetData();
            }
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetData();
        }
    }
}