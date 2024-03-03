using ApplicationCore.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTOs.Comercial;

namespace ApplicationCore.Commands.Comercial
{
    public class CreateComercialCommand : ComercialDto, IRequest<Response<int>>
    {
    }
}
