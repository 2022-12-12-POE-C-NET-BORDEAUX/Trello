using Microsoft.EntityFrameworkCore;

namespace TPtrello.Modeles;

public class Repository<T> where T : class
{
	private TrelloContext _context = new TrelloContext();

	private DbSet<T> _dbSet;

	public Repository()
	{
		_dbSet = _context.Set<T>();
	}

	public List<T> GetAll()
	{
		return _dbSet.ToList();
	}

	public T? GetById(int id)
	{
		return _dbSet.Find(id);
	}

	public void Add(T entity)
	{
		_dbSet.Add(entity);
		_context.SaveChanges();
	}

	public void Update(T entity)
	{
		_context.Entry(entity).State = EntityState.Modified;
		_context.SaveChanges();
	}

	public void Delete(T entity)
	{
		_dbSet.Remove(entity);
		_context.SaveChanges();
	}

	public void DeleteById(int id)
	{
		T? entity = GetById(id);
		if (entity != null)
			Delete(entity);
	}
}
