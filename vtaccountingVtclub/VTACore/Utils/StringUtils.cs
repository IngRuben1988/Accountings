using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using static VTAworldpass.VTACore.Cores.Globales.Enumerables;

namespace VTAworldpass.Web.VTACore.Utils
{
    public class StringUtils
    {
        private static readonly Char[] ReplacementChars = new[] { 'á', 'Á', 'é', 'É', 'í', 'Í', 'ó', 'Ó', 'ú', 'Ú', 'ü', 'ñ', 'Ñ', ' ', '[', ']', '(', ')', };
        private static readonly Dictionary<Char, Char> ReplacementMappings = new Dictionary<Char, Char>
        {  { 'á', 'a'}, { 'Á', 'A'},  { 'é', 'e'}, { 'É', 'E'}, { 'í', 'i'}, { 'Í', 'I'}, { 'ó', 'o'}, { 'Ó', 'O'}, { 'ú', 'u'}, { 'Ú', 'U'}, { 'ü', 'u'}, { 'ñ', 'n'}, { 'Ñ', 'n'}, { ' ', '-'}, { '[', '-'}, { ']', '-'}, { '(', '-'}, { ')', '-'}
        };

        public static string RemoveSpecialCharacters(string source)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in source)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string ReplaceSpecialCharacters(string source)
        {
            var startIndex = 0;
            var currentIndex = 0;
            var result = new StringBuilder(source.Length);

            while ((currentIndex = source.IndexOfAny(ReplacementChars, startIndex)) != -1)
            {
                result.Append(source.Substring(startIndex, currentIndex - startIndex));
                result.Append(ReplacementMappings[source[currentIndex]]);

                startIndex = currentIndex + 1;
            }

            if (startIndex == 0)
                return source;

            result.Append(source.Substring(startIndex));

            return result.ToString();
        }

        public static string CutLenghtString120Characters(string source)
        {

            var startIndex = 0;
            string result = "";
            result = source.Length >= (int)DescriptionVTA.MaxQuickDescriptionFileCharacters ? source.Substring(startIndex, (int)DescriptionVTA.MaxQuickDescriptionFileCharacters) : source;

            return result;
        }

        public static string padNumString(string in_str, int num_chars, char fill_char = '0', bool to_left = false)
        {
            in_str = in_str ?? "0";
            return (in_str.Length >= num_chars) ? in_str.Substring((in_str.Length - num_chars), num_chars) : ((to_left) ? in_str.PadRight(num_chars, fill_char) : in_str.PadLeft(num_chars, fill_char));
        }

        public static int forceIntParse(string in_str)
        {
            int la_resp;

            in_str = in_str ?? "0";
            int.TryParse(in_str, out la_resp);

            return la_resp;
        }

        public static long forceLongParse(int in_int)
        {
            long la_resp = 0;

            la_resp = Convert.ToInt64(in_int);

            return la_resp;
        }

    }
}