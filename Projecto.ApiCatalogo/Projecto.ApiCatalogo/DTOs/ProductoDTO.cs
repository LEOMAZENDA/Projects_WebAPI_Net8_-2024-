using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecto.ApiCatalogo.DTOs
{
    public class ProductoDTO
    {
        public int ProductoId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public string? ImagemUrl { get; set; }
        public decimal Preco { get; set; }
        public int CategotiaId { get; set; }
        // public float Estoque { get; set; }
    }
}