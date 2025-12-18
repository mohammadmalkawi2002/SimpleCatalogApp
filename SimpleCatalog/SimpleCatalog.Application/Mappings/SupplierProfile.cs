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
    public class SupplierProfile:Profile
    {
        public SupplierProfile()
        {
            //Dto ==> Entity
            CreateMap<CreateSupplierDto, Supplier>();
            CreateMap<UpdateSupplierDto, Supplier>();

            //Entity ==> Dto
            CreateMap<Supplier,SupplierDto>();
        }
    }
}
