using DoneThatMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoneThatMVC
{
    public class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=XARIS-PC\SQLEXPRESS;Database=DoneThat;MultipleActiveResultSets=true;Integrated security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .HasMany(u => u.requests);

            modelBuilder.Entity<User>()
                .HasMany(u => u.acceptances);

            modelBuilder.Entity<User>()
                .HasMany(u => u.messagesReceived);

            modelBuilder.Entity<User>()
                .HasMany(u => u.tasks);

            modelBuilder.Entity<Models.Task>()
                .HasOne(u => u.User)
                .WithMany(t => t.tasks).HasForeignKey(i => i.UserId);

            modelBuilder.Entity<Models.Task>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Models.Task>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Friendship>()
                .HasKey(c => new { c.AcceptRequestId, c.MakeRequestId });

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.MakeRequestUser)
                .WithMany(i => i.requests)
                .HasForeignKey(c => c.MakeRequestId);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.AcceptRequestUser)
                .WithMany(i => i.acceptances)
                .HasForeignKey(c => c.AcceptRequestId);

            modelBuilder.Entity<Message>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Message>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Message>()
                .HasOne(u => u.Receiver)
                .WithMany(i => i.messagesReceived)
                .HasForeignKey(r => r.ReceiverId);

            modelBuilder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(i => i.messagesSent)
                .HasForeignKey(s => s.SenderId);


        }
    }
}
