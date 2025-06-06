﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SelectionModule.Infrastructure;

#nullable disable

namespace SelectionModule.Infrastructure.Migrations
{
    [DbContext(typeof(SelectionDbContext))]
    [Migration("20250519184933_update vacancy")]
    partial class updatevacancy
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SelectionModule.Domain.Entites.Candidate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("SelectionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SelectionId")
                        .IsUnique();

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("SelectionModule.Domain.Entites.Position", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("SelectionModule.Domain.Entites.Selection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DeadLine")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("SelectionStatus")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Selections");
                });

            modelBuilder.Entity("SelectionModule.Domain.Entites.Vacancy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PositionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.ToTable("Vacancies");
                });

            modelBuilder.Entity("SelectionModule.Domain.Entites.Candidate", b =>
                {
                    b.HasOne("SelectionModule.Domain.Entites.Selection", "Selection")
                        .WithOne("Candidate")
                        .HasForeignKey("SelectionModule.Domain.Entites.Candidate", "SelectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Selection");
                });

            modelBuilder.Entity("SelectionModule.Domain.Entites.Vacancy", b =>
                {
                    b.HasOne("SelectionModule.Domain.Entites.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");
                });

            modelBuilder.Entity("SelectionModule.Domain.Entites.Selection", b =>
                {
                    b.Navigation("Candidate")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
