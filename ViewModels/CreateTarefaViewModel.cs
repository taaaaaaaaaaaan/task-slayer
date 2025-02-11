using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task_slayer.ViewModels
{
    public class CreateTarefaViewModel
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataConclusao { get; set; }
        public int StatusId { get; set; }
        public int CategoriaId { get; set; }
        public string UsuarioId { get; set; }
    }
}