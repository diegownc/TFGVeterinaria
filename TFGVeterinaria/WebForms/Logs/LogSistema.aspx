<%@ Page Title="Logs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogSistema.aspx.cs" Inherits="TFGVeterinaria.LogSistema" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        th{
            color: white !important;
        }

    </style>
<div class="container">
   <div class="row justify-content-center align-items-center" style="height: 100%; width:100%;">
       <asp:GridView ID="myGridView"   PagerStyle-ForeColor="White"  ForeColor="White"  RowStyle-ForeColor="White" AlternatingRowStyle-BackColor="#858585" HeaderStyle-ForeColor="White" runat="server" CssClass="table table-striped table-bordered" 
                AutoGenerateColumns="False"  AllowPaging="True" 
                                            PageSize="10" 
                                            OnPageIndexChanging="myGridView_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="ID" ItemStyle-ForeColor="White" HeaderText="ID" SortExpression="ID"/>
                <asp:BoundField DataField="UBICACION" ItemStyle-ForeColor="White" HeaderText="Ubicacion" />
                <asp:BoundField DataField="STACKTRACE" ItemStyle-ForeColor="White" HeaderText="Stacktrace" />
                <asp:BoundField DataField="ERROR_MESSAGE" ItemStyle-ForeColor="White" HeaderText="Error" />
                <asp:BoundField DataField="FECHA" ItemStyle-ForeColor="White" HeaderText="Fecha" />
            </Columns>
        </asp:GridView>
   </div>
 </div>
</asp:Content>
