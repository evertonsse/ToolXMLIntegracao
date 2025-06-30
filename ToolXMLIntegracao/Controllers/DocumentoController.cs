using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToolXMLIntegracao.Models;

namespace ToolXMLIntegracao.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase {


        private readonly AppDbContext _dbContext; 

        public DocumentoController(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get() {
            
            return Ok("Documento endpoint is working.");
        }

        [HttpPost]
        public IActionResult Put([FromBody] Documento documento) {
            if (documento == null) {
                return BadRequest("Documento não pode ser nulo.");
            }
           
            if (string.IsNullOrEmpty(documento.ChaveAcesso) || string.IsNullOrEmpty(documento.CNPJ)) {
                return BadRequest("ChaveAcesso and CNPJ are required.");
            }


            try {
                _dbContext.Documentos.Update(documento);
                _dbContext.SaveChanges();
                return Ok("Documento salvo com sucesso");

            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

    }
}
