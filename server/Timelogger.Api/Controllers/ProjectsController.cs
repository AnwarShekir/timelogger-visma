using System;
using Microsoft.AspNetCore.Mvc;
using Timelogger.Api.Models.Project;
using Timelogger.Api.Services.Project;
using Timelogger.Api.Services.TimeRegistration;

namespace Timelogger.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectsController : BaseController
	{
		private readonly IProjectService _projectService;
		private readonly ITimeRegistrationService _timeRegistrationService;

		public ProjectsController(IProjectService projectService, ITimeRegistrationService timeRegistrationService)
		{
			_projectService = projectService;
			_timeRegistrationService = timeRegistrationService;
		}

		//Methoeds would be Async in a real application, but since we run with a in memory database, there is no need.

		// GET api/projects
		[HttpGet]
		public IActionResult Get()
		{
            try
            {
				var result = _projectService.GetList();
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
				var result = _projectService.GetSingle(id);
				return Ok(result);
			}
			catch (System.Exception ex)
			{
				return ServerError(ex.Message);
			}
		}

		[HttpGet]
		[Route("{id}/timeregistrations")]
		public IActionResult GetProjectTimeRegistrations([FromRoute] Guid id)
        {
			try
			{
				var result = _timeRegistrationService.GetProjectTimeRegistrations(id);
				return Ok(result);
			}
			catch (System.Exception ex)
			{
				return ServerError(ex.Message);
			}
		}

		[HttpPost]
		public IActionResult Create([FromBody] CreateProject dto)
        {
			try
			{
				_projectService.Create(dto);
				return Ok();
			}
			catch (System.Exception ex)
			{
				return ServerError(ex.Message);
			}
		}
	}
}
