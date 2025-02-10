using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using task_slayer.Data.Repositories.Interfaces;

namespace task_slayer.Data.Entities
{
    public class Usuario : IdentityUser
    {
        public ICollection<Tarefa> Tarefas { get; set; }
        public ICollection<Categoria> Categorias { get; set; }
    }
}