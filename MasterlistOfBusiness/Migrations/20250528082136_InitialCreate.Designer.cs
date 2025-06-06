﻿// <auto-generated />
using System;
using MasterlistOfBusiness.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MasterlistOfBusiness.Migrations
{
    [DbContext(typeof(MOBContext))]
    [Migration("20250528082136_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("MasterlistOfBusiness.Models.Inwentarz", b =>
                {
                    b.Property<int>("id_inwentarza")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Kontoid_konta")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Produktid_produktu")
                        .HasColumnType("INTEGER");

                    b.Property<int>("cena")
                        .HasColumnType("INTEGER");

                    b.Property<int>("id_konta")
                        .HasColumnType("INTEGER");

                    b.Property<int>("id_produktu")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ilosc")
                        .HasColumnType("INTEGER");

                    b.HasKey("id_inwentarza");

                    b.HasIndex("Kontoid_konta");

                    b.HasIndex("Produktid_produktu");

                    b.ToTable("Inwentarz");
                });

            modelBuilder.Entity("MasterlistOfBusiness.Models.Konto", b =>
                {
                    b.Property<int>("id_konta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NazwaUzytkownika")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Platforma")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("Sprzedawcaid_sprzedawcy")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_sprzedawcy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("link")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.HasKey("id_konta");

                    b.HasIndex("Sprzedawcaid_sprzedawcy");

                    b.ToTable("Konto");
                });

            modelBuilder.Entity("MasterlistOfBusiness.Models.Produkt", b =>
                {
                    b.Property<int>("id_produktu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("opis")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("typ")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("id_produktu");

                    b.ToTable("Produkt");
                });

            modelBuilder.Entity("MasterlistOfBusiness.Models.Sprzedawca", b =>
                {
                    b.Property<int>("id_sprzedawcy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Uzytkowniklogin")
                        .HasColumnType("TEXT");

                    b.Property<string>("login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id_sprzedawcy");

                    b.HasIndex("Uzytkowniklogin");

                    b.ToTable("Sprzedawca");
                });

            modelBuilder.Entity("MasterlistOfBusiness.Models.Transakcja", b =>
                {
                    b.Property<int>("id_transakcji")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Inwentarzid_inwentarza")
                        .HasColumnType("INTEGER");

                    b.Property<int>("id_konta")
                        .HasColumnType("INTEGER");

                    b.Property<int>("id_produktu")
                        .HasColumnType("INTEGER");

                    b.HasKey("id_transakcji");

                    b.HasIndex("Inwentarzid_inwentarza");

                    b.ToTable("Transakcja");
                });

            modelBuilder.Entity("MasterlistOfBusiness.Models.Uzytkownik", b =>
                {
                    b.Property<string>("login")
                        .HasColumnType("TEXT");

                    b.Property<string>("haslo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("typ")
                        .HasColumnType("TEXT");

                    b.HasKey("login");

                    b.ToTable("Uzytkownik");
                });

            modelBuilder.Entity("MasterlistOfBusiness.Models.Inwentarz", b =>
                {
                    b.HasOne("MasterlistOfBusiness.Models.Konto", "Konto")
                        .WithMany("Inwentarze")
                        .HasForeignKey("Kontoid_konta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MasterlistOfBusiness.Models.Produkt", "Produkt")
                        .WithMany("Inwentarze")
                        .HasForeignKey("Produktid_produktu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Konto");

                    b.Navigation("Produkt");
                });

            modelBuilder.Entity("MasterlistOfBusiness.Models.Konto", b =>
                {
                    b.HasOne("MasterlistOfBusiness.Models.Sprzedawca", "Sprzedawca")
                        .WithMany("Konta")
                        .HasForeignKey("Sprzedawcaid_sprzedawcy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sprzedawca");
                });

            modelBuilder.Entity("MasterlistOfBusiness.Models.Sprzedawca", b =>
                {
                    b.HasOne("MasterlistOfBusiness.Models.Uzytkownik", "Uzytkownik")
                        .WithMany("Sprzedawca")
                        .HasForeignKey("Uzytkowniklogin");

                    b.Navigation("Uzytkownik");
                });

            modelBuilder.Entity("MasterlistOfBusiness.Models.Transakcja", b =>
                {
                    b.HasOne("MasterlistOfBusiness.Models.Inwentarz", "Inwentarz")
                        .WithMany()
                        .HasForeignKey("Inwentarzid_inwentarza");

                    b.Navigation("Inwentarz");
                });

            modelBuilder.Entity("MasterlistOfBusiness.Models.Konto", b =>
                {
                    b.Navigation("Inwentarze");
                });

            modelBuilder.Entity("MasterlistOfBusiness.Models.Produkt", b =>
                {
                    b.Navigation("Inwentarze");
                });

            modelBuilder.Entity("MasterlistOfBusiness.Models.Sprzedawca", b =>
                {
                    b.Navigation("Konta");
                });

            modelBuilder.Entity("MasterlistOfBusiness.Models.Uzytkownik", b =>
                {
                    b.Navigation("Sprzedawca");
                });
#pragma warning restore 612, 618
        }
    }
}
