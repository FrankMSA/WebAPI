using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NomesController : ControllerBase
    {
        List<string> Nomes = new List<string>()
        {
            "Batman",
            "Aquaman",
            "Flash"
        };

        [HttpGet]
        [Route("Nomes")]
        public IActionResult getNomes()
        {
            return Ok(Nomes);
        }

        [HttpGet("{id}")]
        public IActionResult getNome(string id)
        {
            for (int i = 0; i < Nomes.Count; i++)
            {
                if (Nomes[i] == id)
                {
                    return Ok(Nomes[i]);
                }
            }
            return NotFound();
        }

        [HttpPost]

        public IActionResult CadastrarNome(string nome)
        {
            Nomes.Add(nome);

            return CreatedAtAction("getNome", new { id = nome}, nome );
        }

        [HttpPut]

        public IActionResult AlterarNome(string nome, string novoNome)
        {
            for (int i = 0; i<Nomes.Count; i++)
            {
                if (Nomes[i] == nome)
                {
                    Nomes[i] = novoNome;
                    return Ok(Nomes);
                }
            }
            return BadRequest("Não foi encontrado o nome para alteração");
        }

        [HttpDelete]
        public IActionResult DeletarNome(string Nome)
        {
            if(Nomes.Contains(Nome))
            {
                Nomes.Remove(Nome);
                return Ok(Nomes);
            }
            return BadRequest("Nome não encontrado.");
        }
    }
}
