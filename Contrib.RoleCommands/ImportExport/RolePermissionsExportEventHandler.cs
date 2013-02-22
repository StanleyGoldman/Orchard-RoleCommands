using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Orchard.Events;
using Orchard.Roles.Services;

namespace Contrib.RoleCommands.ImportExport
{
    public interface IExportEventHandler : IEventHandler
    {
        void Exporting(dynamic context);
        void Exported(dynamic context);
    }

    public class RolePermissionsExportHandler : IExportEventHandler
    {
        private readonly IRoleService _roleService;

        public RolePermissionsExportHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public void Exporting(dynamic context)
        {
        }

        public void Exported(dynamic context)
        {
            var stepPrefix = string.Format("{0} - ", RolePermissionsCustomExportStep.ExportStep);

            var customSteps = ((IEnumerable<string>)context.ExportOptions.CustomSteps)
                .Where(customStep => customStep.StartsWith(stepPrefix))
                .ToArray();

            if (!customSteps.Any())
                return;

            var rolePermissions = new XElement(RolePermissionsCustomExportStep.ExportStep);
            context.Document.Element("Orchard").Add(rolePermissions);

            foreach (var customStep in customSteps)
            {
                var role = customStep.Substring(stepPrefix.Length);
                var roleElement = new XElement(role);
                rolePermissions.Add(roleElement);

                foreach (var permission in _roleService.GetPermissionsForRoleByName(role))
                {
                    roleElement.Add(new XElement("Add") { Value = permission });
                }
            }
        }
    }
}

