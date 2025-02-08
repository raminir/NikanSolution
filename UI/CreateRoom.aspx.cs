using Application.Services;
using Microsoft.Practices.Unity;
using System;

namespace UI
{
    public partial class CreateRoom : System.Web.UI.Page
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
            DepartmentIdTextBox.Text = Request.QueryString["DepartmentId"];
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string departmentId = DepartmentIdTextBox.Text;
            departmentService.GenerateRoomAsync(new Models.Room() { Name = name, DepartmentId = int.Parse(departmentId) });
            string result = $"{name} ذخیره شد.";
            Response.Write($"<script>alert('{result}');</script>");

        }
    }
}