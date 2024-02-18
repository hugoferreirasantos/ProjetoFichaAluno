using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FichaAluno.Models.Domain
{
    public class AlunoModel : Interface
    {
        public int MATRICULA { set; get; }

        [Required(ErrorMessage = "Digite um nome!")]
        public string NOME { set; get; }
        public string? CPF { set; get; }

        [Required(ErrorMessage = "Digite uma data de nascimento!")]
        public DateTime NASCIMENTO { set; get; }

        public string CPF_FORMATADO { get { return CPF.FormatarCPF(); } }

        public string NASCIMENTO_FORMATADO { get { return NASCIMENTO.ToString("dd/MM/yyyy"); } }

        public EnumeradorSexo? SEXO { set; get; }

        DateTime Interface.NASCIMENTO { get => NASCIMENTO; set => NASCIMENTO = value; }
        EnumeradorSexo Interface.SEXO { get => SEXO.Value; set => SEXO = value; }
    }
}

public static class StringExtensions
{
    public static string FormatarCPF(this string cpf)
    {
        if (string.IsNullOrEmpty(cpf))
            return "";

        var cpfNumeros = Regex.Replace(cpf, @"\D", ""); 

        if (cpfNumeros.Length != 11)
            return "";


        return $"{cpfNumeros.Substring(0, 3)}.{cpfNumeros.Substring(3, 3)}.{cpfNumeros.Substring(6, 3)}-{cpfNumeros.Substring(9, 2)}";
    }
}
