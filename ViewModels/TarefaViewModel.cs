using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task_slayer.ViewModels
{
    public class TarefaViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataConclusao { get; set; }
        public StatusViewModel Status { get; set; }
        public CategoriaViewModel Categoria { get; set; }
    }
}