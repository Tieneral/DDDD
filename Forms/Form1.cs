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

            dgvGuests.AllowUserToAddRows = true;
            dgvGuests.AllowUserToDeleteRows = true;
            dgvGuests.ReadOnly = false;
            dgvGuests.EditMode = DataGridViewEditMode.EditOnEnter;
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