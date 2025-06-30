using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolXMLIntegracao.Models
{
    [Table("documentos")]
    public class Documento    {

        [Key]
        [Column("chave_acesso")]
        [StringLength(44)]
        public string? ChaveAcesso { get; set; }

        [Column("cnpj")]
        [Required]
        [StringLength(14)]
        public string CNPJ { get; set; } = string.Empty;

        [Column("tipo")]
        [Required]
        [StringLength(2)]
        public string Tipo { get; set; } = string.Empty;

        [Column("conteudo")]
        public string? Conteudo { get; set; }

        [Column("data_autorizacao")]
        public DateTime? DataAutorizacao { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        
        [ForeignKey("CNPJ")]
        public virtual Empresa Empresa { get; set; } = null!;
    }
}
