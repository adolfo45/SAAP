namespace ComiteAgua.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using ComiteAgua.Models.Seguridad;
    using System.ComponentModel.DataAnnotations.Schema;
    using ComiteAgua.Global;
    using ComiteAgua.Models.Notificaciones;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using ComiteAgua.Models.Direcciones;
    using ComiteAgua.Models.TransaccionesAutomaticas;
    using ComiteAgua.Models.Recibos;
    using ComiteAgua.Models.Sensores;
    using ComiteAgua.Models.Servicios;
    using ComiteAgua.Models.Rentas;
    using System.Configuration;
    using AdsertiVS2017ClassLibrary;
    using ComiteAgua.Domain.Seguridad;

    public class DataContext : DbContext
    {
               
        #region * Constructor generado por Comité Agua *
        public DataContext() : base("name=ComiteAgua")
        {
            //Database.Connection.ConnectionString = AdsertiFunciones.DesencriptarTexto(ConfigurationManager.ConnectionStrings["ComiteAgua"].ConnectionString);
            //Database.Connection.ConnectionString = AdsertiFunciones.DesencriptarTexto(ConfigurationManager.ConnectionStrings["ComiteAgua"].ConnectionString);
        }
        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<EstadoCivil> EstadoCivil { get; set; }
        public DbSet<PersonaFisica> PersonaFisica { get; set; }
        public DbSet<PersonalidadJuridica> PersonalidadJuridica { get; set; }
        public DbSet<MultiComite> MultiComite { get; set; }
        public DbSet<Sucursal> Sucursal { get; set; }
        public DbSet<MultiComiteSucursal> MultiComiteSucursal { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<PersonaSeguridad> PersonaSeguridad { get; set; }
        public DbSet<Propietario> Propietario { get; set; }
        public DbSet<Toma> Toma { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Tarifa> Tarifa { get; set; }
        public DbSet<Direccion> Direccion { get; set; }
        public DbSet<ConceptoPago> ConceptoPago { get; set; }
        public DbSet<Pago> Pago { get; set; }
        public DbSet<PeriodoPago> PeriodoPago { get; set; }
        public DbSet<ConceptoConvenio> ConceptoConvenio { get; set; }
        public DbSet<EstatusConvenio> EstatusConvenio { get; set; }
        public DbSet<PeriodoPagoConvenio> PeriodoPagoConvenio { get; set; }
        public DbSet<Convenio> Convenio { get; set; }
        public DbSet<LiquidacionToma> LiquidacionToma { get; set; }
        public DbSet<Gasto> Gasto { get; set; }
        public DbSet<ArchivoGasto> ArchivoGasto { get; set; }
        public DbSet<Asunto> Asunto { get; set; }
        public DbSet<AsuntoDescripcion> AsuntoDescripcion { get; set; }
        public DbSet<Servicio> Servicio { get; set; }
        public DbSet<Notificacion> Notificacion { get; set; }
        public DbSet<TipoNotificacion> TipoNotificacion { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<UsuarioRol> UsuarioRol { get; set; }
        public DbSet<Calles> Calles { get; set; }
        public DbSet<TiposCalle> TiposCalle { get; set; }
        public DbSet<Colonias> Colonias { get; set; }
        public DbSet<CtoOpcionesNotificacion> CtoOpcionesNotificacion { get; set; }
        public DbSet<TransaccionAutomatica> TransaccionAutomatica { get; set; }
        public DbSet<TipoTransaccionAutomatica> TipoTransaccionAutomatica { get; set; }
        public DbSet<Recibo> Recibo { get; set; }
        public DbSet<Reporte> Reporte { get; set; }
        public DbSet<Sensor> Sensor { get; set; }
        public DbSet<EvidenciaServicio> EvidenciaServicio { get; set; }
        public DbSet<DescuentoProntoPago> DescuentoProntoPago { get; set; }
        public DbSet<Constancia> Constancia { get; set; }
        public DbSet<TiposConstancia> TiposConstancia { get; set; }
        public DbSet<Renta> Renta { get; set; }
        public DbSet<TipoRenta> TipoRenta { get; set; }
        public DbSet<CambioPropietario> CambioPropietario { get; set; }
        public DbSet<PersonaMoral> PersonaMoral { get; set; }
        public DbSet<ArchivoPersona> ArchivoPersona { get; set; }
        public DbSet<TipoArchivo> TipoArchivo { get; set; }
        public DbSet<TipoDescuento> TipoDescuento { get; set; }
        public DbSet<Descuento> Descuento { get; set; }
        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            #region * Usuario *

            modelBuilder.Entity<Usuario>().ToTable("Usuarios", "Seguridad")
              .HasKey(a => a.UsuarioId);

            modelBuilder.Entity<Usuario>().Property(x => x.UserName)
                       .HasMaxLength(50)
                       .IsRequired();

            modelBuilder.Entity<Usuario>().Property(x => x.Password)
                       .HasMaxLength(8)
                       .IsRequired();
            modelBuilder.Entity<Usuario>().Property(x => x.TokenFirebase)
                       .HasMaxLength(255)
                       .IsOptional();

            #endregion

            #region * PersonaSeguridad *

            modelBuilder.Entity<PersonaSeguridad>().ToTable("PersonasSeguridad", "Seguridad")
            .HasKey(x => new { x.PersonaId });

            modelBuilder.Entity<PersonaSeguridad>().Property(x => x.Rfc)
                     .HasMaxLength(50)
                     .IsOptional();

            modelBuilder.Entity<PersonaSeguridad>().Property(x => x.Nombre)
                     .HasMaxLength(50)
                     .IsRequired();

            modelBuilder.Entity<PersonaSeguridad>().Property(x => x.ApellidoPaterno)
                    .HasMaxLength(50)
                    .IsRequired();

            modelBuilder.Entity<PersonaSeguridad>().Property(x => x.ApellidoMaterno)
                    .HasMaxLength(50)
                    .IsRequired();

            modelBuilder.Entity<PersonaSeguridad>().Property(x => x.CorreoElectronico)
                    .HasMaxLength(50)
                    .IsOptional();

            modelBuilder.Entity<PersonaSeguridad>().Property(x => x.Telefono)
                .HasMaxLength(15)
                    .IsOptional();

            modelBuilder.Entity<PersonaSeguridad>().Property(x => x.EstadoCivilId)
                    .IsOptional();

            modelBuilder.Entity<PersonaSeguridad>().Property(x => x.Cargo)
                     .HasMaxLength(100)
                     .IsOptional();

            #endregion

            #region * EstadoCivil *

            modelBuilder.Entity<EstadoCivil>().ToTable("EstadosCiviles", "Global")
              .HasKey(a => a.EstadoCivilId);

            modelBuilder.Entity<EstadoCivil>().Property(x => x.Nombre)
               .HasColumnName("EstadoCivil")
               .HasMaxLength(50)
               .IsRequired();

            #endregion

            #region * PersonaFisica *

            modelBuilder.Entity<PersonaFisica>().ToTable("PersonasFisicas", "Comite")
             .HasKey(a => a.PersonaId);

            modelBuilder.Entity<PersonaFisica>().Property(pf => pf.PersonaId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<PersonaFisica>().Property(x => x.Nombre)
                      .HasMaxLength(1000)
                      .IsRequired();

            modelBuilder.Entity<PersonaFisica>().Property(x => x.ApellidoPaterno)
                      .HasMaxLength(100)
                      .IsOptional();

            modelBuilder.Entity<PersonaFisica>().Property(x => x.ApellidoMaterno)
                      .HasMaxLength(100)
                      .IsOptional();

            modelBuilder.Entity<PersonaFisica>().Property(x => x.CorreoElectronico)
                      .HasMaxLength(50)
                      .IsOptional();

            modelBuilder.Entity<PersonaFisica>().Property(x => x.Rfc)
                      .HasMaxLength(50)
                      .IsOptional();

            modelBuilder.Entity<PersonaFisica>().Property(x => x.EstadoCivilId)
                      .IsOptional();

            modelBuilder.Entity<PersonaFisica>().Property(x => x.Telefono)
                      .HasMaxLength(15)
                      .IsOptional();

            modelBuilder.Entity<PersonaFisica>()
                .HasRequired(pf => pf.Persona)
                .WithOptional(p => p.PersonaFisica);

            modelBuilder.Entity<PersonaFisica>()
               .HasRequired(pf => pf.EstadoCivil)
               .WithMany(n => n.PersonasFisicas)
               .HasForeignKey(n => n.EstadoCivilId);

            #endregion

            #region * PersonaMoral *

            modelBuilder.Entity<PersonaMoral>().ToTable("PersonasMorales", "Comite")
             .HasKey(a => a.PersonaId);

            modelBuilder.Entity<PersonaMoral>().Property(pf => pf.PersonaId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<PersonaFisica>().Property(x => x.Nombre)
                      .HasMaxLength(50)
                      .IsRequired();           

            modelBuilder.Entity<PersonaMoral>().Property(x => x.Rfc)
                      .HasMaxLength(50)
                      .IsOptional();

            modelBuilder.Entity<PersonaMoral>().Property(x => x.CorreoElectronico)
                      .HasMaxLength(50)
                      .IsOptional();

            modelBuilder.Entity<PersonaMoral>()
                .HasRequired(pf => pf.Persona)
                .WithOptional(p => p.PersonaMoral);

            #endregion

            #region * PersonalidadJuridica *

            modelBuilder.Entity<PersonalidadJuridica>().ToTable("PersonalidadesJuridicas", "Global")
             .HasKey(a => a.PersonalidadJuridicaId);

            modelBuilder.Entity<PersonalidadJuridica>().Property(x => x.Nombre)
               .HasColumnName("PersonalidadJuridica")
               .HasMaxLength(50)
               .IsRequired();

            #endregion

            #region * MultiComite *

            modelBuilder.Entity<MultiComite>().ToTable("MultiComites", "Seguridad")
             .HasKey(a => a.MultiComiteId);

            modelBuilder.Entity<MultiComite>().Property(x => x.Nombre)
               .HasColumnName("MultiComite")
               .HasMaxLength(50)
               .IsRequired();

            modelBuilder.Entity<MultiComite>().Property(x => x.Rfc)
              .HasMaxLength(50)
              .IsOptional();

            modelBuilder.Entity<MultiComite>().Property(x => x.RazonSocial)
             .HasMaxLength(50)
             .IsOptional();

            modelBuilder.Entity<MultiComite>().Property(x => x.Delegacion)
             .HasMaxLength(100)
             .IsRequired();

            #endregion

            #region * Sucursal *

            modelBuilder.Entity<Sucursal>().ToTable("Sucursales", "Seguridad")
             .HasKey(a => a.SucursalId);

            modelBuilder.Entity<Sucursal>().Property(x => x.Nombre)
               .HasColumnName("Sucursal")
               .HasMaxLength(50)
               .IsRequired();

            #endregion

            #region * MultiComiteSucursal *

            modelBuilder.Entity<MultiComiteSucursal>().ToTable("MultiComitesSucursales", "Seguridad")
            .HasKey(mes => new { mes.MultiComiteId, mes.SucursalId });

            modelBuilder.Entity<MultiComiteSucursal>().Property(mes => mes.MultiComiteId)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<MultiComiteSucursal>().Property(mes => mes.SucursalId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            #endregion

            #region * Persona *

            modelBuilder.Entity<Persona>().ToTable("Personas", "Comite")
            .HasKey(x => new { x.PersonaId });

            #endregion

            #region * Propietario  *

            modelBuilder.Entity<Propietario>().ToTable("Propietarios", "Comite")
             .HasKey(a => a.PropietarioId);

            #endregion

            #region * Tomas *

            modelBuilder.Entity<Toma>().ToTable("Tomas", "Comite")
             .HasKey(a => a.TomaId);

            modelBuilder.Entity<Toma>().Property(x => x.Folio)
            .IsRequired();

            modelBuilder.Entity<Toma>().Property(x => x.Observaciones)
           .HasMaxLength(255)
           .IsOptional();

            #endregion

            #region * Categorias *

            modelBuilder.Entity<Categoria>().ToTable("Categorias", "Comite")
             .HasKey(a => a.CategoriaId);

            modelBuilder.Entity<Categoria>().Property(x => x.Nombre)
              .HasColumnName("Categoria")
              .HasMaxLength(100)
              .IsRequired();

            modelBuilder.Entity<Categoria>().Property(x => x.Abreviatura)
             .HasMaxLength(50)
             .IsRequired();

            #endregion

            #region * Tarifa *

            modelBuilder.Entity<Tarifa>().ToTable("Tarifas", "Comite")
            .HasKey(a => a.TarifaId);

            #endregion

            #region * Direccion * 

            modelBuilder.Entity<Direccion>().ToTable("Direcciones", "Comite")
            .HasKey(a => a.DireccionId);

            modelBuilder.Entity<Direccion>().Property(x => x.Calle)
                      .HasMaxLength(100)
                      .IsOptional();

            modelBuilder.Entity<Direccion>().Property(x => x.NumInt)
                     .HasMaxLength(70)
                     .IsOptional();

            modelBuilder.Entity<Direccion>().Property(x => x.NumExt)
                     .HasMaxLength(70)
                     .IsOptional();

            modelBuilder.Entity<Direccion>().Property(x => x.Colonia)
                      .HasMaxLength(100)
                      .IsOptional();

            modelBuilder.Entity<Direccion>().Property(x => x.CalleId)
           .IsOptional();

            modelBuilder.Entity<Direccion>().Property(x => x.TipoCalleId)
           .IsOptional();

            modelBuilder.Entity<Direccion>().Property(x => x.ColoniaId)
           .IsOptional();

            #endregion

            #region * Colonias *

            modelBuilder.Entity<Colonias>().ToTable("CtoColonias", "Comite")
           .HasKey(a => a.ColoniaId);

            modelBuilder.Entity<Colonias>().Property(x => x.Nombre)
            .HasColumnName("Colonia")
            .HasMaxLength(255)
            .IsRequired();

            #endregion

            #region * Calles *

            modelBuilder.Entity<Calles>().ToTable("CtoCalles", "Comite")
            .HasKey(a => a.CalleId);

            modelBuilder.Entity<Calles>().Property(x => x.Nombre)
            .HasColumnName("Calle")
            .HasMaxLength(255)
            .IsRequired();

            #endregion

            #region * TipoCalles *

            modelBuilder.Entity<TiposCalle>().ToTable("CtoTipoCalles", "Comite")
           .HasKey(a => a.TipoCalleId);

            modelBuilder.Entity<TiposCalle>().Property(x => x.Nombre)
            .HasColumnName("TipoCalle")
            .HasMaxLength(255)
            .IsRequired();

            #endregion

            #region * ConceptoPago *

            modelBuilder.Entity<ConceptoPago>().ToTable("ConceptosPago", "Comite")
            .HasKey(a => a.ConceptoPagoId);

            modelBuilder.Entity<ConceptoPago>().Property(x => x.Nombre)
              .HasColumnName("ConceptoPago")
              .HasMaxLength(100)
              .IsRequired();

            #endregion                                  

            #region * Pago *

            modelBuilder.Entity<Pago>().ToTable("Pagos", "Comite")
            .HasKey(a => a.PagoId);

            modelBuilder.Entity<Pago>().Property(x => x.NoRecibo)
            .HasMaxLength(10)
            .IsOptional();

            modelBuilder.Entity<Pago>().Property(x => x.DescuentoId)
                .IsOptional();

            #endregion

            #region * PeriodoPago *

            modelBuilder.Entity<PeriodoPago>().ToTable("PeriodosPago", "Comite")
            .HasKey(a => a.PeriodoPagoId);

            modelBuilder.Entity<PeriodoPago>().Property(cpm => cpm.MesAnoInicio)
              .HasColumnType("date");

            modelBuilder.Entity<PeriodoPago>().Property(cpm => cpm.MesAnoFin)
              .HasColumnType("date");

            #endregion

            #region * Concepto Convenio *

            modelBuilder.Entity<ConceptoConvenio>().ToTable("ConceptosConvenio", "Comite")
            .HasKey(a => a.ConceptoConvenioId);

            modelBuilder.Entity<ConceptoConvenio>().Property(x => x.Nombre)
             .HasColumnName("ConceptoConvenio")
             .HasMaxLength(100)
             .IsRequired();

            #endregion

            #region * Estatus Convenio *

            modelBuilder.Entity<EstatusConvenio>().ToTable("EstatusConvenios", "Comite")
            .HasKey(a => a.EstatusConvenioId);

            modelBuilder.Entity<EstatusConvenio>().Property(x => x.Nombre)
             .HasColumnName("EstatusConvenio")
             .HasMaxLength(100)
             .IsRequired();

            #endregion

            #region * Periodo Pago Convenio *

            modelBuilder.Entity<PeriodoPagoConvenio>().ToTable("PeriodosPagoConvenio", "Comite")
            .HasKey(a => a.PeriodoPagoConvenioId);

            modelBuilder.Entity<PeriodoPagoConvenio>().Property(x => x.Nombre)
             .HasColumnName("PeriodoPagoConvenio")
             .HasMaxLength(100)
             .IsRequired();

            #endregion

            #region * Convenio *

            modelBuilder.Entity<Convenio>().ToTable("Convenios", "Comite")
            .HasKey(a => a.ConvenioId);

            modelBuilder.Entity<Convenio>().Property(x => x.Observaciones)
           .HasMaxLength(255)
           .IsOptional();

            modelBuilder.Entity<Convenio>().Property(x => x.RutaArchivo)
           .HasMaxLength(400)
           .IsRequired();

            modelBuilder.Entity<Convenio>().Property(cpm => cpm.FechaTermino)
             .HasColumnType("date");

            modelBuilder.Entity<Convenio>().Property(cpm => cpm.PeriodoInicio)
             .HasColumnType("date");

            modelBuilder.Entity<Convenio>().Property(cpm => cpm.PeriodoFin)
            .HasColumnType("date");

            #endregion

            #region * Liquidacion Toma *

            modelBuilder.Entity<LiquidacionToma>().ToTable("LiquidacionesToma", "Comite")
            .HasKey(a => a.LiquidacionTomaId);

            modelBuilder.Entity<LiquidacionToma>().Property(x => x.Nombre)
           .HasColumnName("LiquidacionToma")
           .HasMaxLength(100)
           .IsRequired();

            #endregion

            #region * Gasto *

            modelBuilder.Entity<Gasto>().ToTable("Gastos", "Comite")
           .HasKey(x => new { x.GastoId });

            modelBuilder.Entity<Gasto>().Property(x => x.Concepto)
            .HasMaxLength(1000)
            .IsRequired();

            #endregion

            #region * ArchivoGasto *

            modelBuilder.Entity<ArchivoGasto>().ToTable("ArchivosGasto", "Comite")
           .HasKey(x => new { x.ArchivoGastoId });

            modelBuilder.Entity<ArchivoGasto>().Property(x => x.Nombre)
            .HasMaxLength(255)
            .IsRequired();

            #endregion

            #region * Asunto *

            modelBuilder.Entity<Asunto>().ToTable("Asuntos", "Comite")
           .HasKey(x => new { x.AsuntoId });

            modelBuilder.Entity<Asunto>().Property(x => x.Nombre)
          .HasColumnName("Asunto")
          .HasMaxLength(1000)
          .IsRequired();

            #endregion

            #region * AsuntoDescripcion *

            modelBuilder.Entity<AsuntoDescripcion>().ToTable("AsuntosDescripcion", "Comite")
           .HasKey(x => new { x.AsuntoDescripcionId });

            modelBuilder.Entity<AsuntoDescripcion>().Property(x => x.Nombre)
           .HasColumnName("AsuntoDescripcion")
           .HasMaxLength(1000)
           .IsRequired();

            #endregion

            #region * Servicio *

            modelBuilder.Entity<Servicio>().ToTable("Servicios", "Comite")
           .HasKey(x => new { x.ServicioId });

            modelBuilder.Entity<Servicio>().Property(x => x.TomaId)
          .IsOptional();

            modelBuilder.Entity<Servicio>().Property(x => x.Nombre)
           .HasMaxLength(255)
           .IsOptional();

            modelBuilder.Entity<Servicio>().Property(x => x.Direccion)
           .HasMaxLength(1000)
           .IsOptional();

            modelBuilder.Entity<Servicio>().Property(x => x.Colonia)
           .HasMaxLength(100)
           .IsOptional();

            modelBuilder.Entity<Servicio>().Property(x => x.Telefono)
           .HasMaxLength(15)
           .IsOptional();

            modelBuilder.Entity<Servicio>().Property(x => x.UrlArchivo)
           .HasMaxLength(1000);

            modelBuilder.Entity<Servicio>().Property(x => x.UbicacionServicio)
           .HasMaxLength(5000)
           .IsOptional();

            modelBuilder.Entity<Servicio>().Property(x => x.Otro)
           .HasMaxLength(5000)
           .IsOptional();

            modelBuilder.Entity<Servicio>().Property(x => x.Observaciones)
           .HasMaxLength(5000)
           .IsOptional();

            modelBuilder.Entity<Servicio>()
               .HasRequired(p => p.UsuarioAlta)
               .WithMany(u => u.ServicioAlta)
               .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<Servicio>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.ServicioCambio)
                .HasForeignKey(p => p.UsuarioCambioId);

            #endregion

            #region * EstatusServicio *

            modelBuilder.Entity<EstatusServicio>().ToTable("EstatusServicios", "Comite")
           .HasKey(x => new { x.EstatusServicioId });

            modelBuilder.Entity<EstatusServicio>().Property(x => x.Nombre)
           .HasColumnName("EstatusServicio")
           .HasMaxLength(1000)
           .IsRequired();

            #endregion

            #region * Notificacion *

            modelBuilder.Entity<Notificacion>().ToTable("Notificaciones", "Comite")
           .HasKey(x => new { x.NotificacionId });

            modelBuilder.Entity<Notificacion>()
               .HasRequired(p => p.UsuarioAlta)
               .WithMany(u => u.NotificacionesAlta)
               .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<Notificacion>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.NotificacionesCambio)
                .HasForeignKey(p => p.UsuarioCambioId);

            modelBuilder.Entity<Notificacion>()
               .HasRequired(p => p.UsuarioNotifico)
               .WithMany(u => u.NotificacionesNotifico)
               .HasForeignKey(p => p.UsuarioNotificoId);

            modelBuilder.Entity<Notificacion>().Property(x => x.Observaciones)
           .HasMaxLength(255)
           .IsOptional();

            modelBuilder.Entity<Notificacion>().Property(x => x.OpcionNotificacionId)
           .IsOptional();

            #endregion

            #region * TipoNotificacion *

            modelBuilder.Entity<TipoNotificacion>().ToTable("TipoNotificaciones", "Comite")
           .HasKey(x => new { x.TipoNotificacionId });

            modelBuilder.Entity<TipoNotificacion>().Property(x => x.Nombre)
           .HasColumnName("TipoNotificacion")
           .HasMaxLength(1000)
           .IsRequired();

            #endregion

            #region * UsuarioRol *

            modelBuilder.Entity<UsuarioRol>().ToTable("UsuariosRoles", "Seguridad")
            .HasKey(mes => new { mes.UsuarioId, mes.RolId });

            modelBuilder.Entity<UsuarioRol>().Property(mes => mes.RolId)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<UsuarioRol>().Property(mes => mes.UsuarioId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            #endregion

            #region * Rol *

            modelBuilder.Entity<Rol>().ToTable("Roles", "Seguridad")
             .HasKey(a => a.RolId);

            modelBuilder.Entity<Rol>().Property(x => x.Nombre)
               .HasColumnName("Rol")
               .HasMaxLength(255)
               .IsRequired();

            #endregion

            #region * CtoOpcionesNotificaciones *

            modelBuilder.Entity<CtoOpcionesNotificacion>().ToTable("CtoOpcionNotificaciones", "Comite")
           .HasKey(x => new { x.OpcionNotificacionId });

            modelBuilder.Entity<CtoOpcionesNotificacion>().Property(x => x.Nombre)
           .HasColumnName("OpcionNotificacion")
           .HasMaxLength(1000)
           .IsRequired();

            #endregion

            #region * TransaccionesAutomaticas *

            modelBuilder.Entity<TransaccionAutomatica>().ToTable("TransaccionesAutomaticas", "Comite")
                .HasKey(x => new { x.TransaccionAutomaticaId });

            modelBuilder.Entity<TransaccionAutomatica>()
                .HasRequired(p => p.UsuarioAlta)
                .WithMany(u => u.TransaccionesAlta)
                .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<TransaccionAutomatica>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.TransaccionesCambio)
                .HasForeignKey(p => p.UsuarioCambioId);

            #endregion

            #region * TipoTransaccionAutomatica *

            modelBuilder.Entity<TipoTransaccionAutomatica>().ToTable("TiposTransaccionAutomatica", "Comite")
                .HasKey(x => new { x.TipoTransaccionAutomaticaId });

            modelBuilder.Entity<TipoTransaccionAutomatica>().Property(x => x.Nombre)
                .HasColumnName("TipoTransaccionAutomatica")
                .HasMaxLength(1000)
                .IsRequired();

            #endregion

            #region * Recibo *

            modelBuilder.Entity<Recibo>().ToTable("Recibos", "Comite")
                .HasKey(x => new { x.ReciboId });

            modelBuilder.Entity<Recibo>()
                .HasRequired(p => p.UsuarioAlta)
                .WithMany(u => u.ReciboAlta)
                .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<Recibo>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.ReciboCambio)
                .HasForeignKey(p => p.UsuarioCambioId);

            modelBuilder.Entity<Recibo>().Property(x => x.Observaciones)
                .HasMaxLength(255)
                .IsOptional();

            modelBuilder.Entity<Recibo>().Property(x => x.Adicional)
                .HasMaxLength(255)
                .IsOptional();

            modelBuilder.Entity<Recibo>().Property(x => x.CodigoQRurl)
                .HasMaxLength(500)
                .IsOptional();

            modelBuilder.Entity<Recibo>().Property(x => x.CantidadLetra)
                .HasMaxLength(255)
                .IsOptional();

            modelBuilder.Entity<Recibo>().Property(x => x.RenglonAdicional1)
                .HasMaxLength(250)
                .IsOptional();

            modelBuilder.Entity<Recibo>().Property(x => x.RenglonAdicional2)
               .HasMaxLength(250)
               .IsOptional();

            modelBuilder.Entity<Recibo>().Property(x => x.RenglonAdicional3)
               .HasMaxLength(250)
               .IsOptional();
            modelBuilder.Entity<Recibo>().Property(x => x.Concepto)
               .HasMaxLength(50)
               .IsOptional();

            #endregion

            #region * Reporte *

            modelBuilder.Entity<Reporte>().ToTable("Reportes", "Comite")
                .HasKey(x => new { x.ReporteId });

            modelBuilder.Entity<Reporte>()
                .HasRequired(p => p.UsuarioAlta)
                .WithMany(u => u.ReporteAlta)
                .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<Reporte>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.ReporteCambio)
                .HasForeignKey(p => p.UsuarioCambioId);

            modelBuilder.Entity<Reporte>().Property(x => x.Observaciones)
                .HasMaxLength(500)
                .IsOptional();

            modelBuilder.Entity<Reporte>().Property(x => x.UrlImagen)
                .HasMaxLength(255)
                .IsRequired();

            #endregion

            #region * Sensor *

            modelBuilder.Entity<Sensor>().ToTable("Sensores", "Comite")
                .HasKey(x => new { x.LecturaSensorId });

            modelBuilder.Entity<Sensor>().Property(x => x.Valor)
                .HasMaxLength(50)
                .IsOptional();

            #endregion

            #region * EvidenciaServicio *

            modelBuilder.Entity<EvidenciaServicio>().ToTable("EvidenciaServicios", "Comite")
               .HasKey(x => new { x.EvidenciaServicioId });

            modelBuilder.Entity<EvidenciaServicio>().Property(x => x.Observaciones)
              .HasMaxLength(500)
              .IsOptional();

            modelBuilder.Entity<EvidenciaServicio>().Property(x => x.UrlImagen)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<EvidenciaServicio>()
                .HasRequired(p => p.UsuarioAlta)
                .WithMany(u => u.EvidenciaServicioAlta)
                .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<EvidenciaServicio>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.EvidenciaServicioCambio)
                .HasForeignKey(p => p.UsuarioCambioId);

            #endregion

            #region * DescuentoProntoPago *

            modelBuilder.Entity<DescuentoProntoPago>().ToTable("DescuentosProntoPago", "Comite")
                .HasKey(x => new { x.DescuentoId });

            modelBuilder.Entity<DescuentoProntoPago>()
                .HasRequired(p => p.UsuarioAlta)
                .WithMany(u => u.DescuentoProntoPagoAlta)
                .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<DescuentoProntoPago>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.DescuentoProntoPagoCambio)
                .HasForeignKey(p => p.UsuarioCambioId);
            #endregion

            #region * Constancia *
            modelBuilder.Entity<Constancia>().ToTable("Constancias", "Comite")
                .HasKey(x => new { x.ConstanciaId });

            modelBuilder.Entity<Constancia>()
               .HasRequired(p => p.UsuarioAlta)
               .WithMany(u => u.ConstanciaAlta)
               .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<Constancia>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.ConstanciaCambio)
                .HasForeignKey(p => p.UsuarioCambioId);

            modelBuilder.Entity<Constancia>().Property(x => x.NoInt)
                .HasMaxLength(10)
                .IsOptional();

            modelBuilder.Entity<Constancia>().Property(x => x.NoExt)
                .HasMaxLength(10)
                .IsOptional();

            modelBuilder.Entity<Constancia>().Property(x => x.FechaLetra)
               .HasMaxLength(100)
               .IsOptional();

            modelBuilder.Entity<Constancia>().Property(x => x.Propietario)
                .HasMaxLength(100)
                .IsOptional();
            #endregion

            #region * TiposConstancia *
            modelBuilder.Entity<TiposConstancia>().ToTable("TiposConstancia", "Comite")
                .HasKey(x => new { x.TipoConstanciaId });

            modelBuilder.Entity<TiposConstancia>()
               .HasRequired(p => p.UsuarioAlta)
               .WithMany(u => u.TiposConstanciaAlta)
               .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<TiposConstancia>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.TiposConstanciaCambio)
                .HasForeignKey(p => p.UsuarioCambioId);

            modelBuilder.Entity<TiposConstancia>().Property(x => x.Nombre)
                .HasMaxLength(100)
                .IsRequired();
            #endregion

            #region * Renta *

            modelBuilder.Entity<Renta>().ToTable("Rentas", "Comite")
               .HasKey(x => new { x.RentaId });

            modelBuilder.Entity<Renta>()
               .HasRequired(p => p.UsuarioAlta)
               .WithMany(u => u.RentaAlta)
               .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<Renta>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.RentaCambio)
                .HasForeignKey(p => p.UsuarioCambioId);

            modelBuilder.Entity<Renta>().Property(x => x.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Renta>().Property(x => x.ApellidoPaterno)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Renta>().Property(x => x.ApellidoMaterno)
                .HasMaxLength(50)
                .IsOptional();

            modelBuilder.Entity<Renta>().Property(x => x.Calle)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Renta>().Property(x => x.Colonia)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Renta>().Property(x => x.Municipio)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Renta>().Property(x => x.NoInt)
                .HasMaxLength(10)
                .IsOptional();
            modelBuilder.Entity<Renta>().Property(x => x.Observaciones)
               .HasMaxLength(47)
               .IsOptional();
            modelBuilder.Entity<Renta>().Property(x => x.NoExt)
               .HasMaxLength(10)
               .IsOptional();

            modelBuilder.Entity<Renta>().Property(x => x.Costo)
               .IsRequired();

            #endregion

            #region * TipoRenta *

            modelBuilder.Entity<TipoRenta>().ToTable("TiposRenta", "Comite")
              .HasKey(x => new { x.TipoRentaId });

            modelBuilder.Entity<TipoRenta>()
              .HasRequired(p => p.UsuarioAlta)
              .WithMany(u => u.TipoRentaAlta)
              .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<TipoRenta>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.TipoRentaCambio)
                .HasForeignKey(p => p.UsuarioCambioId);

            modelBuilder.Entity<TipoRenta>().Property(x => x.Nombre)
                .HasMaxLength(150)
                .IsRequired();

            #endregion

            #region * CambioPropietario *
            modelBuilder.Entity<CambioPropietario>().ToTable("CambiosPropietario", "Comite")
             .HasKey(a => a.CambioPropietarioId);

            modelBuilder.Entity<CambioPropietario>().Property(pf => pf.PersonaId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<CambioPropietario>().Property(x => x.Nombre)
                      .HasMaxLength(1000)
                      .IsRequired();

            modelBuilder.Entity<CambioPropietario>().Property(x => x.ApellidoPaterno)
                      .HasMaxLength(100)
                      .IsOptional();

            modelBuilder.Entity<CambioPropietario>().Property(x => x.ApellidoMaterno)
                      .HasMaxLength(100)
                      .IsOptional();

            modelBuilder.Entity<CambioPropietario>().Property(x => x.CorreoElectronico)
                      .HasMaxLength(50)
                      .IsOptional();

            modelBuilder.Entity<CambioPropietario>().Property(x => x.Rfc)
                      .HasMaxLength(50)
                      .IsOptional();

            modelBuilder.Entity<CambioPropietario>().Property(x => x.EstadoCivilId)
                      .IsOptional();

            modelBuilder.Entity<CambioPropietario>().Property(x => x.Telefono)
                      .HasMaxLength(15)
                      .IsOptional();
            modelBuilder.Entity<CambioPropietario>()
             .HasRequired(p => p.UsuarioAlta)
             .WithMany(u => u.CambioPropietarioAlta)
             .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<CambioPropietario>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.CambioPropietarioCambio)
                .HasForeignKey(p => p.UsuarioCambioId);
            #endregion

            #region * TiposPersona *
            modelBuilder.Entity<TipoPersona>().ToTable("TiposPersona", "Comite")
             .HasKey(a => a.TipoPersonaId);

            modelBuilder.Entity<TipoPersona>().Property(x => x.Nombre)
              .HasColumnName("TipoPersona")
              .HasMaxLength(50)
              .IsRequired();

            modelBuilder.Entity<TipoPersona>()
             .HasRequired(p => p.UsuarioAlta)
             .WithMany(u => u.TipoPersonaAlta)
             .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<TipoPersona>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.TipoPersonaCambio)
                .HasForeignKey(p => p.UsuarioCambioId);
            #endregion

            #region * CambioPropietarioPersonaMoral *
            modelBuilder.Entity<CambioPropietarioPersonaMoral>().ToTable("CambiosPropietarioPersonaMoral", "Comite")
             .HasKey(a => a.CambioPropietarioPersonaMoralId);

            modelBuilder.Entity<CambioPropietarioPersonaMoral>().Property(x => x.Nombre)
                      .HasMaxLength(255)
                      .IsRequired();

            modelBuilder.Entity<CambioPropietarioPersonaMoral>().Property(x => x.Rfc)
                      .HasMaxLength(50)
                      .IsOptional();

            modelBuilder.Entity<CambioPropietarioPersonaMoral>().Property(x => x.CorreoElectronico)
                      .HasMaxLength(50)
                      .IsOptional();
            modelBuilder.Entity<CambioPropietarioPersonaMoral>()
            .HasRequired(p => p.UsuarioAlta)
            .WithMany(u => u.CambioPropietarioMoralAlta)
            .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<CambioPropietarioPersonaMoral>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.CambioPropietarioMoralCambio)
                .HasForeignKey(p => p.UsuarioCambioId);
            #endregion

            #region * ArchivoPersona *
            modelBuilder.Entity<ArchivoPersona>().ToTable("ArchivosPersona", "Comite")
             .HasKey(a => a.ArchivoPersonaId);

            modelBuilder.Entity<ArchivoPersona>().Property(x => x.Nombre)
                      .HasMaxLength(150)
                      .IsRequired();

            modelBuilder.Entity<ArchivoPersona>().Property(x => x.UrlArchivo)
                      .HasMaxLength(250)
                      .IsRequired();
           
            modelBuilder.Entity<ArchivoPersona>()
            .HasRequired(p => p.UsuarioAlta)
            .WithMany(u => u.ArchivoPersonaAlta)
            .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<ArchivoPersona>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.ArchivoPersonaCambio)
                .HasForeignKey(p => p.UsuarioCambioId);
            #endregion

            #region * ArchivoPersona *
            modelBuilder.Entity<TipoArchivo>().ToTable("TiposArchivo", "Comite")
             .HasKey(a => a.TipoArchivoId);

            modelBuilder.Entity<TipoArchivo>().Property(x => x.Nombre)
                      .HasMaxLength(150)
                      .IsRequired();

            modelBuilder.Entity<TipoArchivo>()
            .HasRequired(p => p.UsuarioAlta)
            .WithMany(u => u.TipoArchivoAlta)
            .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<TipoArchivo>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.TipoArchivoCambio)
                .HasForeignKey(p => p.UsuarioCambioId);
            #endregion

            #region * TipoDescuento *
            modelBuilder.Entity<TipoDescuento>().ToTable("TiposDescuento", "Comite")
             .HasKey(a => a.TipoDescuentoId);

            modelBuilder.Entity<TipoDescuento>().Property(x => x.Nombre)
              .HasColumnName("TipoDescuento")
              .HasMaxLength(150)
              .IsRequired();

            modelBuilder.Entity<TipoDescuento>()
            .HasRequired(p => p.UsuarioAlta)
            .WithMany(u => u.TipoDescuentoAlta)
            .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<TipoDescuento>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.TipoDescuentoCambio)
                .HasForeignKey(p => p.UsuarioCambioId);
            #endregion

            #region * Descuento *
            modelBuilder.Entity<Descuento>().ToTable("Descuentos", "Comite")
             .HasKey(a => a.DescuentoVariosId);

            modelBuilder.Entity<Descuento>().Property(x => x.Porcentaje)
             .IsRequired();

            modelBuilder.Entity<Descuento>()
            .HasRequired(p => p.UsuarioAlta)
            .WithMany(u => u.DescuentoAlta)
            .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<Descuento>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.DescuentoCambio)
                .HasForeignKey(p => p.UsuarioCambioId);
            #endregion

            #region * DescuentoPago *
            modelBuilder.Entity<DescuentoPago>().ToTable("DescuentosPagos", "Comite")
             .HasKey(a => a.DescuentoPagoId);

            modelBuilder.Entity<DescuentoPago>()
            .HasRequired(p => p.UsuarioAlta)
            .WithMany(u => u.DescuentoPagoAlta)
            .HasForeignKey(p => p.UsuarioAltaId);

            modelBuilder.Entity<DescuentoPago>()
                .HasOptional(p => p.UsuarioCambio)
                .WithMany(u => u.DescuentoPagoCambio)
                .HasForeignKey(p => p.UsuarioCambioId);
            #endregion
        } // protected override void OnModelCreating(DbModelBuilder modelBuilder)
        public string LlenarTexto()
        {
            var seguridadDomain = new SeguridadDomain();
            string encriptar = AdsertiFunciones.EncriptarTexto(ConfigurationManager.ConnectionStrings["ComiteAgua"].ConnectionString);
            var text = seguridadDomain.LlenarTexto(encriptar);
            return text;
        }//public string LlenarTexto()
        public string LimpiarTexto()
        {
            var seguridadDomain = new SeguridadDomain();            
            var text = seguridadDomain.LimpiarTexto(ConfigurationManager.ConnectionStrings["ComiteAgua"].ConnectionString);            
            return text;
        }//public string LimpiarTexto()
        #endregion
    } //  public class DataContext : DbContext
} // namespace ComiteAgua.Models