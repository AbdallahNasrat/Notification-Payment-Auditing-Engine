namespace DotNet_DependencyInjection_Lab
{
    public class AuditService
    {
        public readonly IGuidService _singletonGuid;
        public readonly IGuidService _scopedGuid;
        public readonly IGuidService _transientGuid;

        public AuditService(IGuidService singletonGuid , IGuidService scopedGuid , IGuidService transientGuid )
        {
            _singletonGuid = singletonGuid;
            _scopedGuid = scopedGuid;
            _transientGuid = transientGuid;           
        }
    }
}
