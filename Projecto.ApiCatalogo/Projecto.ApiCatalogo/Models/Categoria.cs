using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Projecto.ApiCatalogo.Validation;

namespace Projecto.ApiCatalogo.Models;
[Table("Categorias")]
public class Categoria
{
    public Categoria()
    {
        Productos = new Collection<Producto>();
    }
    [Key]
    public int CategoriaId  { get; set; }
    [Required]
    [StringLength(80)]
    [InicialMaiuscula] // Vaidação customizada
    public string? Nome  { get; set; }
    
    [Required]
    [StringLength(300)]
    [BindNever] //indiica que esta propriedade será ingorada no Bind
    public string? ImagemUrl  { get; set; }
    
    public DateTime DataCadastro { get; set; }
    
    [JsonIgnore]
    public ICollection<Producto> Productos { get; set; }
}