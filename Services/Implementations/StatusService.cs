using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task_slayer.Data.Repositories.Interfaces;
using task_slayer.Services.Interfaces;
using task_slayer.ViewModels;

namespace task_slayer.Services.Implementations
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
        public async Task<StatusViewModel[]> GetAllStatus()
        {
            var statuses = await _statusRepository.GetAllStatus();
            return [.. statuses.Select(e => new StatusViewModel
            {
                Id = e.Id,
                Nome = e.Nome
            })];
        }
    }
}