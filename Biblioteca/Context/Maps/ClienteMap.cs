using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Context.Maps
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("cliente", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(f => f.Id)
                .HasColumnName("cd_cliente");

            builder.Property(f => f.Nome)
                .HasColumnName("nm_cliente").IsRequired();

            builder.Property(f => f.Cpf)
                .HasColumnName("cpf_cliente").IsRequired();

        }
    }
}
