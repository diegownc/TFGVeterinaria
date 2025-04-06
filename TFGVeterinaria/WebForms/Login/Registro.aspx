<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="TFGVeterinaria.Registro" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
        <div class="row justify-content-center align-items-center" style="height: 100%;">
            <div class="col-md-7">
                <div class="card bg-secondary text-white">
                    <div class="card-header text-center">
                        <h4>Registro</h4>
                    </div>
                    <div class="card-body">
                        <form id="loginForm">
                            <!-- Username -->
                            <div class="form-group">
                                <label for="txtUserName">Usuario</label>
                                <asp:TextBox CssClass="form-control mb-1" style="max-width: 100%" placeholder="Ingrese su nombre de usuario" ID="txtUserName" runat="server" />
                                <asp:RequiredFieldValidator 
                                    ID="rfvUserName" 
                                    runat="server" 
                                    ControlToValidate="txtUserName" 
                                    ErrorMessage="El usuario es obligatorio" 
                                    CssClass="error-message"
                                    Display="Dynamic" ValidationGroup="ValidationGroup" />
                            </div>
                      
                            <!-- Name -->
                            <div class="form-group mt-2">
                                <label for="txtName">Nombre</label>
                                <asp:TextBox CssClass="form-control mb-1" style="max-width: 100%" placeholder="Ingrese su nombre de usuario" ID="txtName" runat="server" />
                                <asp:RequiredFieldValidator 
                                    ID="rfvNombre" 
                                    runat="server" 
                                    ControlToValidate="txtName" 
                                    ErrorMessage="El nombre es obligatorio" 
                                    CssClass="error-message"
                                    Display="Dynamic" ValidationGroup="ValidationGroup" />
                            </div>
                            <!-- Name -->
                            <div class="form-group mt-2">
                                <label for="txtEmail">Email</label>
                                <asp:TextBox CssClass="form-control mb-1" style="max-width: 100%" placeholder="Ingrese su Email" ID="txtEmail" runat="server" />                                
                                <asp:RegularExpressionValidator 
                                    ID="revEmail" 
                                    runat="server" 
                                    Display="Dynamic"
                                    ControlToValidate="txtEmail" 
                                    ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" 
                                    ErrorMessage="Por favor, ingrese un correo electrónico válido. Ejemplo: correo@gmail.com" 
                                    CssClass="error-message" ValidationGroup="ValidationGroup" />
                                <asp:RequiredFieldValidator 
                                    ID="rfvNombreEmail" 
                                    runat="server" 
                                    Display="Dynamic"
                                    ControlToValidate="txtEmail" 
                                    ErrorMessage="El email es obligatorio" 
                                    CssClass="error-message" ValidationGroup="ValidationGroup" />
                            </div>
                            <!-- Password -->
                            <div class="form-group mt-2">
                                <label for="txtPassword">Contraseña</label>
                                <asp:TextBox  CssClass="form-control"  ID="txtPassword" runat="server" TextMode="Password" placeholder="Ingrese su contraseña" style="max-width: 100%" />
                                <label for="txtConfirmPassword">Repite la Contraseña</label>
                                <asp:TextBox  CssClass="form-control mb-1"  ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="Ingrese su contraseña otra vez"  style="max-width: 100%"/>
                                
                                <!-- Validadores -->
                                <div style="display: inline-block">
                                    <asp:RequiredFieldValidator 
                                         ID="rfvPassword" 
                                         runat="server" 
                                         ControlToValidate="txtPassword" 
                                         ErrorMessage="La contraseña es obligatoria" 
                                         CssClass="error-message" Display="Dynamic" ValidationGroup="ValidationGroup" />
                                </div>
                                <div style="display: inline-block" class="mt-2">
                                     <asp:RequiredFieldValidator 
                                           ID="RequiredFieldValidator1" 
                                           runat="server" 
                                           ControlToValidate="txtConfirmPassword" 
                                           ErrorMessage="Repetir la contraseña es obligatorio" 
                                           CssClass="error-message" Display="Dynamic" ValidationGroup="ValidationGroup" />
                                </div>
                                <div style="display: inline-block" class="mt-2">
                                    <asp:RegularExpressionValidator 
                                        ID="revPasswordLength" 
                                        runat="server" 
                                        ControlToValidate="txtPassword" 
                                        ValidationExpression="^.{8,}$" 
                                        ErrorMessage="La contraseña debe tener al menos 8 caracteres" 
                                        CssClass="error-message" Display="Dynamic" ValidationGroup="ValidationGroup" />
                                </div>
                                <div class="mt-2">
                                    <asp:RegularExpressionValidator 
                                        ID="RegularExpressionValidator1" 
                                        runat="server" 
                                        ControlToValidate="txtPassword" 
                                        ValidationExpression="^(?=.*[A-Z]).*$"
                                        ErrorMessage="La contraseña debe contener al menos una letra mayúscula" 
                                        CssClass="error-message" Display="Dynamic" ValidationGroup="ValidationGroup"/>

                                </div>
                                <div style="display: inline-block" class="mt-2">
                                    <asp:RegularExpressionValidator 
                                        ID="revPasswordLowerCase" 
                                        runat="server" 
                                        ControlToValidate="txtPassword" 
                                        ValidationExpression="^(?=.*[a-z]).*$"
                                        ErrorMessage="La contraseña debe contener al menos una letra minúscula" 
                                        CssClass="error-message" Display="Dynamic" ValidationGroup="ValidationGroup" />
                                </div>
                                <div style="display: inline-block" class="mt-2">
                                    <asp:RegularExpressionValidator 
                                        ID="revPasswordNumber" 
                                        runat="server" 
                                        ControlToValidate="txtPassword" 
                                        ValidationExpression="^(?=.*\d).*$"
                                        ErrorMessage="La contraseña debe contener al menos un número" 
                                        CssClass="error-message" Display="Dynamic" ValidationGroup="ValidationGroup" />
                                </div>
                                <div style="display: inline-block" class="mt-2">
                                    <asp:RegularExpressionValidator 
                                        ID="revPasswordSpecialChar" 
                                        runat="server" 
                                        ControlToValidate="txtPassword" 
                                        ValidationExpression="^(?=.*[@$!%*?&]).*$"
                                        ErrorMessage="La contraseña debe contener al menos un carácter especial (@, $, %, etc.)" 
                                        CssClass="error-message" Display="Dynamic" ValidationGroup="ValidationGroup" />
                                </div>
                                <div style="display: inline-block" class="mt-2">
                                    <asp:CompareValidator 
                                        ID="cvConfirmPassword" 
                                        runat="server" 
                                        ControlToValidate="txtConfirmPassword" 
                                        ControlToCompare="txtPassword" 
                                        ErrorMessage="Las contraseñas no coinciden" 
                                        Display="Dynamic" CssClass="error-message" ValidationGroup="ValidationGroup" />
                                </div>
                                
                            <div class="form-check form-switch">
                              <input id="chkVeterinario" class="form-check-input" type="checkbox" role="switch" runat="server" checked>
                              <label class="form-check-label" for="flexSwitchCheckChecked">Registrarse como Veterinario</label>
                            </div>
                            <!-- Error Message -->
                            <div>
                                <span id="lblMessage" runat="server" ></span>
                            </div>
                            <!-- Login Button -->
                            <div class="text-center" style="margin-top: 20px">
                                <asp:Button ID="btnRegistrarse" runat="server" Text="Registrarse"  OnClientClick="return Page_ClientValidate('ValidationGroup');" CssClass="btn btn-success" OnClick="btnRegistrarse_ServerClick" /> 
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
