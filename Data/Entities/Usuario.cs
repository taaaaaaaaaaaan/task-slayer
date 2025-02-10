using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace task_slayer.Data.Entities
{
    public class Usuario : IdentityUser
    {
        public ICollection<Tarefa> Tarefas { get; set; }
    }
}