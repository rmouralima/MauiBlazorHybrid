using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MauiBlazorHybrid.Data.Models
{
    public class Cadastro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(150, ErrorMessage = "O campo Nome deve ter no máximo 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Email é obrigatório.")]        
        [MaxLength(50, ErrorMessage = "O campo Email deve ter no máximo 50 caracteres.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
        [MaxLength(15, ErrorMessage = "O campo Telefone deve ter no máximo 15 caracteres.")]
        public string Telefone { get; set; } = string.Empty;
    }
}
