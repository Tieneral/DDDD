using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDD.Modules
{
    public class Guests
    {
        [Browsable(false)]
        public int Id { get; set; }

        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Фамилия")]
        public string Surname { get; set; }

        [DisplayName("Отчество")]
        public string LastName { get; set; }

        [DisplayName("Телефон")]
        public string Phone { get; set; }
        [DisplayName("Пасспорт")]
        public string Passport { get; set; }
        [DisplayName("ID комнаты")]
        public int RoomID { get; set; }

    }
}
