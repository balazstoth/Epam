namespace Airports
{
    class Country
    {
        private static int currentID = 1;

        public int Id { get; set; }
        public string Name { get; set; }
        public string ThreeLetterISOCode { get; set; }
        public string TwoLetterISOCode { get; set; }

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

        public static void ResetStaticID()
        {
            Country.currentID = 1;
        }
    }
}
