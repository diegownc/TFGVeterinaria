<%@ Page Title="Noticias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Noticias.aspx.cs" Inherits="TFGVeterinaria.Noticias" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        th{
            color: white !important;
        }
</style>
    <script type="text/javascript">
    var deleteItemID = null;  // Variable para almacenar el ID de la mascota a eliminar

    // Esta función se llama cuando se hace clic en el botón de eliminar
    function showDeleteConfirmation(id) {
        // Guardamos el ID del item que se va a eliminar desde el CommandArgument del botón
        deleteItemID = id;
        // Mostramos el modal
        $('#confirmDeleteModal').modal('show');

        // Evitamos que el postback ocurra de inmediato
        return false;
    }

    function DeleteConfirmation() {
        if (deleteItemID != null) {
            //alert("ID a eliminar: " + deleteItemID);
            $('#MainContent_DELETE_FIELD').val(deleteItemID);
            // Realizamos el postback para eliminar el registro, usando el ID de la mascota
            //__doPostBack('myGridView', 'Delete$' + deleteItemID);
            __doPostBack("myGridView", "Delete$" + deleteItemID);

        }
        $('#confirmDeleteModal').modal('hide');  // Ocultamos el modal
    }

    function CancelConfirmation() {
        $('#confirmDeleteModal').modal('hide');  // Ocultamos el modal
    }

    function abrirModal(btn) {
        var id = $(btn).data("id");
        if (!id) return;

        $('#iframeModal').attr('src', '/Noticias_Encuesta/' + id);
        var modal = new bootstrap.Modal(document.getElementById('modalEncuesta'));
        modal.show();
    }
    </script>
<div class="container">
        <asp:HiddenField ID="DELETE_FIELD" runat="server" Value="" />
        
   <div class="modal fade" id="modalEncuesta" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
      <div class="modal-dialog modal-xl modal-dialog-centered">
        <div class="modal-content bg-dark text-white">
          <div class="modal-header">
            <h5 class="modal-title" id="modalLabel">Encuesta</h5>
            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
          </div>
          <div class="modal-body p-0">
            <iframe src="" id="iframeModal" width="100%" height="600px" frameborder="0"></iframe>
          </div>
        </div>
      </div>
    </div>


        <!-- Modal de confirmación de eliminación -->
        <div class="modal fade" id="confirmDeleteModal" tabindex="-1" role="dialog" aria-labelledby="confirmDeleteModalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header bg-dark">
                <h5 class="modal-title" id="confirmDeleteModalLabel">Confirmar eliminación</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body  bg-dark">
                ¿Estás seguro de que deseas eliminar esta noticia?
              </div>
              <div class="modal-footer bg-dark">
                <button type="button" class="btn btn-secondary" data-dismiss="modal"  onclick="CancelConfirmation()">Cancelar</button>
                <button type="button" class="btn btn-danger" id="confirmDeleteButton" onclick="DeleteConfirmation()">Eliminar</button>
              </div>
            </div>
          </div>
        </div>
        <div class="row justify-content-center align-items-center" style="height: 100%;">
             <asp:Button ID="btnActualizar" runat="server" Text="Actualizar Noticias"  OnClick="btnActualizar_Click" CssClass="btn btn-success m-2" />

            <asp:GridView ID="gvLecciones" runat="server" AutoGenerateColumns="False" OnRowCommand="gvLecciones_RowCommand" CssClass="table" OnRowDataBound="gvLecciones_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <img src='<%# Eval("ImagenURL") %>' alt="Imagen" width="120" style="height: auto"; />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div style="color: white">
                                <strong><%# Eval("Titulo") %></strong><br />
                                <span><%# Eval("Descripcion") %></span><br /><br />
                                <strong>Autor: </strong><em><%# Eval("Autor") %></em><br />
                                <strong>Fecha: </strong><span><%# Eval("Fecha", "{0:dd/MM/yyyy}") %></span>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnAcceder" runat="server" Text="Detalle" CommandName="Acceder" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-primary btn-sm"/>
                            <asp:Button ID="btnDelete" runat="server" Text="Eliminar"  CommandName="Delete" CssClass="btn btn-danger btn-sm" OnClientClick="return showDeleteConfirmation(this);"/>
                            <asp:Button ID="btnAbrirModal" runat="server" Text="Ver Lección"
                                CssClass="btn btn-info mt-2"
                                OnClientClick="abrirModal(this); return false;"
                                data-id='<%# Eval("ID") %>' Visible='<%# Convert.ToInt32(Eval("PREGUNTAS")) > 0 %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

         </div>
       
    </div>
</asp:Content>
