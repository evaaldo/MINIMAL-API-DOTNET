﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using RangoAgil.API.DbContexts;

#nullable disable

namespace MINIMAL_API.Migrations
{
    [DbContext(typeof(RangoDbContext))]
    [Migration("20241009002509_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("IngredienteRango", b =>
                {
                    b.Property<int>("IngredientesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RangosId")
                        .HasColumnType("INTEGER");

                    b.HasKey("IngredientesId", "RangosId");

                    b.HasIndex("RangosId");

                    b.ToTable("IngredienteRango");

                    b.HasData(
                        new
                        {
                            IngredientesId = 1,
                            RangosId = 1
                        },
                        new
                        {
                            IngredientesId = 2,
                            RangosId = 1
                        },
                        new
                        {
                            IngredientesId = 3,
                            RangosId = 1
                        },
                        new
                        {
                            IngredientesId = 4,
                            RangosId = 1
                        },
                        new
                        {
                            IngredientesId = 5,
                            RangosId = 1
                        },
                        new
                        {
                            IngredientesId = 6,
                            RangosId = 1
                        },
                        new
                        {
                            IngredientesId = 7,
                            RangosId = 1
                        },
                        new
                        {
                            IngredientesId = 8,
                            RangosId = 1
                        },
                        new
                        {
                            IngredientesId = 14,
                            RangosId = 1
                        },
                        new
                        {
                            IngredientesId = 9,
                            RangosId = 2
                        },
                        new
                        {
                            IngredientesId = 19,
                            RangosId = 2
                        },
                        new
                        {
                            IngredientesId = 11,
                            RangosId = 2
                        },
                        new
                        {
                            IngredientesId = 12,
                            RangosId = 2
                        },
                        new
                        {
                            IngredientesId = 13,
                            RangosId = 2
                        },
                        new
                        {
                            IngredientesId = 2,
                            RangosId = 2
                        },
                        new
                        {
                            IngredientesId = 21,
                            RangosId = 2
                        },
                        new
                        {
                            IngredientesId = 8,
                            RangosId = 2
                        },
                        new
                        {
                            IngredientesId = 1,
                            RangosId = 3
                        },
                        new
                        {
                            IngredientesId = 12,
                            RangosId = 3
                        },
                        new
                        {
                            IngredientesId = 17,
                            RangosId = 3
                        },
                        new
                        {
                            IngredientesId = 14,
                            RangosId = 3
                        },
                        new
                        {
                            IngredientesId = 2,
                            RangosId = 3
                        },
                        new
                        {
                            IngredientesId = 16,
                            RangosId = 3
                        },
                        new
                        {
                            IngredientesId = 23,
                            RangosId = 3
                        },
                        new
                        {
                            IngredientesId = 8,
                            RangosId = 3
                        },
                        new
                        {
                            IngredientesId = 1,
                            RangosId = 4
                        },
                        new
                        {
                            IngredientesId = 18,
                            RangosId = 4
                        },
                        new
                        {
                            IngredientesId = 16,
                            RangosId = 4
                        },
                        new
                        {
                            IngredientesId = 20,
                            RangosId = 4
                        },
                        new
                        {
                            IngredientesId = 22,
                            RangosId = 4
                        },
                        new
                        {
                            IngredientesId = 2,
                            RangosId = 4
                        },
                        new
                        {
                            IngredientesId = 21,
                            RangosId = 4
                        },
                        new
                        {
                            IngredientesId = 8,
                            RangosId = 4
                        },
                        new
                        {
                            IngredientesId = 24,
                            RangosId = 5
                        },
                        new
                        {
                            IngredientesId = 10,
                            RangosId = 5
                        },
                        new
                        {
                            IngredientesId = 23,
                            RangosId = 5
                        },
                        new
                        {
                            IngredientesId = 2,
                            RangosId = 5
                        },
                        new
                        {
                            IngredientesId = 12,
                            RangosId = 5
                        },
                        new
                        {
                            IngredientesId = 18,
                            RangosId = 5
                        },
                        new
                        {
                            IngredientesId = 14,
                            RangosId = 5
                        },
                        new
                        {
                            IngredientesId = 20,
                            RangosId = 5
                        },
                        new
                        {
                            IngredientesId = 13,
                            RangosId = 5
                        });
                });

            modelBuilder.Entity("RangoAgil.API.Entities.Ingrediente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ingredientes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Carne de Vaca"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Cebola"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Cerveja Escura"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Fatia de Pão Integral"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Mostarda"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Chicória"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Maionese"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Vários Temperos"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Mexilhões"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Aipo"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Batatas Fritas"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Tomate"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Extrato de Tomate"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Folha de Louro"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Cenoura"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Alho"
                        },
                        new
                        {
                            Id = 17,
                            Name = "Vinho Tinto"
                        },
                        new
                        {
                            Id = 18,
                            Name = "Leite de Coco"
                        },
                        new
                        {
                            Id = 19,
                            Name = "Gengibre"
                        },
                        new
                        {
                            Id = 20,
                            Name = "Pimenta Malagueta"
                        },
                        new
                        {
                            Id = 21,
                            Name = "Tamarindo"
                        },
                        new
                        {
                            Id = 22,
                            Name = "Peixe Firme"
                        },
                        new
                        {
                            Id = 23,
                            Name = "Pasta de Gengibre e Alho"
                        },
                        new
                        {
                            Id = 24,
                            Name = "Garam Masala"
                        });
                });

            modelBuilder.Entity("RangoAgil.API.Entities.Rango", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Rangos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Ensopado Flamengo de Carne de Vaca com Chicória"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Mexilhões com Batatas Fritas"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Ragu alla Bolognese"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Rendang"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Masala de Peixe"
                        });
                });

            modelBuilder.Entity("IngredienteRango", b =>
                {
                    b.HasOne("RangoAgil.API.Entities.Ingrediente", null)
                        .WithMany()
                        .HasForeignKey("IngredientesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RangoAgil.API.Entities.Rango", null)
                        .WithMany()
                        .HasForeignKey("RangosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
