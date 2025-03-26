<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WeblateDotNetPoc._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
         <asp:Literal runat="server" Text="<%$ Resources:Strings,create %>" />
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <span>Exemplos: </span>
        <asp:Button runat="server" Text="<%$ Resources:Strings,Temos uma problema! Aqui não tem tradução %>"/>
        <asp:Button runat="server" Text="<%$ Resources:Strings,save %>"/>
        <asp:Button runat="server" Text="<%$ Resources:Strings,edit %>"/>
        <asp:Button runat="server" Text="<%$ Resources:Strings,delete %>"/>
    </div>
    
    <div class="row" style="margin-top: 10px;">
        <asp:TextBox ID="txtLanguageCode" runat="server" CssClass="form-control" Width="100px" />
        <asp:Button ID="btnCarregarTraducao" runat="server" Text="Carregar Traduções"
            OnClick="btnCarregarTraducao_Click" CssClass="btn btn-primary" />
    </div>


</asp:Content>
