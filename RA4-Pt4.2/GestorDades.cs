using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RA4_Pt4._2
{
    public class GestorDades
    {
        private static GestorDades _instancia;
        public List<UsuariBase> LlistaUsuaris { get; set; }

        private GestorDades()
        {
            LlistaUsuaris = new List<UsuariBase>();
            InicialitzarDades();
        }

        public static GestorDades Instancia
        {
            get
            {
                if (_instancia == null) _instancia = new GestorDades();
                return _instancia;
            }
        }

        private void InicialitzarDades()
        {
            LlistaUsuaris.Add(new UsuariEstudiant { Id = 1, Nom = "Joan", Correu = "joan@itb.cat", NotaMitjana = 8.5 });
            LlistaUsuaris.Add(new UsuariAdministrador { Id = 2, Nom = "Marta", Correu = "marta@itb.cat", Departament = "IT" });
        }
    }
}
