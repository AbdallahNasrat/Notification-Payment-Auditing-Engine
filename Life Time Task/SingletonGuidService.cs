
namespace DotNet_DependencyInjection_Lab
{
    public class SingletonGuidService : IGuidService
    {
        public Guid Value { get; } = Guid.NewGuid();
    }
}
