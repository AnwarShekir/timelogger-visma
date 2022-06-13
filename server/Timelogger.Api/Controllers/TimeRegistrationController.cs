using System;
using Microsoft.AspNetCore.Mvc;
using Timelogger.Api.Models.TimeRegistration;
using Timelogger.Api.Services.TimeRegistration;

namespace Timelogger.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TimeRegistrationController : BaseController
	{
		private readonly ITimeRegistrationService _timeRegistrationService;

		public TimeRegistrationController(ITimeRegistrationService timeRegistrationService)
		{
			_timeRegistrationService = timeRegistrationService;
		}


		[HttpGet]
		public IActionResult Get()
		{
            try
            {
				var result = _timeRegistrationService.GetList();
				return Ok(result);
            }
            catch (System.Exception ex)
            {
				return ServerError(ex.Message);
            }
		}

		[HttpGet]
		[Route("{id}")]
		public IActionResult GetSingle([FromRoute] Guid id)
		{
			try
			{
				var result = _timeRegistrationService.GetSingle(id);
				return Ok(result);
			}
			catch (System.Exception ex)
			{
				return ServerError(ex.Message);
			}
		}

		[HttpPost]
		public IActionResult Create([FromBody] CreateTimeRegistration dto)
		{
			try
			{
				 _timeRegistrationService.Create(dto);
				return Ok();
			}
			catch (System.Exception ex)
			{
				return ServerError(ex.Message);
			}
		}
	}
}
