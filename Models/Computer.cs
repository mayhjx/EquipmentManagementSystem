using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class Computer
    {
        public int ID { get; set; }

        public string IP { get; set; }

        public string Account { get; set; }

        public string password { get; set; }

        public string InstrumentID { get; set; }
        public Instrument instrument { get; set; }

    }
}