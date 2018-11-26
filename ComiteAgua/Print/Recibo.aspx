<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recibo.aspx.cs" Inherits="ComiteAgua.Print.Recibo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>      
</head>

<body>

    <form id="form" runat="server">

        <!--Tabla fecha-->
        <div style="padding-left: 680px; padding-top: 110px; position:absolute">
            <table style="width: 150%">
                <tr>
                    <td>
                        <label runat="server" id="FechaTextBox"></label>
                    </td>
                </tr>
            </table>
        </div>

        <!--Tabla datos generales-->
        <div style="padding-left: 150px; padding-top: 152px; position:absolute">
            <table style="width: 200%">
                <tr>
                    <td>
                        <label runat="server" id="UsuarioTextBox"></label>
                    </td>                   
                </tr>
            </table>
        </div>
         <div style="padding-left: 150px; padding-top: 167px; position:absolute">
            <table style="width: 200%">
                 <tr>
                    <td>
                        <label runat="server" id="DireccionTextBox"></label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding-left: 150px; padding-top: 184px; position:absolute">
            <table style="width: 200%">
                <tr>
                    <td>
                        <label runat="server" id="ConceptoPagoTextBox"></label>
                    </td>
                </tr>
            </table>
        </div>       

        <!--Tipo pago-->
        <div style="padding-left: 150px; padding-top: 210px; position:absolute"">
            <table style="width: 200%">
                <tr>
                    <td>
                        <label runat="server" id="TipoPagoTextBox"></label>
                    </td>
                </tr>
            </table>
        </div>       

        <!--Tabla periodo de pago-->
        <div style="padding-left: 150px; padding-top: 300px; position:absolute"">
            <table style="width: 200%">
                <tr>
                    <td>
                        <label runat="server" id="PeriodoPagoTextBox"></label>
                    </td>
                </tr>
            </table>
        </div>

        <!--Folio-->
        <div style="padding-left: 700px; padding-top: 150px; position:absolute"">
            <table style="width: 200%">
                <tr>
                    <td>
                         <label runat="server" id="FolioTextBox"></label>
                    </td>
                </tr>
            </table>
        </div>

        <!--Importe-->
        <div style="padding-left: 730px; padding-top: 225px; position:absolute"">
            <table style="width: 200%">
                <tr>
                    <td>
                         <label runat="server" id="SubTotalTextBox"></label>  
                    </td>
                </tr>
            </table>
        </div>

        <!--Descuento-->
        <div style="padding-left: 730px; padding-top: 240px; position:absolute"">
            <table style="width: 200%">
                <tr>
                    <td>
                         <label runat="server" id="DescuentoTextBox"></label>
                    </td>
                </tr>
            </table>
        </div>

        <!--Abono-->
        <div style="padding-left: 730px; padding-top: 410px; position:absolute"">
            <table style="width: 200%">
                <tr>
                    <td>
                         <label runat="server" id="AbonoTextBox"></label>
                    </td>
                </tr>
            </table>
        </div>

        <!--Total-->
        <div style="padding-left: 730px; padding-top: 330px; position:absolute"">
            <table style="width: 200%">
                <tr>
                    <td>
                         <label runat="server" id="TotalTextBox"></label>
                    </td>
                </tr>
            </table>
        </div>

        <!--Cantidad con letra-->
        <div style="padding-left: 150px; padding-top: 332px; position:absolute"">
            <table style="width: 250%">
                <tr>
                    <td>
                         <input runat="server" type="text" id="CantidadLetraTextBox" style="width:100%;border:none;background-color:aliceblue"/>
                    </td>
                </tr>
            </table>
        </div>
                    

         <!--Texto personal-->
        <div style="padding-left: 150px; padding-top: 227px; position:absolute"">
            <table style="width: 250%">
                <tr>
                    <td>
                        <input runat="server" type="text" id="TextoPersonalTextBox" style="width:100%;border:none;background-color:aliceblue" />
                    </td>
                </tr>
            </table>
        </div>
         <!--Texto personal 1-->
        <div style="padding-left: 150px; padding-top: 248px; position:absolute"">
            <table style="width: 250%">
                <tr>
                    <td>
                        <input runat="server" type="text" id="Text1" style="width:100%;border:none;background-color:aliceblue" />
                    </td>
                </tr>
            </table>
        </div>

        <!--Botones-->
        <div style="padding-top:700px">
            <input type="button" value="Imprimir" onclick="window.print()" /><br />
            <asp:Button Text="Regresar" runat="server" OnClick="Unnamed_Click" />
        </div>

    </form>

</body>
</html>


