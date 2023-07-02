using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DONEMPROGRAMLAYICI
{
    [Serializable]
    public class CustomExeption : Exception
    {
        public CustomExeption()
        { }

        public CustomExeption(string message)
            : base(message)
        { }

        public CustomExeption(string nesne, int altDeger, int ustDeger)
            : base($"{nesne} değeri {altDeger}-{ustDeger} arasında olmalı.")
        { }


    }
}
