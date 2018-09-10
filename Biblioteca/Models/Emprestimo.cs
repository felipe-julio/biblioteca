using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Emprestimo
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid IdCliente { get; set; }

        public Cliente Cliente { get; set; }

        [Required]
        public Guid IdLivro { get; set; }

        public Livro Livro { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DataDeInicio { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DataDeDevolucao { get; set; }
    }
}
