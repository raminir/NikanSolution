<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" id="appointments" style="text-align: -webkit-center;">
        <asp:Repeater ID="rpt" runat="server">
            <ItemTemplate>
                <div class="card card-footer w-25 ">
                    <h2>نوبت شماره</h2>
                    <h1><%#Eval("Ticket.TicketNumber") %></h1>
                    <h2>به </h2>
                    <h1><%#Eval("Room.Name") %></h1>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
