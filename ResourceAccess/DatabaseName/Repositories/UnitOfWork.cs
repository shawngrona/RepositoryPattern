using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResourceAccess.DatabaseName.Context;
using System.Data.Common;
using ResourceAccess.DatabaseName.Entities;
using System.Data.Entity.Validation;

namespace ResourceAccess.DatabaseName.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DBContext _rrptdbContext;

        public UnitOfWork()
        {
            _rrptdbContext = new DBContext("<connection string>");
        }

        public UnitOfWork(DbConnection conn)
        {
            _rrptdbContext = new DBContext(conn);
        }

        private GenericRepository<SampleEntity> m_SampleEntityRepository;

        public GenericRepository<SampleEntity> SampleEntityRepository
        {
            get
            {
                return m_SampleEntityRepository ??
                       (m_SampleEntityRepository = new GenericRepository<SampleEntity>(_rrptdbContext));
            }
        }

        public void Save()
        {
            try
            {
                _rrptdbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            //_rrptdbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    _rrptdbContext.Dispose();
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
