using System.ComponentModel.DataAnnotations;

namespace FichaAluno.Models.Domain
{
    public class AlunoModel : Interface
    {
        public int MATRICULA { set; get; }

        [Required(ErrorMessage = "Digite um nome!")]
        public string NOME { set; get; }
        public string? CPF { set; get; }

        public string? NASCIMENTO { set; get; }

        //public string NASCIMENTO_FORMATADO { get { return NASCIMENTO?.ToString("dd/MM/yyyy"); } }
        public EnumeradorSexo? SEXO { set; get; }
        //DateTime Interface.NASCIMENTO { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        EnumeradorSexo Interface.SEXO { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        
    }
}
