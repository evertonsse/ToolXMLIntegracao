using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolXMLIntegracao.Models
{
    [Table("empresa_contador")]
    public class EmpresaContador {

        [Column("cnpj_empresa")]
        [StringLength(14)]
        public string CnpjEmpresa { get; set; } = string.Empty;

        [Column("cnpj_contador")]
        [StringLength(14)]
        public string CnpjContador { get; set; } = string.Empty;

        // Navegação
        [ForeignKey("CnpjEmpresa")]
        public virtual Empresa Empresa { get; set; } = null!;

        [ForeignKey("CnpjContador")]
        public virtual Contador Contador { get; set; } = null!;
    }
}
