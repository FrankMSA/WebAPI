using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

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
    }
}
