using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using SocialPets.Models;

namespace SocialPets.DBConnection
{

    public class SocialPetsDB : DbContext

    {
        public SocialPetsDB(DbContextOptions<SocialPetsDB> options) : base(options)
        {
        }

        public DbSet<Album> AlbumDb { get; set; }
        public DbSet<Amigos> AmigoDb { get; set; }
        public DbSet<Categoria> CategoriaDb { get; set; }
        public DbSet<Ciudad> CiudadDb { get; set; }
        public DbSet<Comentario> ComentarioDb { get; set; }
        public DbSet<Conversacion> ConversacionDb { get; set; }
        public DbSet<Corazon> CorazonDb { get; set; }
        public DbSet<Grupo> GrupoDb { get; set; }
        public DbSet<Imagen> ImagenesDb { get; set; }
        public DbSet<Mensage> MensageDb { get; set; }
        public DbSet<Nivel> NivelDb { get; set; }
        public DbSet<Notificacion> NotificacionDb { get; set; }
        public DbSet<Perfil> PerfilesDb { get; set; }
        public DbSet<Publicaciones> PublicacionDb { get; set; }
        public DbSet<PublicacionImagen> PublicacionImagenDb { get; set; }
        public DbSet<Recuperacion> RecuperacionDb { get; set; }
        public DbSet<Sentimiento> SentimientoDb { get; set; }
        public DbSet<Usuario> UsuarioDb { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Album>(table =>
                {
                    table.ToTable(nameof(Album));
                    table.HasKey(a => a.id_album);
                    table.Property(e => e.creado_en);
                    table.Property(e => e.nivel_id_nivel);
                    table.Property(e => e.titulo).IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
                   
                }
                );
            modelBuilder.Entity<Amigos>(table =>
            {
                table.ToTable(nameof(Amigos));
                table.HasKey(a => a.id_amigos);
                table.Property(e => e.creado_en);
                table.Property(e => e.leido);
                table.Property(e => e.perfil_id_perfil)
                .HasMaxLength(200)
                .IsUnicode(false);
            
            }
                );

            modelBuilder.Entity<Categoria>(table =>
            {
                table.ToTable(nameof(Categoria));
                table.HasKey(a => a.id_categoria);
                table.Property(e => e.nombre);
                table.Property(e => e.publicaciones_id_publicaciones)
                .HasMaxLength(200)
                .IsUnicode(false);
            
            }
                );

            modelBuilder.Entity<Ciudad>(table =>
            {
                table.ToTable(nameof(Ciudad));
                table.HasKey(a => a.id_ciudad);
                table.Property(e => e.name);
                table.Property(e => e.prefijo);
            }        );

            modelBuilder.Entity<Comentario>(table =>
            {
                table.ToTable(nameof(Comentario));
                table.HasKey(a => a.id_comentario);
                table.Property(e => e.contenido).IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);
                table.Property(e => e.creado_en);
                table.Property(e => e.referencia_id);
                table.Property(e => e.tipo_id);
                table.Property(e => e.usuario_id_usuario).IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            });
    



           modelBuilder.Entity<Conversacion>(table =>
                {
                    table.ToTable(nameof(Conversacion));
                    table.HasKey(a => a.id_conversacion);
                    table.Property(a => a.creado_en);
                    table.Property(a => a.usuario_id_usuario);
                    table.Property(a => a.usuario_id_recibe);

                }
                );

            modelBuilder.Entity<Corazon>(table =>
            {
                table.ToTable(nameof(Corazon));
                table.HasKey(a => a.id_corazon);
                table.Property(e => e.creado_en);
                table.Property(e => e.referencia_id);
                table.Property(e => e.tipo_id);
                table.Property(e => e.usuario_id_usuario)
            .HasMaxLength(200)
                .IsUnicode(false);
            
            }
               );


            modelBuilder.Entity<Grupo>(table =>
            {
                table.ToTable(nameof(Grupo));
                table.HasKey(a => a.id_grupo);
                table.Property(e => e.creado_en);
                table.Property(e => e.descripcion);
                table.Property(e => e.estado);
                table.Property(e => e.imagen);
                table.Property(e => e.titulo);
                table.Property(e => e.usuario_id_usuario)
            .HasMaxLength(200)
                .IsUnicode(false);
            
            }
               );

            modelBuilder.Entity<Imagen>(table =>
            {
                table.ToTable(nameof(Imagen));
                table.HasKey(a => a.id_imagen);
                table.Property(e => e.album_id_album);
                table.Property(e => e.contenido);
                table.Property(e => e.creado_en);
                table.Property(e => e.fuente);                
                table.Property(e => e.titulo);
                table.Property(e => e.usuario_id_usuario)
            .HasMaxLength(200)
                .IsUnicode(false);
     
            }
         );


            modelBuilder.Entity<Mensage>(table =>
                {
                    table.ToTable(nameof(Mensage));
                    table.HasKey(a => a.id_mensage);
                    table.Property(a => a.contenido);
                    table.Property(a => a.usuario_id_usuario);
                    table.Property(a => a.conversacion_id_conversacion);
                    table.Property(a => a.creado_en);
                    table.Property(a => a.leido);
                }
                );


            modelBuilder.Entity<Nivel>(table =>
            {
                table.ToTable(nameof(Nivel));
                table.HasKey(a => a.id_nivel);
                table.Property(e => e.name)
            .HasMaxLength(200)
                .IsUnicode(false);
            
            }
         );

            modelBuilder.Entity<Notificacion>(table =>
            {
                table.ToTable(nameof(Notificacion));
                table.HasKey(a => a.id_notificacion);
                table.Property(e => e.creado_en);
                table.Property(e => e.leido);
                table.Property(e => e.referencia_id);
                table.Property(e => e.tipo_id);
                table.Property(e => e.usuario_id_usuario)
            .HasMaxLength(200)
                .IsUnicode(false);
            
            }
         );
            modelBuilder.Entity<Perfil>(table =>
            {
                table.ToTable(nameof(Perfil));
                table.HasKey(a => a.id_perfil);
                table.Property(e => e.biografia);
                table.Property(e => e.ciudad_id_ciudad);
                table.Property(e => e.correo_electronico_id);
                table.Property(e => e.dia_cumpleaños);
                table.Property(e => e.direccion);
                table.Property(e => e.genero);
                table.Property(e => e.imagen_perfil);
                table.Property(e => e.imagen_portada);
                table.Property(e => e.me_gusta);
                table.Property(e => e.nivel_id_nivel);
                table.Property(e => e.no_me_gusta);
                table.Property(e => e.numero_telefono);
                table.Property(e => e.sentimiento_id_sentimiento);
                table.Property(e => e.titulo);
                table.Property(e => e.usuario_id_usuario)
            .HasMaxLength(200)
                .IsUnicode(false);
            
                        }
                 );

            modelBuilder.Entity<PublicacionImagen>(table =>
            {
                table.ToTable(nameof(PublicacionImagen));
                table.HasKey(a => a.id_publicacion_imagen);
                table.Property(e => e.imagen_id_imagen);
                table.Property(e => e.publicacion_id_publicacion)
            .HasMaxLength(200)
                .IsUnicode(false);
            
            }
     );

            modelBuilder.Entity<Publicaciones>(table =>
            {
                table.ToTable(nameof(Publicaciones));
                table.HasKey(a => a.id_publicacion);
                
                table.Property(e => e.comenzar_en);
                table.Property(e => e.contenido);
                table.Property(e => e.finalizado_en);
                table.Property(e => e.latitud);
                table.Property(e => e.longitud);
                table.Property(e => e.nivel_id_nivel);
                table.Property(e => e.referencia_autor_id);
                table.Property(e => e.referencia_receptor_id);
                table.Property(e => e.tipo_publicacion_id);
                table.Property(e => e.tipo_receptor_id);
                table.Property(e => e.titulo)
            .HasMaxLength(200)
                .IsUnicode(false);
            
            }
    );


            modelBuilder.Entity<Recuperacion>(table =>
            {
                table.ToTable(nameof(Recuperacion));
                table.HasKey(a => a.id_recuperacion);
                table.Property(e => e.creado_en);
                table.Property(e => e.en_uso);
                table.Property(e => e.usuario_id_usuario)
            .HasMaxLength(200)
                .IsUnicode(false);
            
            }
    );


            modelBuilder.Entity<Sentimiento>(table =>
            {
                table.ToTable(nameof(Sentimiento));
                table.HasKey(a => a.id_sentimiento);
                table.Property(e => e.name)
            .HasMaxLength(200)
                .IsUnicode(false);
            
            }
         );

            modelBuilder.Entity<Usuario>(table =>
            {
                table.ToTable(nameof(Usuario));
                table.HasKey(a => a.id_usuario);
                table.Property(e => e.activo);
                table.Property(e => e.creado_en);
                table.Property(e => e.apellido);
                table.Property(e => e.correo_electronico);
                table.Property(e => e.contrasena_hash);
                table.Property(e => e.nombre).IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
                table.Property(e => e.nombre_usuario).IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            }
               );


        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
