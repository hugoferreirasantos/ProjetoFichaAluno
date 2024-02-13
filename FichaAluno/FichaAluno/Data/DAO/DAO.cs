using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Services;

namespace FichaAluno.Data.DAO
{
    public class DAO
    {
        private static string pUser = "SYSDBA";
        private static string pPassword = "masterkey";
        private static string pDatabase = "localhost:D:\\Ambiente_Desenvolvimento\\Aulas_C#\\ProjetoCadastroAluns\\FichaAluno\\FichaAluno\\Dados\\DBESCOLAR.FDB";
        private static string pDataSource = "localhost";
        private static int pPort = 3050;
        private static int pDialet = 3;
        private static string pCharset = FbCharset.Utf8.ToString();

        public FbConnection connection;

        public bool bconexao { get; set; }

        public DAO()
        {
            FbConnectionStringBuilder stringconnection = new FbConnectionStringBuilder()
            {
                Port = pPort,
                UserID=pUser,
                Password=pPassword,
                Database=pDatabase,
                Dialect=pDialet,
                Charset=pCharset
            };

            try
            {
                connection = new FbConnection(stringconnection.ToString());
                connection.Open();
                bconexao = true;
            }catch (Exception ex)
            {
                bconexao = false;
            }

        }


    }
}
