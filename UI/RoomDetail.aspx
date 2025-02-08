<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RoomDetail.aspx.cs" Inherits="UI.Room" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater ID="rpt" runat="server" ItemType="Application.Dtos.TicketInRoomDto">
        <HeaderTemplate>
            <table class="table">
                <tr>
                    <td>تاریخ
                    </td>
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
                    <%#Item.CreatedAt.ToPersianDate() %>
                </td>
                <td>
                    <%#Item.TicketNumber%>   
                </td>
                <td>
                    <%# GetStatusDisplayName(Eval("StatusId")) %>
                </td>
                <td>
                    <%#Item.RoomName %>
                </td>
                <td>
                    <%#Item.DepartmentName %>
                </td>
                <td>

                    <asp:Button Visible="<%#Item.StatusId==Models.StatusEnum.Waiting %>" CssClass="btn btn-outline-primary" ID="InProgressButton" CommandName="<%#(int)Models.StatusEnum.InProgress%>" CommandArgument='<%#Item.Id%>' OnClick="StatusButton_Click" runat="server" Text="فراخوان " />
                    <asp:Button Visible="<%#Item.StatusId==Models.StatusEnum.InProgress %>" CssClass="btn btn-outline-success" ID="DoneButton" CommandName="<%#(int)Models.StatusEnum.Done%>" CommandArgument='<%#Item.Id%>' OnClick="StatusButton_Click" runat="server" Text="انجام شد" />
                    <asp:Button Visible="<%#Item.StatusId==Models.StatusEnum.InProgress %>" CssClass="btn btn-outline-danger" ID="Button1" CommandName="<%#(int)Models.StatusEnum.cancel%>" CommandArgument='<%#Item.Id%>' OnClick="StatusButton_Click" runat="server" Text="عدم مراجعه" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
