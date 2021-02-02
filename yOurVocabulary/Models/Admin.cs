using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace yOurVocabulary.Models
{
    public class Admin
    {
        public class RoleListModel
        {
            public List<IdentityRole> RoleList{ get; set; }
            public IdentityRole DefaultRole { get; set; }
        }
        public class UserListModel
        {
            public List<ApplicationUser> UserList { get; set; }
            public List<IdentityRole> RoleList { get; set; }
        }
        public class ChangeRoleModel
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public string CurrentRoleName { get; set; }
            public int  SelectedRoleId { get; set; }
            public List<IdentityRole> RoleList { get; set; }
        }
    }
}