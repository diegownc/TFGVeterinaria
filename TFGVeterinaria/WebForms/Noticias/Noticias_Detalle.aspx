<%@ Page Title="Noticias Detalle" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Noticias_Detalle.aspx.cs" Inherits="TFGVeterinaria.Noticias_Detalle" %>


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
            <div class="col-6 text-right">
                <div runat="server" id="seleccionarArchivo" class="mb-3">
                    <label for="customFile" class="form-label">Selecciona un archivo</label>
                    <input type="file" class="form-control" id="customFile">
                </div>
                <asp:Image ID="ImagenNoticia" runat="server" CssClass="img-thumbnail" AlternateText="Noticia" Width="300" />
                <asp:button class="btn btn-primary m-lg-3" runat="server"  OnClick="Editar_Click" ID="EditarBtn" Text="Editar"></asp:button>
                <asp:button class="btn btn-success m-lg-3" runat="server" OnClick="Guardar_Click" ID="GuardarBtn" Text="Guardar"></asp:button>
                <a class="btn btn-secondary" id="btnVolver" runat="server" href="~/Noticias">Volver</a>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="ID_NOTICIA" Value="-1"/>
        <div class="row justify-content-center align-items-center" style="height: 100%; width:100%;">
             <form>
                <div class="form-group mt-3">
                    <label for="txtTitulo" class="form-label fw-bold">Titulo</label>
                    <asp:TextBox ID="txtTitulo" style="max-width: 100%" runat="server" CssClass="form-control"/>
                </div>
                 <div class="form-group mt-1">
                     <label for="txtDescripcion" class="form-label fw-bold">Descripción</label>
                    <asp:TextBox TextMode="MultiLine" style="max-width: 100%; width: 100%;" rows="3" runat="server" ID="txtDescripcion" class="form-control"  placeholder="Escribe el contenido aquí..."></asp:TextBox>
                </div>
                <div class="form-group mt-1">
                     <label for="txtContenido" class="form-label fw-bold">Contenido</label>
                    <asp:TextBox TextMode="MultiLine" style="max-width: 100%; width: 100%;" rows="10" runat="server" ID="txtContenido" class="form-control"  placeholder="Escribe el contenido aquí..."></asp:TextBox>
                </div>
                 <asp:Button CssClass="btn btn-success" ID="btnCrearPregunta" Text="Añadir una pregunta" runat="server" OnClick="btnCrearPregunta_Click" />
                 <div class="form-group mt-1">
                     <asp:GridView ID="GridPreguntas"   PagerStyle-ForeColor="White"  ForeColor="White"  RowStyle-ForeColor="White" AlternatingRowStyle-BackColor="#858585" HeaderStyle-ForeColor="White" runat="server" CssClass="table table-striped table-bordered" 
                             AutoGenerateColumns="False" OnRowEditing="GridPreguntas_RowEditing" 
                                 OnRowUpdating="GridPreguntas_RowUpdating" 
                                 OnRowCancelingEdit="GridPreguntas_RowCancelingEdit" OnRowDataBound="GridPreguntas_RowDataBound"  OnRowCommand="GridPreguntas_RowCommand">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="80px">
                                 <ItemTemplate>
                                     <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CssClass="btn btn-warning" Width="80">
                                         <i class="bi bi-pencil"></i> Editar
                                     </asp:LinkButton>
                                      <asp:LinkButton ID="btnEliminar" runat="server" CommandName="DeletePersonalizado" Width="80" CssClass="btn btn-sm btn-danger mt-2" Text="Eliminar" CommandArgument='<%# Eval("ID") %>' >
                                    </asp:LinkButton>
                                 </ItemTemplate>
                                 <EditItemTemplate>
                                     <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-success me-1" Width="80">
                                         <i class="bi bi-check"></i> Guardar
                                     </asp:LinkButton>
                                     <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CssClass="btn btn-secondary mt-2" Width="80">
                                         <i class="bi bi-x"></i> Cancelar
                                     </asp:LinkButton>
                                 </EditItemTemplate>
                            </asp:TemplateField>
                             
                            <asp:TemplateField HeaderText="Pregunta" ItemStyle-Width="300px">
                                  <EditItemTemplate>
                                      <asp:TextBox ID="txtPregunta" Text='<%#Eval("PREGUNTA") %>' style="max-width: 100%" runat="server" CssClass="form-control"/> <br />
                                      <asp:TextBox ID="txtTextoAdicional" Text='<%#Eval("TEXTOADICIONAL") %>' TextMode="MultiLine" style="max-width: 100%; width: 100%;" rows="3" runat="server" class="form-control"  placeholder="Escribe el texto explicando la respuesta o un dato adicional..."></asp:TextBox>
                                  </EditItemTemplate>
                                  <ItemTemplate>
                                      <asp:Label ID="lbpregunta" CssClass="fw-bold" runat="server" Text='<%#Eval("PREGUNTA") %>' /> <br />
                                      <asp:Label ID="lbtextoadicional" runat="server" Text='<%#Eval("TEXTOADICIONAL") %>' />
                                  </ItemTemplate>
                             </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="ID" />
                            <asp:BoundField DataField="RESPUESTA_A" ItemStyle-ForeColor="White" HeaderText="A" />
                            <asp:BoundField DataField="RESPUESTA_B" ItemStyle-ForeColor="White" HeaderText="B" />
                            <asp:BoundField DataField="RESPUESTA_C" ItemStyle-ForeColor="White" HeaderText="C" />
                            <asp:BoundField DataField="RESPUESTA_D" ItemStyle-ForeColor="White" HeaderText="D" />
 

                            <asp:TemplateField HeaderText="Respuesta" ItemStyle-Width="50px">
                                 <EditItemTemplate>
                                     <asp:DropDownList ID="ddlrespuestas" runat="server"  CssClass="form-select" />
                                 </EditItemTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="lbrespuesta" runat="server" Text='<%# Eval("RESPUESTA") %>' />
                                 </ItemTemplate>
                             </asp:TemplateField>

                         </Columns>
                     </asp:GridView>
                 </div>
            </form>
         </div>
       
    </div>
</asp:Content>
