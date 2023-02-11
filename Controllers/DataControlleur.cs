using Microsoft.AspNetCore.Mvc;
using TPtrello.Modeles;


[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
	private static TrelloContext _context = new TrelloContext();

	[HttpGet]
	public List<Utilisateur> Get()
	{
		List<Utilisateur> Users = _context.Utilisateurs.ToList();
		return Users;
	}
}


[ApiController]
[Route("/api/[controller]")]
public class ApiProjetController : ControllerBase
{
	private static TrelloContext _context = new TrelloContext();

	[HttpPost]
	public void Post([FromBody] Projet tmp, int id)
	{
		Utilisateur tmpusr = _context.Utilisateurs.First(x => x.Id == id);
		ProjetUtilisateur liens = new ProjetUtilisateur(tmpusr, tmp);
		_context.Add(liens);
		_context.SaveChanges();
	}

	[HttpGet]
	public List<Projet> Get()
	{
		List<Projet> Users = _context.Projets.ToList();
		return Users;
	}
}
