using System;
using System.Collections.Generic;
using System.Text;

namespace martinopapa_bot
{
    class ProntoSoccorso
    {
        public string nome { get; set; }
        public string codice { get; set; }
        public Attesa attesa { get; set; }

        public ProntoSoccorso(string nome, string codice, Attesa attesa)
        {
            this.nome = nome;
            this.codice = codice;
            this.attesa = attesa;
        }
    }
}
