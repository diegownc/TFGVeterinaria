 <%@ Page Title="Detalle" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="Mascotas_Detalle.aspx.cs" Inherits="TFGVeterinaria.Mascotas_Detalle" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <style>
        body {
            background-color: #121212;
            color: #e0e0e0;
        }

        .form-control, .form-select {
            background-color: #e3e3e3; 
            color: #4f4f4f;
            border-color: #333;
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
        th{
            color: white !important;
        }
    </style>
<div class="container">
    <div class="row" style="height: 100%;">
         <div class="alert alert-success alert-dismissible fade show" id="alertaExito" runat="server" role="alert">
            <strong>¡Éxito!</strong> Se han guardado los datos correctamente.
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
        <div class="d-flex mb-2">        
            <div runat="server" id="seleccionarArchivo" class="mb-3">
                <label for="customFile" class="form-label">Selecciona un archivo</label>
                <input type="file" class="form-control" id="customFile">
            </div>
            <asp:Image ID="ImagenPerro" runat="server" ImageUrl="~/Imagenes/Perro2.jpg" CssClass="img-thumbnail" AlternateText="Perro" Width="100" />     
            <asp:button class="btn btn-primary m-lg-3" runat="server"  OnClick="Editar_Click" ID="EditarBtn" Text="Editar"></asp:button>
            <asp:button class="btn btn-success m-lg-3" runat="server" OnClick="Guardar" ID="GuardarBtn" Text="Guardar"></asp:button>
            <a class="btn btn-secondary m-lg-3" id="LoginBtn" runat="server" href="~/Mascotas">Volver</a>
        </div>
       
        <!-- Pestañas -->
        <ul class="nav nav-tabs mb-3" id="formTabs" role="tablist">
            <li class="nav-item">
                <button class="nav-link active" data-bs-toggle="tab" data-bs-target="#ficha" type="button">Ficha</button>
            </li>
            <li class="nav-item">
                <button class="nav-link" data-bs-toggle="tab" data-bs-target="#medicamentos" type="button">Medicamentos</button>
            </li>
            <li class="nav-item">
                <button class="nav-link" data-bs-toggle="tab" data-bs-target="#tratamientos" type="button">Tratamientos</button>
            </li>
            <li class="nav-item">
                <button class="nav-link" data-bs-toggle="tab" data-bs-target="#histmedicamentos" type="button">Historial Medicamentos</button>
            </li>
            <li class="nav-item">
                <button class="nav-link" data-bs-toggle="tab" data-bs-target="#histtratamientos" type="button">Historial Tratamientos</button>
            </li>
        </ul>

        <div class="tab-content"> 
            <!-- Ficha -->
            <div class="tab-pane fade show active" id="ficha">
                <form id="formficha">
                    <div class="mb-3">
                        <label for="nombre" class="form-label">Nombre Completo</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Text='<%# Bind("Nombre") %>' />
                    </div>
                    <div class="mb-3">
                        <label for="edad" class="form-label">Edad</label>
                        <asp:TextBox ID="txtEdad" runat="server" CssClass="form-control" Text='<%# Bind("Edad") %>' />
                    </div>
                    <div class="mb-3">
                        <label for="peso" class="form-label">Peso</label>
                        <asp:TextBox ID="txtPeso" runat="server" CssClass="form-control" Text='<%# Bind("Peso") %>' />
                    </div>
                    <div class="mb-3">
                        <label for="sexo" class="form-label">Sexo</label>
                        <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-select">
                            <asp:ListItem>Masculino</asp:ListItem>
                            <asp:ListItem>Femenino</asp:ListItem>
                            <asp:ListItem>Otro</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    
                </form>
            </div>
             
            <!-- Medicamentos -->
            <div class="tab-pane fade" id="medicamentos">
                <form id="formmedicamentos">
                    <div class="mb-3">
                        <label for="txtMedicamento" class="form-label">Nombre del Medicamento</label>
                        <asp:TextBox ID="txtMedicamento" runat="server" CssClass="form-control" Text='<%# Bind("Medicamento") %>' />
                    </div>
                    <div class="mb-3">
                        <label for="txtDosis" class="form-label">Dosis</label>
                        <asp:TextBox ID="txtDosis" runat="server" CssClass="form-control" Text='<%# Bind("Dosis") %>' />
                    </div>
                    <div class="mb-3">
                        <label for="txtDuracionMedicamentos" class="form-label">Duracion</label>
                        <asp:TextBox ID="txtDuracionMedicamento" runat="server" CssClass="form-control" Text='<%# Bind("Duracion") %>' />
                    </div>
                    <div class="mb-3">
                        <label for="txtFrecuenciaMedicamentos" class="form-label">Frecuencia</label>
                        <asp:TextBox ID="txtFrecuenciaMedicamento" runat="server" CssClass="form-control" Text='<%# Bind("Frecuencia") %>' />
                    </div>
                     <div class="mb-3">
                         <label for="txtObsMedicamentos" class="form-label">Observaciones</label>
                         <asp:TextBox ID="txtObsMedicamento" runat="server" CssClass="form-control" Text='<%# Bind("Observaciones") %>' TextMode="MultiLine" Rows="3" />
                     </div>  
                </form>
            </div>
             
            <!-- Tratamientos -->
            <div class="tab-pane fade" id="tratamientos">
                <form id="formtratamientos">
                    <div class="mb-3">
                        <label for="txtTipo" class="form-label">Tipo de Tratamiento</label>
                        <asp:TextBox ID="txtTipo" runat="server" CssClass="form-control" Text='<%# Bind("Tipo") %>' />
                    </div>
                    <div class="mb-3">
                        <label for="txtDuracionTratamiento" class="form-label">Duración Estimada</label>
                        <asp:TextBox ID="txtDuracionTratamiento" runat="server" CssClass="form-control" Text='<%# Bind("Duracion") %>' />
                    </div>
                    <div class="mb-3">
                        <label for="txtFrecuenciaTratamiento" class="form-label">Frecuencia</label>
                        <asp:TextBox ID="txtFrecuenciaTratamiento" runat="server" CssClass="form-control" Text='<%# Bind("Frecuencia") %>' />
                    </div>
                    <div class="mb-3">
                        <label for="txtObservaciones" class="form-label">Observaciones</label>
                        <asp:TextBox ID="txtObsTratamiento" runat="server" CssClass="form-control" Text='<%# Bind("Observaciones") %>' TextMode="MultiLine" Rows="3" />
                    </div>
                    
                </form> 
            </div>
 
            <div class="tab-pane fade" id="histmedicamentos">
                <asp:GridView ID="DTmedicamentos"  PagerStyle-ForeColor="White"  ForeColor="White"  RowStyle-ForeColor="White" AlternatingRowStyle-BackColor="#858585" HeaderStyle-ForeColor="White" runat="server" CssClass="table table-striped table-bordered" 
                                AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="ID" ItemStyle-ForeColor="White" HeaderText="ID" SortExpression="ID" Visible="false" />
                        <asp:BoundField DataField="FECHA" ItemStyle-ForeColor="White" HeaderText="Fecha" SortExpression="Fecha" />
                        <asp:BoundField DataField="NOMBRE" ItemStyle-ForeColor="White" HeaderText="Nombre" SortExpression="Nombre" />
                        <asp:BoundField DataField="DOSIS" ItemStyle-ForeColor="White" HeaderText="Dosis" SortExpression="Dosis" />
                        <asp:BoundField DataField="FRECUENCIA" ItemStyle-ForeColor="White" HeaderText="Frecuencia" SortExpression="Frecuencia" />
                        <asp:BoundField DataField="DURACION" ItemStyle-ForeColor="White" HeaderText="Duración" SortExpression="Duracion" />
                        <asp:BoundField DataField="OBS" ItemStyle-ForeColor="White" HeaderText="Observaciones" SortExpression="Observaciones" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="tab-pane fade" id="histtratamientos">
                <asp:GridView ID="DTtratamientos"  PagerStyle-ForeColor="White"  ForeColor="White"  RowStyle-ForeColor="White" AlternatingRowStyle-BackColor="#858585" HeaderStyle-ForeColor="White" runat="server" CssClass="table table-striped table-bordered" 
                            AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="ID" ItemStyle-ForeColor="White" HeaderText="ID" SortExpression="ID" Visible="false" />
                        <asp:BoundField DataField="FECHA" ItemStyle-ForeColor="White" HeaderText="Fecha" SortExpression="Nombre" />
                        <asp:BoundField DataField="TIPO" ItemStyle-ForeColor="White" HeaderText="Tipo de Tratamiento" SortExpression="Nombre" />
                        <asp:BoundField DataField="FRECUENCIA" ItemStyle-ForeColor="White" HeaderText="Frecuencia" SortExpression="Frecuencia" />
                        <asp:BoundField DataField="DURACION" ItemStyle-ForeColor="White" HeaderText="Duracion" SortExpression="Nombre" />
                        <asp:BoundField DataField="OBS" ItemStyle-ForeColor="White" HeaderText="Observaciones" SortExpression="Edad" />
                    </Columns>
             </asp:GridView>
            </div>
        </div>
    </div>
</div>
</asp:Content>
