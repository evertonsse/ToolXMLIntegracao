using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolXMLIntegracao.Models
{
    [Table("empresas")]
    public class Empresa    {

        [Key]
        [Column("cnpj")]
        [StringLength(14)]
        public string CNPJ { get; set; } = string.Empty;

        [Column("razao_social")]
        [Required]
        [StringLength(200)]
        public string RazaoSocial { get; set; } = string.Empty;

        [Column("fantasia")]
        [StringLength(200)]
        public string? Fantasia { get; set; }

        [Column("status")]
        [Required]
        public bool Status { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        // Navegação
        public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
        public virtual ICollection<EmpresaContador> EmpresaContadores { get; set; } = new List<EmpresaContador>();
    }

}
