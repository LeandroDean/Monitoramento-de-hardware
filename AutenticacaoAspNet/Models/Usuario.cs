using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutenticacaoAspNet.Models
{
    [Table("Usuarios")] // nome da tabela que sera gerado no bd para armazenar as info dos usuarios
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]   //Quantidade de caractere, se quiserem mudar ...
        public string Nome { get; set; }

        [Required]
        [MaxLength(100)]
        public string Login { get; set; }

        [Required]
        [MaxLength(100)]
        public string Senha { get; set; }
    }
}