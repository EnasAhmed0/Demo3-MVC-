﻿using AutoMapper;
using Demo3MVC.DAL.Models;
using Demo3MVC.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo3MVC.PL.Controllers
{
	[Authorize]
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager,
							  IMapper mapper)
        {
			_userManager = userManager;
            _mapper = mapper;
        }
		#region Index
		public async Task<IActionResult> Index(string SearchValue)
		{
			if (string.IsNullOrEmpty(SearchValue))
			{
				var Users = await _userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					Fname = U.FName,
					Lname = U.LName,
					Email = U.Email,
					PhoneNumber = U.PhoneNumber,
					Roles = _userManager.GetRolesAsync(U).Result
				}).ToListAsync();
				return View(Users);
			}
			else
			{
				var User = await _userManager.FindByEmailAsync(SearchValue);
				var MappedUser = new UserViewModel()
				{
					Id = User.Id,
					Fname = User.FName,
					Lname = User.LName,
					Email = User.Email,
					PhoneNumber = User.PhoneNumber,
					Roles = _userManager.GetRolesAsync(User).Result
				};
				return View(new List<UserViewModel> { MappedUser });
			}
		}
		#endregion

		#region Detailes

		public async Task<IActionResult> Details(string Id, string ViewName = "Details")
		{
			if (Id is null)
				return BadRequest();
			var User = await _userManager.FindByIdAsync(Id);
			if (User is null)
				return NotFound();

			var MappedUser = _mapper.Map<ApplicationUser, UserViewModel>(User);
			return View(ViewName, MappedUser);

		}

		#endregion

		#region Edit

		public async Task<IActionResult> Edit(string Id)
		{
			return await Details(Id, "Edit");
		}

		[HttpPost]
		public async Task<IActionResult> Edit(UserViewModel model, [FromRoute] string id)
		{
			if (id != model.Id)
				return BadRequest();
			if (ModelState.IsValid)
			{
				try
				{
					var User = await _userManager.FindByIdAsync(id);
					User.PhoneNumber = model.PhoneNumber;
					User.FName = model.Fname;
					User.LName = model.Lname;

					await _userManager.UpdateAsync(User);
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View(model);
		}
		#endregion

		#region Delete
		public async Task<IActionResult> Delete(string Id)
		{
			return await Details(Id, "Delete");
		}

		[HttpPost]
		public async Task<IActionResult> ConfirmDelete(string id)
		{
			try
			{
				var user = await _userManager.FindByIdAsync(id);
				await _userManager.DeleteAsync(user);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
                ModelState.AddModelError(string.Empty, ex.Message);
				return RedirectToAction("Error", "Home");
            }
        }
		#endregion
	}
}
