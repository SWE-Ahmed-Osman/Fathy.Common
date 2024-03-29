﻿using Fathy.Common.Startup;
using Microsoft.AspNetCore.Identity;

namespace Fathy.Common.Auth.Admin.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Response> AddToRoleAsync(string userEmail, string role)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        return user is null
            ? ResponseFactory.Fail(ErrorsList.UserEmailNotFound(userEmail))
            : (await _userManager.AddToRoleAsync(user, role)).ToApplicationResponse();
    }

    public async Task<Response> CreateRoleAsync(string role) =>
        (await _roleManager.CreateAsync(new IdentityRole(role))).ToApplicationResponse();
}