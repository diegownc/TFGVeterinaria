<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Noticias_Encuesta.aspx.cs" Inherits="TFGVeterinaria.Noticias_Encuesta" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Pregunta</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-dark text-white">
    <script>
    function cerrarModalDesdeIframe() {
        const modal = window.parent.bootstrap.Modal.getInstance(
            window.frameElement.closest('.modal')
        );
        if (modal) {
            modal.hide();
        }
    }
    </script>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see https://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>

    <div class="container mt-3">
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
       </div>
        <div class="row justify-content-end" style="width:25%;">
            <Button onclick="cerrarModalDesdeIframe()"  class="btn btn-secondary mt-2"> Salir </Button>
        </div>
    </div>
    </form>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/Scripts/bootstrap.js") %>
    </asp:PlaceHolder>
</body>
</html>
