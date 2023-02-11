using System;
using System.Collections.Generic;

namespace TPtrello.Modeles;

public partial class Commentaire
{
	public int Id { get; set; }

	public string Contenu { get; set; } = null!;

	public DateTime DateCreation { get; set; }

	public int UtilisateurID { get; set; }

	public int IdCarte { get; set; }

	public virtual Carte IdCarteNavigation { get; set; } = null!;

	public virtual Utilisateur UtilisateurIDNavigation { get; set; } = null!;
}
