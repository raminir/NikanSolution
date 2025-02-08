<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Room.aspx.cs" Inherits="UI.Room" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater ID="rpt" runat="server">
        <HeaderTemplate>
            <table class="table">
                <tr>
                    <td>شماره 
                    </td>
                    <td>وضعیت
                    </td>
                    <td>نام بخش
                    </td>
                    <td>نام دپارتمان
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%#Eval("Ticket.TicketNumber") %>   
                </td>
                <td>
                    <%# GetStatusDisplayName(Eval("StatusId")) %>
                </td>
                <td>
                    <%#Eval("Room.Name") %>
                </td>
                <td>
                    <%#Eval("Room.Department.Name") %>
                </td>
                <td>
                    <asp:Button CssClass="btn btn-outline-primary" ID="InProgressButton" CommandName="<%#(int)Models.StatusEnum.InProgress%>" CommandArgument='<%#Eval("Id")%>' OnClick="StatusButton_Click" runat="server" Text="فراخوان " />
                    <asp:Button CssClass="btn btn-outline-success" ID="DoneButton" CommandName="<%#(int)Models.StatusEnum.Done%>" CommandArgument='<%#Eval("Id")%>' OnClick="StatusButton_Click" runat="server" Text="انجام شد" />
                    <asp:Button CssClass="btn btn-outline-danger" ID="Button1" CommandName="<%#(int)Models.StatusEnum.cancel%>" CommandArgument='<%#Eval("Id")%>' OnClick="StatusButton_Click" runat="server" Text="عدم مراجعه" />
                </td>
                <td>
                    <asp:Button CssClass="btn btn-warning" CommandArgument='<%#Eval("Id")%>' OnClick="Button2_Click" ID="Button2" runat="server" Text="انتقال به اتاق بعدی" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
