<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReciboSistema.aspx.cs" Inherits="ComiteAgua.Print.ReciboSistema" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        td {
            height: 16px;
        }

        .ImprimirButton {            
            background-image:url("../Images/Recibo/Imprimir.png");
            background-repeat: no-repeat;
            height: 100px;
            width: 100px;
            background-position: center;
            background-color: transparent;
            border: none;
            cursor: pointer;
        }

        .RegresarButton {            
            background-image: url("../Images/Recibo/Regresar.png"); 
            background-repeat: no-repeat;
            height: 100px;
            width: 200px;
            background-position: center;
            background-color: transparent;
            border: none;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form" runat="server">

        <!--Tabla fecha-->
        <div style="padding-left: 880px; padding-top: 70px; position: absolute">
            <table style="width: 150%; font-size: 15px;">
                <tr>                    
                    <td style="visibility: hidden;">
                        <label style="visibility: hidden;">-</label></td>
                </tr>               
                <tr>
                    <td style="text-align: center;">
                        <label runat="server" id="NoReciboTextBox">xxx</label>
                    </td>
                </tr>
                <tr>
                    <td style="visibility: hidden;">
                        <label style="visibility: hidden;">-</label></td>
                </tr>
                <tr>
                    <td style="visibility: hidden;">
                        <label style="visibility: hidden;">-</label></td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        <label runat="server" id="FechaTextBox">xxx</label>
                    </td>
                </tr>
            </table>
        </div>
        <label id="CanceladoText" runat="server" style="padding-left: 630px; padding-top: 340px; position: absolute; font-size:25px"><u>CANCELADO</u></label>
        <div style="padding-left: 18px; padding-top: 220px; position: absolute">
            <table border="1" style="border-collapse: collapse; font-size: 15px;">

                <tr>
                    <td style="width: 200px; height: 5px">
                        <label>USUARIO (A)</label></td>
                    <td style="width: 400px; height: 5px">
                        <label runat="server" id="UsuarioTextBox"></label>
                    </td>
                    <td style="width: 160px; visibility: hidden;"></td>
                    <td>CLAVE DE CONTROL</td>
                    <td style="width: 120px; text-align: center; height: 5px">
                        <label runat="server" id="FolioTextBox"></label>
                    </td>
                </tr>
                <tr>
                    <td>DOMICILIO</td>
                    <td>
                        <label runat="server" id="DireccionTextBox"></label>
                    </td>
                    <td style="visibility: hidden;"></td>
                    <td style="visibility: hidden;"></td>
                    <td style="visibility: hidden;"></td>
                </tr>
                <tr>
                    <td>CONCEPTO DE COBRO</td>
                    <td>
                        <label runat="server" id="ConceptoPagoTextBox"></label>
                    </td>
                    <td style="visibility: hidden;"></td>
                    <td style="visibility: hidden;"></td>
                    <td style="visibility: hidden;"></td>
                </tr>
                <tr>
                    <td>
                        <label style="visibility: hidden;">-</label></td>
                    <td>
                        <label id="ObservacionesTextBox" runat="server" />
                    </td>
                    <td style="visibility: hidden;"></td>
                    <td style="visibility: hidden;"></td>
                    <td style="visibility: hidden;"></td>
                </tr>
                <tr>
                    <td>
                        <label style="visibility: hidden;">-</label></td>
                    <td>
                        <label id="AdicionalTextBox" runat="server" />
                    </td>
                    <td style="visibility: hidden;"></td>
                    <td>IMPORTE</td>
                    <td>
                        <label runat="server" id="SubTotalTextBox"></label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label style="visibility: hidden;">-</label></td>
                    <td>
                        <label id="RenglonAdicional1" runat="server" />
                    </td>
                    <td style="visibility: hidden;"></td>
                    <td>DESCUENTO</td>
                    <td>
                        <label runat="server" id="DescuentoTextBox"></label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label style="visibility: hidden;">-</label></td>
                    <td>
                        <label id="RenglonAdicional2" runat="server" />
                    </td>
                    <td style="visibility: hidden;"></td>
                    <td>RECARGOS</td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <label style="visibility: hidden;">-</label></td>
                    <td>
                        <label id="RenglonAdicional3" runat="server" />
                    </td>
                    <td style="visibility: hidden;"></td>
                    <td>MULTAS</td>
                    <td></td>
                </tr>
                <tr>
                    <td style="visibility: hidden;">
                        <label style="visibility: hidden;">-</label></td>
                    <td style="visibility: hidden;">
                        <label style="visibility: hidden;">-</label></td>
                    <td style="visibility: hidden;"></td>
                    <td>GASTOS/EJEC.</td>
                    <td></td>
                </tr>
                <tr>
                    <td>PERIODO DE PAGO</td>
                    <td>
                        <label runat="server" id="PeriodoPagoTextBox" />
                    </td>
                    <td style="visibility: hidden;"></td>
                    <td>OTROS</td>
                    <td><label runat="server" id="OtroTextBox"></label></td>
                </tr>
                <tr>
                    <td style="visibility: hidden;">
                        <label style="visibility: hidden;">-</label></td>
                    <td style="visibility: hidden;">
                        <label style="visibility: hidden;">-</label></td>
                    <td style="visibility: hidden;"></td>
                    <td>SUB-TOTAL</td>
                    <td></td>
                </tr>
                <tr>
                    <td>CANTIDAD CON LETRA</td>
                    <td>
                        <label id="CantidadLetraTextBox" runat="server" />
                    </td>
                    <td style="visibility: hidden;"></td>
                    <td>TOTAL$</td>
                    <td>
                        <label runat="server" id="TotalTextBox"></label>
                    </td>
                </tr>
                <tr>
                    <td style="visibility: hidden;">
                        <label style="visibility: hidden;">-</label></td>
                    <td style="visibility: hidden;">
                        <label style="visibility: hidden;">-</label></td>
                    <td style="visibility: hidden;">
                        <label style="visibility: hidden;">-</label></td>
                    <td style="visibility: hidden;">
                        <label style="visibility: hidden;">-</label></td>
                    <td style="visibility: hidden;">
                        <label style="visibility: hidden;">-</label></td>
                </tr>
                <tr>
                    <td style="visibility: hidden;">
                        <label style="visibility: hidden;">-</label></td>
                    <td style="visibility: hidden;">
                        <label style="visibility: hidden;">-</label></td>
                    <td style="visibility: hidden;">
                        <label style="visibility: hidden;">-</label></td>
                    <td style="visibility: hidden;">
                        <label style="visibility: hidden;">-</label></td>
                    <td style="visibility: hidden;">
                        <label style="visibility: hidden;">-</label></td>
                </tr>
            </table>
        </div>

        <div style="padding-left: 0px; padding-top: 520px; position: absolute">

            <td>
                <asp:Image runat="server" ID="CodigoImg" /></td>

        </div>

        <!--Botones-->
        <div style="padding-top: 100px; padding-left: 1120px;">

            <input class="ImprimirButton" type="button" runat="server" onclick="window.print()" title="Imprimir"/><br />

            <br />
            <br />
            <br />

            <asp:Button CssClass="RegresarButton" runat="server" OnClick="Unnamed_Click" title="Regresar" />

        </div>
        
    <asp:UpdatePanel ID="HiddenFieldsUpdatePanel" runat="server" UpdateMode="Always">
       
        <ContentTemplate>
            <asp:scriptmanager runat="server"></asp:scriptmanager>
            <asp:HiddenField ID="ReciboIdHiddenField" runat="server" /> 
            <asp:HiddenField ID="UrlOrigenHiddenField" runat="server" /> 
            <asp:HiddenField ID="ConvenioId" runat="server" /> 
        </ContentTemplate>
    </asp:UpdatePanel>    

    </form>   

</body>
</html>

