using FichaAluno.Models.Domain;
using FirebirdSql.Data.FirebirdClient;
using System.Globalization;
using System.Linq.Expressions;
using System.Security;
using System.Security.Cryptography;

namespace FichaAluno.Models.Repository
{
    public class RepositorioAluno : RepositorioAbstrato<AlunoModel>
    {
        public override void Add(AlunoModel entity)
        {
            string insertSQL = "INSERT INTO TBALUNO(NOME,CPF,SEXO,DTNASCIMENTO) VALUES(@Nome, @Cpf, @Sexo, @Nascimento)";

            using(FbCommand cmd = new FbCommand(insertSQL, dao.connection))
            {
                cmd.Parameters.AddWithValue("@Nome", entity.NOME);
                if (entity.CPF != null)
                {
                    cmd.Parameters.AddWithValue("@Cpf", entity.CPF);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Cpf", DBNull.Value);
                }
                string nascimentoFormatado = entity.NASCIMENTO.ToString("yyyyMMdd");
                int nascimentoAsInt = int.Parse(nascimentoFormatado);
                cmd.Parameters.AddWithValue("@Nascimento", nascimentoAsInt);
                cmd.Parameters.AddWithValue("@Sexo", entity.SEXO);

                cmd.ExecuteNonQuery();

            }

        }

        public override void Remove(AlunoModel entity)
        {
            string removeSQL = "DELETE FROM TBALUNO WHERE MATRICULA = @Matricula";

            using (FbCommand cmd = new FbCommand(removeSQL, dao.connection))
            {
                cmd.Parameters.AddWithValue("@Matricula", entity.MATRICULA);
                cmd.ExecuteNonQuery();
            }
            
        }

        public override void Update(AlunoModel entity)
        {
            string updateSQL = "UPDATE TBALUNO SET NOME = @Nome, CPF = @Cpf, SEXO = @Sexo, DTNASCIMENTO = @Nascimento WHERE MATRICULA = @Matricula";

            using (FbCommand cmd = new FbCommand(updateSQL, dao.connection))
            {
                cmd.Parameters.AddWithValue("@Nome", entity.NOME);
                if(entity.CPF != null)
                {
                    cmd.Parameters.AddWithValue("@Cpf", entity.CPF);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Cpf", DBNull.Value);
                }
                if(entity.NASCIMENTO != null)
                {
                    string nascimentoFormatado = entity.NASCIMENTO.ToString("yyyyMMdd");
                    int nascimentoAsInt = int.Parse(nascimentoFormatado);
                    cmd.Parameters.AddWithValue("@Nascimento", nascimentoAsInt);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Nascimento", DBNull.Value);
                }
                
                cmd.Parameters.AddWithValue("@Sexo", entity.SEXO);
                cmd.Parameters.AddWithValue("@Matricula", entity.MATRICULA);

                cmd.ExecuteNonQuery();
            }

        }

        public override IEnumerable<AlunoModel> GetAll()
        {
            string selectSQL = "SELECT MATRICULA,NOME,SEXO,DTNASCIMENTO,CPF FROM TBALUNO A ORDER BY NOME";

            List<AlunoModel> alunos = new List<AlunoModel>();

            using (FbCommand cmd = new FbCommand(selectSQL, dao.connection))
            {
                using (FbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string dtnascimentoString = reader.GetString(reader.GetOrdinal("DTNASCIMENTO"));

                        AlunoModel aluno = new AlunoModel()
                        {
                            MATRICULA = reader.GetInt32(reader.GetOrdinal("MATRICULA")),
                            NOME = reader.IsDBNull(reader.GetOrdinal("NOME")) ? null : reader.GetString(reader.GetOrdinal("NOME")),
                            CPF = reader.IsDBNull(reader.GetOrdinal("CPF")) ? null : reader.GetString(reader.GetOrdinal("CPF")),
                            NASCIMENTO = DateTime.ParseExact(dtnascimentoString, "yyyyMMdd", CultureInfo.InvariantCulture),
                            SEXO = reader.IsDBNull(reader.GetOrdinal("SEXO")) ? null : (EnumeradorSexo)reader.GetInt32(reader.GetOrdinal("SEXO"))
                        };

                        

                        alunos.Add(aluno);
                    }
                }
            }

            return alunos;
        }

        public override AlunoModel Get(string predicate, params object[] parameters)
        {
            string selectSQL = "SELECT MATRICULA,NOME,SEXO,DTNASCIMENTO,CPF FROM TBALUNO A WHERE " + predicate;

            AlunoModel aluno = null;

            using (FbCommand cmd = new FbCommand(selectSQL, dao.connection))
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@p" + i, parameters[i]);
                }

                using (FbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string dataComoString = reader.GetInt32(reader.GetOrdinal("DTNASCIMENTO")).ToString();
                        aluno = new AlunoModel()
                        {
                            MATRICULA = reader.GetInt32(reader.GetOrdinal("MATRICULA")),
                            NOME = reader.IsDBNull(reader.GetOrdinal("NOME")) ? null : reader.GetString(reader.GetOrdinal("NOME")),
                            CPF = reader.IsDBNull(reader.GetOrdinal("CPF")) ? null : reader.GetString(reader.GetOrdinal("CPF")),
                            NASCIMENTO = DateTime.ParseExact(dataComoString, "yyyyMMdd", CultureInfo.InvariantCulture),
                            SEXO = reader.IsDBNull(reader.GetOrdinal("SEXO")) ? null : (EnumeradorSexo)reader.GetInt32(reader.GetOrdinal("SEXO"))
                        };
                    }
                }
            }

            return aluno;
        }

        public IEnumerable<AlunoModel> GetAllGetByMatricula(string matricula)
        {
            string selectSQL = "SELECT MATRICULA,NOME,SEXO,DTNASCIMENTO ,CPF  FROM TBALUNO A WHERE CAST(MATRICULA AS VARCHAR(20)) LIKE @matricula ORDER BY NOME";

            List<AlunoModel> alunos = new List<AlunoModel>();

            using (FbCommand cmd = new FbCommand(selectSQL, dao.connection))
            {
                cmd.Parameters.AddWithValue("@matricula", "%" + matricula + "%");

                using (FbDataReader reader = cmd.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        string dtnascimentoString = reader.GetString(reader.GetOrdinal("DTNASCIMENTO"));
                        AlunoModel aluno = new AlunoModel()
                        {
                            MATRICULA = reader.GetInt32(reader.GetOrdinal("MATRICULA")),
                            NOME = reader.IsDBNull(reader.GetOrdinal("NOME")) ? null : reader.GetString(reader.GetOrdinal("NOME")),
                            CPF = reader.IsDBNull(reader.GetOrdinal("CPF")) ? null : reader.GetString(reader.GetOrdinal("CPF")),
                            NASCIMENTO = DateTime.ParseExact(dtnascimentoString, "yyyyMMdd", CultureInfo.InvariantCulture),
                            SEXO = reader.IsDBNull(reader.GetOrdinal("SEXO")) ? null : (EnumeradorSexo)reader.GetInt32(reader.GetOrdinal("SEXO"))
                        };

                        alunos.Add(aluno);
                    }
                }
            }

            return alunos;
        }

        public IEnumerable<AlunoModel> GetAllGetByNome(string nome)
        {
            string selectSQL = "SELECT MATRICULA,NOME,SEXO,DTNASCIMENTO ,CPF  FROM TBALUNO A WHERE CAST(NOME AS VARCHAR(100)) LIKE @nome ORDER BY NOME";

            List<AlunoModel> alunos = new List<AlunoModel>();

            using (FbCommand cmd = new FbCommand(selectSQL, dao.connection))
            {
                cmd.Parameters.AddWithValue("@nome", "%" + nome + "%");

                using (FbDataReader reader = cmd.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        string dtnascimentoString = reader.GetString(reader.GetOrdinal("DTNASCIMENTO"));
                        AlunoModel aluno = new AlunoModel()
                        {
                            MATRICULA = reader.GetInt32(reader.GetOrdinal("MATRICULA")),
                            NOME = reader.IsDBNull(reader.GetOrdinal("NOME")) ? null : reader.GetString(reader.GetOrdinal("NOME")),
                            CPF = reader.IsDBNull(reader.GetOrdinal("CPF")) ? null : reader.GetString(reader.GetOrdinal("CPF")),
                            NASCIMENTO = DateTime.ParseExact(dtnascimentoString, "yyyyMMdd", CultureInfo.InvariantCulture),
                            SEXO = reader.IsDBNull(reader.GetOrdinal("SEXO")) ? null : (EnumeradorSexo)reader.GetInt32(reader.GetOrdinal("SEXO"))
                        };

                        alunos.Add(aluno);
                    }
                }
            }

            return alunos;
        }



        internal AlunoModel? GetSingle(string v, object[] objects)
        {
            throw new NotImplementedException();
        }
    }
}
