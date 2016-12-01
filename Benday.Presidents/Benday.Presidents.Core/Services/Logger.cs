using Benday.Presidents.Common;
using Benday.Presidents.Core.DataAccess;
using Benday.Presidents.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Benday.Presidents.Core.Services
{
    public class Logger : ILogger
    {
        private IPresidentsDbContext _DatabaseContext;
        private IFeatureManager _FeatureManager;

        public Logger(IPresidentsDbContext databaseContext, IFeatureManager featureManager)
        {
            if (featureManager == null)
                throw new ArgumentNullException("featureManager", "featureManager is null.");
            if (databaseContext == null)
                throw new ArgumentNullException("databaseContext", "databaseContext is null.");

            _DatabaseContext = databaseContext;
            _FeatureManager = featureManager;
        }

        public void LogCustomerSatisfaction(string feedback)
        {
            if (_FeatureManager.CustomerSatisfaction == false)
            {
                return;
            }

            var entry = GetPopulatedLogEntry();

            entry.LogType = "CustomerSatisfaction";
            entry.FeatureName = String.Empty;
            entry.Message = feedback;

            _DatabaseContext.LogEntries.Add(entry);
            _DatabaseContext.SaveChanges();
        }

        public void LogFeatureUsage(string featureName)
        {
            if (_FeatureManager.FeatureUsageLogging == false)
            {
                return;
            }

            var entry = GetPopulatedLogEntry();

            entry.LogType = "FeatureUsage";
            entry.FeatureName = featureName;

            _DatabaseContext.LogEntries.Add(entry);
            _DatabaseContext.SaveChanges();
        }

        private LogEntry GetPopulatedLogEntry()
        {
            var returnValue = new LogEntry();

            var context = HttpContext.Current;

            string username = String.Empty;
            string referrer = String.Empty;
            string requestUrl = String.Empty;
            string userAgent = String.Empty;
            string ipAddress = String.Empty;

            if (context != null)
            {
                if (context.Request != null)
                {
                    referrer = SafeToString(context.Request.UrlReferrer);
                    requestUrl = SafeToString(context.Request.Url);
                    userAgent = context.Request.UserAgent;
                    ipAddress = context.Request.UserHostAddress;
                }

                if (context.User != null && context.User.Identity != null)
                {
                    username = context.User.Identity.Name;
                }
            }

            returnValue.LogDate = DateTime.UtcNow;
            returnValue.ReferrerUrl = referrer;
            returnValue.RequestUrl = requestUrl;
            returnValue.UserAgent = userAgent;
            returnValue.Username = username;
            returnValue.RequestIpAddress = ipAddress;
            returnValue.Message = String.Empty;

            return returnValue;
        }

        private string SafeToString(Uri value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
