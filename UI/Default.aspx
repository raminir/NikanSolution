<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: -webkit-center;">
        <asp:Label CssClass="h1" ID="Label1" runat="server"></asp:Label>
        <asp:Button CssClass="btn btn-success hstack" OnClick="Button1_Click" ID="Button1" runat="server" Text="دریافت نوبت" />
    </div>
</asp:Content>
