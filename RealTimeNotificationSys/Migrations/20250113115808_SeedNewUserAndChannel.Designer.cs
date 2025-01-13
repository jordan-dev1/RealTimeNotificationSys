﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealTimeNotificationSys.Infrastructure.Data;

#nullable disable

namespace RealTimeNotificationSys.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250113115808_SeedNewUserAndChannel")]
    partial class SeedNewUserAndChannel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.Channel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Channels");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Sports"
                        },
                        new
                        {
                            ID = 2,
                            Name = "News"
                        },
                        new
                        {
                            ID = 3,
                            Name = "Tech"
                        });
                });

            modelBuilder.Entity("Core.Entities.Notification", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("ChannelId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ID");

                    b.HasIndex("ChannelId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Core.Entities.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Email = "john@example.com",
                            Name = "John Doe",
                            PasswordHash = "AQAAAAIAAYagAAAAEKUCE0ixT0B3HUln90mLx/Gb678uTByVI31W6eb2lGr5xKojXrXMr+tJuzEBF5RxJA=="
                        },
                        new
                        {
                            ID = 2,
                            Email = "jane@example.com",
                            Name = "Jane Smith",
                            PasswordHash = "AQAAAAIAAYagAAAAEMiVFgElNB2ys7F5nWyjMDB6I8yzpWvamLowiIaAs9DzemDzI4ufZR6BzxPLp8xqpw=="
                        });
                });

            modelBuilder.Entity("RealTimeNotificationSys.Core.Entities.UserChannel", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ChannelId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ChannelId");

                    b.HasIndex("ChannelId");

                    b.ToTable("UserChannels");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            ChannelId = 1
                        },
                        new
                        {
                            UserId = 1,
                            ChannelId = 2
                        },
                        new
                        {
                            UserId = 2,
                            ChannelId = 3
                        });
                });

            modelBuilder.Entity("Core.Entities.Notification", b =>
                {
                    b.HasOne("Core.Entities.Channel", "Channel")
                        .WithMany()
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");
                });

            modelBuilder.Entity("RealTimeNotificationSys.Core.Entities.UserChannel", b =>
                {
                    b.HasOne("Core.Entities.Channel", "Channel")
                        .WithMany("SubscribedUsers")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "User")
                        .WithMany("SubscribedChannels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Entities.Channel", b =>
                {
                    b.Navigation("SubscribedUsers");
                });

            modelBuilder.Entity("Core.Entities.User", b =>
                {
                    b.Navigation("SubscribedChannels");
                });
#pragma warning restore 612, 618
        }
    }
}
