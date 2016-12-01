using Benday.Presidents.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Benday.Presidents.Core.DataAccess
{
    public class SqlEntityFrameworkFeatureRepository :
            SqlEntityFrameworkRepositoryBase<Feature>, IFeatureRepository
    {
        public SqlEntityFrameworkFeatureRepository(
            IPresidentsDbContext context) : base(context)
        {

        }

        public void Delete(Feature deleteThis)
        {
            throw new NotImplementedException();
        }

        public IList<Feature> GetAll()
        {
            return (
                from temp in Context.Features
                select temp
                ).ToList();
        }

        public IList<Feature> GetByUsername(string username)
        {
            return (
                from temp in Context.Features
                where (temp.Username == username || temp.Username == String.Empty)
                select temp
                ).ToList();
        }

        public Feature GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Feature saveThis)
        {
            throw new NotImplementedException();
        }
    }
}
