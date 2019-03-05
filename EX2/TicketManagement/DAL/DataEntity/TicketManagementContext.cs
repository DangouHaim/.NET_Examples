using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using DAL.DataEntity;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public partial class TicketManagementContext : DbContext
    {
        public TicketManagementContext()
        {
            
        }

        public TicketManagementContext(DbContextOptions<TicketManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventArea> EventArea { get; set; }
        public virtual DbSet<EventFile> EventFile { get; set; }
        public virtual DbSet<EventSeat> EventSeat { get; set; }
        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<Layout> Layout { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }
        public virtual DbSet<Seat> Seat { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Venue> Venue { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Layout)
                    .WithMany(p => p.Area)
                    .HasForeignKey(d => d.LayoutId)
                    .HasConstraintName("FK_Layout_Area");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasOne(d => d.EventSeat)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.EventSeatId)
                    .HasConstraintName("FK_EventSeat_Cart");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_User_Cart");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Event__737584F6009075AD")
                    .IsUnique();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.EventDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.HasOne(d => d.Layout)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.LayoutId)
                    .HasConstraintName("FK_Layout_Event");
            });

            modelBuilder.Entity<EventArea>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventArea)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Event_EventArea");

                entity.HasOne(d => d.Layout)
                    .WithMany(p => p.EventArea)
                    .HasForeignKey(d => d.LayoutId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Layout_EventArea");
            });

            modelBuilder.Entity<EventFile>(entity =>
            {
                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventFile)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Event_EventFile");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.EventFile)
                    .HasForeignKey(d => d.FileId)
                    .HasConstraintName("FK_File_EventFile");
            });

            modelBuilder.Entity<EventSeat>(entity =>
            {
                entity.HasOne(d => d.EventArea)
                    .WithMany(p => p.EventSeat)
                    .HasForeignKey(d => d.EventAreaId)
                    .HasConstraintName("FK_EventArea_EventSeat");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<Layout>(entity =>
            {
                entity.HasIndex(e => e.Description)
                    .HasName("UQ__Layout__4EBBBAC9AB1A7F68")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.HasOne(d => d.Venue)
                    .WithMany(p => p.Layout)
                    .HasForeignKey(d => d.VenueId)
                    .HasConstraintName("FK_Venue_Layout");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.Property(e => e.AreaDescription)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.EventDate).HasColumnType("datetime");

                entity.Property(e => e.EventDescription)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Purchase)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_User_Purchase");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Seat)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("FK_Area_Seat");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Balance).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMail")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(512);
            });

            modelBuilder.Entity<Venue>(entity =>
            {
                entity.HasIndex(e => e.Description)
                    .HasName("UQ__Venue__4EBBBAC9356A159D")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.Property(e => e.Phone).HasMaxLength(30);
            });
        }

        public IQueryable<Cart> GetCart(Nullable<int> uid)
        {
            var param = new SqlParameter("uid", uid);

            return Cart.FromSql("SELECT * FROM [Cart] WHERE [UserId] = @uid", param);
        }

        public IQueryable<File> GetAttachedFilesForEvent(Nullable<int> eventId)
        {
            var param = new SqlParameter("@EventId", eventId);

            return File.FromSql("exec GetAttachedFilesForEvent @EventId", param);
        }

        public int AttachFileToEvent(int eventId, string url)
        {
            SqlParameter[] @params =
            {
                new SqlParameter("@returnVal", SqlDbType.Int) {Direction = ParameterDirection.Output},
                new SqlParameter("@EventId", SqlDbType.Int) {Direction = ParameterDirection.Input, Value = eventId},
                new SqlParameter("@Url", SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = url}
            };

            Database.ExecuteSqlCommand("exec @returnVal=" + "AttachFileToEvent @EventId, @Url", @params);

            return (int)@params[0].Value;
        }

        public bool FromCart(int userId, int eventSeatId)
        {
            SqlParameter[] @params =
            {
                new SqlParameter("@UID", SqlDbType.Int) {Direction = ParameterDirection.Input, Value = userId},
                new SqlParameter("@EventSeatId", SqlDbType.Int) {Direction = ParameterDirection.Input, Value = eventSeatId}
            };


            return Database.ExecuteSqlCommand("exec " + "FromCart @UID, @EventSeatId", @params) > 0;
        }

        public int ToCart(int userId, int eventSeatId)
        {
            SqlParameter[] @params =
            {
                new SqlParameter("@returnVal", SqlDbType.Int) {Direction = ParameterDirection.Output},
                new SqlParameter("@uid", SqlDbType.Int) {Direction = ParameterDirection.Input, Value = userId},
                new SqlParameter("@eventSeatId", SqlDbType.Int) {Direction = ParameterDirection.Input, Value = eventSeatId}
            };


            Database.ExecuteSqlCommand("exec @returnVal=" + "ToCart @uid, @eventSeatId", @params);

            return (int)@params[0].Value;
        }

        public int Buy(int userId, int eventSeatId)
        {
            SqlParameter[] @params =
            {
                new SqlParameter("@returnVal", SqlDbType.Int) {Direction = ParameterDirection.Output},
                new SqlParameter("@UID", SqlDbType.Int) {Direction = ParameterDirection.Input, Value = userId},
                new SqlParameter("@EventSeatId", SqlDbType.Int) {Direction = ParameterDirection.Input, Value = eventSeatId}
            };


            Database.ExecuteSqlCommand("exec @returnVal=" + "Buy @UID, @EventSeatId", @params);

            return (int)@params[0].Value;
        }

        public int AddEvent(string name, string description, int layoutId, DateTime eventDate)
        {
            if (string.IsNullOrEmpty(name)
            || string.IsNullOrEmpty(description)
            || DateTime.MinValue == eventDate)
            {
                return -500;
            }
            SqlParameter[] @params =
            {
                new SqlParameter("@returnVal", SqlDbType.Int) {Direction = ParameterDirection.Output},
                new SqlParameter("@Name", SqlDbType.VarChar) {Direction = ParameterDirection.Input, Value = name},
                new SqlParameter("@Description", SqlDbType.VarChar) {Direction = ParameterDirection.Input, Value = description},
                new SqlParameter("@LayoutId", SqlDbType.Int) {Direction = ParameterDirection.Input, Value = layoutId},
                new SqlParameter("@EventDate", SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = eventDate},
            };


            Database.ExecuteSqlCommand("exec @returnVal=" + "AddEvent @Name, @Description, @LayoutId, @EventDate", @params);

            return (int)@params[0].Value;
        }

        public bool UpdateEvent(int eventId, string name, string description, int layoutId, DateTime eventDate)
        {
            if (string.IsNullOrEmpty(name)
                || string.IsNullOrEmpty(description)
                || DateTime.MinValue == eventDate)
            {
                return false;
            }
            SqlParameter[] @params =
            {
                new SqlParameter("@returnVal", SqlDbType.Int) {Direction = ParameterDirection.Output},
                new SqlParameter("@EventId", SqlDbType.Int) {Direction = ParameterDirection.Input, Value = eventId},
                new SqlParameter("@Name", SqlDbType.VarChar) {Direction = ParameterDirection.Input, Value = name},
                new SqlParameter("@Description", SqlDbType.VarChar) {Direction = ParameterDirection.Input, Value = description},
                new SqlParameter("@LayoutId", SqlDbType.Int) {Direction = ParameterDirection.Input, Value = layoutId},
                new SqlParameter("@EventDate", SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = eventDate},
            };


            Database.ExecuteSqlCommand("exec @returnVal=" + "UpdateEvent @EventId, @Name, @Description, @LayoutId, @EventDate", @params);

            return (int)@params[0].Value > 0;
        }

        public bool DeleteEvent(int eventId)
        {
            SqlParameter[] @params =
            {
                new SqlParameter("@returnVal", SqlDbType.Int) {Direction = ParameterDirection.Output},
                new SqlParameter("@EventId", SqlDbType.Int) {Direction = ParameterDirection.Input, Value = eventId}
            };


            Database.ExecuteSqlCommand("exec @returnVal=" + "DeleteEvent @EventId", @params);

            return (int)@params[0].Value > 0;
        }

        public int TicketCount(int eventId)
        {
            SqlParameter[] @params =
            {
                new SqlParameter("@returnVal", SqlDbType.Int) {Direction = ParameterDirection.Output},
                new SqlParameter("@EventId", SqlDbType.Int) {Direction = ParameterDirection.Input, Value = eventId}
            };


            Database.ExecuteSqlCommand("exec @returnVal=" + "EventTicketsCount @EventId", @params);

            return (int)@params[0].Value;
        }

        public int TicketCountTotal(int eventId)
        {
            SqlParameter[] @params =
            {
                new SqlParameter("@returnVal", SqlDbType.Int) {Direction = ParameterDirection.Output},
                new SqlParameter("@EventId", SqlDbType.Int) {Direction = ParameterDirection.Input, Value = eventId}
            };


            Database.ExecuteSqlCommand("exec @returnVal=" + "EventTicketsCountTotal @EventId", @params);

            return (int)@params[0].Value;
        }
    }
}
