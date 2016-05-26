using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaryKey.Dominio
{
    public class Produto : EntidadeBase
    {
        public String Nome { get; set; }
        public decimal ValorProduto { get; set; }
        public int Quantidade { get; set; }
        public int PercentualDesconto { get; set; }


        public decimal GetValorCompra()
        {
            return this.ValorProduto - ((this.ValorProduto * this.PercentualDesconto) / 100); 
        }
    }
}
