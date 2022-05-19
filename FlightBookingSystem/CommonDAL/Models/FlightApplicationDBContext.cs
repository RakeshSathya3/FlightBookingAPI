using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CommonDAL.Models
{
    public partial class FlightApplicationDBContext : DbContext
    {
        public FlightApplicationDBContext()
        {
        }

        public FlightApplicationDBContext(DbContextOptions<FlightApplicationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblBooking> TblBookings { get; set; }
        public virtual DbSet<TblFlight> TblFlights { get; set; }
        public virtual DbSet<TblPassenger> TblPassengers { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=CTSDOTNET013;Database=FlightApplicationDB;User ID=sa;Password=pass@word1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblBooking>(entity =>
            {
                entity.HasKey(e => e.Pnr);

                entity.ToTable("tblBooking");

                entity.Property(e => e.Pnr).HasColumnName("PNR");

                entity.Property(e => e.ArrivalDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DepartureDateTime).HasColumnType("datetime");

                entity.Property(e => e.FlightNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsOneWay)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblFlight>(entity =>
            {
                entity.HasKey(e => e.FlightNo);

                entity.ToTable("tblFlight");

                entity.Property(e => e.FlightNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ArrivalDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DepartureDateTime).HasColumnType("datetime");

                entity.Property(e => e.FlightName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FromLocation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Y')");

                entity.Property(e => e.MealOption)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PriceBc)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("PriceBC");

                entity.Property(e => e.PriceNonBc)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("PriceNonBC");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ToLocation)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblPassenger>(entity =>
            {
                entity.HasKey(e => e.PassengerId);

                entity.ToTable("tblPassenger");

                entity.Property(e => e.PassengerId).HasColumnName("PassengerID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsMealOpted)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.MealType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PassengerGender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PassengerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PassengerSeatNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pnr).HasColumnName("PNR");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("tblUser");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ContactNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Password)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
