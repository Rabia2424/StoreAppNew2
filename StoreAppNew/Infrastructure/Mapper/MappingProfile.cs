using AutoMapper;
using Entities.Dto;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace StoreAppNew2.Infrastructure.Mapper
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<ProductDtoForInsertion, Product>();   
            CreateMap<ProductDtoForUpdate, Product>().ReverseMap();
            CreateMap<UserDtoForCreation, IdentityUser>();
            CreateMap<RegisterDto, IdentityUser>();
        }
    }
}
