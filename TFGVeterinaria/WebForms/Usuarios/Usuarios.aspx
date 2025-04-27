<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="TFGVeterinaria.Usuarios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        th{
            color: white !important;
        }
        .form-check.form-switch .form-check-input {
        width: 2em;
        height: 1em;
    }
    </style>
<div class="container">
    <div class="alert alert-success alert-dismissible fade show" id="alertaExito" runat="server" role="alert">
        <strong>¡Éxito!</strong> Se han guardado los datos correctamente.
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
   <div class="row justify-content-center align-items-center" style="height: 100%; width:100%;">
       <asp:GridView ID="GridView1"   PagerStyle-ForeColor="White"  ForeColor="White"  RowStyle-ForeColor="White" AlternatingRowStyle-BackColor="#858585" HeaderStyle-ForeColor="White" runat="server" CssClass="table table-striped table-bordered" 
                AutoGenerateColumns="False" OnRowEditing="GridView1_RowEditing" 
                    OnRowUpdating="GridView1_RowUpdating" 
                    OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound">
           <Columns>
               <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-warning">
                        <i class="bi bi-pencil"></i> Editar
                    </asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-sm btn-success me-1">
                        <i class="bi bi-check"></i> Guardar
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CssClass="btn btn-sm btn-secondary">
                        <i class="bi bi-x"></i> Cancelar
                    </asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Usuario"  ItemStyle-ForeColor="White">
                    <ItemTemplate>
                        <asp:Label ID="lblUsuario" style="color: white;" runat="server" Text='<%# Eval("Usuario") %>' CssClass="form-control-plaintext" />
                    </ItemTemplate>
               </asp:TemplateField>
               
               <asp:BoundField DataField="NOMBRE" ItemStyle-ForeColor="White" HeaderText="Nombre" />
               <asp:BoundField DataField="EMAIL" ItemStyle-ForeColor="White" HeaderText="Email" />
               <asp:TemplateField HeaderText="Activo">
                    <EditItemTemplate>
                          <div class="form-check form-switch">
                                <input type="checkbox" runat="server" class="form-check-input" id="chkActivo"
                                    Checked='<%# Convert.ToInt32(Eval("ACTIVO")) == 1 %>' />
                            </div>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblActivo" runat="server" Text='<%# Convert.ToInt32(Eval("ACTIVO")) == 1 ? "Sí" : "No" %>' />
                    </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Perfil">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlRoles" runat="server"  CssClass="form-select" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblRol" runat="server" Text='<%# Eval("PERFIL") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

               <asp:TemplateField HeaderText="Verificado">
                     <EditItemTemplate>
                           <div class="form-check form-switch">
                                <input type="checkbox" runat="server" class="form-check-input" id="chkVerificado"
                                    Checked='<%# Convert.ToInt32(Eval("VERIFICADO")) == 1 %>' />
                            </div>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="lblVerificado" runat="server" Text='<%# Convert.ToInt32(Eval("VERIFICADO")) == 1 ? "Sí" : "No" %>' />
                     </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
   </div>
 </div>
</asp:Content>
