<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiciosEditar.aspx.cs" Inherits="TFGVeterinaria.ServiciosEditar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Reserva</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/noUiSlider/15.6.1/nouislider.min.css" rel="stylesheet" />
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
            width: 80%;
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

        .form-check-input{
            width: 20px !important;
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
                <asp:Image ID="ImagenServicio" runat="server" CssClass="img-thumbnail" AlternateText="Servicio" Width="300" Height="200" />

                <!-- Formulario -->
                <div class="row justify-content-center align-items-center" style="height: 100%; width:100%;">
                    <div class="form-container">
                        <label for="txtTitulo"> Titulo </label>
                        <asp:TextBox ID="txtTitulo" runat="server" placeholder="Escribe un titulo..." 
                            CssClass="form-control"></asp:TextBox> 
                        <label for="txtPrecio"> Precio </label>
                        <asp:TextBox ID="txtPrecio" runat="server" placeholder="Establece un precio..." TextMode="Number" 
                            CssClass="form-control"></asp:TextBox> 
                        <label for="txtUbicacion"> Ubicación </label>
                        <asp:TextBox ID="txtUbicacion" runat="server" placeholder="Escribe una ubicación..." 
                            CssClass="form-control"></asp:TextBox> 
                        <label for="txtDescripcion"> Descripción </label>
                        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" 
                            placeholder="Descripción" CssClass="form-control" Rows="4"></asp:TextBox>
                        <br />
                        <label for="slider-range">Establece un horario:</label>
                        <div id="slider-range"></div>
                        <p>Intervalo: <span id="slider-range-value"></span></p>
                        <asp:HiddenField ID="hfMin" runat="server" />
                        <asp:HiddenField ID="hfMax" runat="server" />
    
                        <div class="form-group">
                            <label>Días de la semana:</label><br />
    
                            <div class="form-check form-check-inline">
                                <input class="form-check-input" checked type="checkbox" id="chkLunes" value="lunes">
                                <label class="form-check-label" for="chkLunes">Lunes</label>
                            </div>
                            <div class="form-check form-check-inline">
                                <input class="form-check-input" checked type="checkbox" id="chkMartes" value="martes">
                                <label class="form-check-label" for="chkMartes">Martes</label>
                            </div>
                            <div class="form-check form-check-inline">
                                <input class="form-check-input" checked type="checkbox" id="chkMiercoles" value="miércoles">
                                <label class="form-check-label" for="chkMiercoles">Miércoles</label>
                            </div>
                            <div class="form-check form-check-inline">
                                <input class="form-check-input" checked type="checkbox" id="chkJueves" value="jueves">
                                <label class="form-check-label" for="chkJueves">Jueves</label>
                            </div>
                            <div class="form-check form-check-inline">
                                <input class="form-check-input" checked type="checkbox" id="chkViernes" value="viernes">
                                <label class="form-check-label" for="chkViernes">Viernes</label>
                            </div>
                            <div class="form-check form-check-inline">
                                <input class="form-check-input" checked type="checkbox" id="chkSabado" value="sábado">
                                <label class="form-check-label" for="chkSabado">Sábado</label>
                            </div>
                            <div class="form-check form-check-inline">
                                <input class="form-check-input" checked type="checkbox" id="chkDomingo" value="domingo">
                                <label class="form-check-label" for="chkDomingo">Domingo</label>
                            </div>
                    </div>

                        
                    <asp:Button ID="btnGuardar" style="background-color: #198754" runat="server" Text="Guardar" 
                        CssClass="btn btn-success mt-2" />
                    </div>                
                </div>
            </div>
    </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/noUiSlider/15.6.1/nouislider.min.js"></script>
    <script>
        var slider = document.getElementById('slider-range');
        var output = document.getElementById('slider-range-value');

        noUiSlider.create(slider, {
            start: [8, 17],  // valores iniciales
            connect: true,
            range: {
                'min': 0,
                'max': 24
            },
            tooltips: true,  // muestra los valores en el control
            step: 1
        });

        slider.noUiSlider.on('update', function (values, handle) {
            output.innerHTML = `${Math.round(values[0])} - ${Math.round(values[1])}`;

            // Actualizar campos ocultos para enviar al servidor
            document.getElementById('<%= hfMin.ClientID %>').value = Math.round(values[0]);
            document.getElementById('<%= hfMax.ClientID %>').value = Math.round(values[1]);
        });
    </script>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/Scripts/bootstrap.js") %>
    </asp:PlaceHolder>
</body>
</html>
