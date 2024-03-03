using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Commands.Logs;
using ApplicationCore.Wrappers;
using AutoMapper;
using Infraestructure.Persistence;
using MediatR;

namespace Infraestructure.EventHandlers.Comercial
{
    public class CreateLogsHandler : IRequestHandler<CreateLogsCommand, Response<int>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateLogsHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateLogsCommand request, CancellationToken cancellationToken)
        {
            var l = new CreateLogsCommand();
            l.Ip = request.Ip;
            l.Fecha = request.Fecha;
            l.Datos = request.Datos;
            l.Response = request.Response;
            l.NombreFuncion = request.NombreFuncion;

            var lo = _mapper.Map<Domain.Entities.Logs>(l);
            await _context.logs.AddAsync(lo);
            await _context.AddRangeAsync();
            await _context.SaveChangesAsync();

            return new Response<int>(lo.Id, "Registro Creado");
        }
    }
}

