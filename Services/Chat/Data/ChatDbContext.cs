using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Chat.Models;

namespace Chat.Data
{
    public partial class ChatDbContext : DbContext
    {
        public ChatDbContext()
        {
        }

        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActiveUser> ActiveUsers { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Dtos.RoomResponseDto> GetUserRoomsByUserId { get; set; }
        public virtual DbSet<RoomsUser> RoomsUsers { get; set; }
        public virtual DbSet<SeenedMessage> SeenedMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActiveUser>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.ConnectionId, "UQ__ActiveUs__A041D5C18D685074")
                    .IsUnique();

                entity.Property(e => e.ConnectionId)
                    .HasMaxLength(10)
                    .HasColumnName("connectionId")
                    .IsFixedLength();

                entity.Property(e => e.RoomUsersId).HasColumnName("roomUsersId");

                entity.HasOne(d => d.RoomUsers)
                    .WithMany()
                    .HasForeignKey(d => d.RoomUsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ActiveUse__roomU__2C3393D0");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.message)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("message");

                entity.Property(e => e.ParentMessageId)
                    .HasColumnName("parentMessageId")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RoomId).HasColumnName("roomId");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Messages__roomId__276EDEB3");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("name");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

             modelBuilder.Entity<Dtos.RoomResponseDto>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("name");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.UserId).HasColumnName("userId");
                entity.Property(e => e.UnSeenedMessages).HasColumnName("UnSeenedMessages");
            });


            modelBuilder.Entity<RoomsUser>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoomId).HasColumnName("roomId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomsUsers)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RoomsUser__roomI__2A4B4B5E");
            });

            modelBuilder.Entity<SeenedMessage>(entity =>
            {
                entity.Property(e => e.Id)                
                    .HasColumnName("id");

                entity.Property(e => e.MessageId).HasColumnName("messageId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.SeenedMessages)
                    .HasForeignKey(d => d.MessageId)
                    .HasConstraintName("FK__SeenedMes__messa__5FB337D6");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
