using System.ComponentModel.DataAnnotations;

namespace FichaAluno.Models.Domain
{
    public class AlunoModel : Interface
    {
        public int MATRICULA { set; get; }

        [Required(ErrorMessage = "Digite um nome!")]
        public string NOME { set; get; }
        public string? CPF { set; get; }

        public DateTime NASCIMENTO { set; get; }

        public string NASCIMENTO_FORMATADO { get { return NASCIMENTO.ToString("dd/MM/yyyy"); } }
        public EnumeradorSexo? SEXO { set; get; }

        // Implemente esses métodos conforme necessário
        DateTime Interface.NASCIMENTO { get => NASCIMENTO; set => NASCIMENTO = value; }
        EnumeradorSexo Interface.SEXO { get => SEXO.Value; set => SEXO = value; }
    }
}
