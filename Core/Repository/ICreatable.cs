namespace Core.Repository
{
    public interface ICreatable<T>
    {
        public void Create(T entity);    
    }

    public interface IUpdatable<T>
    {
        public void Update(T entity);
    }

    public interface IDeletable
    {
        public void Delete(int id);
    }

    public interface IDetailsable<T>
    {
        public T Details(int id);
    }

    public interface ICruddable<T> : IDetailsable<T>, ICreatable<T>, IUpdatable<T>, IDeletable, IExistable
    { 
    }

    public interface IExistable
    {
        public bool Exists(int id);
    }

    public interface ISaveable
    {
        public bool Save();
    }

}
