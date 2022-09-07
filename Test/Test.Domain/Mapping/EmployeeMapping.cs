using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.Domain.Mapping
{
    public class EmployeeMapping : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(h=> h.Id);
            builder.Property(p=> p.Name).IsRequired().HasMaxLength(20);
            builder.Property(p=> p.LastName).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.Phone).IsRequired();
            //builder.Property(p => p.Photo).IsRequired();
            builder.Property(p => p.HiringDate).IsRequired();
        }
    }
}
