using BattAPI.Domain.Entities.Files;
using BattAPI.Domain.Repositories;
using System.Linq.Expressions;

namespace BattAPI.Infra.Data.Repositories.Files
{
    public class NoteAttachmentRepository(AppDbContext context) : Repository<NoteAttachmentMeta>(context), INoteAttachmentRepository
    {
        protected override Expression<Func<NoteAttachmentMeta, object?>>[] Includes => [img => img.Note];
    }
}
