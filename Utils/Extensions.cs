using CinefiloWASM.Consts;
using System.Linq;

namespace CinefiloWASM.Utils
{

    public static class StringExtensions
    {
        public static string BuildImageURI(this string posterPath, string size = "200")
        {
            if (string.IsNullOrEmpty(posterPath))
            {
                return ResourceConsts.IMG_URL_NOT_SET;
            }
            return string.Format(ResourceConsts.IMG_URL, posterPath, size);
        }

        public static string BuildAbreviatedDescription(this string movieDescription, int length=15)
        {
            if (string.IsNullOrEmpty(movieDescription))
            {
                return string.Empty;
            }

            var words = movieDescription.Split(' ').Take(length);
            var abreviatedOverView = string.Join(" ", words);
            abreviatedOverView = string.Concat(abreviatedOverView, "...");

            return abreviatedOverView;
        }
    }
}