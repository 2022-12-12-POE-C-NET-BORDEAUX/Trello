using System;
using System.Collections.Generic;

namespace TPtrello.Modeles;

public partial class Utilisateur
{
	public int Id { get; set; }

	public string Nom { get; set; } = null!;

	public string Email { get; set; } = null!;

	public string MotDePasse { get; set; } = null!;

	public DateTime DateInscription { get; set; }

	public virtual List<Commentaire> Commentaires { get; } = new List<Commentaire>();
	public virtual List<ProjetUtilisateur> ProjetUtilisateurs { get; set; } = new();

	public Utilisateur(string nom, string email, string motDePasse)
	{
		Nom = nom;
		Email = email;
		MotDePasse = motDePasse;
		DateInscription = DateTime.Now;
	}
}
