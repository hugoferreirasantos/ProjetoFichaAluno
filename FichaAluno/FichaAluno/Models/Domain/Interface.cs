
namespace FichaAluno.Models.Domain
{
    public interface Interface
    {
        int MATRICULA { set; get; }
        string NOME { set; get; }

        string CPF { set; get; }
        string NASCIMENTO { set; get; }
        EnumeradorSexo SEXO { set; get; }


    }
}
