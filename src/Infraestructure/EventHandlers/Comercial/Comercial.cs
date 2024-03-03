using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Commands.Comercial;
using ApplicationCore.DTOs.Logs;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using AutoMapper;
using Infraestructure.Persistence;
using Infraestructure.Services;
using MediatR;

namespace Infraestructure.EventHandlers.Comercial
{
    public class CreateComercialHandler : IRequestHandler<CreateComercialCommand, Response<int>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDashboardService _dashboardService;

        public CreateComercialHandler(ApplicationDbContext context, IMapper mapper, IDashboardService dashboardService)
        {
            _context = context;
            _mapper = mapper;
            _dashboardService = dashboardService;
        }

        public async Task<Response<int>> Handle(CreateComercialCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.nombre) || string.IsNullOrEmpty(request.apellido1) || string.IsNullOrEmpty(request.apellido2))
                {
                    throw new ArgumentException("Uno o más campos están vacíos.");
                }

                if (request.comisión <= 0)
                {
                    throw new ArgumentException("El valor de la comisión debe ser mayor que cero.");
                }

                var u = new CreateComercialCommand();

                u.nombre = request.nombre;
                u.apellido1 = request.apellido1;
                u.apellido2 = request.apellido2;
                u.comisión = request.comisión;

                var us = _mapper.Map<Domain.Entities.comercial>(u);
                await _context.comercial.AddAsync(us);
                await _context.SaveChangesAsync();

                var jsonData = JsonSerializer.Serialize(u);

                var log = new LogsDto();
                log.Fecha = DateTime.Now;
                log.Response = "200";
                log.NombreFuncion = "CreateComercial";
                log.Datos = jsonData;

                await _dashboardService.CreateLogs(log);

                return new Response<int>(us.pkcomercial, "Registro Creado");
            }
            catch (Exception ex)
            {
                var log = new LogsDto();

                log.Fecha = DateTime.Now;
                log.Response = "500";
                log.NombreFuncion = "CreateComercial";
                log.Datos = ex.Message;

                await _dashboardService.CreateLogs(log);
                return new Response<int>(0, $"[Error]: Al crear el registro: {ex.Message}");
            }
        }
    }
}
