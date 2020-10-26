
using CinefiloWASM.Services;

namespace CinefiloWASM.Singleton
{

    public class Global
    {
        private static Global _instance;

        public static Global Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Global();
                }
                return _instance;
            }
        }

        static IService _baseService;
        public IService BaseService
        {
            get
            {
                if (_baseService == null)
                {
                    _baseService = ServiceFactory.GetService();
                }
                return _baseService;
            }
        }
    }
}