using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Shared;

public static class Permissions
{
    //public static class Category
    //{
    //    public const string Create = "Create";
    //    public const string Update = "Update";
    //    public const string Delete = "Delete";

    //    public static List<string> All = new()
    //    {
    //        Create,
    //        Update,
    //        Delete
    //    };
    //}

    public static class Account
    {
        public const string AddRole = "Account.AddRole";

        public static List<string> All = new()
        {
            AddRole
        };
    }

    public static class Role
    {
        public const string GetAllPermissions = "Role.GetAllPermissions";
        public const string Create = "Role.Create";

        public static List<string> All = new()
        {
            GetAllPermissions,
            Create
        };
    }
}
