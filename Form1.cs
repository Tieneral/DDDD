using System;
using System.Windows.Forms;

namespace DDDD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            GetData();
        }

        public void GetData()
        {
            dgvRooms.DataSource = HotelDatabase.GetRooms();
            dgvGuests.DataSource = HotelDatabase.GetGuests();
        }

        private void ‰Ó·‡‚ËÚ¸√ÓÒÚˇToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGuestForm newGuestForm = new NewGuestForm();
            newGuestForm.ShowDialog(); 

            GetData();
        }
    }
}