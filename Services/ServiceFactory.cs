namespace CinefiloWASM.Services
{
    public static class ServiceFactory
    {
        public static IService GetService()
        {
            // if (ENV.isDevelopment)
            // {
            //     return MockServices();
            // }

            return new ResourceService();
        }
    }
}