<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiciosCita.aspx.cs" Inherits="TFGVeterinaria.ServiciosCita" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Reserva</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #121212;
            color: white;
        }
        .container {
            display: flex;
            justify-content: space-between;
            padding: 20px;
        }
        .form-container {
            width: 45%;
            padding: 20px;
            border-radius: 10px;
        }
        .form-container h2 {
            color: white;
            margin-bottom: 20px;
        }
        .form-container input,
        .form-container select,
        .form-container button {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border-radius: 5px;
        }
        .form-container input,
        .form-container textarea,
        .form-container select{
            background-color: #333;
            color: white;
            border: 1px solid #444;
        }

        .form-container input:focus,
        .form-container textarea:focus,
        .form-container select:focus{
            background-color: #717171;
            color: white;
        }

        .form-container textarea::placeholder,
        .form-container input::placeholder{
            color: white;
        }

        .form-container button:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body class="bg-dark">
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
            <div class="container">
                <!-- Calendario -->
                <div class="card">
                    <div class="card-header bg-primary text-white">
                        Selecciona una fecha
                    </div>
                    <div class="card-body p-2">
                        <asp:Calendar ID="Calendar1" runat="server" CssClass="table table-bordered calendar-bootstrap" 
                            NextPrevStyle-ForeColor="black" 
                            TitleStyle-CssClass="text-center bg-light font-weight-bold"
                            DayStyle-CssClass="text-center"
                            OtherMonthDayStyle-CssClass="text-muted"
                            TodayDayStyle-CssClass="bg-info text-white font-weight-bold"
                            SelectedDayStyle-CssClass="bg-primary text-white" />
                    </div>
                </div>
                <!-- Formulario -->
                <div class="form-container">
                    <h2>Formulario de Reserva</h2>
                    <asp:TextBox ID="txtEmail" runat="server" placeholder="Correo electrónico" 
                        CssClass="form-control"></asp:TextBox>
                    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" 
                        placeholder="Descripción" CssClass="form-control" Rows="4"></asp:TextBox>
                    <asp:DropDownList ID="ddlMascotas" runat="server" CssClass="form-control mt-2">
                        <asp:ListItem Text="Seleccione una Mascota" Value=""></asp:ListItem>
                        <asp:ListItem Text="Juanita" Value="Juanita"></asp:ListItem>
                        <asp:ListItem Text="María" Value="María"></asp:ListItem>
                        <asp:ListItem Text="Pedrito" Value="Pedrito"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlHora" runat="server" CssClass="form-control mt-2">
                        <asp:ListItem Text="Seleccione hora" Value=""></asp:ListItem>
                        <asp:ListItem Text="08:00 AM" Value="08:00 AM"></asp:ListItem>
                        <asp:ListItem Text="09:00 AM" Value="09:00 AM"></asp:ListItem>
                        <asp:ListItem Text="10:00 AM" Value="10:00 AM"></asp:ListItem>
                        <asp:ListItem Text="11:00 AM" Value="11:00 AM"></asp:ListItem>
                        <asp:ListItem Text="12:00 PM" Value="12:00 PM"></asp:ListItem>
                        <asp:ListItem Text="01:00 PM" Value="01:00 PM"></asp:ListItem>
                        <asp:ListItem Text="02:00 PM" Value="02:00 PM"></asp:ListItem>
                        <asp:ListItem Text="03:00 PM" Value="03:00 PM"></asp:ListItem>
                        <asp:ListItem Text="04:00 PM" Value="04:00 PM"></asp:ListItem>
                        <asp:ListItem Text="05:00 PM" Value="05:00 PM"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnEnviar" style="background-color: #0861a1" runat="server" Text="Enviar" 
                        CssClass="btn btn-primary" />
                </div>
            </div>
    </form>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/Scripts/bootstrap.js") %>
    </asp:PlaceHolder>
</body>
</html>
