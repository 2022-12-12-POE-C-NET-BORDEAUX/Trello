using System;
using System.Collections.Generic;

namespace TPtrello.Modeles;

public partial class Liste
{
	public int Id { get; set; }

	public string Nom { get; set; } = null!;

	public int ProjetId { get; set; }

	public virtual List<Carte> Cartes { get; } = new List<Carte>();

	public virtual Projet ProjetIdNavigation { get; set; } = null!;

	public Liste()
	{
	}
	public Liste(string nom, Projet Projet)
	{
		Nom = nom;
		ProjetId = Projet.Id;
		Projet.Listes.Add(this);
	}
}
