﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Touhou_Songs.Data;

#nullable disable

namespace Touhou_Songs.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231115053733_Add_OfficialSong-Character_M-N_Relation")]
    partial class Add_OfficialSongCharacter_MN_Relation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Touhou_Songs.App.Official.Characters.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OriginGameId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OriginGameId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("Touhou_Songs.App.Official.OfficialGames.OfficialGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("GameCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NumberCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OfficialGames");
                });

            modelBuilder.Entity("Touhou_Songs.App.Official.OfficialSongs.OfficialSong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Context")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("OfficialSongs");
                });

            modelBuilder.Entity("Touhou_Songs.App.Official._JoinEntities.CharacterOfficialSong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CharacterId")
                        .HasColumnType("integer");

                    b.Property<int>("OfficialSongId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("OfficialSongId");

                    b.ToTable("CharacterOfficialSongs");
                });

            modelBuilder.Entity("Touhou_Songs.App.Official.Characters.Character", b =>
                {
                    b.HasOne("Touhou_Songs.App.Official.OfficialGames.OfficialGame", "OriginGame")
                        .WithMany()
                        .HasForeignKey("OriginGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OriginGame");
                });

            modelBuilder.Entity("Touhou_Songs.App.Official.OfficialSongs.OfficialSong", b =>
                {
                    b.HasOne("Touhou_Songs.App.Official.OfficialGames.OfficialGame", "Game")
                        .WithMany("Songs")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Touhou_Songs.App.Official._JoinEntities.CharacterOfficialSong", b =>
                {
                    b.HasOne("Touhou_Songs.App.Official.Characters.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Touhou_Songs.App.Official.OfficialSongs.OfficialSong", "OfficialSong")
                        .WithMany()
                        .HasForeignKey("OfficialSongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("OfficialSong");
                });

            modelBuilder.Entity("Touhou_Songs.App.Official.OfficialGames.OfficialGame", b =>
                {
                    b.Navigation("Songs");
                });
#pragma warning restore 612, 618
        }
    }
}
