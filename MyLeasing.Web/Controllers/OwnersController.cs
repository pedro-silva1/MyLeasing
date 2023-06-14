﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;
using MyLeasing.Web.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IUserHelper _userHelper;

        public OwnersController(IOwnerRepository repository, IUserHelper userHelper)
        {
            _ownerRepository = repository;
            _userHelper = userHelper;
        }

        // GET: Owners
        public IActionResult Index()
        {
            return View(_ownerRepository.GetAll().OrderBy(x => x.FirstName));
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OwnerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (viewModel.ProfileImageFile != null && viewModel.ProfileImageFile.Length > 0)
                {

                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\owners", 
                        file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await viewModel.ProfileImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/owners/{file}";
                }

                var owner = this.ToOwner(viewModel, path);

                owner.User = await _userHelper.GetUserByEmailAsync("pedrosilva@gmail.com");
                await _ownerRepository.AddAsync(owner);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        private Owner ToOwner(OwnerViewModel viewModel, string path)
        {
            return new Owner
            {
                Id = viewModel.Id,
                Document = viewModel.Document,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                FixedPhone = viewModel.FixedPhone,
                CellPhone = viewModel.CellPhone,
                Address = viewModel.Address,
                ProfilePictureUrl = path,
                User = viewModel.User,
            };
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            var viewModel = this.ToOwnerViewModel(owner);

            return View(viewModel);
        }

        private OwnerViewModel ToOwnerViewModel(Owner owner)
        {
            return new OwnerViewModel()
            {
                Id = owner.Id,
                Document = owner.Document,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                FixedPhone = owner.FixedPhone,
                CellPhone = owner.CellPhone,
                Address = owner.Address,
                ProfilePictureUrl = owner.ProfilePictureUrl,
                User = owner.User,

            };
        }

        // POST: Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OwnerViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var path = viewModel.ProfilePictureUrl;

                    if (viewModel.ProfileImageFile != null && viewModel.ProfileImageFile.Length > 0)
                    {
                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";

                        path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\images\\owners",
                            file);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await viewModel.ProfileImageFile.CopyToAsync(stream);
                        }

                        path = $"~/images/owners/{file}";
                    }

                    var owner = this.ToOwner(viewModel, path);

                    owner.User = await _userHelper.GetUserByEmailAsync("pedrosilva@gmail.com");
                    await _ownerRepository.UpdateAsync(owner);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _ownerRepository.ExistsAsync(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _ownerRepository.GetByIdAsync(id);
            await _ownerRepository.RemoveAsync(owner);
            return RedirectToAction(nameof(Index));
        }
    }
}
