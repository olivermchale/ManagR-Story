using Microsoft.EntityFrameworkCore;
using StoryService.Models.Dtos;
using System;

namespace StoryService.Data
{
    public class StoryServiceDb : DbContext
    {
        public DbSet<SuperStoryDto> SuperStories { get; set; }
        public DbSet<StoryDto> Stories { get; set; }
        public DbSet<TaskDto> Tasks { get; set; }

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

            modelBuilder.Entity<SuperStoryDto>(s =>
            {
                s.Property(p => p.Id).IsRequired();
                s.Property(p => p.Title).IsRequired();
                s.Property(p => p.Description).IsRequired();
                s.Property(p => p.CreatedOn).IsRequired();
                s.Property(p => p.CreatedBy).IsRequired();
                s.Property(p => p.DueBy).IsRequired();
                s.Property(p => p.Prority).IsRequired();
                s.Property(p => p.Status).IsRequired();
                s.Property(p => p.IsComplete).IsRequired();
                s.Property(p => p.IsActive).IsRequired();
                s.Property(p => p.BoardId).IsRequired();
            });

            modelBuilder.Entity<StoryDto>(s =>
            {
                s.Property(p => p.Id).IsRequired();
                s.Property(p => p.Title).IsRequired();
                s.Property(p => p.Description).IsRequired();
                s.Property(p => p.CreatedOn).IsRequired();
                s.Property(p => p.CreatedBy).IsRequired();
                s.Property(p => p.DueBy).IsRequired();
                s.Property(p => p.Prority).IsRequired();
                s.Property(p => p.Status).IsRequired();
                s.Property(p => p.IsComplete).IsRequired();
                s.Property(p => p.StoryPoints).IsRequired();
                s.Property(p => p.IsActive).IsRequired();
                s.Property(p => p.BoardId).IsRequired();
            });

            modelBuilder.Entity<TaskDto>(s =>
            {
                s.Property(p => p.Id).IsRequired();
                s.Property(p => p.Title).IsRequired();
                s.Property(p => p.Description).IsRequired();
                s.Property(p => p.CreatedOn).IsRequired();
                s.Property(p => p.CreatedBy).IsRequired();
                s.Property(p => p.DueBy).IsRequired();
                s.Property(p => p.Prority).IsRequired();
                s.Property(p => p.Status).IsRequired();
                s.Property(p => p.IsComplete).IsRequired();
                s.Property(p => p.IsActive).IsRequired();
                s.Property(p => p.BoardId).IsRequired();
            });
        }

    }
}
