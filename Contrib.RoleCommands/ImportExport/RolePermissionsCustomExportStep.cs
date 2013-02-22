using System.Collections.Generic;
using Orchard.Events;
using Orchard.Roles.Services;

namespace Contrib.RoleCommands.ImportExport
{
    public interface ICustomExportStep : IEventHandler
    {
        void Register(IList<string> steps);
    }

    public class RolePermissionsCustomExportStep : ICustomExportStep
    {
        public const string ExportStep = "RolePermissions";

        private readonly IRoleService _roleService;

        public RolePermissionsCustomExportStep(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public void Register(IList<string> steps)
        {
            foreach (var roleRecord in _roleService.GetRoles())
            {
                steps.Add(string.Format("{0} - {1}", ExportStep, roleRecord.Name));
            }
        }
    }
}