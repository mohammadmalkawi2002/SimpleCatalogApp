using AutoMapper;
using SimpleCatalog.Application.DTOs;
using SimpleCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCatalog.Application.Mappings
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
           // DTOs ==>Entity
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            //Entity ==> Dto 
            CreateMap<Category, CategoryDto>();

        }
    }
}
