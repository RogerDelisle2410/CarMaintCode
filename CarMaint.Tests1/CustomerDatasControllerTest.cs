// <copyright file="CustomerDatasControllerTest.cs">Copyright ©  2017</copyright>
using System;
using System.Web.Mvc;
using CarMaint.Controllers;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarMaint.Controllers.Tests
{
    /// <summary>This class contains parameterized unit tests for CustomerDatasController</summary>
    [PexClass(typeof(CustomerDatasController))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class CustomerDatasControllerTest
    {
        /// <summary>Test stub for Edit(Nullable`1&lt;Int32&gt;)</summary>
        [PexMethod]
        public ActionResult EditTest(
            [PexAssumeUnderTest]CustomerDatasController target,
            int? id
        )
        {
            ActionResult result = target.Edit(id);
            return result;
            // TODO: add assertions to method CustomerDatasControllerTest.EditTest(CustomerDatasController, Nullable`1<Int32>)
        }
    }
}
