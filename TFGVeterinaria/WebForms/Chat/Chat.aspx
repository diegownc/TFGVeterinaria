<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="TFGVeterinaria.Chat" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<style>
        th{
            color: white !important;
        }

        .mensaje{
            color: white;
        }

        .fechamsg{
            color: white;
            font-size: 12px;
        }

        .userpanel{
            text-align: right;
        }
        
        #itemsPantalla > :first-child {
            width: 17%;
        }

        #itemsPantalla > :nth-child(2) {
            width: 80%;
            max-height: 600px;
            overflow-y: scroll;
        }

        .paneluser{
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        .txtpersonalizado{
            max-width: 75%;
            background-color: #a5a5a566;
            color: white;
        }

         
        .txtpersonalizado:focus{
            max-width: 75%;
            background-color: #a5a5a566;
            color: white;
        }

        .cabecerachat{
            position: sticky;
            top: 0;
            background-color:#ff00004a;
        }
</style>
<div class="container">
    <script type="text/javascript">
        $(window).on("load", function () {
            let segundoDiv = $("#itemsPantalla").children()[1];
            $(segundoDiv).scrollTop($(segundoDiv).height());
        });
    </script>
    <div class="row justify-content-center align-items-center" style="height: 100%;">

        <asp:HiddenField runat="server" id="destinatario" Value=""/>
        <div id="itemsPantalla" style="width: 100%; display: flex; max-height: 70%;">
            <asp:GridView ID="gvUsuariosChat" runat="server" AutoGenerateColumns="False" OnRowCommand="gvUsuariosChat_RowCommand" CssClass="table">
                <Columns>
                    <asp:TemplateField >
                        <ItemTemplate>
                            <asp:Panel ID="usrpanel" CssClass="paneluser" runat="server" Width="100%">
                                <svg style="width: 70px" data-name="Layer 1" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"><path fill="#9b9b9b" d="M8 0a8 8 0 1 0 8 8 8 8 0 0 0-8-8zm0 15a7 7 0 0 1-5.19-2.32 2.71 2.71 0 0 1 1.7-1 13.11 13.11 0 0 0 1.29-.28 2.32 2.32 0 0 0 .94-.34 1.17 1.17 0 0 0-.27-.7 3.61 3.61 0 0 1-1.32-2.87A3.18 3.18 0 0 1 8 4.07a3.18 3.18 0 0 1 2.86 3.42 3.6 3.6 0 0 1-1.32 2.88 1.13 1.13 0 0 0-.27.69 2.68 2.68 0 0 0 .93.31 10.81 10.81 0 0 0 1.28.23 2.63 2.63 0 0 1 1.78 1A7 7 0 0 1 8 15z"/></svg>
                                <asp:Label runat="server"  CssClass="mensaje"  Text='<%# Eval("NOMBRE") %>'></asp:Label>
                                <asp:Button ID="btnAcceder" runat="server" Text="Acceder Chat" CommandName="ACCEDER_CHAT" CommandArgument='<%# Eval("USUARIO") %>' CssClass="btn btn-primary btn-sm"/>
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:GridView ID="gvChat" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowDataBound="gvChat_RowDataBound"  HeaderStyle-CssClass="cabecerachat">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HiddenField id="usuario_uno" runat="server" Value='<%# Eval("USR1") %>' />
                            <asp:Panel ID="panelmensaje" runat="server" Width="100%">
                                <asp:Label runat="server"  CssClass="mensaje" Text='<%# Eval("MENSAJE") %>'></asp:Label><br />
                                <asp:Label runat="server"  CssClass="fechamsg" Text='<%# Eval("FECHA") %>'></asp:Label>
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div style="display: flex; width: 100%; margin-top: 10px;">
            <asp:TextBox ID="txtEnviar" runat="server" CssClass="form-control w-75 txtpersonalizado"  ToolTip="Escibir texto a enviar..." ></asp:TextBox>
            <asp:Button  ID="btEnviar" runat="server"  Text="Enviar" CssClass="btn btn-success w-25" OnClick="btEnviar_Click" />
        </div>
    </div>
</div>
</asp:Content>

