<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateDepartment.aspx.cs" Inherits="UI.CreateDepartment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mb-3">
        <label class="form-label">نام</label>
        <asp:TextBox CssClass="form-control" ID="txtName" runat="server" required="true"></asp:TextBox><br />
        <asp:Button CssClass="btn btn-primary" ID="btnSubmit" runat="server" Text="ثبت" OnClick="btnSubmit_Click" />
    </div>
</asp:Content>
