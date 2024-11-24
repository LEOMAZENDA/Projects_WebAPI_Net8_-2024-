using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Projecto.ApiCatalogo.Models;

namespace Projecto.ApiCatalogo.DTOs.Mapping 
{
    public class ProductoDTOMappingProfile : Profile
    {
        public ProductoDTOMappingProfile()
        {
            CreateMap<Producto, ProductoDTO>().ReverseMap();
            // CreateMap<Categoria, CategoriaDTO>();
        }
        
    }
}