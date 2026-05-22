using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class TarefasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefasController(AppDbContext context)
        {
            _context = context;
        }

        // 1.GET: api/tarefas 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefas()
        {
            return await _context.Tarefas.ToListAsync();
        }

        // 2. GET: api/tarefas/{id} 
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> GetTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
            {
                return NotFound(new { mensagem = "Tarefa não encontrada." });
            }

            return tarefa;
        }

        // 3. POST: api/tarefas 
        [HttpPost]
        public async Task<ActionResult<Tarefa>> PostTarefa(Tarefa tarefa)
        {
            // Garante que a data de criação seja a data atual do servidor
            tarefa.DataCriacao = System.DateTime.Now;
            
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            // Retorna o HTTP 201 (Created) e indica onde encontrar o recurso criado
            return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.Id }, tarefa);
        }

        // 4. PUT: api/tarefas/{id} 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarefa(int id, Tarefa tarefa)
        {
            if (id != tarefa.Id)
            {
                return BadRequest(new { mensagem = "O ID informado não coincide com a tarefa." });
            }

            _context.Entry(tarefa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarefaExiste(id))
                {
                    return NotFound(new { mensagem = "Tarefa não encontrada para atualização." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // HTTP 204 (Sucesso sem conteúdo de retorno)
        }

        // 5. DELETE: api/tarefas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound(new { mensagem = "Tarefa não encontrada para exclusão." });
            }

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return NoContent(); // HTTP 204
        }

        // Método auxiliar interno para verificar existência
        private bool TarefaExiste(int id)
        {
            return _context.Tarefas.Any(e => e.Id == id);
        }
    }
}