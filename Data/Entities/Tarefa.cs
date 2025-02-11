using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace task_slayer.Data.Entities
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Titulo { get; set; }
        
        [StringLength(1000)]
        public string Descricao { get; set; }
        
        [Required]
        public DateTime DataConclusao { get; set; }
        
        [Required]
        public Status Status { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        
        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        
        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }
        [MaxLength(20)]
        [MinLength(20)]
        [Required]
        public short Dificuldade {get;set;}

        public Categoria Categoria { get; set; }
        public bool IsDeleted {get;set;}
    }
}