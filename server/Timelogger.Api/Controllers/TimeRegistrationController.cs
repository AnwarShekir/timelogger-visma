using System;
using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Create([FromBody] CreateTimeRegistration dto)
		{
			try
			{
				 _timeRegistrationService.Create(dto);
				return Ok();
			}
			catch(ArgumentException e)
            {
				return BadRequest(e.Message);
            }
			catch(InvalidOperationException e)
            {
				return NotFound(e.Message);
            }
			catch (System.Exception ex)
			{
				return ServerError(ex.Message);
			}
		}
	}
}
