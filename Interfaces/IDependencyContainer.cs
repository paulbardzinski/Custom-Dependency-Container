namespace App1.Interfaces
{
    public interface IDependencyContainer : IDisposable
    {
        public void Register<TInterface, TImplementation>()
            where TInterface: class
            where TImplementation: class, TInterface;
        public TService Resolve<TService>();
        public object Resolve(Type type);
    }
}
