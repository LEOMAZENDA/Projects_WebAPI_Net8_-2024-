using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Projecto.ApiCatalogo.Validation;

namespace Projecto.ApiCatalogo.Models;

[Table("Productos")]
public class Producto : IValidatableObject
{
    [Key] 
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

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(1, 10000,ErrorMessage = "O preço deve estar entre {1} e {2}")]
    public decimal Preco { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategotiaId { get; set; }
    [JsonIgnore]
    public Categoria? Categotia { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(this.Descricao))
        {
            var primeiraLetra = this.Descricao[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper()) {
                yield return new 
                    ValidationResult("A Primeira letra de descrição deve ser  maiúscula",
                        new[]
                        { nameof(this.Descricao)} 
                        );
            }
        }

        if (this.Estoque <= 0)
        {
            yield return new 
                ValidationResult("O estoque deve ser maior do que zero (0)",
                    new[]
                        { nameof(this.Nome)} 
                );
        }
    }
}