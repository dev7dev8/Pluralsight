using Benday.Presidents.Common;
using Benday.Presidents.Core.DataAccess;
using Benday.Presidents.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Benday.Presidents.Core.Features
{
    public class FeatureManager : IFeatureManager
    {
        [Microsoft.Practices.Unity.InjectionConstructor]
        public FeatureManager(IFeatureRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository", "Argument cannot be null.");
            }

            Initialize(repository);
        }

        /// <summary>
        /// This constructor is for testing.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="username"></param>
        public FeatureManager(IFeatureRepository repository, string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("username is null or empty.", "username");

            if (repository == null)
            {
                throw new ArgumentNullException("repository", "Argument cannot be null.");
            }

            Initialize(repository, username);
        }

        public FeatureManager(IList<Feature> features)
        {
            if (features == null)
                throw new ArgumentNullException("features", "features is null.");

            Initialize(features);
        }

        private Dictionary<string, bool> _FeatureConfigurations;

        private void Initialize(IList<Feature> features)
        {
            _FeatureConfigurations = new Dictionary<string, bool>();

            foreach (var feature in features)
            {
                if (_FeatureConfigurations.ContainsKey(feature.Name) == true)
                {
                    _FeatureConfigurations.Remove(feature.Name);
                }

                _FeatureConfigurations.Add(feature.Name, feature.IsEnabled);
            }
        }

        private bool IsEnabled(string featureName, bool defaultValue)
        {
            if (_FeatureConfigurations.ContainsKey(featureName) == true)
            {
                return _FeatureConfigurations[featureName];
            }
            else
            {
                return defaultValue;
            }
        }

        public bool CustomerSatisfaction
        {
            get
            {
                return IsEnabled("CustomerSatisfaction", false);
            }
        }

        public bool FeatureUsageLogging
        {
            get
            {
                return IsEnabled("FeatureUsageLogging", false);
            }
        }

        public bool PerformanceCounters
        {
            get
            {
                return IsEnabled("PerformanceCounters", false);
            }
        }

        public bool Search
        {
            get
            {
                return IsEnabled("Search", true);
            }
        }

        public bool SearchByBirthDeathState
        {
            get
            {
                return IsEnabled("SearchByBirthDeathState", false);
            }
        }
        private string GetUsername()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;

            return username;
        }

        private void Initialize(IFeatureRepository repository)
        {
            string username = GetUsername();

            Initialize(repository, username);
        }

        private void Initialize(IFeatureRepository repository, string username)
        {
            var features = repository.GetByUsername(username);

            var featuresForThisUser =
                (
                from temp in features
                where String.IsNullOrWhiteSpace(temp.Username) == false
                select temp
                ).ToList();

            foreach (var userSpecificFeature in featuresForThisUser)
            {
                // if there's a user-specific feature config, remove the non-user-specific feature
                RemoveGenericUserFeatureConfiguration(features, userSpecificFeature);
            }

            Initialize(features);
        }

        private void RemoveGenericUserFeatureConfiguration(IList<Feature> features, Feature userSpecificFeature)
        {
            var match = (from temp in features
                         where temp.Name == userSpecificFeature.Name && temp.Username == String.Empty
                         select temp).FirstOrDefault();

            if (match != null)
            {
                features.Remove(match);
            }
        }
    }
}
