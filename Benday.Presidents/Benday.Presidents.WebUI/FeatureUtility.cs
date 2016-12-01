using Benday.Presidents.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Benday.Presidents.WebUI
{
    public class FeatureUtility
    {
        public static IFeatureManager GetFeatures()
        {
            return System.Web.Mvc.DependencyResolver.Current.GetService(
                typeof(IFeatureManager)) as IFeatureManager;
        }
    }
}