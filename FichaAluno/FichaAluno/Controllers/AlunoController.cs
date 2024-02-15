using FichaAluno.Models.Domain;
using FichaAluno.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            try
            {
                var alunos = repositorio.GetAll();
                return View(alunos);
            }
            catch (Exception ex)
            {
                var erro = new ErrorModel { ErrorMessage = ex.Message };
                ModelState.AddModelError("", erro.ErrorMessage);
            }

            return View();
        }

        [HttpGet]
        public IActionResult Formulario(int? id)
        {
            try
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
            catch (Exception ex)
            {
                var erro = new ErrorModel { ErrorMessage = ex.Message };
                ModelState.AddModelError("", erro.ErrorMessage);
            }

            return View();

        }

        [HttpGet]
        public IActionResult Excluir(int? id)
        {
            try
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
            catch (Exception ex)
            {
                var erro = new ErrorModel { ErrorMessage = ex.Message };
                ModelState.AddModelError("", erro.ErrorMessage);
            }

            return View();
        }


        [HttpPost]
        public IActionResult Index(string opcaoBusca, string valorBusca)
        {
            IEnumerable<AlunoModel> alunos;

            if (string.IsNullOrEmpty(valorBusca))
            {
                alunos = repositorio.GetAll();
                return RedirectToAction("Index");
            }
            else if (opcaoBusca == "matricula")
            {
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
                try
                {
                    ValidacaoCPF validacaoCPF = new ValidacaoCPF();

                    if (aluno.CPF == null)
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
                    else if(!validacaoCPF.ValidarCPF(aluno.CPF))
                    {

                        TempData["ShowErrorModal"] = true;
                        return View(aluno);

                    }
                    else
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
                    
                }
                catch (Exception ex)
                {
                    var erro = new ErrorModel { ErrorMessage = ex.Message };
                    ModelState.AddModelError("", erro.ErrorMessage);
                    TempData["ShowErrorModal"] = true;
                }
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
