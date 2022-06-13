using System;
using Microsoft.AspNetCore.Mvc;
using Timelogger.Api.Models.Company;
using Timelogger.Api.Services.Company;
using Timelogger.Api.Services.Project;

namespace Timelogger.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CompanyController : BaseController
	{
		private readonly ICompanyService _companyService;
		private readonly IProjectService _projectService;

		public CompanyController(ICompanyService companyService,IProjectService projectService)
		{
			_companyService= companyService;
			_projectService = projectService;
		}


		[HttpGet]
		public IActionResult Get()
		{
            try
            {
				var result = _companyService.GetList();
				return Ok(result);
            }
            catch (System.Exception ex)
            {
				return ServerError(ex.Message);
            }
		}

		[HttpGet]
		[Route("search")]
		public IActionResult FindCompanyByName([FromQuery] string query)
        {
            try
            {
				if (query == null) return BadRequest("No search query");
				var result = _companyService.Find(query);
				return Ok(result);
            }
            catch (Exception ex)
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
				var result = _companyService.GetSingle(id);
				return Ok(result);
			}
			catch (System.Exception ex)
			{
				return ServerError(ex.Message);
			}
		}

		[HttpGet]
		[Route("{id}/projects")]
		public IActionResult GetCompanyProjects([FromRoute] Guid id)
        {
			try
			{
				var result = _projectService.GetCompanyProjects(id);
				return Ok(result);
			}
			catch (System.Exception ex)
			{
				return ServerError(ex.Message);
			}
		}



		[HttpPost]
		public IActionResult Create([FromBody] CreateCompany dto)
		{
			try
			{
				  _companyService.Create(dto);
				return Ok();
			}
			catch (System.Exception ex)
			{
				return ServerError(ex.Message);
			}
		}
	}
}
