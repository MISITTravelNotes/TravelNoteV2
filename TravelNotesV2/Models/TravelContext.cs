using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TravelNotesV2.Models;

public partial class TravelContext : DbContext
{
    public TravelContext(DbContextOptions<TravelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<model_list> model_list { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<model_list>(entity =>
        {
            entity.HasKey(e => e.modelId);

            entity.Property(e => e.modelName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
