using Microsoft.AspNetCore.Mvc;

using RpgApi.Data;
using RpgApi.Models;
using RpgApi.Models.Enuns;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PersonagemController : ControllerBase {

        private readonly DataContext _context;
        
        public PersonagensController(DataContext context) {
            
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id) {

            try {
                Personagem p = await _context.Personagens.FirstOrDefaultAsync(pBusca => pBusca.Id == id);
                return Ok(p);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(Usuario u) {
            try {
                Usuario uRetornado = await _context.Usuarios.FirstOrDefaultAsync(x => x.Usarname == u.Usarname && x.Email == u.Email);

                if (uRetornado == null)
                    throw new Exception("Usuário não encontrado");
                
                return Ok(uRetornado);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() {
            try {
                List<Personagem> pList = await  _context.Personagens.ToListAsync();
                return Ok(pList);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Persongem novoPersonagem) {
            try {
                if (novoPersonagem.PontosVida > 100)
                    throw new Exception("Pontos de Vida não podem ser maior que 100!");

                await _context.Personagens.AddAsync(novoPersonagem);
                await _context.SaveChangesAsync();

                return Ok(novoPersonagem.Id);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Personagem novoPersonagem) {
            try {
                if (novoPersonagem.PontosVida > 100)
                    throw new Exception("Pontos de Vida não podem ser maior que 100!");
                
                _context.Personagens.Update(novoPersonagem);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch (Excetion ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try {
                Personagem pRemover = await _context.Personagens.FirstOrDefaultAsync(p => p.Id == id);

                _context.Personagens.Remove(pRemover);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}