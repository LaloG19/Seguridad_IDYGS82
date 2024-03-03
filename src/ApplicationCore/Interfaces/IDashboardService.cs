using ApplicationCore.DTOs;
using ApplicationCore.DTOs.Logs;
using ApplicationCore.Wrappers;

namespace ApplicationCore.Interfaces
{
    public interface IDashboardService
    {
        Task<Response<object>> GetData();
        Task<Response<string>> GetIp();
        Task<Response<int>> CreateLogs(LogsDto request);
    }
}
