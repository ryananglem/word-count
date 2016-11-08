using System.ComponentModel;
using System.Text;

namespace wordcount.services
{
    public static class StringExtension
    {
        public static string StripExtraCharacters(this string s)
        {
            var sb = new StringBuilder();
            foreach (char c in s)
            {
                if (!char.IsPunctuation(c) && !char.IsControl(c))
                    sb.Append(c);
            }
            return sb.ToString();
        }
    }
}
