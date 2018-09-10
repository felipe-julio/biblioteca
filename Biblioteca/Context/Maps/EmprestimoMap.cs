using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Context.Maps
{
    public class EmprestimoMap : IEntityTypeConfiguration<Emprestimo>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Emprestimo> builder)
        {
            builder.ToTable("emprestimo", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(f => f.Id)
                .HasColumnName("cd_emprestimo");

            builder.Property(f => f.DataDeDevolucao)
                .HasColumnName("data_dev_emprestimo");

            builder.Property(f => f.DataDeInicio)
               .HasColumnName("data_in_emprestimo");

            builder.Property(f => f.IdCliente)
                .HasColumnName("cd_cliente");

            builder.HasOne(s => s.Cliente)
                .WithMany()
                .HasForeignKey(e => e.IdCliente)
                .IsRequired();

            builder.Property(f => f.IdLivro)
                .HasColumnName("cd_livro");

            builder.HasOne(s => s.Livro)
                .WithMany()
                .HasForeignKey(e => e.IdLivro)
                .IsRequired();

        }
    }
}
