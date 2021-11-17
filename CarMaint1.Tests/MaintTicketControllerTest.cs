using System.Web.Mvc;
// <copyright file="MaintTicketControllerTest.cs">Copyright ©  2017</copyright>

using System;
using CarMaint.Controllers;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarMaint.Controllers.Tests
{
    [TestClass]
    [PexClass(typeof(MaintTicketController))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class MaintTicketControllerTest
    {

        [PexMethod(MaxBranches = 20000)]
        public ActionResult SelectCar(
            [PexAssumeUnderTest]MaintTicketController target,
            string engineType,
            int carId
        )
        {
            ActionResult result = target.SelectCar(engineType, carId);
            return result;
            // TODO: add assertions to method MaintTicketControllerTest.SelectCar(MaintTicketController, String, Int32)
        }
    }
}
