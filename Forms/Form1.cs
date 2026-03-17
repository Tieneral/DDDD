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

            // Настройка DataGridView для редактирования
            SetupDataGridViews();

            GetData();
        }

        private void SetupDataGridViews()
        {
            // Настройка dgvRooms
            dgvRooms.AllowUserToAddRows = true;
            dgvRooms.AllowUserToDeleteRows = true;
            dgvRooms.ReadOnly = false;
            dgvRooms.EditMode = DataGridViewEditMode.EditOnEnter;

            // Настройка dgvGuests
            dgvGuests.AllowUserToAddRows = true;
            dgvGuests.AllowUserToDeleteRows = true;
            dgvGuests.ReadOnly = false;
            dgvGuests.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        public void GetData()
        {
            try
            {
                // Получаем данные в виде DataTable для Rooms
                roomsTable = GetRoomsDataTable();
                dgvRooms.DataSource = roomsTable;

                // Скрываем колонку ID, если она есть
                if (dgvRooms.Columns["Id"] != null)
                    dgvRooms.Columns["Id"].Visible = false;

                // Получаем данные в виде DataTable для Guests
                guestsTable = GetGuestsDataTable();
                dgvGuests.DataSource = guestsTable;

                // Скрываем колонку ID, если она есть
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
            adapter.Fill(dataTable);

            return dataTable;
        }

        private DataTable GetGuestsDataTable()
        {
            var dataTable = new DataTable();

            using var conn = new SQLiteConnection($"Data Source={HotelDatabase.ConnectionString};Version=3;");
            conn.Open();

            string query = "SELECT Id, Name as 'Имя', Surname as 'Фамилия', " +
                          "Last_Name as 'Отчество', Phone as 'Телефон', " +
                          "Passport as 'Паспорт', RoomID as 'Номер комнаты' FROM Guest";

            using var adapter = new SQLiteDataAdapter(query, conn);
            adapter.Fill(dataTable);

            return dataTable;
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

        // Новый пункт меню для удаления комнаты
        private void удалитьНомерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteRoomForm deleteRoomForm = new DeleteRoomForm();

            // Показываем форму и проверяем результат
            if (deleteRoomForm.ShowDialog() == DialogResult.OK)
            {
                // Если удаление прошло успешно, обновляем данные
                GetData();
            }
        }

        // Дополнительный метод для обновления данных после изменений
        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetData();
        }
    }
}