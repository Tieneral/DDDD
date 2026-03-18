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
            dgvRooms.AllowUserToAddRows = false;
            dgvRooms.AllowUserToDeleteRows = true;
            dgvRooms.ReadOnly = false;
            dgvRooms.EditMode = DataGridViewEditMode.EditOnEnter;

            dgvRooms.CellValidating += DgvRooms_CellValidating;
            dgvRooms.CellEndEdit += DgvRooms_CellEndEdit;
            dgvRooms.UserDeletedRow += DgvRooms_UserDeletedRow;

            dgvGuests.AllowUserToAddRows = false;
            dgvGuests.AllowUserToDeleteRows = false;
            dgvGuests.ReadOnly = true;
            dgvGuests.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void DgvRooms_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string columnName = dgvRooms.Columns[e.ColumnIndex].Name;
            string newValue = e.FormattedValue?.ToString()?.Trim() ?? "";

            if (columnName == "Номер в фонде")
            {
                if (string.IsNullOrWhiteSpace(newValue))
                {
                    dgvRooms.Rows[e.RowIndex].ErrorText = "Номер комнаты не может быть пустым";
                    e.Cancel = true;
                }
                else if (newValue.Length > 10)
                {
                    dgvRooms.Rows[e.RowIndex].ErrorText = "Номер комнаты не может быть длиннее 10 символов";
                    e.Cancel = true;
                }
                else
                {
                    try
                    {
                        using var conn = new SQLiteConnection($"Data Source={HotelDatabase.ConnectionString};Version=3;");
                        conn.Open();

                        int rowId = Convert.ToInt32(dgvRooms.Rows[e.RowIndex].Cells["Id"].Value);

                        string checkQuery = "SELECT COUNT(*) FROM Room WHERE Number = @number AND Id != @id";
                        using var cmd = new SQLiteCommand(checkQuery, conn);
                        cmd.Parameters.AddWithValue("@number", newValue);
                        cmd.Parameters.AddWithValue("@id", rowId);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count > 0)
                        {
                            dgvRooms.Rows[e.RowIndex].ErrorText = "Комната с таким номером уже существует";
                            e.Cancel = true;
                        }
                        else
                        {
                            dgvRooms.Rows[e.RowIndex].ErrorText = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка проверки уникальности: {ex.Message}");
                    }
                }
            }

            else if (columnName == "Этаж")
            {
                if (string.IsNullOrWhiteSpace(newValue))
                {
                    dgvRooms.Rows[e.RowIndex].ErrorText = "Этаж не может быть пустым";
                    e.Cancel = true;
                }
                else if (!int.TryParse(newValue, out int floor) || floor < 1 || floor > 100)
                {
                    dgvRooms.Rows[e.RowIndex].ErrorText = "Этаж должен быть числом от 1 до 100";
                    e.Cancel = true;
                }
                else
                {
                    dgvRooms.Rows[e.RowIndex].ErrorText = "";
                }
            }

            else if (columnName == "Тип номера")
            {
                if (string.IsNullOrWhiteSpace(newValue))
                {
                    dgvRooms.Rows[e.RowIndex].ErrorText = "Тип номера не может быть пустым";
                    e.Cancel = true;
                }
                else if (!int.TryParse(newValue, out int category) || category < 1 || category > 5)
                {
                    dgvRooms.Rows[e.RowIndex].ErrorText = "Тип номера должен быть числом от 1 до 5";
                    e.Cancel = true;
                }
                else
                {
                    dgvRooms.Rows[e.RowIndex].ErrorText = "";
                }
            }

            else if (columnName == "Занят?")
            {
                if (!string.IsNullOrWhiteSpace(newValue) &&
                    newValue.ToLower() != "да" &&
                    newValue.ToLower() != "нет" &&
                    newValue.ToLower() != "yes" &&
                    newValue.ToLower() != "no")
                {
                    dgvRooms.Rows[e.RowIndex].ErrorText = "Значение должно быть 'Да' или 'Нет'";
                    e.Cancel = true;
                }
                else
                {
                    dgvRooms.Rows[e.RowIndex].ErrorText = "";
                }
            }
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

                    using var transaction = conn.BeginTransaction();

                    try
                    {
                        string deleteGuestsQuery = "DELETE FROM Guest WHERE RoomID = @roomId";
                        using var deleteGuestsCmd = new SQLiteCommand(deleteGuestsQuery, conn, transaction);
                        deleteGuestsCmd.Parameters.AddWithValue("@roomId", rowId);

                        int deletedGuestsCount = deleteGuestsCmd.ExecuteNonQuery();

                        string deleteRoomQuery = "DELETE FROM Room WHERE Id = @id";
                        using var deleteRoomCmd = new SQLiteCommand(deleteRoomQuery, conn, transaction);
                        deleteRoomCmd.Parameters.AddWithValue("@id", rowId);

                        deleteRoomCmd.ExecuteNonQuery();

                        transaction.Commit();

                        if (deletedGuestsCount > 0)
                        {
                            MessageBox.Show($"Номер и {deletedGuestsCount} гостей успешно удалены.",
                                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw; 
                    }
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