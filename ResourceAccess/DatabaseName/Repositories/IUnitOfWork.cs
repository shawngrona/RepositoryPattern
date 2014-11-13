using System;
using ResourceAccess.DatabaseName.Entities;

namespace ResourceAccess.DatabaseName.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<SampleEntity> SampleEntityRepository { get; }
 
        void Save();
    }
}
