﻿// <auto-generated />
using System;
using ManagementTalent.Infra.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ManagementTalent.Infra.Migrations
{
    [DbContext(typeof(MTDbContext))]
    [Migration("20240610205031_relations")]
    partial class relations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ManagementTalent.Domain.Entity.AvaliationContext.Assessment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("JobRoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("JobRoleId");

                    b.ToTable("Assessments");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.AvaliationContext.GroupParameter", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AssessmentId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("GroupParamTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Weight")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("AssessmentId");

                    b.ToTable("GroupParameters");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.AvaliationContext.GroupParameterJobParameter", b =>
                {
                    b.Property<string>("GroupParameterId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("JobParameterBaseId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("GroupParameterId", "JobParameterBaseId");

                    b.HasIndex("JobParameterBaseId");

                    b.ToTable("GroupParameterJobParameter");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.AvaliationContext.JobParameterBase", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Expected")
                        .HasColumnType("int");

                    b.Property<string>("JobParamTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Observation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Weight")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("JobParameterBases");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.AvaliationContext.JobParameterSeniority", b =>
                {
                    b.Property<string>("JobParametersBaseId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("SeniorityId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("JobParametersBaseId", "SeniorityId");

                    b.HasIndex("SeniorityId");

                    b.ToTable("JobParameterSeniority");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.AvaliationContext.Seniority", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<Guid>("JobRoleId")
                        .HasColumnType("char(36)");

                    b.Property<string>("SeniorityName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SeniorityRelevanceInWorkDay")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("JobRoleId");

                    b.ToTable("Senioritys");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.Colab", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("JobRoleId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("SeniorityId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SupervisorId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("JobRoleId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("SeniorityId");

                    b.HasIndex("SupervisorId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.JobRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("JobRoles");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.ResultContext.AssessmentParamResult", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Expected")
                        .HasColumnType("int");

                    b.Property<string>("GroupParameterResultId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("JobParamTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Observation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("RealityResult")
                        .HasColumnType("int");

                    b.Property<double?>("Weight")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("GroupParameterResultId");

                    b.ToTable("AssessmentParamResults");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.ResultContext.AssessmentResult", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ActualJobName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ActualSeniorityName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ActualSupervisorName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CollaboratorId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("NextAssessment")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("Result")
                        .HasColumnType("int");

                    b.Property<string>("SupervisorId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CollaboratorId");

                    b.ToTable("AssessmentResults");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.ResultContext.GroupParameterResult", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AssessmentResultId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AssessmentTamplateId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("GroupParamTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Weight")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("AssessmentResultId");

                    b.ToTable("GroupParameterResults");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.Supervisor", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("SinceAtInJob")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("SupFatherId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Supervisors");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.AvaliationContext.Assessment", b =>
                {
                    b.HasOne("ManagementTalent.Domain.Entity.JobRole", "JobRole")
                        .WithMany()
                        .HasForeignKey("JobRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobRole");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.AvaliationContext.GroupParameter", b =>
                {
                    b.HasOne("ManagementTalent.Domain.Entity.AvaliationContext.Assessment", null)
                        .WithMany("GroupParameters")
                        .HasForeignKey("AssessmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.AvaliationContext.GroupParameterJobParameter", b =>
                {
                    b.HasOne("ManagementTalent.Domain.Entity.AvaliationContext.GroupParameter", "GroupParameter")
                        .WithMany("GroupParameterJobParameters")
                        .HasForeignKey("GroupParameterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementTalent.Domain.Entity.AvaliationContext.JobParameterBase", "JobParameterBase")
                        .WithMany("GroupParameterJobParameters")
                        .HasForeignKey("JobParameterBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupParameter");

                    b.Navigation("JobParameterBase");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.AvaliationContext.JobParameterSeniority", b =>
                {
                    b.HasOne("ManagementTalent.Domain.Entity.AvaliationContext.JobParameterBase", "JobParameterBase")
                        .WithMany("JobParameterSeniorities")
                        .HasForeignKey("JobParametersBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementTalent.Domain.Entity.AvaliationContext.Seniority", "Seniority")
                        .WithMany("JobParameterSeniorities")
                        .HasForeignKey("SeniorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobParameterBase");

                    b.Navigation("Seniority");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.AvaliationContext.Seniority", b =>
                {
                    b.HasOne("ManagementTalent.Domain.Entity.JobRole", "JobRoleName")
                        .WithMany("Seniorities")
                        .HasForeignKey("JobRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobRoleName");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.Colab", b =>
                {
                    b.HasOne("ManagementTalent.Domain.Entity.JobRole", "JobRole")
                        .WithMany()
                        .HasForeignKey("JobRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementTalent.Domain.Entity.AvaliationContext.Seniority", "Seniority")
                        .WithMany()
                        .HasForeignKey("SeniorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementTalent.Domain.Entity.Supervisor", "Supervisor")
                        .WithMany("Colabs")
                        .HasForeignKey("SupervisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobRole");

                    b.Navigation("Seniority");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.ResultContext.AssessmentParamResult", b =>
                {
                    b.HasOne("ManagementTalent.Domain.Entity.ResultContext.GroupParameterResult", "GroupParameterResult")
                        .WithMany("AssessmentParam")
                        .HasForeignKey("GroupParameterResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupParameterResult");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.ResultContext.AssessmentResult", b =>
                {
                    b.HasOne("ManagementTalent.Domain.Entity.Colab", "Collaborator")
                        .WithMany()
                        .HasForeignKey("CollaboratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collaborator");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.ResultContext.GroupParameterResult", b =>
                {
                    b.HasOne("ManagementTalent.Domain.Entity.ResultContext.AssessmentResult", "AssessmentResult")
                        .WithMany("GroupParameterResults")
                        .HasForeignKey("AssessmentResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssessmentResult");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ManagementTalent.Domain.Entity.Colab", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ManagementTalent.Domain.Entity.Colab", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementTalent.Domain.Entity.Colab", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ManagementTalent.Domain.Entity.Colab", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.AvaliationContext.Assessment", b =>
                {
                    b.Navigation("GroupParameters");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.AvaliationContext.GroupParameter", b =>
                {
                    b.Navigation("GroupParameterJobParameters");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.AvaliationContext.JobParameterBase", b =>
                {
                    b.Navigation("GroupParameterJobParameters");

                    b.Navigation("JobParameterSeniorities");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.AvaliationContext.Seniority", b =>
                {
                    b.Navigation("JobParameterSeniorities");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.JobRole", b =>
                {
                    b.Navigation("Seniorities");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.ResultContext.AssessmentResult", b =>
                {
                    b.Navigation("GroupParameterResults");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.ResultContext.GroupParameterResult", b =>
                {
                    b.Navigation("AssessmentParam");
                });

            modelBuilder.Entity("ManagementTalent.Domain.Entity.Supervisor", b =>
                {
                    b.Navigation("Colabs");
                });
#pragma warning restore 612, 618
        }
    }
}