<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Departments.aspx.cs" Inherits="UI.Departments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater ID="rpt" runat="server" ItemType="Models.Department">
        <HeaderTemplate>
            <table class="table">
                <tr>
                    <td>شماره 
                    </td>

                    <td>نام 
                    </td>
                    <td>
                        <a class="btn btn-primary" href="/CreateDepartment">ایجاد دپارتمان </a>
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%#:Item.Id %>
                </td>
                <td>
                    <%#:Item.Name %>
                </td>

                <td>
                    <a class="btn btn-warning" href="<%#"/CreateRoom?DepartmentId="+Eval("Id") %>">ایجاد بخش </a>
                </td>
                <td>
                    <asp:Button CssClass="btn btn-success hstack" OnClick="Button1_Click" CommandArgument='<%#Eval("Id")%>' ID="Button1" runat="server" Text="دریافت نوبت" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
