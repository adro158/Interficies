namespace RA4_Pt4._2
{
    public abstract class UsuariBase
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Correu { get; set; }
    }

    public class UsuariEstudiant : UsuariBase
    {
        public double NotaMitjana { get; set; }
    }

    public class UsuariAdministrador : UsuariBase
    {
        public string Departament { get; set; }
    }
}