using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolXMLIntegracao.Models
{
    [Table("contadores")]
    public class Contador    {

        [Key]       
        [Column("cpf_cnpj")]        
        [StringLength(14)]
        public string CpfCnpj { get; set; } = string.Empty;

        [Column("nome")]
        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;

        [Column("senha")]
        [Required]
        [StringLength(255)]
        public string Senha { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        // Navegação
        public virtual ICollection<EmpresaContador> EmpresaContadores { get; set; } = new List<EmpresaContador>();
    }
}
