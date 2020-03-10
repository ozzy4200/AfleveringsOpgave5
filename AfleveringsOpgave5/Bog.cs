using System;
using System.Collections.Generic;
using System.Text;

namespace AfleveringsOpgave5
{
    public class Bog
    {
        //Instance Field
        private string _titel;
        private string _forfatter;
        private int _sideTal;
        private string _isbn13;

        //Konstruktør
        public Bog(string titel, string forfatter, int sideTal, string isbn13)
        {
            titel = titel;
            forfatter = forfatter;
            sideTal = sideTal;
            Isbn13 = isbn13;
        }

        public Bog()
        {

        }

        //Properties
        public string Titel
        {
            get => _titel;
            set
            {
                if (value == null) throw new NullReferenceException("Bogen skal have en titel");
                _titel = value;
            }
        }

        public string Forfatter
        {
            get => _forfatter;
            set
            {
                if (value == null) throw new NullReferenceException("Skriv et navn");
                if (value.Length < 2) throw new ArgumentException("Forfatter navn skal være minimum 2 tegn langt");
                _forfatter = value;
            }

        }

        public int SideTal
        {
            get => _sideTal;
            set
            {
                if (value <= 4) throw new ArgumentOutOfRangeException("Bogen er for kort");
                if (value >= 1000) throw new ArgumentOutOfRangeException("Bogen er for lang");
                _sideTal = value;
            }
        }

        public string Isbn13
        {
            get => _isbn13;

            set

            {
                _isbn13 = value;
                if (value.Length == 13) _isbn13 = value;
                else if (value.Length != 13) throw new ArgumentException("ISBN skal være 13 cifre");

            }

        }

        public override string ToString()
        {
            return "Bogens titel er" + "" + Titel + "" + "Skrevet af" + "" + Forfatter + "" + "Bogen har" + "" +
                   SideTal + "" + "sider" + "" + "ISBN nummeret er" + "" + Isbn13;
        }
    }
}

