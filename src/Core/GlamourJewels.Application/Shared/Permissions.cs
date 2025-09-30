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
    public static class CartPermissions
    {
        public const string Create = "Cart.Create";    // Buyer
        public const string View = "Cart.View";        // Buyer (own cart)
        public const string Update = "Cart.Update";    // Admin
        public const string Delete = "Cart.Delete";    // Admin

        public static List<string> All = new()
        {
            Create, View, Update, Delete
        };
    }

    public static class FavoritePermissions
    {
        public const string Create = "Favorite.Create";    // Buyer
        public const string View = "Favorite.View";        // Buyer (own), Admin, Moderator
        public const string ViewAll = "Favorite.ViewAll";  // Admin, Moderator
        public const string Delete = "Favorite.Delete";    // Buyer (own) or Admin

        public static List<string> All = new()
    {
        Create, View, ViewAll, Delete
    };
    }

    public static class ReviewPermissions
    {
        public const string Create = "Review.Create";      // Buyer
        public const string View = "Review.View";          // Buyer(own), Seller (own products), Admin, Moderator
        public const string ViewAll = "Review.ViewAll";    // Admin, Moderator
        public const string Approve = "Review.Approve";    // Admin, Moderator
        public const string Delete = "Review.Delete";      // Admin (or Buyer own via service)

        public static List<string> All = new()
    {
        Create, View, ViewAll, Approve, Delete
    };
    }

    public static class ProductImagePermissions
    {
        public const string Create = "ProductImage.Create";   // Seller, Admin
        public const string Update = "ProductImage.Update";   // Seller(owner), Admin
        public const string Delete = "ProductImage.Delete";   // Seller(owner), Admin
        public const string View = "ProductImage.View";       // Buyer, Seller, Admin, Moderator

        public static List<string> All = new() { Create, Update, Delete, View };
    }

    public static class ProductSpecificationPermissions
    {
        public const string Create = "ProductSpecification.Create";  // Seller, Admin
        public const string Update = "ProductSpecification.Update";  // Seller, Admin
        public const string Delete = "ProductSpecification.Delete";  // Seller, Admin
        public const string View = "ProductSpecification.View";      // All roles

        public static List<string> All = new() { Create, Update, Delete, View };
    }

    public static class ProductTagPermissions
    {
        public const string Create = "ProductTag.Create"; // Admin, Seller
        public const string Update = "ProductTag.Update"; // Admin, Seller
        public const string Delete = "ProductTag.Delete"; // Admin, Seller
        public const string View = "ProductTag.View";     // Hər kəs

        public static List<string> All = new() { Create, Update, Delete, View };
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
