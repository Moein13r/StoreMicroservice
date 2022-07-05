using AutoMapper;
using Products.DTOs;
using Products.Models;

namespace Products.Profiles
{
    public class ProdcutsProfile:Profile
    {
        public ProdcutsProfile()
        {
            //source -> destination
            CreateMap<ProductCreate ,Product>();
            CreateMap<ProductUpdate ,Product>();
            CreateMap<ProductPublishedDto ,Product>();
            CreateMap<ProductCreate ,ProductPublishedDto>();
        }
    }
}