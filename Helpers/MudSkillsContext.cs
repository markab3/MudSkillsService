using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MudSkillsService.Models;

namespace MudSkillsService.Helpers
{
    public partial class MudSkillsContext : DbContext
    {
        public MudSkillsContext()
        {
        }

        public MudSkillsContext(DbContextOptions<MudSkillsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Mud> Mud { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<PlayerSkill> PlayerSkills { get; set; } = null!;
        public virtual DbSet<Skill> Skills { get; set; } = null!;
        public virtual DbSet<WebsiteUser> WebsiteUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost; Database=MudSkills; Username=MudSkillsService; Password=v5LZX3i1juuf");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mud>(entity =>
            {
                entity.Property(e => e.MudId)
                    .HasColumnName("MudID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.MudName).HasMaxLength(50);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("Player");

                entity.HasIndex(e => e.MudId, "fki_FK_Player_Mud");

                entity.HasIndex(e => e.WebsiteUserId, "fki_Player_WebsiteUser_fkey");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("PlayerID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.MudId).HasColumnName("MudID");

                entity.Property(e => e.PlayerName).HasMaxLength(50);

                entity.Property(e => e.WebsiteUserId).HasColumnName("WebsiteUserID");

                entity.HasOne(d => d.Mud)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.MudId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Player_Mud_fkey");

                entity.HasOne(d => d.WebsiteUser)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.WebsiteUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Player_WebsiteUser_fkey");
            });

            modelBuilder.Entity<PlayerSkill>(entity =>
            {
                entity.ToTable("PlayerSkill");

                entity.HasIndex(e => e.PlayerId, "fki_PlayerSkill_Player_fkey");

                entity.HasIndex(e => e.SkillId, "fki_PlayerSkill_Skill_fkey");

                entity.Property(e => e.PlayerSkillId)
                    .HasColumnName("PlayerSkillID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.SkillId).HasColumnName("SkillID");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerSkills)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PlayerSkill_Player_fkey");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.PlayerSkills)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PlayerSkill_Skill_fkey");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("Skill");

                entity.HasIndex(e => e.MudId, "fki_Skill_Mud_fkey");

                entity.Property(e => e.SkillId)
                    .HasColumnName("SkillID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.MudId).HasColumnName("MudID");

                entity.Property(e => e.SkillName).HasColumnType("character varying");

                entity.HasOne(d => d.Mud)
                    .WithMany(p => p.Skills)
                    .HasForeignKey(d => d.MudId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Skill_Mud_fkey");
            });

            modelBuilder.Entity<WebsiteUser>(entity =>
            {
                entity.ToTable("WebsiteUser");

                entity.Property(e => e.WebsiteUserId)
                    .HasColumnName("WebsiteUserID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Password).HasMaxLength(200);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
