using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace task_slayer.Data.Entities
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        
        public ICollection<Tarefa> Tarefas { get; set; }
        public Usuario Usuario { get; set; }
        [ForeignKey("Usuario")]
        public string UsuarioId {get; set;}
    }
}