using Benday.Presidents.Common;
using Benday.Presidents.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Benday.Presidents.Core.Services
{
    public interface ILogger
    {
        void LogFeatureUsage(string featureName);
        void LogCustomerSatisfaction(string feedback);
    }
}
