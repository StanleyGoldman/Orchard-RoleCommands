using System;
using System.Collections.Generic;
using System.Linq;
using Orchard.Logging;
using Orchard.Recipes.Models;
using Orchard.Recipes.Services;
using Orchard.Roles.Models;
using Orchard.Roles.Services;

namespace Contrib.RoleCommands.ImportExport
{
    public class RolePermissionsRecipeHandler : IRecipeHandler
    {
        private readonly IRoleService _roleService;

        public RolePermissionsRecipeHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public void ExecuteRecipeStep(RecipeContext recipeContext)
        {
            if (!String.Equals(recipeContext.RecipeStep.Name, RolePermissionsCustomExportStep.ExportStep, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var stepElement = recipeContext.RecipeStep.Step;

            foreach (var roleElement in stepElement.Elements())
            {
                var roleName = roleElement.Name.LocalName;
                var roleRecord = _roleService.GetRoleByName(roleName);

                var permissionsForRole = roleRecord == null
                    ? new List<string>()
                    : _roleService.GetPermissionsForRoleByName(roleName).ToList();

                if (roleRecord == null)
                {
                    _roleService.CreateRole(roleName);
                    roleRecord = _roleService.GetRoleByName(roleName);
                }

                foreach (var permissionOperation in roleElement.Elements())
                {
                    switch (permissionOperation.Name.LocalName)
                    {
                        case "Add":
                            permissionsForRole.Add(permissionOperation.Value);
                            break;

                        case "Remove":
                            permissionsForRole.Remove(permissionOperation.Value);
                            break;
                    }
                }

                _roleService.UpdateRole(roleRecord.Id, roleName,  permissionsForRole);
            }

            recipeContext.Executed = true;
        }
    }
}
