
namespace DotNet_DependencyInjection_Lab
{
    public class TransientGuidService : IGuidService
    {
        public Guid Value { get; } = Guid.NewGuid();
    }
}
