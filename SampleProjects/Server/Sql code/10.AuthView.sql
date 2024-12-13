use cafeteriaDB
go

create or alter view V_Menu
as
	select id menuID, Title menuName, ParentId menuParent
    from AspNetMenu
go

create or alter view V_Role_Menu
as
	select anr.Name roleName, anrm.MenuId menuID, anrm.Id rolemenuID
    from AspNetRoles anr, AspNetRoleMenu anrm
    where anr.id = anrm.RoleId
go

create or alter view V_Permission_RoleMenu
as
	select mp.RoleMenuId rolemenuID, p.Name permissionName
    from MenuPermission mp, Permission p
    where mp.PermissionId = p.Id
go