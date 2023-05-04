using Infraestructure.Core.Repository.Interface;
using Infraestructure.Entity.Models;

namespace Infraestructure.Core.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        IRepository<StudentEntity> StudentRepository { get; }

        Task<int> Save();
    }
}
