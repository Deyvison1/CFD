﻿// <auto-generated />
using System;
using CFD.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CFD.Repositorio.Migrations
{
    [DbContext(typeof(CFDContext))]
    partial class CFDContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CFD.Dominio.Divida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataCompra");

                    b.Property<DateTime?>("DataModificacao");

                    b.Property<DateTime?>("DataRegistro");

                    b.Property<DateTime?>("DataVencimento");

                    b.Property<string>("DescricaoProduto");

                    b.Property<int>("FormaCompra");

                    b.Property<int>("Parcela");

                    b.Property<int>("Situacao");

                    b.Property<string>("Titulo");

                    b.Property<int>("UserId");

                    b.Property<decimal>("ValorParcela");

                    b.Property<decimal>("ValorTotal");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Dividas");
                });

            modelBuilder.Entity("CFD.Dominio.Renda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataRenda");

                    b.Property<string>("Descricao");

                    b.Property<int>("Tipo");

                    b.Property<string>("Titulo");

                    b.Property<int>("UserId");

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Rendas");
                });

            modelBuilder.Entity("CFD.Dominio.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Nome");

                    b.Property<int>("Papel");

                    b.Property<string>("Senha");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CFD.Dominio.Divida", b =>
                {
                    b.HasOne("CFD.Dominio.User", "Usuario")
                        .WithMany("Dividas")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CFD.Dominio.Renda", b =>
                {
                    b.HasOne("CFD.Dominio.User")
                        .WithMany("Rendas")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
