using TPtrello.Modeles;

public class ProjetUtilisateur
{
	public int UtilisateurID { get; set; }
	public Utilisateur Utilisateur { get; set; }

	public int ProjetId { get; set; }
	public Projet Projet { get; set; }

	public ProjetUtilisateur()
	{
	}

	public ProjetUtilisateur(Utilisateur utilisateur, Projet projet)
	{
		Utilisateur = utilisateur;
		UtilisateurID = utilisateur.Id;
		Projet = projet;
		ProjetId = projet.Id;
	}
}
