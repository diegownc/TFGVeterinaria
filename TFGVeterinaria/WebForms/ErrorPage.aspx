<%@ Page Title="Error Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="TFGVeterinaria.ErrorPage" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            background-color: #f8d7da;
            color: #721c24;
            font-family: Arial, sans-serif;
        }
        .error-container {
            text-align: center;
            margin-top: 50px;
        }
        .error-container h1 {
            font-size: 50px;
        }
        .error-container p {
            font-size: 20px;
        }
</style>
<div class="container">
    <div class="container error-container">
        <h1>¡Oops! Algo salió mal.</h1>
        <p>Se ha producido un error inesperado. Por favor, intente nuevamente más tarde.</p>
        <p runat="server" id="errormsg"></p>
    </div>
 </div>
</asp:Content>
