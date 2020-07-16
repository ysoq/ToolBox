using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Akeem.Web.ToolBox.Models
{
    public partial class ToolsContext : DbContext
    {
        public ToolsContext()
        {
        }

        public ToolsContext(DbContextOptions<ToolsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ToolShortUrl> ToolShortUrl { get; set; }
        public virtual DbSet<ToolShortUrlReport> ToolShortUrlReport { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToolShortUrl>(entity =>
            {
                entity.ToTable("tool_short_url");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(7)");

                entity.Property(e => e.Compress)
                    .HasColumnName("compress")
                    .HasColumnType("varchar(8)")
                    .HasComment("短网址")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreTime)
                    .HasColumnName("cre_time")
                    .HasColumnType("datetime")
                    .HasComment("添加时间");

                entity.Property(e => e.ExpiredTime)
                    .HasColumnName("expired_time")
                    .HasColumnType("datetime")
                    .HasComment("过期时间");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("varchar(1000)")
                    .HasComment("原网址")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<ToolShortUrlReport>(entity =>
            {
                entity.ToTable("tool_short_url_report");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(7)");

                entity.Property(e => e.WatchNum)
                    .HasColumnName("watch_num")
                    .HasColumnType("int(7)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
