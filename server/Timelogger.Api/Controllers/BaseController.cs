using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Timelogger.Api.Common;

namespace Timelogger.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected new IActionResult Ok()
        {
            return base.Ok(Envelope.Ok());
        }

        protected IActionResult BadRequest(string errorMessage)
        {
            return base.BadRequest(Envelope.Error(errorMessage));
        }

        protected IActionResult Ok<T>(T result)
        {
            return base.Ok(Envelope.Ok(result));
        }

        protected IActionResult NotFound(string errorMessage)
        {
            return base.NotFound(Envelope.Error(errorMessage));
        }

        protected IActionResult ServerError(string errorMessage = null)
        {
            return base.StatusCode(500, Envelope.Error("Internal Server Error. Error: " + errorMessage));
        }
    }
}
