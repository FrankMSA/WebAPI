using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebAPI.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private List<Aluno> AlunosMatriculados { get; set; } = new List<Aluno>();
        public AlunosController()
        {
            Aluno aluno = new Aluno
            {
                Matricula = "1",
                Curso = "TI",
                Nome = "Batman",
                Semestre = 2
            };

            var aluno2 = new Aluno
            {
                Matricula = "2",
                Curso = "ADS",
                Nome = "Hulk",
                Semestre = 1
            };

            AlunosMatriculados.Add(aluno);
            AlunosMatriculados.Add(aluno2);
        }

        [HttpGet]
        public IActionResult GetAlunos()
        {
            return Ok(AlunosMatriculados);
        }

        [HttpGet("{matricula}")]
        public IActionResult GetAluno(string matricula)
        {
            List<Aluno> AlunoComMatricula = new List<Aluno>();

            AlunoComMatricula = AlunosMatriculados.Where(alunoMatriculado => alunoMatriculado.Matricula == matricula).ToList();

            if (AlunoComMatricula.Count > 0)
                return Ok(AlunoComMatricula[0]);
            else
                return BadRequest("Aluno não encontrado");
        }
        [HttpPost]
        public IActionResult CadastrarAluno(Aluno aluno)
        {
            var maior = AlunosMatriculados.Max(aluno => aluno.Matricula);
            int matricula = int.Parse(maior) + 1;
            aluno.Matricula = matricula.ToString();

            AlunosMatriculados.Add(aluno);

            return CreatedAtAction("GetAluno", new { matricula = aluno.Matricula }, AlunosMatriculados);
        }
        [HttpPut]
        public IActionResult AtualizarAluno(Aluno aluno, string matricula)
        {
            List<Aluno> listaFiltrada = AlunosMatriculados.Where(aluno => aluno.Matricula == matricula).ToList();
            if (listaFiltrada.Count == 0)
                return BadRequest("Aluno não encontrado para atualização");
            Aluno alunoAtualizar = listaFiltrada.First();

            alunoAtualizar.Curso = aluno.Curso;
            alunoAtualizar.Nome = aluno.Nome;
            alunoAtualizar.Semestre = aluno.Semestre;

            return Ok(alunoAtualizar);
        }
        [HttpDelete]
        public IActionResult DeletarAluno(string matricula)
        {
            List<Aluno> listaFiltrada = AlunosMatriculados.Where(aluno => aluno.Matricula == matricula).ToList();

            if(listaFiltrada.Count == 0)
                return BadRequest("Aluno não encontrado para remoção");
            Aluno alunoAtualizar = listaFiltrada.First();
            AlunosMatriculados.Remove(alunoAtualizar);

            return Ok(AlunosMatriculados);
        }
    }
}
