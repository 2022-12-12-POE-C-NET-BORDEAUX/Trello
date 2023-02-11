using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TPtrello.Modeles;

public partial class TrelloContext : DbContext
{
	public TrelloContext()
	{
		Database.EnsureCreated();
	}

	public TrelloContext(DbContextOptions<TrelloContext> options)
		: base(options)
	{
	}

	public virtual DbSet<Carte> Cartes { get; set; }

	public virtual DbSet<Commentaire> Commentaires { get; set; }

	public virtual DbSet<Etiquette> Etiquettes { get; set; }

	public virtual DbSet<Liste> Listes { get; set; }

	public virtual DbSet<Projet> Projets { get; set; }

	public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

	public virtual DbSet<ProjetUtilisateur> ProjetUtilisateurs { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=trello;User=root;Password=0000;");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Carte>(entity =>
		{
			entity.HasOne(d => d.IdListeNavigation).WithMany(p => p.Cartes)
				.HasForeignKey(d => d.IdListe);
		});

		modelBuilder.Entity<Commentaire>(entity =>
		{
			entity.HasOne(d => d.IdCarteNavigation).WithMany(p => p.Commentaires)
				.HasForeignKey(d => d.IdCarte);

			entity.HasOne(d => d.UtilisateurIDNavigation).WithMany(p => p.Commentaires)
				.HasForeignKey(d => d.UtilisateurID);
		});

		modelBuilder.Entity<Etiquette>(entity =>
		{
			entity.HasOne(d => d.IdCarteNavigation).WithMany(p => p.Etiquettes)
				.HasForeignKey(d => d.IdCarte);
		});

		modelBuilder.Entity<Liste>(entity =>
		{
			entity.HasOne(d => d.ProjetIdNavigation).WithMany(p => p.Listes)
				.HasForeignKey(d => d.ProjetId);
		});

		modelBuilder.Entity<ProjetUtilisateur>(entity =>
		{
			entity.HasKey(e => new { e.UtilisateurID, e.ProjetId })
				.HasName("PRIMARY");

			entity.ToTable("utilisateur_Projet");

			entity.HasIndex(e => e.ProjetId, "id_Projet");

			entity.HasOne(d => d.Utilisateur).WithMany(p => p.ProjetUtilisateurs)
				.HasForeignKey(d => d.UtilisateurID).OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(d => d.Projet).WithMany(p => p.ProjetUtilisateurs)
				.HasForeignKey(d => d.ProjetId).OnDelete(DeleteBehavior.Restrict);
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
