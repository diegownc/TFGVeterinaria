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
        $('#modalLabel').text('Reserva');
        $('#iframeModal').attr('src', '/ServiciosCita/' + id);
        var modal = new bootstrap.Modal(document.getElementById('modalServicio'));
        modal.show();
    }

    function abrirModalEditar(btn) {
        var id = $(btn).data("id");
        if (!id) return;

        $('#modalLabel').text('Editar');
        $('#iframeModal').attr('src', '/ServiciosEditar/' + id);
        var modal = new bootstrap.Modal(document.getElementById('modalServicio'));
        modal.show();
    }
    </script>
<div class="container">
    <asp:HiddenField ID="DELETE_FIELD" runat="server" Value="" />
     
    <div class="modal fade" id="modalServicio" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
       <div class="modal-dialog modal-xl modal-dialog-centered">
         <div class="modal-content bg-dark text-white">
           <div class="modal-header">
             <h5 class="modal-title" id="modalLabel">Reserva</h5>
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
          <div class="modal-body bg-dark">
            ¿Estás seguro de que deseas eliminar este servicio?
          </div>
          <div class="modal-footer bg-dark">
            <button type="button" class="btn btn-secondary" data-dismiss="modal"  onclick="CancelConfirmation()">Cancelar</button>
            <button type="button" class="btn btn-danger" id="confirmDeleteButton" onclick="DeleteConfirmation()">Eliminar</button>
          </div>
        </div>
      </div>
    </div>

   <div class="row justify-content-center align-items-center" style="height: 100%; width:100%;">
      <asp:ListView ID="lvServicios" runat="server" DataKeyNames="Id"  OnItemDataBound="lvServicios_ItemDataBound" >
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
                                <strong>Descripción: </strong> <%# Eval("DESCRIPCION") %> <br />
                            </p>
                            <asp:Button ID="btnAbrirModal" runat="server" Text="Solicitar Cita" CssClass="btn btn-sm btn-secondary w-100" OnClientClick="abrirModal(this); return false;" data-id='<%# Eval("ID") %>' />
                            <asp:Button ID="btnEdit" runat="server" Text="Editar" CssClass="btn btn-sm btn-warning w-100 mt-1" OnClientClick="abrirModalEditar(this); return false;" data-id='<%# Eval("ID") %>' />
                            <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CssClass="btn btn-danger btn-sm w-100 mt-1" CommandName="Delete" OnClientClick="return showDeleteConfirmation(this);" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>


   </div>
 </div>
</asp:Content>
