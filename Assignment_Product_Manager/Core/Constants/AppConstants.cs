using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Product_Manager.Core.Constants
{
    public static class AppConstants
    {
        public static class StoredProcedures
        {
            public const string LoginUser = "sp_LoginUser";
            public const string RegisterUser = "sp_RegisterUser";
            public const string AddProduct = "sp_AddProduct";
            public const string UpdateProduct = "sp_UpdateProduct";
            public const string DeleteProduct = "sp_DeleteProduct";
            public const string GetProductsByUser = "sp_GetProductsByUser";
            public const string GetProductById = "sp_GetProductById";
            public const string GetDiscountRules = "sp_GetDiscountRules";
            public const string CheckUserExists = "sp_CheckUserExists";
        }

        public static class SessionKeys
        {
            public const string CurrentUserId = "CurrentUserId";
            public const string CurrentUsername = "CurrentUsername";
            public const string CurrentUserFullName = "CurrentUserFullName";
        }

        public static class Validation
        {
            public const int MinPasswordLength = 6;
            public const int MaxPasswordLength = 50;
            public const int MaxUsernameLength = 30;
            public const int MaxProductNameLength = 100;
            public const decimal MaxPrice = 9_999_999.99m;
            public const int MaxStock = 100_000;
            public const int MaxDescriptionLength = 500;
        }
    }
}
