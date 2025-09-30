using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Shared;

public static class Permissions
{
    public static class Product
    {
        public const string Create = "Product.Create";
        public const string Update = "Product.Update";
        public const string Delete = "Product.Delete";

        public static List<string> All = new()
        {
            Create,
            Update,
            Delete
        };
    }
    public static class Category
    {
        public const string Create = "Category.Create";
        public const string Update = "Category.Update";
        public const string Delete = "Category.Delete";
        public const string View = "Category.View";

        public static List<string> All = new()
        {
            Create,
            Update,
            Delete,
            View
        };
    }

    public static class OrderPermissions
    {
        public const string Create = "Order.Create";          // Buyer
        public const string ViewById = "Order.ViewById";      // Buyer(own), Seller, Admin, Moderator
        public const string ViewAll = "Order.ViewAll";        // Admin, Moderator, Seller
        public const string Update = "Order.Update";          // Admin
        public const string Delete = "Order.Delete";          // Admin

        public static List<string> All = new()
        {
            Create, ViewById, ViewAll, Update, Delete
        };
    }

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
