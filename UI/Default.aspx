<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater ID="rpt" runat="server">
        <HeaderTemplate>
            <table class="table">
                <tr>
                    <td>
                        TicketNumber
                    </td>
                    <td>
                        Status
                    </td>
                    <td>
                        Room name
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%#Eval("Ticket.TicketNumber") %>
                </td>
                <td>
                    <%#Eval("StatusId") %>
                </td>
                <td>
                    <%#Eval("Room.Name") %>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:Button OnClick="Button1_Click" ID="Button1" runat="server" Text="Button" />
</asp:Content>
