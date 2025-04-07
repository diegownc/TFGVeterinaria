<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mascotas.aspx.cs" Inherits="TFGVeterinaria.Mascotas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        th{
            color: white !important;
        }
    </style>
<div class="container">
        <div class="row justify-content-center align-items-center" style="height: 100%;">
             <div class="container mt-5">
             <h2>Mascotas</h2>
             <asp:GridView ID="myGridView"   PagerStyle-ForeColor="White"  ForeColor="White"  RowStyle-ForeColor="White" AlternatingRowStyle-BackColor="#858585" HeaderStyle-ForeColor="White" runat="server" CssClass="table table-striped table-bordered" 
                             AutoGenerateColumns="False" OnRowCommand="myGridView_RowCommand" 
                             OnRowEditing="myGridView_RowEditing" OnRowUpdating="myGridView_RowUpdating" 
                             OnRowDeleting="myGridView_RowDeleting" OnRowCancelingEdit="myGridView_RowCancelingEdit">
                 <Columns>
                     <asp:ButtonField CommandName="Select" Text="Detalle" ButtonType="Button" ControlStyle-CssClass="btn btn-primary btn-sm" />
                     <asp:ButtonField CommandName="Edit" Text="Editar" ButtonType="Button" ControlStyle-CssClass="btn btn-warning btn-sm" />
                     <asp:CommandField ShowDeleteButton="True" ButtonType="Button"   ControlStyle-CssClass="btn btn-danger btn-sm" />
                     <asp:BoundField DataField="ID" ItemStyle-ForeColor="White" HeaderText="ID" SortExpression="ID" />
                     <asp:BoundField DataField="Nombre" ItemStyle-ForeColor="White" HeaderText="Nombre" SortExpression="Nombre" />
                     <asp:BoundField DataField="Edad" ItemStyle-ForeColor="White" HeaderText="Edad" SortExpression="Edad" />
                 </Columns>
             </asp:GridView>
         </div>
        </div>
    </div>
</asp:Content>
