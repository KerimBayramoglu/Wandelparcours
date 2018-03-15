﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPIUnitTests.TestHelpers.Extensions;

// ReSharper disable once CheckNamespace
namespace WebAPIUnitTests.ControllerTests.Abstract
{
    // ReSharper disable once UnusedTypeParameter
    public partial class ARestControllerTests<T>
    {
        #region PropertySelectors

        [TestMethod]
        public void ConvertUnknownStringToSelector()
        {
            ActionExtensions.ShouldCatchWebArgumentException(
                () =>
                {
                    var _ = CreateNewController().PropertySelectors["1randomProperty"];
                },
                "propertyName",
                "the property doesn't exist");
        }

        [TestMethod]
        public abstract void ConvertStringToSelector();

        #endregion PropertySelectors


        #region ConvertStringsToSelectors

        [TestMethod]
        public void ConvertNullStringsToSelectors()
        {
            ActionExtensions.ShouldCatchWebArgumentException(
                () => CreateNewController().ConvertStringsToSelectors(null),
                "propertyNames",
                "the propertyNames cannot be null");
        }

        [TestMethod]
        public void ConvertEmptyStringsToSelectors()
        {
            ActionExtensions.ShouldCatchWebArgumentException(
                () => CreateNewController().ConvertStringsToSelectors(new string[] { }),
                "propertyNames",
                "the propertyNames cannot be empty");
        }

        [TestMethod]
        public abstract void ConvertUnknownStringsToSelectors();

        [TestMethod]
        public abstract void ConvertStringsToSelectorsWithSomeUnknownStrings();

        [TestMethod]
        public abstract void ConvertStringsToSelectors();

        #endregion ConvertStringsToSelectors
    }
}