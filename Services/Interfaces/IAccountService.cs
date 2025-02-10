using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task_slayer.Data.Entities;

namespace task_slayer.Services.Interfaces
{
    public interface IAccountService
    {
        Task<string> GenerateJWToken(Usuario user);
    }
}