using Microsoft.AspNetCore.Mvc;
using TPtrello.Modeles;

public class ConnexionController : Controller
{
	public static Utilisateur? user = null;
	public ConnexionController()
	{
	}

	public IActionResult Connexion()
	{
		return View();
	}

	[HttpPost]
	public IActionResult Connexion(string email, string mdp)
	{
		TrelloContext _context = new TrelloContext();
		Utilisateur tmp = _context.Utilisateurs.FirstOrDefault(x => x.Email == email && x.MotDePasse == mdp)!;
		if (tmp != null)
		{
			user = tmp;
			Console.WriteLine("Connexion réussie");
			return RedirectToAction("Index", "Home");
		}
		else
		{
			Console.WriteLine("Connexion echouée");
			Console.WriteLine("Email: " + email);
			Console.WriteLine("Mdp: " + mdp);
			return RedirectToAction("Connexion", "Connexion");
		}
	}

	public IActionResult Deconnexion()
	{
		user = null;
		return RedirectToAction("Connexion", "Connexion");
	}

	[HttpPost]
	public IActionResult Inscription(string nom, string email, string mdp)
	{
		TrelloContext _context = new TrelloContext();
		Utilisateur tmp = _context.Utilisateurs.FirstOrDefault(x => x.Email == email);
		if (tmp == null)
		{
			Utilisateur user = new Utilisateur(nom, email, mdp);
			_context.Utilisateurs.Add(user);
			_context.SaveChanges();

			return RedirectToAction("Connexion", "Connexion");
		}
		else
		{
			return RedirectToAction("Connexion", "Connexion");
		}
	}
}
