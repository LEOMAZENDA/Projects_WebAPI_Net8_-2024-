using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Projecto.ApiCatalogo.Validation;

namespace Projecto.ApiCatalogo.DTOs
{
    public class ProductoDTO
    {  
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório")]
        [StringLength(30, ErrorMessage = "Onome deve estar entre 3 a 20 caracteres", MinimumLength = 3)]
        [InicialMaiuscula] // Vaidação customizada
        public string? Nome { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "A descrição deve ter no maximo {1} caracteres")]
        public string? Descricao { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 10)]
        public string? ImagemUrl { get; set; }

        // public float Estoque { get; set; } 
        
        [Required]
        [Range(1, 10000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
        public decimal Preco { get; set; }
        public int CategotiaId { get; set; }
        
    }
}