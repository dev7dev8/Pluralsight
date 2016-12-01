using Benday.Presidents.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Benday.Presidents.Core.DataAccess
{
    public interface IFeatureRepository : IRepository<Feature>
    {
        IList<Feature> GetByUsername(string username);
    }
}
