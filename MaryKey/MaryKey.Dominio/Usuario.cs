using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaryKey.Dominio
{
    public class Usuario : EntidadeBase
    {
        public String Login { get; set; }
        public String Senha { get; set; }
        public ICollection<Permissao> Permissoes { get; set; }
    }
}
