using System;
using System.Collections.Generic;
using System.Linq;
using ResourceAccess.DatabaseName.Repositories;
using ResourceAccess.DatabaseName.Entities;

namespace DataAccess
{
    public class SampleDA 
    {
        private readonly IUnitOfWork m_UnitOfWork;

        public SampleDA()
        {
            m_UnitOfWork = new UnitOfWork();
        }
        public SampleDA(IUnitOfWork unitOfWork)
        {
            m_UnitOfWork = unitOfWork;
        }

        public List<SampleEntity> GetAll()
        {
            var results = m_UnitOfWork.SampleEntityRepository.Get();
            if (results == null)
            {
                return null;
            }

            var brokerFtp = results.OrderByDescending(x => x.theInt).ToList();
            return brokerFtp;
        }

        public SampleEntity Get(int id)
        {
            var result = m_UnitOfWork.SampleEntityRepository.GetByID(id);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public SampleEntity GetByString(string theString)
        {
            var result = m_UnitOfWork.SampleEntityRepository.Get(x => x.theString == theString).FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public SampleEntity AddOrUpdate(int id, SampleEntity sampleEntity)
        {
            var result = m_UnitOfWork.SampleEntityRepository.Get(x => x.theInt == id).FirstOrDefault();
            if (result == null)
            {
                result.theInt = sampleEntity.theInt;
                result.theString = sampleEntity.theString;
                m_UnitOfWork.SampleEntityRepository.Insert(result);
            }
            else
            {
                result.theString = sampleEntity.theString;
                m_UnitOfWork.SampleEntityRepository.Update(result);
            }
            m_UnitOfWork.Save();

            return result;
        }

        public bool Delete(int id)
        {
            m_UnitOfWork.SampleEntityRepository.Delete(id);
            m_UnitOfWork.Save();
            return true;
        }
    }
}
