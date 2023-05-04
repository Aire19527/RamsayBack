using Infraestructure.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Core.Data
{
    public partial class DataContext: DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<StudentEntity> Students { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentEntity>(entity =>
            {
                entity.ToTable("Student");

                entity.HasIndex(e => e.Username, "IX_Student_Username")
                    .IsUnique();

                entity.Property(e => e.Career).HasColumnType("VARCHAR(50)");

                entity.Property(e => e.FirstName).HasColumnType("VARCHAR(20)");

                entity.Property(e => e.LastName).HasColumnType("VARCHAR(20)");

                entity.Property(e => e.Username).HasColumnType("VARCHAR(20)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
