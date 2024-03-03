using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using AutoMapper;
using Dapper;
using Infraestructure.Persistence;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Net;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Bcpg.OpenPgp;
using MimeKit.Cryptography;
using ApplicationCore.DTOs.Logs;
using ApplicationCore.Commands.Logs;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Infraestructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public DashboardService(ApplicationDbContext dbContext, ICurrentUserService currentUserService, IMapper mapper)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Response<object>> GetData()
        {
            object list = new object();
            list = await _dbContext.comercial.ToListAsync();

            return new Response<object> ( list );
        }

        public async Task<Response<string>> GetIp()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress IpAddress = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            var IP = IpAddress?.ToString() ?? "No se pudo obtener la IP";
            return new Response<string> ( IP );
        }

        public async Task<Response<int>> CreateLogs(LogsDto request)
        {
            var IpAddress = await GetIp();
             var db = IpAddress.Message.ToString();
            var l = new LogsDto();
            l.Ip = db;
            l.Fecha = request.Fecha;
            l.Datos = request.Datos;
            l.Response = request.Response;
            l.NombreFuncion = request.NombreFuncion;

            var lo = _mapper.Map<Domain.Entities.Logs>(l);
            await _dbContext.logs.AddAsync(lo);
            await _dbContext.SaveChangesAsync();

            return new Response<int>(lo.Id, "Registro Creado");
        }

    }
}
