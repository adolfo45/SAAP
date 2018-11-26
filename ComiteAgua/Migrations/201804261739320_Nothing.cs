namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nothing : DbMigration
    {
        public override void Up()
        {
            //    CreateTable(
            //        "Comite.ArchivosGasto",
            //        c => new
            //            {
            //                ArchivoGastoId = c.Int(nullable: false, identity: true),
            //                GastoId = c.Int(nullable: false),
            //                Nombre = c.String(nullable: false, maxLength: 255),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.ArchivoGastoId)
            //        .ForeignKey("Comite.Gastos", t => t.GastoId)
            //        .Index(t => t.GastoId);

            //    CreateTable(
            //        "Comite.Gastos",
            //        c => new
            //            {
            //                GastoId = c.Int(nullable: false, identity: true),
            //                SucursalId = c.Int(nullable: false),
            //                MultiComiteId = c.Int(nullable: false),
            //                Concepto = c.String(nullable: false, maxLength: 1000),
            //                Total = c.Decimal(nullable: false, precision: 18, scale: 2),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.GastoId)
            //        .ForeignKey("Seguridad.MultiComitesSucursales", t => new { t.MultiComiteId, t.SucursalId })
            //        .Index(t => new { t.MultiComiteId, t.SucursalId });

            //    CreateTable(
            //        "Seguridad.MultiComitesSucursales",
            //        c => new
            //            {
            //                MultiComiteId = c.Int(nullable: false),
            //                SucursalId = c.Int(nullable: false),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => new { t.MultiComiteId, t.SucursalId })
            //        .ForeignKey("Seguridad.MultiComites", t => t.MultiComiteId)
            //        .ForeignKey("Seguridad.Sucursales", t => t.SucursalId)
            //        .Index(t => t.MultiComiteId)
            //        .Index(t => t.SucursalId);

            //    CreateTable(
            //        "Seguridad.MultiComites",
            //        c => new
            //            {
            //                MultiComiteId = c.Int(nullable: false, identity: true),
            //                MultiComite = c.String(nullable: false, maxLength: 50),
            //                Rfc = c.String(maxLength: 50),
            //                RazonSocial = c.String(maxLength: 50),
            //                Delegacion = c.String(nullable: false, maxLength: 100),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.MultiComiteId);

            //    CreateTable(
            //        "Seguridad.PersonasSeguridad",
            //        c => new
            //            {
            //                PersonaId = c.Int(nullable: false, identity: true),
            //                MultiComiteId = c.Int(nullable: false),
            //                Rfc = c.String(maxLength: 50),
            //                Nombre = c.String(nullable: false, maxLength: 50),
            //                ApellidoPaterno = c.String(nullable: false, maxLength: 50),
            //                ApellidoMaterno = c.String(nullable: false, maxLength: 50),
            //                FechaNacimiento = c.DateTime(nullable: false),
            //                EstadoCivilId = c.Int(),
            //                CorreoElectronico = c.String(maxLength: 50),
            //                Telefono = c.String(maxLength: 15),
            //                Cargo = c.String(maxLength: 100),
            //                AutorizaConvenios = c.Boolean(),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.PersonaId)
            //        .ForeignKey("Global.EstadosCiviles", t => t.EstadoCivilId)
            //        .ForeignKey("Seguridad.MultiComites", t => t.MultiComiteId)
            //        .Index(t => t.MultiComiteId)
            //        .Index(t => t.EstadoCivilId);

            //    CreateTable(
            //        "Comite.Convenios",
            //        c => new
            //            {
            //                ConvenioId = c.Int(nullable: false, identity: true),
            //                ConceptoConvenioId = c.Int(nullable: false),
            //                PersonaId = c.Int(),
            //                TomaId = c.Int(nullable: false),
            //                EstatusConvenioId = c.Int(nullable: false),
            //                PeriodoPagoConvenioId = c.Int(nullable: false),
            //                PeriodoInicio = c.DateTime(storeType: "date"),
            //                PeriodoFin = c.DateTime(storeType: "date"),
            //                FechaInicio = c.DateTime(nullable: false),
            //                FechaFin = c.DateTime(nullable: false),
            //                SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
            //                Descuento = c.Decimal(precision: 18, scale: 2),
            //                Total = c.Decimal(nullable: false, precision: 18, scale: 2),
            //                FechaTermino = c.DateTime(storeType: "date"),
            //                Observaciones = c.String(maxLength: 255),
            //                PimerPago = c.Decimal(precision: 18, scale: 2),
            //                Pagos = c.Decimal(precision: 18, scale: 2),
            //                RutaArchivo = c.String(nullable: false, maxLength: 400),
            //                NoTarjeta = c.String(),
            //                Eliminado = c.Boolean(),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.ConvenioId)
            //        .ForeignKey("Comite.ConceptosConvenio", t => t.ConceptoConvenioId)
            //        .ForeignKey("Comite.EstatusConvenios", t => t.EstatusConvenioId)
            //        .ForeignKey("Comite.Tomas", t => t.TomaId)
            //        .ForeignKey("Comite.PeriodosPagoConvenio", t => t.PeriodoPagoConvenioId)
            //        .ForeignKey("Seguridad.PersonasSeguridad", t => t.PersonaId)
            //        .Index(t => t.ConceptoConvenioId)
            //        .Index(t => t.PersonaId)
            //        .Index(t => t.TomaId)
            //        .Index(t => t.EstatusConvenioId)
            //        .Index(t => t.PeriodoPagoConvenioId);

            //    CreateTable(
            //        "Comite.ConceptosConvenio",
            //        c => new
            //            {
            //                ConceptoConvenioId = c.Int(nullable: false, identity: true),
            //                ConceptoConvenio = c.String(nullable: false, maxLength: 100),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.ConceptoConvenioId);

            //    CreateTable(
            //        "Comite.EstatusConvenios",
            //        c => new
            //            {
            //                EstatusConvenioId = c.Int(nullable: false, identity: true),
            //                EstatusConvenio = c.String(nullable: false, maxLength: 100),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.EstatusConvenioId);

            //    CreateTable(
            //        "Comite.Pagos",
            //        c => new
            //            {
            //                PagoId = c.Int(nullable: false, identity: true),
            //                ConceptoPagoId = c.Int(nullable: false),
            //                TomaId = c.Int(nullable: false),
            //                ConvenioId = c.Int(),
            //                SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
            //                Descuento = c.Decimal(precision: 18, scale: 2),
            //                Total = c.Decimal(nullable: false, precision: 18, scale: 2),
            //                Abono = c.Decimal(precision: 18, scale: 2),
            //                Activo = c.Boolean(nullable: false),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //                NotificacionId = c.Int(),
            //                NoRecibo = c.String(maxLength: 10),
            //            })
            //        .PrimaryKey(t => t.PagoId)
            //        .ForeignKey("Comite.ConceptosPago", t => t.ConceptoPagoId)
            //        .ForeignKey("Comite.Convenios", t => t.ConvenioId)
            //        .ForeignKey("Comite.Notificaciones", t => t.NotificacionId)
            //        .ForeignKey("Comite.Tomas", t => t.TomaId)
            //        .Index(t => t.ConceptoPagoId)
            //        .Index(t => t.TomaId)
            //        .Index(t => t.ConvenioId)
            //        .Index(t => t.NotificacionId);

            //    CreateTable(
            //        "Comite.ConceptosPago",
            //        c => new
            //            {
            //                ConceptoPagoId = c.Int(nullable: false, identity: true),
            //                ConceptoPago = c.String(nullable: false, maxLength: 100),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.ConceptoPagoId);

            //    CreateTable(
            //        "Comite.Notificaciones",
            //        c => new
            //            {
            //                NotificacionId = c.Int(nullable: false, identity: true),
            //                TipoNotificacionId = c.Int(nullable: false),
            //                TomaId = c.Int(nullable: false),
            //                OpcionNotificacionId = c.Int(),
            //                TotalPagar = c.Decimal(nullable: false, precision: 18, scale: 2),
            //                FechaEntrega = c.DateTime(nullable: false),
            //                UsuarioNotificoId = c.Int(nullable: false),
            //                Activa = c.Boolean(nullable: false),
            //                Observaciones = c.String(maxLength: 255),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.NotificacionId)
            //        .ForeignKey("Comite.CtoOpcionNotificaciones", t => t.OpcionNotificacionId)
            //        .ForeignKey("Comite.TipoNotificaciones", t => t.TipoNotificacionId)
            //        .ForeignKey("Comite.Tomas", t => t.TomaId)
            //        .ForeignKey("Seguridad.Usuarios", t => t.UsuarioAltaId)
            //        .ForeignKey("Seguridad.Usuarios", t => t.UsuarioCambioId)
            //        .ForeignKey("Seguridad.Usuarios", t => t.UsuarioNotificoId)
            //        .Index(t => t.TipoNotificacionId)
            //        .Index(t => t.TomaId)
            //        .Index(t => t.OpcionNotificacionId)
            //        .Index(t => t.UsuarioNotificoId)
            //        .Index(t => t.UsuarioAltaId)
            //        .Index(t => t.UsuarioCambioId);

            //    CreateTable(
            //        "Comite.CtoOpcionNotificaciones",
            //        c => new
            //            {
            //                OpcionNotificacionId = c.Int(nullable: false, identity: true),
            //                OpcionNotificacion = c.String(nullable: false, maxLength: 1000),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.OpcionNotificacionId);

            //    CreateTable(
            //        "Comite.TipoNotificaciones",
            //        c => new
            //            {
            //                TipoNotificacionId = c.Int(nullable: false, identity: true),
            //                TipoNotificacion = c.String(nullable: false, maxLength: 1000),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.TipoNotificacionId);

            //    CreateTable(
            //        "Comite.Tomas",
            //        c => new
            //            {
            //                TomaId = c.Int(nullable: false, identity: true),
            //                CategoriaId = c.Int(nullable: false),
            //                LiquidacionTomaId = c.Int(),
            //                PropietarioId = c.Int(nullable: false),
            //                DireccionId = c.Int(),
            //                Folio = c.String(nullable: false, maxLength: 20),
            //                Observaciones = c.String(maxLength: 255),
            //                Activa = c.Boolean(nullable: false),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //                Pasiva = c.Boolean(),
            //            })
            //        .PrimaryKey(t => t.TomaId)
            //        .ForeignKey("Comite.Categorias", t => t.CategoriaId)
            //        .ForeignKey("Comite.Direcciones", t => t.DireccionId)
            //        .ForeignKey("Comite.LiquidacionesToma", t => t.LiquidacionTomaId)
            //        .ForeignKey("Comite.Propietarios", t => t.PropietarioId)
            //        .Index(t => t.CategoriaId)
            //        .Index(t => t.LiquidacionTomaId)
            //        .Index(t => t.PropietarioId)
            //        .Index(t => t.DireccionId);

            //    CreateTable(
            //        "Comite.Categorias",
            //        c => new
            //            {
            //                CategoriaId = c.Int(nullable: false, identity: true),
            //                Categoria = c.String(nullable: false, maxLength: 100),
            //                Abreviatura = c.String(nullable: false, maxLength: 50),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.CategoriaId);

            //    CreateTable(
            //        "Comite.Tarifas",
            //        c => new
            //            {
            //                TarifaId = c.Int(nullable: false, identity: true),
            //                CategoriaId = c.Int(nullable: false),
            //                MesAno = c.Int(nullable: false),
            //                CostoTarifa = c.Decimal(nullable: false, precision: 18, scale: 2),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.TarifaId)
            //        .ForeignKey("Comite.Categorias", t => t.CategoriaId)
            //        .Index(t => t.CategoriaId);

            //    CreateTable(
            //        "Comite.Direcciones",
            //        c => new
            //            {
            //                DireccionId = c.Int(nullable: false, identity: true),
            //                Calle = c.String(maxLength: 100),
            //                NumInt = c.String(maxLength: 70),
            //                NumExt = c.String(maxLength: 70),
            //                Colonia = c.String(maxLength: 100),
            //                CalleId = c.Int(),
            //                TipoCalleId = c.Int(),
            //                ColoniaId = c.Int(),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.DireccionId)
            //        .ForeignKey("Comite.CtoCalles", t => t.CalleId)
            //        .ForeignKey("Comite.CtoColonias", t => t.ColoniaId)
            //        .ForeignKey("Comite.CtoTipoCalles", t => t.TipoCalleId)
            //        .Index(t => t.CalleId)
            //        .Index(t => t.TipoCalleId)
            //        .Index(t => t.ColoniaId);

            //    CreateTable(
            //        "Comite.CtoCalles",
            //        c => new
            //            {
            //                CalleId = c.Int(nullable: false, identity: true),
            //                Calle = c.String(nullable: false, maxLength: 255),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.CalleId);

            //    CreateTable(
            //        "Comite.CtoColonias",
            //        c => new
            //            {
            //                ColoniaId = c.Int(nullable: false, identity: true),
            //                Colonia = c.String(nullable: false, maxLength: 255),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.ColoniaId);

            //    CreateTable(
            //        "Comite.CtoTipoCalles",
            //        c => new
            //            {
            //                TipoCalleId = c.Int(nullable: false, identity: true),
            //                TipoCalle = c.String(nullable: false, maxLength: 255),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.TipoCalleId);

            //    CreateTable(
            //        "Comite.LiquidacionesToma",
            //        c => new
            //            {
            //                LiquidacionTomaId = c.Int(nullable: false, identity: true),
            //                LiquidacionToma = c.String(nullable: false, maxLength: 100),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.LiquidacionTomaId);

            //    CreateTable(
            //        "Comite.PeriodosPago",
            //        c => new
            //            {
            //                PeriodoPagoId = c.Int(nullable: false, identity: true),
            //                TomaId = c.Int(),
            //                PagoId = c.Int(),
            //                MesAnoInicio = c.DateTime(storeType: "date"),
            //                MesAnoFin = c.DateTime(storeType: "date"),
            //                UltimoPeriodoPago = c.String(),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.PeriodoPagoId)
            //        .ForeignKey("Comite.Pagos", t => t.PagoId)
            //        .ForeignKey("Comite.Tomas", t => t.TomaId)
            //        .Index(t => t.TomaId)
            //        .Index(t => t.PagoId);

            //    CreateTable(
            //        "Comite.Propietarios",
            //        c => new
            //            {
            //                PropietarioId = c.Int(nullable: false, identity: true),
            //                PersonaId = c.Int(nullable: false),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.PropietarioId)
            //        .ForeignKey("Comite.Personas", t => t.PersonaId)
            //        .Index(t => t.PersonaId);

            //    CreateTable(
            //        "Comite.Personas",
            //        c => new
            //            {
            //                PersonaId = c.Int(nullable: false, identity: true),
            //                PersonalidadJuridicaId = c.Int(nullable: false),
            //                SucursalId = c.Int(nullable: false),
            //                MultiComiteId = c.Int(nullable: false),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.PersonaId)
            //        .ForeignKey("Seguridad.MultiComitesSucursales", t => new { t.MultiComiteId, t.SucursalId })
            //        .ForeignKey("Global.PersonalidadesJuridicas", t => t.PersonalidadJuridicaId)
            //        .Index(t => t.PersonalidadJuridicaId)
            //        .Index(t => new { t.MultiComiteId, t.SucursalId });

            //    CreateTable(
            //        "Comite.PersonasFisicas",
            //        c => new
            //            {
            //                PersonaId = c.Int(nullable: false),
            //                Nombre = c.String(nullable: false, maxLength: 50),
            //                ApellidoPaterno = c.String(maxLength: 100),
            //                ApellidoMaterno = c.String(maxLength: 100),
            //                FechaNacimiento = c.DateTime(),
            //                EstadoCivilId = c.Int(nullable: false),
            //                Telefono = c.String(maxLength: 15),
            //                CorreoElectronico = c.String(maxLength: 50),
            //                Rfc = c.String(maxLength: 50),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.PersonaId)
            //        .ForeignKey("Global.EstadosCiviles", t => t.EstadoCivilId)
            //        .ForeignKey("Comite.Personas", t => t.PersonaId)
            //        .Index(t => t.PersonaId)
            //        .Index(t => t.EstadoCivilId);

            //    CreateTable(
            //        "Global.EstadosCiviles",
            //        c => new
            //            {
            //                EstadoCivilId = c.Int(nullable: false, identity: true),
            //                EstadoCivil = c.String(nullable: false, maxLength: 50),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.EstadoCivilId);

            //    CreateTable(
            //        "Global.PersonalidadesJuridicas",
            //        c => new
            //            {
            //                PersonalidadJuridicaId = c.Int(nullable: false, identity: true),
            //                PersonalidadJuridica = c.String(nullable: false, maxLength: 50),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.PersonalidadJuridicaId);

            //    CreateTable(
            //        "Comite.PersonasMorales",
            //        c => new
            //            {
            //                PersonaId = c.Int(nullable: false),
            //                Nombre = c.String(),
            //                Rfc = c.String(maxLength: 50),
            //                PaginaWeb = c.String(maxLength: 150),
            //                CorreoElectronico = c.String(maxLength: 50),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.PersonaId)
            //        .ForeignKey("Comite.Personas", t => t.PersonaId)
            //        .Index(t => t.PersonaId);

            //    CreateTable(
            //        "Comite.Servicios",
            //        c => new
            //            {
            //                ServicioId = c.Int(nullable: false, identity: true),
            //                AsuntoDescripcionId = c.Int(nullable: false),
            //                EstatusServicioId = c.Int(nullable: false),
            //                TomaId = c.Int(),
            //                Propietario = c.Boolean(nullable: false),
            //                Nombre = c.String(maxLength: 255),
            //                Direccion = c.String(maxLength: 1000),
            //                Colonia = c.String(maxLength: 100),
            //                Telefono = c.String(maxLength: 15),
            //                UbicacionServicio = c.String(),
            //                Otro = c.String(),
            //                Observaciones = c.String(),
            //                UrlArchivo = c.String(maxLength: 1000),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.ServicioId)
            //        .ForeignKey("Comite.AsuntosDescripcion", t => t.AsuntoDescripcionId)
            //        .ForeignKey("Comite.EstatusServicios", t => t.EstatusServicioId)
            //        .ForeignKey("Comite.Tomas", t => t.TomaId)
            //        .Index(t => t.AsuntoDescripcionId)
            //        .Index(t => t.EstatusServicioId)
            //        .Index(t => t.TomaId);

            //    CreateTable(
            //        "Comite.AsuntosDescripcion",
            //        c => new
            //            {
            //                AsuntoDescripcionId = c.Int(nullable: false, identity: true),
            //                AsuntoDescripcion = c.String(nullable: false, maxLength: 1000),
            //                AsuntoId = c.Int(nullable: false),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.AsuntoDescripcionId)
            //        .ForeignKey("Comite.Asuntos", t => t.AsuntoId)
            //        .Index(t => t.AsuntoId);

            //    CreateTable(
            //        "Comite.Asuntos",
            //        c => new
            //            {
            //                AsuntoId = c.Int(nullable: false, identity: true),
            //                Asunto = c.String(nullable: false, maxLength: 1000),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.AsuntoId);

            //    CreateTable(
            //        "Comite.EstatusServicios",
            //        c => new
            //            {
            //                EstatusServicioId = c.Int(nullable: false, identity: true),
            //                EstatusServicio = c.String(nullable: false, maxLength: 1000),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.EstatusServicioId);

            //    CreateTable(
            //        "Seguridad.Usuarios",
            //        c => new
            //            {
            //                UsuarioId = c.Int(nullable: false, identity: true),
            //                PersonaId = c.Int(nullable: false),
            //                UserName = c.String(nullable: false, maxLength: 50),
            //                Password = c.String(nullable: false, maxLength: 8),
            //                IntentosFallidosLogin = c.Short(),
            //                FechaUltimoLogin = c.DateTime(),
            //                Activo = c.Boolean(),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.UsuarioId)
            //        .ForeignKey("Seguridad.PersonasSeguridad", t => t.PersonaId)
            //        .Index(t => t.PersonaId);

            //    CreateTable(
            //        "Comite.TransaccionesAutomaticas",
            //        c => new
            //            {
            //                TransaccionAutomaticaId = c.Int(nullable: false, identity: true),
            //                TipoTransaccionAutomaticaId = c.Int(nullable: false),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.TransaccionAutomaticaId)
            //        .ForeignKey("Comite.TiposTransaccionAutomatica", t => t.TipoTransaccionAutomaticaId)
            //        .ForeignKey("Seguridad.Usuarios", t => t.UsuarioAltaId)
            //        .ForeignKey("Seguridad.Usuarios", t => t.UsuarioCambioId)
            //        .Index(t => t.TipoTransaccionAutomaticaId)
            //        .Index(t => t.UsuarioAltaId)
            //        .Index(t => t.UsuarioCambioId);

            //    CreateTable(
            //        "Comite.TiposTransaccionAutomatica",
            //        c => new
            //            {
            //                TipoTransaccionAutomaticaId = c.Int(nullable: false, identity: true),
            //                TipoTransaccionAutomatica = c.String(nullable: false, maxLength: 1000),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.TipoTransaccionAutomaticaId);

            //    CreateTable(
            //        "Seguridad.UsuariosRoles",
            //        c => new
            //            {
            //                UsuarioId = c.Int(nullable: false),
            //                RolId = c.Int(nullable: false),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => new { t.UsuarioId, t.RolId })
            //        .ForeignKey("Seguridad.Roles", t => t.RolId)
            //        .ForeignKey("Seguridad.Usuarios", t => t.UsuarioId)
            //        .Index(t => t.UsuarioId)
            //        .Index(t => t.RolId);

            //    CreateTable(
            //        "Seguridad.Roles",
            //        c => new
            //            {
            //                RolId = c.Int(nullable: false, identity: true),
            //                Rol = c.String(nullable: false, maxLength: 255),
            //                Activo = c.Boolean(nullable: false),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.RolId);

            //    CreateTable(
            //        "Comite.PeriodosPagoConvenio",
            //        c => new
            //            {
            //                PeriodoPagoConvenioId = c.Int(nullable: false, identity: true),
            //                PeriodoPagoConvenio = c.String(nullable: false, maxLength: 100),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.PeriodoPagoConvenioId);

            //    CreateTable(
            //        "Seguridad.Sucursales",
            //        c => new
            //            {
            //                SucursalId = c.Int(nullable: false, identity: true),
            //                Sucursal = c.String(nullable: false, maxLength: 50),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.SucursalId);

            //    CreateTable(
            //        "Comite.Descuentos",
            //        c => new
            //            {
            //                DescuentoId = c.Int(nullable: false, identity: true),
            //                Descuento = c.String(nullable: false, maxLength: 100),
            //                ModoDescuentoId = c.Int(nullable: false),
            //                PeriodoDescuentoInicio = c.DateTime(storeType: "date"),
            //                PeriodoDescuentoFin = c.DateTime(storeType: "date"),
            //                MesAnoPago = c.DateTime(storeType: "date"),
            //                Total = c.Decimal(nullable: false, precision: 18, scale: 2),
            //                Activo = c.Boolean(nullable: false),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.DescuentoId)
            //        .ForeignKey("Comite.ModoDescuento", t => t.ModoDescuentoId)
            //        .Index(t => t.ModoDescuentoId);

            //    CreateTable(
            //        "Comite.ModoDescuento",
            //        c => new
            //            {
            //                ModoDescuentoId = c.Int(nullable: false, identity: true),
            //                ModoDescuento = c.String(nullable: false, maxLength: 100),
            //                FechaAlta = c.DateTime(nullable: false),
            //                UsuarioAltaId = c.Int(nullable: false),
            //                FechaCambio = c.DateTime(),
            //                UsuarioCambioId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.ModoDescuentoId);

        }

        public override void Down()
        {
            //    DropForeignKey("Comite.Descuentos", "ModoDescuentoId", "Comite.ModoDescuento");
            //    DropForeignKey("Seguridad.MultiComitesSucursales", "SucursalId", "Seguridad.Sucursales");
            //    DropForeignKey("Seguridad.PersonasSeguridad", "MultiComiteId", "Seguridad.MultiComites");
            //    DropForeignKey("Comite.Convenios", "PersonaId", "Seguridad.PersonasSeguridad");
            //    DropForeignKey("Comite.Convenios", "PeriodoPagoConvenioId", "Comite.PeriodosPagoConvenio");
            //    DropForeignKey("Comite.Notificaciones", "UsuarioNotificoId", "Seguridad.Usuarios");
            //    DropForeignKey("Comite.Notificaciones", "UsuarioCambioId", "Seguridad.Usuarios");
            //    DropForeignKey("Comite.Notificaciones", "UsuarioAltaId", "Seguridad.Usuarios");
            //    DropForeignKey("Seguridad.UsuariosRoles", "UsuarioId", "Seguridad.Usuarios");
            //    DropForeignKey("Seguridad.UsuariosRoles", "RolId", "Seguridad.Roles");
            //    DropForeignKey("Comite.TransaccionesAutomaticas", "UsuarioCambioId", "Seguridad.Usuarios");
            //    DropForeignKey("Comite.TransaccionesAutomaticas", "UsuarioAltaId", "Seguridad.Usuarios");
            //    DropForeignKey("Comite.TransaccionesAutomaticas", "TipoTransaccionAutomaticaId", "Comite.TiposTransaccionAutomatica");
            //    DropForeignKey("Seguridad.Usuarios", "PersonaId", "Seguridad.PersonasSeguridad");
            //    DropForeignKey("Comite.Servicios", "TomaId", "Comite.Tomas");
            //    DropForeignKey("Comite.Servicios", "EstatusServicioId", "Comite.EstatusServicios");
            //    DropForeignKey("Comite.Servicios", "AsuntoDescripcionId", "Comite.AsuntosDescripcion");
            //    DropForeignKey("Comite.AsuntosDescripcion", "AsuntoId", "Comite.Asuntos");
            //    DropForeignKey("Comite.Tomas", "PropietarioId", "Comite.Propietarios");
            //    DropForeignKey("Comite.Propietarios", "PersonaId", "Comite.Personas");
            //    DropForeignKey("Comite.PersonasMorales", "PersonaId", "Comite.Personas");
            //    DropForeignKey("Comite.Personas", "PersonalidadJuridicaId", "Global.PersonalidadesJuridicas");
            //    DropForeignKey("Comite.PersonasFisicas", "PersonaId", "Comite.Personas");
            //    DropForeignKey("Comite.PersonasFisicas", "EstadoCivilId", "Global.EstadosCiviles");
            //    DropForeignKey("Seguridad.PersonasSeguridad", "EstadoCivilId", "Global.EstadosCiviles");
            //    DropForeignKey("Comite.Personas", new[] { "MultiComiteId", "SucursalId" }, "Seguridad.MultiComitesSucursales");
            //    DropForeignKey("Comite.PeriodosPago", "TomaId", "Comite.Tomas");
            //    DropForeignKey("Comite.PeriodosPago", "PagoId", "Comite.Pagos");
            //    DropForeignKey("Comite.Pagos", "TomaId", "Comite.Tomas");
            //    DropForeignKey("Comite.Notificaciones", "TomaId", "Comite.Tomas");
            //    DropForeignKey("Comite.Tomas", "LiquidacionTomaId", "Comite.LiquidacionesToma");
            //    DropForeignKey("Comite.Tomas", "DireccionId", "Comite.Direcciones");
            //    DropForeignKey("Comite.Direcciones", "TipoCalleId", "Comite.CtoTipoCalles");
            //    DropForeignKey("Comite.Direcciones", "ColoniaId", "Comite.CtoColonias");
            //    DropForeignKey("Comite.Direcciones", "CalleId", "Comite.CtoCalles");
            //    DropForeignKey("Comite.Convenios", "TomaId", "Comite.Tomas");
            //    DropForeignKey("Comite.Tomas", "CategoriaId", "Comite.Categorias");
            //    DropForeignKey("Comite.Tarifas", "CategoriaId", "Comite.Categorias");
            //    DropForeignKey("Comite.Notificaciones", "TipoNotificacionId", "Comite.TipoNotificaciones");
            //    DropForeignKey("Comite.Pagos", "NotificacionId", "Comite.Notificaciones");
            //    DropForeignKey("Comite.Notificaciones", "OpcionNotificacionId", "Comite.CtoOpcionNotificaciones");
            //    DropForeignKey("Comite.Pagos", "ConvenioId", "Comite.Convenios");
            //    DropForeignKey("Comite.Pagos", "ConceptoPagoId", "Comite.ConceptosPago");
            //    DropForeignKey("Comite.Convenios", "EstatusConvenioId", "Comite.EstatusConvenios");
            //    DropForeignKey("Comite.Convenios", "ConceptoConvenioId", "Comite.ConceptosConvenio");
            //    DropForeignKey("Seguridad.MultiComitesSucursales", "MultiComiteId", "Seguridad.MultiComites");
            //    DropForeignKey("Comite.Gastos", new[] { "MultiComiteId", "SucursalId" }, "Seguridad.MultiComitesSucursales");
            //    DropForeignKey("Comite.ArchivosGasto", "GastoId", "Comite.Gastos");
            //    DropIndex("Comite.Descuentos", new[] { "ModoDescuentoId" });
            //    DropIndex("Seguridad.UsuariosRoles", new[] { "RolId" });
            //    DropIndex("Seguridad.UsuariosRoles", new[] { "UsuarioId" });
            //    DropIndex("Comite.TransaccionesAutomaticas", new[] { "UsuarioCambioId" });
            //    DropIndex("Comite.TransaccionesAutomaticas", new[] { "UsuarioAltaId" });
            //    DropIndex("Comite.TransaccionesAutomaticas", new[] { "TipoTransaccionAutomaticaId" });
            //    DropIndex("Seguridad.Usuarios", new[] { "PersonaId" });
            //    DropIndex("Comite.AsuntosDescripcion", new[] { "AsuntoId" });
            //    DropIndex("Comite.Servicios", new[] { "TomaId" });
            //    DropIndex("Comite.Servicios", new[] { "EstatusServicioId" });
            //    DropIndex("Comite.Servicios", new[] { "AsuntoDescripcionId" });
            //    DropIndex("Comite.PersonasMorales", new[] { "PersonaId" });
            //    DropIndex("Comite.PersonasFisicas", new[] { "EstadoCivilId" });
            //    DropIndex("Comite.PersonasFisicas", new[] { "PersonaId" });
            //    DropIndex("Comite.Personas", new[] { "MultiComiteId", "SucursalId" });
            //    DropIndex("Comite.Personas", new[] { "PersonalidadJuridicaId" });
            //    DropIndex("Comite.Propietarios", new[] { "PersonaId" });
            //    DropIndex("Comite.PeriodosPago", new[] { "PagoId" });
            //    DropIndex("Comite.PeriodosPago", new[] { "TomaId" });
            //    DropIndex("Comite.Direcciones", new[] { "ColoniaId" });
            //    DropIndex("Comite.Direcciones", new[] { "TipoCalleId" });
            //    DropIndex("Comite.Direcciones", new[] { "CalleId" });
            //    DropIndex("Comite.Tarifas", new[] { "CategoriaId" });
            //    DropIndex("Comite.Tomas", new[] { "DireccionId" });
            //    DropIndex("Comite.Tomas", new[] { "PropietarioId" });
            //    DropIndex("Comite.Tomas", new[] { "LiquidacionTomaId" });
            //    DropIndex("Comite.Tomas", new[] { "CategoriaId" });
            //    DropIndex("Comite.Notificaciones", new[] { "UsuarioCambioId" });
            //    DropIndex("Comite.Notificaciones", new[] { "UsuarioAltaId" });
            //    DropIndex("Comite.Notificaciones", new[] { "UsuarioNotificoId" });
            //    DropIndex("Comite.Notificaciones", new[] { "OpcionNotificacionId" });
            //    DropIndex("Comite.Notificaciones", new[] { "TomaId" });
            //    DropIndex("Comite.Notificaciones", new[] { "TipoNotificacionId" });
            //    DropIndex("Comite.Pagos", new[] { "NotificacionId" });
            //    DropIndex("Comite.Pagos", new[] { "ConvenioId" });
            //    DropIndex("Comite.Pagos", new[] { "TomaId" });
            //    DropIndex("Comite.Pagos", new[] { "ConceptoPagoId" });
            //    DropIndex("Comite.Convenios", new[] { "PeriodoPagoConvenioId" });
            //    DropIndex("Comite.Convenios", new[] { "EstatusConvenioId" });
            //    DropIndex("Comite.Convenios", new[] { "TomaId" });
            //    DropIndex("Comite.Convenios", new[] { "PersonaId" });
            //    DropIndex("Comite.Convenios", new[] { "ConceptoConvenioId" });
            //    DropIndex("Seguridad.PersonasSeguridad", new[] { "EstadoCivilId" });
            //    DropIndex("Seguridad.PersonasSeguridad", new[] { "MultiComiteId" });
            //    DropIndex("Seguridad.MultiComitesSucursales", new[] { "SucursalId" });
            //    DropIndex("Seguridad.MultiComitesSucursales", new[] { "MultiComiteId" });
            //    DropIndex("Comite.Gastos", new[] { "MultiComiteId", "SucursalId" });
            //    DropIndex("Comite.ArchivosGasto", new[] { "GastoId" });
            //    DropTable("Comite.ModoDescuento");
            //    DropTable("Comite.Descuentos");
            //    DropTable("Seguridad.Sucursales");
            //    DropTable("Comite.PeriodosPagoConvenio");
            //    DropTable("Seguridad.Roles");
            //    DropTable("Seguridad.UsuariosRoles");
            //    DropTable("Comite.TiposTransaccionAutomatica");
            //    DropTable("Comite.TransaccionesAutomaticas");
            //    DropTable("Seguridad.Usuarios");
            //    DropTable("Comite.EstatusServicios");
            //    DropTable("Comite.Asuntos");
            //    DropTable("Comite.AsuntosDescripcion");
            //    DropTable("Comite.Servicios");
            //    DropTable("Comite.PersonasMorales");
            //    DropTable("Global.PersonalidadesJuridicas");
            //    DropTable("Global.EstadosCiviles");
            //    DropTable("Comite.PersonasFisicas");
            //    DropTable("Comite.Personas");
            //    DropTable("Comite.Propietarios");
            //    DropTable("Comite.PeriodosPago");
            //    DropTable("Comite.LiquidacionesToma");
            //    DropTable("Comite.CtoTipoCalles");
            //    DropTable("Comite.CtoColonias");
            //    DropTable("Comite.CtoCalles");
            //    DropTable("Comite.Direcciones");
            //    DropTable("Comite.Tarifas");
            //    DropTable("Comite.Categorias");
            //    DropTable("Comite.Tomas");
            //    DropTable("Comite.TipoNotificaciones");
            //    DropTable("Comite.CtoOpcionNotificaciones");
            //    DropTable("Comite.Notificaciones");
            //    DropTable("Comite.ConceptosPago");
            //    DropTable("Comite.Pagos");
            //    DropTable("Comite.EstatusConvenios");
            //    DropTable("Comite.ConceptosConvenio");
            //    DropTable("Comite.Convenios");
            //    DropTable("Seguridad.PersonasSeguridad");
            //    DropTable("Seguridad.MultiComites");
            //    DropTable("Seguridad.MultiComitesSucursales");
            //    DropTable("Comite.Gastos");
            //    DropTable("Comite.ArchivosGasto");
        }
    }
}
