using Benday.Presidents.Core.DataAccess;
using Benday.Presidents.Core.Features;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benday.Presidents.UnitTests.Features
{
    [TestClass]
    public class FeatureManagerFixture
    {
        private const string TestUserName = "user@test.org";
        [TestInitialize]
        public void OnTestInitialize()
        {
            _SystemUnderTest = null;
            _FeatureRepositoryInstance = null;
        }

        private FeatureManager _SystemUnderTest;

        private FeatureManager SystemUnderTest
        {
            get
            {
                if (_SystemUnderTest == null)
                {
                    _SystemUnderTest = new FeatureManager(
                        FeatureRepositoryInstance, TestUserName);
                }

                return _SystemUnderTest;
            }
        }

        private MockFeatureRepository _FeatureRepositoryInstance;
        public MockFeatureRepository FeatureRepositoryInstance
        {
            get
            {
                if (_FeatureRepositoryInstance == null)
                {
                    _FeatureRepositoryInstance = new MockFeatureRepository();
                }

                return _FeatureRepositoryInstance;
            }
        }

        [TestMethod]
        public void WhenUsernameIsAvailableAndFeatureIsNotEnabledForAnyoneThenPrivateBetaIsDisabledForFeature()
        {
            SetFeatureFlagData("SearchByBirthDeathState", false);

            Assert.IsFalse(SystemUnderTest.SearchByBirthDeathState);
        }

        [TestMethod]
        public void WhenUsernameIsAvailableAndFeatureIsEnabledForEveryoneThenPrivateBetButNotForCurrentUserThenIsEnabledIsTrue()
        {
            SetFeatureFlagData("SearchByBirthDeathState", true);

            Assert.IsTrue(SystemUnderTest.SearchByBirthDeathState);
        }

        [TestMethod]
        public void WhenUsernameIsAvailableAndFeatureIsNotEnabledForUserThenPrivateBetaIsDisabledForFeature()
        {
            SetFeatureFlagData("SearchByBirthDeathState", false);
            SetFeatureFlagData("SearchByBirthDeathState", false, TestUserName);

            Assert.IsFalse(SystemUnderTest.SearchByBirthDeathState);
        }

        [TestMethod]
        public void WhenUsernameIsAvailableAndUserSpecificFeatureIsEnabledThenFeatureIsEnabled()
        {
            SetFeatureFlagData("SearchByBirthDeathState", false);
            SetFeatureFlagData("SearchByBirthDeathState", true, TestUserName);

            Assert.IsTrue(SystemUnderTest.SearchByBirthDeathState);
        }

        private void SetFeatureFlagData(string featureName, bool isEnabled)
        {
            SetFeatureFlagData(featureName, isEnabled, String.Empty);
        }

        private void SetFeatureFlagData(string featureName, bool isEnabled, string username)
        {
            var temp = new Feature();

            temp.IsEnabled = isEnabled;
            temp.Name = featureName;
            temp.Username = username;

            FeatureRepositoryInstance.GetByUsernameReturnValue.Add(temp);
        }
    }
}
