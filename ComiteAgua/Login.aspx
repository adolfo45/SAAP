<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ComiteAgua.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta name="viewport" content="width=device-width" />

    <title>SAAP&#8482</title>

    <link rel="shortcut icon" href="~/Images/gota2.png" />       
    
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="~/Content/Site.css" rel="stylesheet" />--%>
    <link href="~/Content/ionicons.min.css" rel="stylesheet" />

    <script src="~/Scripts/jquery-3.2.1.min.js"></script>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.1/css/font-awesome.min.css"/>
    
    <style>
        .centrar {
            display:block; 
            width:800px; 
            height:400px; 
            position:absolute; 
            top:50%; 
            left:50%; 
            margin-top:-200px; 
            margin-left:-400px;
        }   
        
        .sombra2 {
            text-shadow: 2px 4px 3px rgba(0,0,0,0.3);
            /*font-size: 80px;*/
            font-weight: bold;
            font-family: 'Arial Black';
            }

        body{            
            background: url(Images/Fondo.jpg) no-repeat center center fixed;
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;
        }

        .blanco{
            background-color:white;
            padding-bottom:30px;
        }
    </style>

</head>
<body>

    <div class="blanco">
        <asp:Image ImageUrl="~/Images/LogoGrande.png" runat="server" />&#174
    </div>
   
   <div class="centrar">
       
      <%-- <video autoplay="autoplay" loop="loop" id="video_background" preload="auto">
           <source src="https://mdbootstrap.com/img/video/animation-intro.mp4" type="video/mp4" />           
       </video>--%>
       
        <form class="form-horizontal" role="form" runat="server">

            <br />

            <div class="row">               
                <div class="col-md-12 text-center">                   
                    <%--<h3 class="sombra2">SISTEMA ADMINISTRATIVO DE AGUA POTABLE (SAAP)&#8482<img src="/Images/SAAP.png"/></h3> --%>
                    <h3 class="sombra2">SISTEMA ADMINISTRATIVO DE AGUA POTABLE &#8482<img src="Images/SAAPRecortada.png"/></h3>                     
                </div>
            </div>

            <br />

            <div class="row">
                <div class="col-md-3"></div>
                <div class="col-md-6">
                    <h4>Inicio Sesión</h4>
                    <hr/>
                </div>
            </div>                      

            <div class="row">
                <div class="col-md-3"></div>
                <div class="col-md-6">
                    <div class="form-group has-danger">
                        <label class="sr-only" for="email">E-Mail Address</label>
                        <div class="input-group mb-2 mr-sm-2 mb-sm-0">
                            <div class="input-group-addon" style="width: 2.6rem"><i class="fa fa-user"></i></div>
                            <input runat="server" id="UsuarioTextBox" type="text" name="email" class="form-control"
                                   placeholder="Usuario" required autofocus/>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-control-feedback">
                        <span class="text-danger align-middle" runat="server" id="UsuarioMessage" visible="false">
                            <i class="fa fa-close"></i> Usuario incorrecto
                        </span>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-3"></div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="sr-only" for="password">Password</label>
                        <div class="input-group mb-2 mr-sm-2 mb-sm-0">
                            <div class="input-group-addon" style="width: 2.6rem"><i class="fa fa-key"></i></div>
                            <input runat="server" id="ContraseñaTextBox" type="password" name="password" class="form-control"
                                   placeholder="Contraseña" required/>
                        </div>
                    </div>
                    <hr/>
                </div>
                <div class="col-md-3">
                    <div class="form-control-feedback">
                        <span class="text-danger align-middle" runat="server" id="ContraseñaMessage" visible="false">
                        <i class="fa fa-close"></i> Contraseña incorrecto   
                        </span>
                    </div>
                </div>
            </div>
                         
            <div class="row">
                <div class="col-md-3"></div>
                <div class="col-md-6 text-center">                    
                    <asp:Button runat="server" id="AceptarButton" Text="Aceptar" class="btn btn-success" OnClick="AceptarButton_Click" ></asp:Button>                   
                </div>
            </div>

        </form>

    </div>
  
    <script src="~/Scripts/popper.js"></script>
    <script src="~/Scripts/bootstrap.min.js"></script>       
    <script src="~/Scripts/jquery.anexgrid.js"></script>
    <script src="~/Scripts/jquery.validate.min.js"></script>
    <script src="~/Scripts/jquery.validate.unobtrusive.min.js"></script>
    <script src="~/Scripts/autoNumeric/autoNumeric-min.js"></script>   

</body>
</html>
