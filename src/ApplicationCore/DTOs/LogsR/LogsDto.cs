using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs.Logs
{
	public class LogsDto
    {
        public int? Id { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreFuncion { get; set; }
        public string Ip { get; set; }
        public string Datos { get; set; }
        public string Response { get; set; }
    }
}
