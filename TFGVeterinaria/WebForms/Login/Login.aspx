<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TFGVeterinaria.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
        <div class="row justify-content-center align-items-center" style="height: 100vh;">
            <div class="col-md-5">
                <div class="card bg-secondary text-white">
                    <div class="card-header text-center">
                        <h4>Iniciar sesión</h4>
                    </div>
                    <div class="card-body">
                        <form id="loginForm">
                             <div class="form-group">
                                 <label for="txtUserName">Usuario</label>
                                 <asp:TextBox CssClass="form-control mb-1" style="max-width: 100%" placeholder="Ingrese su nombre de usuario" ID="txtUserName" runat="server" />
                             </div>
                            <div class="form-group mt-2">
                                <label for="txtPassword">Contraseña</label>
                                <asp:TextBox  CssClass="form-control"  ID="txtPassword" runat="server" TextMode="Password" placeholder="Ingrese su contraseña" style="max-width: 100%" />
                            </div>
                            <!-- Reset Password -->
                            <a href="Login.aspx" style="color: lightskyblue"> ¿Has olvidado tu Contraseña? </a>
                            <!-- Error Message -->
                            <div>
                                <span id="lblMessage" runat="server"></span>
                            </div>
                            <!-- Login Button -->
                            <div class="text-center" style="margin-top: 20px">
                                <button type="submit" id="btnLogin" class="btn btn-primary"  onserverclick="btnLogin_ServerClick" runat="server">Iniciar sesión</button>
                                <a class="btn btn-success" href="Registro" style="color: lightgoldenrodyellow"> Registrarse </a>
                            </div>
                        </form>
                         
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
