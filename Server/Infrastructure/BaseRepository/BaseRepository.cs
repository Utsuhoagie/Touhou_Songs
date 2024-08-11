using Touhou_Songs.Data;

namespace Touhou_Songs.Infrastructure.BaseRepository;

public abstract class BaseRepository
{
	protected readonly AppDbContext _context;

	public BaseRepository(AppDbContext context) => _context = context;
}
