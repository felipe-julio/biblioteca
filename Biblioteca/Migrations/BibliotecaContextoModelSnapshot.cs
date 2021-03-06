﻿// <auto-generated />
using System;
using Biblioteca.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Biblioteca.Migrations
{
    [DbContext(typeof(BibliotecaContexto))]
    partial class BibliotecaContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Biblioteca.Models.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("cd_cliente");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnName("cpf_cliente");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("nm_cliente");

                    b.HasKey("Id");

                    b.ToTable("cliente","dbo");
                });

            modelBuilder.Entity("Biblioteca.Models.Emprestimo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("cd_emprestimo");

                    b.Property<DateTime>("DataDeDevolucao")
                        .HasColumnName("data_dev_emprestimo");

                    b.Property<DateTime>("DataDeInicio")
                        .HasColumnName("data_in_emprestimo");

                    b.Property<Guid>("IdCliente")
                        .HasColumnName("cd_cliente");

                    b.Property<Guid>("IdLivro")
                        .HasColumnName("cd_livro");

                    b.HasKey("Id");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdLivro");

                    b.ToTable("emprestimo","dbo");
                });

            modelBuilder.Entity("Biblioteca.Models.Livro", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("cd_livro");

                    b.Property<string>("Autor")
                        .IsRequired()
                        .HasColumnName("at_livro");

                    b.Property<string>("Editora")
                        .IsRequired()
                        .HasColumnName("ed_livro");

                    b.Property<char>("Situacao")
                        .HasColumnName("st_livro");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("ti_livro");

                    b.HasKey("Id");

                    b.ToTable("livro","dbo");
                });

            modelBuilder.Entity("Biblioteca.Models.Emprestimo", b =>
                {
                    b.HasOne("Biblioteca.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Biblioteca.Models.Livro", "Livro")
                        .WithMany()
                        .HasForeignKey("IdLivro")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
