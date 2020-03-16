using Microsoft.EntityFrameworkCore;
using StoryService.Models.Dtos;
using System;

namespace StoryService.Data
{
    public class StoryServiceDb : DbContext
    {
        public DbSet<BoardDto> Boards { get; set; }
        public DbSet<AgileItemDto> AgileItems { get; set; }

        public StoryServiceDb(DbContextOptions<StoryServiceDb> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BoardDto>(b =>
            {
                b.Property(p => p.Id).IsRequired();
                b.Property(p => p.BoardName).IsRequired();
            });

            modelBuilder.Entity<AgileItemDto>(s =>
            {
                s.Property(p => p.Id).IsRequired();
                s.Property(p => p.ParentId).HasDefaultValue(Guid.Empty);
                s.Property(p => p.Title).IsRequired();
                s.Property(p => p.Description).IsRequired();
                s.Property(p => p.CreatedOn).IsRequired();
                s.Property(p => p.CreatedBy).IsRequired();
                s.Property(p => p.DueBy).IsRequired();
                s.Property(p => p.Priority).IsRequired();
                s.Property(p => p.Status).IsRequired();
                s.Property(p => p.IsComplete).IsRequired();
                s.Property(p => p.IsActive).IsRequired();
                s.Property(p => p.BoardId).IsRequired();
                //todo: add more
            });

        }

    }
}
