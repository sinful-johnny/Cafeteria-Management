using api.Interfaces;
using api.Mappers;
using api.Models.AuthModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

            var permissionDtos = permissionRoles.Select(p => p.ToPermissionDto()).ToList();


            var rolePermissionMap = roleMenus
        .GroupBy(rm => rm.menuID)
        .ToDictionary(
            g => g.Key,
            g => g.Select(rm => new api.Models.AuthModels.RolePermission
            {
                RoleName = rm.roleName,
                PermissionType = permissionRoles
                    .Where(pr => pr.rolemenuID == rm.rolemenuID)
                    .Select(pr => pr.permissionName)
                    .ToList()
            }).ToList()
        );

            // Recursive function to build the menu tree
            List<api.Models.AuthModels.MenuResourceDto> BuildMenuTree(int? parentId)
            {
                return menus
                    .Where(menu => menu.menuParent == parentId)
                    .Select(menu => new api.Models.AuthModels.MenuResourceDto
                    {
                        MenuName = menu.menuName,
                        OwnerRoles = rolePermissionMap.ContainsKey(menu.menuID)
                                        ? rolePermissionMap[menu.menuID]
                                        : new List<api.Models.AuthModels.RolePermission>(),
                        children = BuildMenuTree(menu.menuID) // Recursive call for nested children
                    })
                    .ToList();
            }

            // Build menu tree starting from the root (where parentId is null)
            var menuDtos = BuildMenuTree(null);


            return Ok(menuDtos);
        }
    }
}
