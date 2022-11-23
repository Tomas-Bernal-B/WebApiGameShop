﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPIGameShop;

#nullable disable

namespace WebAPIGameShop.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221123081816_GameShopVideogames")]
    partial class GameShopVideogames
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebAPIGameShop.Entidades.GameShop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("WebAPIGameShop.Entidades.GameShopVideogames", b =>
                {
                    b.Property<int>("GameShopId")
                        .HasColumnType("int");

                    b.Property<int>("VideogameId")
                        .HasColumnType("int");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.HasKey("GameShopId", "VideogameId");

                    b.HasIndex("VideogameId");

                    b.ToTable("GameShopVideogames");
                });

            modelBuilder.Entity("WebAPIGameShop.Entidades.Opinion", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("Contenido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VideoGameID")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("VideoGameID");

                    b.ToTable("Opinions");
                });

            modelBuilder.Entity("WebAPIGameShop.Entidades.VideoGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Genero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("VideoGames");
                });

            modelBuilder.Entity("WebAPIGameShop.Entidades.GameShopVideogames", b =>
                {
                    b.HasOne("WebAPIGameShop.Entidades.GameShop", "GameShop")
                        .WithMany("GameShopVideogames")
                        .HasForeignKey("GameShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPIGameShop.Entidades.VideoGame", "VideoGame")
                        .WithMany("GameShopVideogames")
                        .HasForeignKey("VideogameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameShop");

                    b.Navigation("VideoGame");
                });

            modelBuilder.Entity("WebAPIGameShop.Entidades.Opinion", b =>
                {
                    b.HasOne("WebAPIGameShop.Entidades.VideoGame", "videoGame")
                        .WithMany("Opinions")
                        .HasForeignKey("VideoGameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("videoGame");
                });

            modelBuilder.Entity("WebAPIGameShop.Entidades.GameShop", b =>
                {
                    b.Navigation("GameShopVideogames");
                });

            modelBuilder.Entity("WebAPIGameShop.Entidades.VideoGame", b =>
                {
                    b.Navigation("GameShopVideogames");

                    b.Navigation("Opinions");
                });
#pragma warning restore 612, 618
        }
    }
}
