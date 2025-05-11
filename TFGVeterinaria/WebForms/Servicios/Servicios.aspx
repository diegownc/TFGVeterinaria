<%@ Page Title="Servicios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Servicios.aspx.cs" Inherits="TFGVeterinaria.Servicios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        th{
            color: white !important;
        }

        .card-body.custom-bg {
            background-color: #040404cf;
        }
    </style>
<div class="container">
   <div class="row justify-content-center align-items-center" style="height: 100%; width:100%;">
      <asp:ListView ID="lvServicios" runat="server" DataKeyNames="Id">
            <LayoutTemplate>
                <div class="row" runat="server" id="itemPlaceholder"></div>
            </LayoutTemplate>
            <ItemTemplate>
                <div class="col-md-3 mb-4">
                    <div class="card">
                        <div class="card-body custom-bg rounded-3">
                            <h5 class="card-title text-center"><%# Eval("TITULO") %></h5>
                            <p class="card-text justify-content-between">
                                <asp:Image runat="server" 
                                    ImageUrl='<%# Eval("ImageUrl") %>' 
                                    CssClass="img-fluid" 
                                    Height="150" style="width: auto;" /> <br />
                                <strong>Ubicación:</strong> <%# Eval("UBICACION") %><br />
                                <strong>Veterinario:</strong> <%# Eval("VETERINARIO") %><br />
                                <strong>Precio:</strong> <%# Eval("PRECIO", "{0:C}") %> <br />
                                <strong>Descripción: </strong> <%# Eval("DESCRIPCION") %> 
                            </p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>


   </div>
 </div>
</asp:Content>
