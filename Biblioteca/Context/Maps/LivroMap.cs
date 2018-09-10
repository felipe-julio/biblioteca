using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Context.Maps
{
    public class LivroMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("livro", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(f => f.Id)
                .HasColumnName("cd_livro");

            builder.Property(f => f.Titulo)
                .HasColumnName("ti_livro");

            builder.Property(f => f.Autor)
                .HasColumnName("at_livro");

            builder.Property(f => f.Situacao)
                .HasColumnName("st_livro");

            builder.Property(f => f.Editora)
                .HasColumnName("ed_livro");

        }
    }
}
