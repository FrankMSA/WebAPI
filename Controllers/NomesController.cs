using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NomesController : ControllerBase
    {
        List<string> Nomes = new List<string>()
        {
            "batman",
            "aquaman",
            "hulk"
        };

        [HttpGet]
        [Route("Nomes")]
        public IActionResult getNomes()
        {
            return Ok(Nomes);
        }

        [HttpGet("Nome/{id}")]
        public IActionResult getNome(string id)
        {
            for (int i = 0; i < Nomes.Count; i++)
            {
                if (Nomes[i] == id)
                    return Ok(Nomes[i]);
            }

            return BadRequest("Nome não cadastrado");
        }

        [HttpPost]
        public IActionResult CadastrarNome(string nome)
        {
            Nomes.Add(nome);
            return CreatedAtAction("getNome", new { id = nome }, nome );
        }

        [HttpPut]
        public IActionResult AlterarNome(string nomeAtual, string novoNome)
        {
            for (int i = 0; i < Nomes.Count; i++)
            {
                if (Nomes[i] == nomeAtual)
                {
                    Nomes[i] = novoNome;
                    return Ok(Nomes);
                }
            }
            
            return BadRequest("Não foi encontrado nome para alteração");
        }

        [HttpDelete]
        public IActionResult DeletarNome(string nome)
        {
            if(Nomes.Contains(nome))
            {
                Nomes.Remove(nome);
                return Ok(Nomes);
            }

                return BadRequest("Nome não encontrado");


        }
    }
}
