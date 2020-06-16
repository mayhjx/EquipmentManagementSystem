﻿using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace EquipmentManagementSystem.Authorization
{
    public class Operations
    {
        public static OperationAuthorizationRequirement Create =
            new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };
        public static OperationAuthorizationRequirement Read =
            new OperationAuthorizationRequirement { Name = Constants.ReadOperationName };
        public static OperationAuthorizationRequirement Update =
            new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };
        public static OperationAuthorizationRequirement Delete =
            new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };
        public static OperationAuthorizationRequirement Approve =
            new OperationAuthorizationRequirement { Name = Constants.ApproveOperationName };
    }

    public class Constants
    {
        public static readonly string CreateOperationName = "Create";
        public static readonly string ReadOperationName = "Read";
        public static readonly string UpdateOperationName = "Update";
        public static readonly string DeleteOperationName = "Delete";
        public static readonly string ApproveOperationName = "Approve";

        public static readonly string AdministratorRole = "Administrator";
        public static readonly string DirectorRole = "设备主任";
        public static readonly string ManagerRole = "设备管理员";
        public static readonly string PrincipalRole = "设备负责人";
        public static readonly string TechnicianRole = "技术员";
    }
}
