<%@ Page Title="Citas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Citas.aspx.cs" Inherits="TFGVeterinaria.Citas" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        th{
            color: white !important;
        }

        .msgpersonalizado{
            font-weight: bold;
            text-align: center;
            color: white;
        }
</style>
   
<div class="container">
    <div class="row justify-content-center align-items-center" style="height: 100%;">
        <asp:GridView ID="gvCitas" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCitas_RowCommand" CssClass="table">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" Visible='<%# Convert.ToInt32(Eval("ESTADO")) == 0 %>' CommandName="Confirmar" CssClass="btn btn-success btn-sm w-100" CommandArgument='<%# Eval("ID") %>' />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"  Visible='<%# Convert.ToInt32(Eval("ESTADO")) == 0 %>'  CommandName="Cancelar" CssClass="btn btn-danger btn-sm w-100" CommandArgument='<%# Eval("ID") %>' />
                        <asp:TextBox Text="CONFIRMADO" runat="server" Visible='<%# Convert.ToInt32(Eval("ESTADO")) == 1 %>' CssClass="bg-success msgpersonalizado" > </asp:TextBox>
                        <asp:TextBox Text="CANCELADO" runat="server" Visible='<%# Convert.ToInt32(Eval("ESTADO")) == 2 %>' CssClass="bg-danger msgpersonalizado" > </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FECHA" ItemStyle-ForeColor="White" HeaderText="Fecha" />
                <asp:BoundField DataField="HORA" ItemStyle-ForeColor="White" HeaderText="Hora" />
                <asp:BoundField DataField="DESCRIPCION" ItemStyle-ForeColor="White" HeaderText="Descripción" />
                <asp:BoundField DataField="SERVICIO" ItemStyle-ForeColor="White" HeaderText="Servicio" />
                <asp:BoundField DataField="USUARIO" ItemStyle-ForeColor="White" HeaderText="Usuario" />
                <asp:BoundField DataField="MASCOTA" ItemStyle-ForeColor="White" HeaderText="Mascota" />
            </Columns>
        </asp:GridView>
        </div>
    </div>
</asp:Content>
