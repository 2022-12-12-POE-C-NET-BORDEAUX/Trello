using System;
using System.Collections.Generic;

namespace TPtrello.Modeles;

public partial class Projet
{
	public int Id { get; set; }

	public string Nom { get; set; } = null!;

	public string? Description { get; set; }

	public DateTime DateCreation { get; set; }

	public virtual List<Liste> Listes { get; set; } = new List<Liste>();

	public virtual List<ProjetUtilisateur> ProjetUtilisateurs { get; set; } = new();

	public Projet()
	{
	}

	public Projet(string nom, Utilisateur utilisateur)
	{
		TrelloContext _context = new TrelloContext();
		ProjetUtilisateurs = _context.ProjetUtilisateurs.Where(x => x.ProjetId == Id).ToList();
		Nom = nom;
		Description = null;
		DateCreation = DateTime.Now;
	}

	public Projet(string nom)
	{
		TrelloContext _context = new TrelloContext();
		Nom = nom;
		Description = null;
		DateCreation = DateTime.Now;
	}
}
