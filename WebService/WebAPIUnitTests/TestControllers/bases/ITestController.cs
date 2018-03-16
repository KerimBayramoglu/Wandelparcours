﻿using System.Collections.Generic;
using WebService.Controllers.Bases;
using WebService.Models.Bases;

namespace WebAPIUnitTests.TestControllers.bases
{
    public interface ITestController<T> : IRestController<T> where T : IModelWithID
    {
        IEnumerable<T> GetAll();
        T GetFirst();
    }
}