<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Noticias_Encuesta.aspx.cs" Inherits="TFGVeterinaria.Noticias_Encuesta" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
    <div class="alert alert-success alert-dismissible fade show" id="alertaExito" runat="server" role="alert">
        <strong>¡Éxito!</strong> Enhorabuena has acertado correctamente.
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
    <div class="alert alert-danger alert-dismissible fade show" id="alertaFail" runat="server" role="alert">
        <strong>¡Fallaste!</strong> la respuesta es incorrecta.
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
   <div class="row justify-content-center align-items-center" style="height: 100%; width:100%;">
       <asp:HiddenField runat="server" ID="ID_NOTICIA"/>
       <asp:HiddenField runat="server" ID="LISTAID"/>
       <asp:HiddenField runat="server" ID="RESPUESTA"/>

        <asp:TextBox ID="txtPregunta" Enabled="false" style="max-width: 100%" runat="server" CssClass="form-control"/>
        <asp:TextBox TextMode="MultiLine" Enabled="false" style="max-width: 100%; width: 100%; margin-top: 5px" rows="3" runat="server" ID="txtTextoAdicional" class="form-control" ></asp:TextBox>
        <asp:Button id="BT_A" runat="server" CssClass="btn btn-primary mt-2"  OnClick="BT_A_Click" />
        <asp:Button id="BT_B" runat="server" CssClass="btn btn-primary mt-2"  OnClick="BT_B_Click" />
        <asp:Button id="BT_C" runat="server" CssClass="btn btn-primary mt-2"  OnClick="BT_C_Click" />
        <asp:Button id="BT_D" runat="server" CssClass="btn btn-primary mt-2"  OnClick="BT_D_Click" />
        <asp:Button id="BTSiguiente" runat="server"  CssClass="btn btn-success mt-2" OnClick="BTSiguiente_Click" Text="Siguiente Pregunta"/>
        <asp:Button id="BTSalir" runat="server"  CssClass="btn btn-secondary mt-2"  OnClick="BTSalir_Click" Text="Salir"/>
   </div>
 </div>
</asp:Content>
