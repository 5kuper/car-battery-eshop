using BattAPI.Domain.Entities;
using BattAPI.Domain.Repositories;
using System.Linq.Expressions;

namespace BattAPI.Infra.Data.Repositories
{
    public class NoteRepository(AppDbContext context) : Repository<Note>(context), INoteRepository
    {
        protected override Expression<Func<Note, object?>>[] Includes => [b => b.Attachments];
    }
}
