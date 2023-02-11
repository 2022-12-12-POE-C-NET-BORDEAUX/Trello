using System;
using System.Collections.Generic;

namespace TPtrello.Modeles;

public partial class Carte
{
	public int Id { get; set; }

	public string Titre { get; set; } = null!;

	public string? Description { get; set; }

	public DateTime DateCreation { get; set; }

	public int IdListe { get; set; }

	public virtual List<Commentaire> Commentaires { get; } = new List<Commentaire>();

	public virtual List<Etiquette> Etiquettes { get; } = new List<Etiquette>();

	public virtual Liste IdListeNavigation { get; set; } = null!;

	public Carte()
	{
	}
	public Carte(string titre, Liste liste)
	{
		Titre = titre;
		Description = null;
		DateCreation = DateTime.Now;
		IdListe = liste.Id;
		liste.Cartes.Add(this);
	}

	public Carte(string titre, string Description, Liste liste)
	{
		Titre = titre;
		this.Description = Description;
		DateCreation = DateTime.Now;
		IdListe = liste.Id;
		liste.Cartes.Add(this);
	}
}
