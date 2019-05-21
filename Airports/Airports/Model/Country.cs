using Newtonsoft.Json;
using System;

namespace Airports
{
    class Country
    {
        private static int currentID = 1;

        public int Id { get; set; }
        public string Name { get; set; }
        public string ThreeLetterISOCode { get; set; }
        public string TwoLetterISOCode { get; set; }

        [JsonConstructor]
        public Country(int id, string name, string threeLetterCode, string twoLetterCode)
        {
            Id = id;
            Name = name;
            ThreeLetterISOCode = threeLetterCode;
            TwoLetterISOCode = twoLetterCode;
        }
        public Country(string name, string threeLetterCode, string twoLetterCode)
        {
            Id = currentID++;
            Name = name;
            ThreeLetterISOCode = threeLetterCode;
            TwoLetterISOCode = twoLetterCode;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
