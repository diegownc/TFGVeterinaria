<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mascotas.aspx.cs" Inherits="TFGVeterinaria.Mascotas" %>


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
    </script>
<div class="container">
        <asp:HiddenField ID="DELETE_FIELD" runat="server" Value="" />

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
                ¿Estás seguro de que deseas eliminar esta mascota?
              </div>
              <div class="modal-footer bg-dark">
                <button type="button" class="btn btn-secondary" data-dismiss="modal"  onclick="CancelConfirmation()">Cancelar</button>
                <button type="button" class="btn btn-danger" id="confirmDeleteButton" onclick="DeleteConfirmation()">Eliminar</button>
              </div>
            </div>
          </div>
        </div>
        <div class="row justify-content-center align-items-center" style="height: 100%;">
             <div class="container mt-5">
             <h2>Mascotas</h2>
             <asp:Button ID="btnRedirect" runat="server" Text="Registrar Nueva Mascota" OnClick="btnRedirect_Click" CssClass="btn btn-success m-2" />

             <asp:GridView ID="myGridView"   PagerStyle-ForeColor="White"  ForeColor="White"  RowStyle-ForeColor="White" AlternatingRowStyle-BackColor="#858585" HeaderStyle-ForeColor="White" runat="server" CssClass="table table-striped table-bordered" 
                             AutoGenerateColumns="False" OnRowCommand="myGridView_RowCommand" 
                             OnRowEditing="myGridView_RowEditing" OnRowUpdating="myGridView_RowUpdating" 
                             OnRowCancelingEdit="myGridView_RowCancelingEdit" OnRowDeleting="myGridView_RowDeleting" OnRowDataBound="myGridView_RowDataBound" DataKeyNames="ID">
                 <Columns>
                     <asp:ButtonField CommandName="Select" Text="Detalle" ButtonType="Button" 
                            ControlStyle-CssClass="btn btn-primary btn-sm" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <!-- Botón de eliminación -->
                            <asp:Button ID="btnDelete" runat="server" 
                                Text="Eliminar" 
                                CommandName="Delete"
                                CssClass="btn btn-danger btn-sm" 
                                OnClientClick="return showDeleteConfirmation(this);"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:BoundField DataField="ID" ItemStyle-ForeColor="White" HeaderText="ID" SortExpression="ID" Visible="false" />
                     <asp:BoundField DataField="Dueno" ItemStyle-ForeColor="White" HeaderText="Dueño" SortExpression="Nombre" />
                     <asp:TemplateField HeaderText="Imagen">
                        <ItemTemplate>
                            <!-- Usar el control Image y enlazar la URL -->
                            <asp:Image ID="imgUrl" runat="server" 
                                ImageUrl='<%# Eval("ImageUrl") %>' 
                                CssClass="img-fluid" 
                                Width="100" style="height: auto;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:BoundField DataField="Nombre" ItemStyle-ForeColor="White" HeaderText="Nombre" SortExpression="Nombre" />
                     <asp:BoundField DataField="Edad" ItemStyle-ForeColor="White" HeaderText="Edad" SortExpression="Edad" />
                     <asp:BoundField DataField="Peso" ItemStyle-ForeColor="White" HeaderText="Peso" SortExpression="Peso" />

                 </Columns>
             </asp:GridView>
         </div>
        </div>
    </div>
</asp:Content>
