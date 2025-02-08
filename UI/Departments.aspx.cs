using Application.QueueManagement.Application.Services;
using Application.Services;
using Microsoft.Practices.Unity;
using System;
using System.Web.UI.WebControls;

namespace UI
{
    public partial class Departments : System.Web.UI.Page
    {
        private DepartmentService departmentService;
        private TicketService _ticketService;
        

        protected void Page_Init(object sender, EventArgs e)
        {
            // Retrieve the Unity container from Application state
            var container = (IUnityContainer)Application["UnityContainer"];

            departmentService = container.Resolve<DepartmentService>();
            _ticketService = container.Resolve<TicketService>();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDepartments();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var departmentId = int.Parse(btn.CommandArgument);
            var ticketNumber = _ticketService.GenerateTicketAsync(departmentId);
            Response.Write($"<script>alert('شماره شما :{ticketNumber.ToString()}');</script>");
        }
        private void LoadDepartments()
        {

            var roomId = Request.QueryString["Id"];

            var ds = departmentService.GetAllDepartments();

            rpt.DataSource = ds;
            rpt.DataBind();

        }
    }
}