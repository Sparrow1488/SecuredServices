namespace SecuredServices.Core.Protectors.Processors
{
    public class ProcessorContext<TEntity>
    {
        public TEntity ToProcessEntity { get; set; }
        public TEntity InitialEntity { get; set; }
        public ISessionManager ManagerSession { get; set; }
    }
}
