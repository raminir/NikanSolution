using Application.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI
{
    public partial class CreateDepartment : System.Web.UI.Page
    {
        private DepartmentService departmentService;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Retrieve the Unity container from Application state
            var container = (IUnityContainer)Application["UnityContainer"];

            // Resolve the IUserService dependency
            departmentService = container.Resolve<DepartmentService>();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            departmentService.GenerateDepartmentAsync(name);
            string result = $"{name} ذخیره شد.";
            Response.Write($"<script>alert('{result}');</script>");
        }
    }
}