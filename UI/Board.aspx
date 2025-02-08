<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Board.aspx.cs" Inherits="UI.Board" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="text-align: -webkit-center;">
        <asp:Repeater ID="rpt" runat="server">
            <ItemTemplate>
                <div class="card card-footer w-25 ">
                    <h2>نوبت شماره</h2>
                    <h1 id="ticketNumber"><%#Eval("Ticket.TicketNumber") %></h1>
                    <h2>به </h2>
                    <h1 id="roomNumber"><%#Eval("Room.Name") %></h1>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
