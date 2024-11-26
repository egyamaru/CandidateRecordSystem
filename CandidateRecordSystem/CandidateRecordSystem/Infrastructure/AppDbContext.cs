﻿using CandidateRecordSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CandidateRecordSystem.Infrastructure
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Candidate> Candidate { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>()
                .HasKey(c => c.CandidateId);

            modelBuilder.Entity<Candidate>()
                .Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(15);

            modelBuilder.Entity<Candidate>()
                .Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(15); ;

            modelBuilder.Entity<Candidate>()
                .Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(50); ;

            modelBuilder.Entity<Candidate>()
                .Property(c => c.PreferredTimeToCall)
                .HasColumnType("TIME");

            modelBuilder.Entity<Candidate>()
                .Property(c => c.Comments)
                .IsRequired();

            //create index email for efficient lookup by email
            //assumption: first name and last name are not unique, searches are more likely with emails which are unique
            modelBuilder.Entity<Candidate>()
                .HasIndex(c => c.Email)
                .IsUnique();
        }
    }
}