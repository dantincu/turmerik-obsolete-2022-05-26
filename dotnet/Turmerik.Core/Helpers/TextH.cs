using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.Core.Helpers
{
    public static class TextH
    {
        public static readonly string NL = Environment.NewLine;

        public static string NwLns(int count)
        {
            string retStr = string.Concat(
                Enumerable.Range(0, count).Select(
                    i => NL).ToArray());

            return retStr;
        }

        public static bool ContainsAny(this string str, params char[] chars)
        {
            bool retVal = str.ContainsAny(chars);
            return retVal;
        }

        public static bool ContainsAny(this string str, IEnumerable<char> chars)
        {
            bool retVal = chars.Any(
                c => str.Contains(c));

            return retVal;
        }

        public static char[] GetContained(this string str, params char[] chars)
        {
            char[] retChars = str.GetContained(chars);
            return retChars;
        }

        public static char[] GetContained(this string str, IEnumerable<char> chars)
        {
            char[] retChars = chars.Where(c => str.Contains(c)).ToArray();
            return retChars;
        }

        public static string ReplaceChars(
            this string str,
            Func<char, char> replaceFactory,
            params char[] replacedChars)
        {
            string retStr = str.ReplaceChars(
                replaceFactory, replacedChars);

            return retStr;
        }

        public static string ReplaceChars(
            this string str,
            Func<char, char> replaceFactory,
            IEnumerable<char> replacedChars)
        {
            replaceFactory = replaceFactory.FirstNotNull(
                c => default);

            char[] retChars = str.Select(
                c => replacedChars.Contains(c) ? replaceFactory(c) : c).ToArray();

            string retStr = new string(retChars);
            return retStr;
        }

        public static Tuple<string, string, string> Splice(
            this string inputStr,
            Func<string, int, int> firstSplitter,
            Func<string, int, int> secondSplitter)
        {
            Tuple<string, string, string> retTpl;
            int inputLen = inputStr.Length;

            int firstIdx = firstSplitter(inputStr, inputLen);

            if (firstIdx >= 0)
            {
                string firstStr = inputStr.Substring(0, firstIdx);
                string str = inputStr.Substring(firstIdx);

                int len = str.Length;
                int secondIdx = secondSplitter(str, len);

                if (secondIdx >= 0)
                {
                    string secondStr = str.Substring(0, secondIdx);
                    string thirdStr = str.Substring(secondIdx);

                    retTpl = new Tuple<string, string, string>(
                        firstStr, secondStr, thirdStr);
                }
                else
                {
                    retTpl = new Tuple<string, string, string>(
                        firstStr, str, null);
                }
            }
            else
            {
                retTpl = new Tuple<string, string, string>(
                    inputStr, null, null);
            }

            return retTpl;
        }

        public static Tuple<string, string> SubStr(
            this string inputStr,
            Func<string, int, int> splitter)
        {
            Tuple<string, string> retTpl;
            int inputLen = inputStr.Length;

            int idx = splitter(inputStr, inputLen);

            if (idx >= 0)
            {
                string firstStr = inputStr.Substring(0, idx);
                string secondStr = inputStr.Substring(idx);

                retTpl = new Tuple<string, string>(
                    firstStr, secondStr);
            }
            else
            {
                retTpl = new Tuple<string, string>(
                    inputStr, null);
            }

            return retTpl;
        }

        public static bool StrEquals(this string target, string reference, bool ignoreCase = false)
        {
            bool retVal = string.Compare(target, reference, ignoreCase) == 0;
            return retVal;
        }
    }
}
