using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Shared.Helpers;

public static class PermissionHelper
{
    public static Dictionary<string, List<string>> GetAllPermissions()
    {
        // bütün permission-ları topla
        var result = new Dictionary<string, List<string>>();

        // reflection ilə avtomatik toplamaq üçün:
        var nestedTypes = typeof(Permissions).GetNestedTypes(BindingFlags.Public | BindingFlags.Static);

        foreach (var moduleType in nestedTypes)
        {
            var allField = moduleType.GetField("All", BindingFlags.Public | BindingFlags.Static);
            if (allField != null)
            {
                var permissions = allField.GetValue(null) as List<string>;
                if (permissions != null)
                {
                    result.Add(moduleType.Name, permissions);
                }
            }
        }
        return result;
    }
    // hər biri üçün policy əlavə et
    public static List<string> GetAllPermissionList()
    {
        return GetAllPermissions().SelectMany(x => x.Value).ToList();
    }
}