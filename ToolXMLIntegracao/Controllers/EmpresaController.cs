using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToolXMLIntegracao.Models;

namespace ToolXMLIntegracao.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase {

        private readonly AppDbContext _dbContext;

        public EmpresaController(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        [HttpPost] 
        public IActionResult Post([FromBody] Empresa empresa) {
            if (empresa == null) {
                return BadRequest("Empresa não pode ser nula.");
            }
            if (string.IsNullOrEmpty(empresa.CNPJ)) {
                return BadRequest("CNPJ é obrigatório.");
            }
            try {
                _dbContext.Empresas.Add(empresa);
                _dbContext.SaveChanges();
                return Ok("Empresa salva com sucesso");
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
