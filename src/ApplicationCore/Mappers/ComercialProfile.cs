using ApplicationCore.Commands.Comercial;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Mappers
{
    public class ComercialProfile : Profile
    {
        public ComercialProfile() 
        {
            CreateMap<CreateComercialCommand, comercial>()
                .ForMember(x => x.pkcomercial, y => y.Ignore());
        }
    }
}
