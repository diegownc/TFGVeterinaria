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
                            <!-- Username -->
                            <div class="form-group">
                                <label for="txtUsername">Usuario</label>
                                <input type="text" class="form-control" id="txtUsername" runat="server" placeholder="Ingrese su usuario" style="max-width: 100%" required />
                            </div>
                            <!-- Password -->
                            <div class="form-group">
                                <label for="txtPassword">Contraseña</label>
                                <input type="password" class="form-control" id="txtPassword" runat="server" placeholder="Ingrese su contraseña" style="max-width: 100%" required />
                            </div>
                            <!-- Reset Password -->
                            <a href="Login.aspx" style="color: lightskyblue"> ¿Has olvidado tu Contraseña? </a>
                            <!-- Error Message -->
                            <div>
                                <span id="lblMessage" runat="server" style="color: red;"></span>
                            </div>
                            <!-- Login Button -->
                            <div class="text-center" style="margin-top: 20px">
                                <button type="submit" id="btnLogin" class="btn btn-primary" runat="server">Iniciar sesión</button>
                                <button type="submit" id="btnRegistrarse" class="btn btn-success" runat="server">Registrarse</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
