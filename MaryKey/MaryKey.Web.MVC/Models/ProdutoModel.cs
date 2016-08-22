using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaryKey.Web.MVC.Models
{
    public class ProdutoModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Campo Nome obrigatório!")]
        public String Nome { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal ValorProduto { get; set; }

        [RegularExpression("([0-9]+)")]
        public int Quantidade { get; set; }

        [RegularExpression("([0-9]+)")]
        public int PercentualDesconto { get; set; }

        public decimal Lucro{ get; set; }

        public decimal ValorTotal { get; set; }

        public decimal GetValorCompra()
        {
            return this.ValorProduto - ((this.ValorProduto * this.PercentualDesconto) / 100);
        }

    }
}
