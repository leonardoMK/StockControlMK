using System.Collections.Generic;

namespace MaryKey.Dominio
{
    public class Permissao : EntidadeBase
    {
        public const string ADMIN = "ADMIN";
        public const string OPERADOR = "OPERADOR";
        public string Nome { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }

    }

}