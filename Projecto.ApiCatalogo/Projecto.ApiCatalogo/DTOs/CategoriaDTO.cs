using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Projecto.ApiCatalogo.Validation;

namespace Projecto.ApiCatalogo.DTOs
{
    public class CategoriaDTO
    {   
    public int CategoriaId  { get; set; }
    public string? Nome  { get; set; }
    public string? ImagemUrl  { get; set; }
    
    public DateTime DataCadastro { get; set; }

    }
}