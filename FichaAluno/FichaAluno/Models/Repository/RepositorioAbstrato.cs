using FichaAluno.Data.DAO;
using FichaAluno.Models.Domain;

namespace FichaAluno.Models.Repository
{
    public abstract class RepositorioAbstrato<T> where T : class
    {
        protected DAO dao;

        public RepositorioAbstrato()
        {
            dao = new DAO();
        }

        public abstract void Add(T entity);
        public abstract void Remove(T entity);
        public abstract void Update(T entity);
        public abstract IEnumerable<T> GetAll();
        public abstract AlunoModel Get(string predicate, params object[] parameters);

    }
}