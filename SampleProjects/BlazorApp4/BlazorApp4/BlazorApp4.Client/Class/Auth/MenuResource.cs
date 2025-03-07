﻿namespace Class.Auth
{
    public class MenuResource
    {
        public required string MenuName { get; set; }
        public required List<RolePermission> OwnerRoles { get; set; }
        public List<MenuResource>? Children { get; set; }
    }

    public class RolePermission
    {
        public required string RoleName { get; set; }
        public required List<string> PermissionType { get; set; }
    }
}
