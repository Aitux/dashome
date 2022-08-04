using Dashome.Core.UnitOfWork;
using Extensions.Hosting.AsyncInitialization;

namespace Dashome.Infrastructure.Initializers;

public class DbInitializer : IAsyncInitializer
{
    private readonly IUnitOfWork _unitOfWork;

    public DbInitializer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task InitializeAsync()
    {
        await _unitOfWork.EnsureInitializedAsync();
    }
}