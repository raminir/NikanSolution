<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rooms.aspx.cs" Inherits="UI.Rooms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater ID="rpt" runat="server">
        <HeaderTemplate>
            <table class="table">
                <tr>
                    <td>شماره 
                    </td>

                    <td>نام 
                    </td>
                    <td>دپارتمان 
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>

                <td>
                    <%#Eval("Id")%>
          </td>
                <td>
                    <%#Eval("Name")%>
                </td>
                <td>
                    <%#Eval("Department.Name")%>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
