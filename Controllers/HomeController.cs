using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPtrello.Modeles;

namespace TPtrello.Controllers;

public class HomeController : Controller
{
	public static TrelloContext _context = new TrelloContext();
	public static Projet? projet;

	public IActionResult Index(int id = 1)
	{
		if (ConnexionController.user != null)
		{
			var proj = _context.Projets.Include(projet => projet.Listes)
			.ThenInclude(listes => listes.Cartes)
			.ThenInclude(cartes => cartes.Commentaires)
			.Where(projets => projets.ProjetUtilisateurs
			.Any(pu => pu.UtilisateurID == ConnexionController.user!.Id))
			.Where(x => x.Id == id)
			.FirstOrDefault();

			if (proj == null)
				return RedirectToAction("AjouterProjet", "Home");
			projet = proj;
			return View(proj);
		}
		else
		{
			return RedirectToAction("Connexion", "Connexion");
		}
	}

	[Route("/projets/add")]
	public IActionResult AjouterProjet()
	{
		return View();
	}

	[HttpPost]
	[Route("/projets/add")]
	public IActionResult AjouterProjet(Projet projet)
	{
		_context.Projets.Add(projet);
		_context.SaveChanges();
		Console.WriteLine(projet.Id);
		_context.ProjetUtilisateurs.Add(new ProjetUtilisateur { ProjetId = projet.Id, UtilisateurID = ConnexionController.user!.Id });
		_context.SaveChanges();
		ViewBag.Projet = projet;
		return RedirectToAction("Index", new { id = projet.Id });
	}

	[Route("/listes/add/")]
	public IActionResult AjouterListe()
	{
		return View();
	}

	[HttpPost]
	[Route("/listes/add/")]
	public IActionResult AjouterListe(string Nom)
	{
		_context.Listes.Add(new Liste(Nom, projet!));
		_context.SaveChanges();
		return RedirectToAction("Index", new { id = projet!.Id });
	}

	[HttpGet]
	[Route("/cartes/add/{id}")]
	public IActionResult AjouterCarte(int id)
	{
		return View(new Carte());
	}

	[HttpPost]
	[Route("/cartes/add/{id}")]
	public IActionResult AjouterCarte(Carte carte, int id)
	{
		carte.Id = 0;
		carte.IdListeNavigation = _context.Listes.Find(id)!;
		_context.Cartes.Add(carte);
		_context.SaveChanges();
		return RedirectToAction("Index", new { id = projet!.Id });
	}

	[HttpGet]
	[Route("/cartes/supp/{id}")]
	public IActionResult SupprimerCarte(int id)
	{
		var carte = _context.Cartes.Find(id);
		_context.Cartes.Remove(carte!);
		_context.SaveChanges();
		return RedirectToAction("Index", new { id = projet!.Id });
	}

	[HttpGet]
	[Route("/listes/supp/{id}")]
	public IActionResult SupprimerListe(int id)
	{
		var liste = _context.Listes.Find(id);
		_context.Listes.Remove(liste!);
		_context.SaveChanges();
		return RedirectToAction("Index", new { id = projet!.Id });
	}

	[HttpGet]
	[Route("/cartes/edit/{id}")]
	public IActionResult EditerCarte(int id)
	{
		var carte = _context.Cartes.Find(id)!;
		return View(carte);
	}

	[HttpPost]
	[Route("/cartes/edit/{id}")]
	public IActionResult EditerCarte(Carte carte, int id)
	{
		Console.WriteLine("carte =" + carte);
		Console.WriteLine("titre =" + carte.Titre);
		var carteBdd = _context.Cartes.Find(id)!;
		carteBdd.Titre = carte.Titre;
		carteBdd.Description = carte.Description;
		_context.SaveChanges();
		return RedirectToAction("Index", new { id = projet!.Id });
	}
}
