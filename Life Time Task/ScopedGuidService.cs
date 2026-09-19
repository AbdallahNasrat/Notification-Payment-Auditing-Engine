
namespace DotNet_DependencyInjection_Lab
{
    public class ScopedGuidService : IGuidService
    {
        public Guid Value { get; } = Guid.NewGuid();

    }
}
