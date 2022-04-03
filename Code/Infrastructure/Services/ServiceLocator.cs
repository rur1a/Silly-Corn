namespace Code.Infrastructure.Services
{
    public class ServiceLocator
    {
        private static ServiceLocator s_instance;
        public static ServiceLocator Container => s_instance ??= new ServiceLocator();

        public void RegisterSingle<TService>(TService implementation) where TService : IService => 
            Implementation<TService>.ServiceInstance = implementation;

        public TService Single<TService>() where TService : IService => 
            Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}