using api.Dtos.MenuResource;
using api.Interfaces;
using api.Mappers;
using api.Models.AuthModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace api.Controllers
{
    [Route("api/MenuResources")]
    [ApiController]
    public class MenuResourceController : Controller
    {
        private readonly IMenuResource_Repository _tableMenuResouceRepo;
        public MenuResourceController(IMenuResource_Repository tableMenuResouceRepo)
        {
            this._tableMenuResouceRepo = tableMenuResouceRepo;
        }

        [HttpGet]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllMenuResource()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (menus, roleMenus, permissionRoles) = await _tableMenuResouceRepo.getAllMenuResource();

            // Map permissions
            var permissionMap = permissionRoles.Select(p => p.ToPermissionDto()).ToList();

            // Map roles and their permissions
            var rolePermissionMap = roleMenus
            .GroupBy(r => r.menuID)
            .ToDictionary(
                group => group.Key,
                group => group.Select(roleMenu =>
                    roleMenu.ToRolePermissionDto(
                        permissionMap.Where(p => p.rolemenuID == roleMenu.rolemenuID).ToList()
                    )
                ).ToList()
            );

            // Recursive function to build the menu tree
            List<MenuResourceDto> BuildMenuTree(int? parentId)
            {
                return menus
                    .Where(menu => menu.menuParent == parentId)
                    .Select(menu => menu.ToMenuResourceDto(
                        BuildMenuTree(menu.menuID), // Recursive call for nested children
                        rolePermissionMap.ContainsKey(menu.menuID)
                        ? rolePermissionMap[menu.menuID] // Roles for this menu
                        : new List<RolePermissionDto>() // No roles found
                    ))
                    .ToList();
            }

            // Build menu tree starting from the root (where parentId is null)
            var menuDtos = BuildMenuTree(null);


            return Ok(menuDtos);
        }
    }
}
