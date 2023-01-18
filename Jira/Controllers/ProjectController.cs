using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.Models.Entities;
using Jira.Models.Intarfaces.Handlers;
using Jira.Models.Intarfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Jira.Controllers
{
	public class ProjectController: Controller
	{
		private readonly IProjectHandler _projectHandler;
		public ProjectController(IProjectHandler projectHandler)
		{
			_projectHandler = projectHandler;
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task Create(Project project)
		{
			await _projectHandler.CreateOrUpdateAsync(project);
		}

		public IActionResult Projects()
		{
			var projects = _projectHandler.GetAllProjects();
				//	if (projects == null || !projects.Any())
			return View(projects);
		}

		public async Task<IActionResult> Project(int projectId)	
		{
			var project = await _projectHandler.GetProjectAsync(projectId);
			if (project == null)
				return RedirectToAction(nameof(Projects));
			return View(project);
		}

		[HttpPost]
		public async Task CreateTask(TaskJira taskJira)
		{			
			var taskStateId = int.Parse(Request.Form["temp"]);
				await _projectHandler.CreateTaskAsync(taskJira, taskStateId);
		}

		[HttpGet]
		public async Task<IActionResult> CreateTaskAsync(int projectId)
		{
			var project = await _projectHandler.GetProjectAsync(projectId);
			ViewBag.TaskStates = project.TaskStates;
			return View();
		}

	}
}
