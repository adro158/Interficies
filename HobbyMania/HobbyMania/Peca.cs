using System;

namespace HobbyMania
{
    public class Peca
    {
        public string Tipus { get; set; }
        public DateTime DataDeteccio { get; set; }

        public Peca(string tipus)
        {
            Tipus = tipus;
            DataDeteccio = DateTime.Now;
        }
    }
}