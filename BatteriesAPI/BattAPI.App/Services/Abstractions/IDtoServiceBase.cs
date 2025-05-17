namespace BattAPI.App.Services.Abstractions
{
    public interface IDtoServiceBase<TInput, TOutput, TPatch>
    {
        Task<IList<TOutput>> ListAsync();
        Task<TOutput?> GetAsync(Guid id);

        Task<Guid> CreateAsync(TInput input);
        Task UpdateAsync(Guid id, TPatch patch);
        Task DeleteAsync(Guid id);
    }
}
