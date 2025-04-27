<%@ Page Tklllllllllllllllllllllllllllllllllllllllllllllllllllllllllitle="Noticias Detalle" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Noticias_Detalle.aspx.cs" Inherits="TFGVeterinaria.Noticias_Detalle" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        th{
            color: white !important;
        }
        .form-control, .form-select {
            background-color: #e3e3e3; 
            color: #4f4f4f;
            border-color: #333;+
                666666666666
        }

        .form-control:focus { 
            background-color: #e3e3e3;
            color: #4f4f4f;
            border-color: #007bff;
        } 

        .nav-tabs .nav-link.active {
            background-color: #333;
            color: #fff;
        }

        .nav-tabs .nav-link{
            color:#9d9d9d;
        }

        .form-control:disabled{
            background-color: #757575;
            color: white;
        }

        .form-select:disabled{
            background-color: #757575;
            color: white;
        }
</style>
<div class="container">
         <div class="alert alert-success alert-dismissible fade show" id="alertaExito" runat="server" role="alert">
            <strong>¡Éxito!</strong> Se han guardado los datos correctamente.
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
         <div class="row">
            <div class="col-6">
                <h3>Contenido</h3>
            </div>
            <div class="col-6 text-right">
                <button id="btnGuardar" class="btn btn-success">Guardar</button>
                <a class="btn btn-secondary" id="btnVolver" runat="server" href="~/Noticias">Volver</a>
            </div>
        </div>
        <div class="row justify-content-center align-items-center" style="height: 100%; width:100%;">
             <form>
                <div class="form-group mt-3">
                    <asp:TextBox ID="txtTitulo" style="max-width: 100%" runat="server" CssClass="form-control"/>
                </div>
                <div class="form-group mt-1">
                    <asp:TextBox TextMode="MultiLine" style="max-width: 100%; width: 100%;" rows="10" runat="server" ID="txtContenido" class="form-control"  placeholder="Escribe el contenido aquí..."></asp:TextBox>
                </div>
            </form>
         </div>
       
    </div>
</asp:Content>
