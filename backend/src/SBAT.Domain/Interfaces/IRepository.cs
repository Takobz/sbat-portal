namespace SBAT.Domain.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        //add generic CRUD
    }

    public class EntityBase 
    {
        public int Id { get; private set; }
    }
}