using System;
using System.Collections.Generic;
using System.Text;

namespace Airports
{
    class Airline
    {
        private static int currentID = 1;

        public string CallSign { get; set; }
        public string IATACode { get; set; }
        public string ICAOCode { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }

        public Airline(int id, string name, string callSign, string IATACode, string ICAOCode)
        {
            Id = id == -1 ? currentID++ : id;
            Name = name;
            CallSign = callSign;
            this.IATACode = IATACode;
            this.ICAOCode = ICAOCode;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
