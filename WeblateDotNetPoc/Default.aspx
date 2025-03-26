<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WeblateDotNetPoc._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row" style="margin-top: 10px;">
        <span>Exemplos: </span>
        <asp:Button runat="server" Text="<%$ Resources:Strings,O desembarque está fora da data %>"/>
    </div>
    
    <div class="row" style="margin-top: 10px;">
        <asp:TextBox ID="txtLanguageCode" runat="server" CssClass="form-control" Width="100px" />
        <asp:Button  style="margin-top: 5px;" ID="btnCarregarTraducao" runat="server" Text="Carregar Traduções"
            OnClick="btnCarregarTraducao_Click" CssClass="btn btn-primary" />
    </div>


</asp:Content>
