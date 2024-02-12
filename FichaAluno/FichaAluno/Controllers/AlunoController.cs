using FichaAluno.Models.Domain;
using FichaAluno.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace FichaAluno.Controllers
{
    public class AlunoController : Controller
    {
        private RepositorioAluno repositorio;

        public AlunoController()
        {
            repositorio = new RepositorioAluno();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var alunos = repositorio.GetAll();
            return View(alunos);
        }

        [HttpGet]
        public IActionResult Formulario(int? id)
        {
            AlunoModel viewModel = null;

            if (id.HasValue)
            {
                viewModel = repositorio.Get("MATRICULA = @p0", new object[] { id });
                if (viewModel == null)
                {
                    return NotFound();
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Excluir(int? id)
        {
            // Busca o aluno pelo ID
            var aluno = repositorio.Get("MATRICULA = @p0", new object[] { id });

            // Verifica se o aluno existe
            if (aluno == null)
            {
                return NotFound();
            }

            // Exclui o aluno
            repositorio.Remove(aluno);

            // Redireciona para a página de lista de alunos
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Index(string opcaoBusca, string valorBusca)
        {
            IEnumerable<AlunoModel> alunos;

            if (opcaoBusca == "matricula")
            {
                //int matricula = int.Parse(valorBusca)
                alunos = repositorio.GetAllGetByMatricula(valorBusca);
            }
            else if (opcaoBusca == "nome")
            {
                alunos = repositorio.GetAllGetByNome(valorBusca);
            }
            else
            {
                alunos = repositorio.GetAll();
            }

            return View(alunos);
        }


        public IActionResult Formulario(AlunoModel aluno)
        {
            if (aluno != null)
            {

                if (aluno.MATRICULA > 0)
                {
                    repositorio.Update(aluno);
                    TempData["ShowSuccessModal"] = true;
                }
                else
                {
                    repositorio.Add(aluno);
                    TempData["ShowSuccessModal"] = true;
                }
                return View(aluno);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View(aluno);
        }

        
    }
            

            
        

}
