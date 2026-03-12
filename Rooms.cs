using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDD
{
    public class Rooms
    {
        [Browsable(false)]
        public int Id { get; set; }

        [DisplayName("Номер в фонде")]
        public string Number { get; set; }

        [DisplayName("Этаж")]
        public string Layer { get; set; }

        [DisplayName("Тип номера")]
        public int Category { get; set; }

        [DisplayName("Занят?")]
        public string Locked { get; set; }
    }
}
