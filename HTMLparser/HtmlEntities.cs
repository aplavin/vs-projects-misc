// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlEntities.cs" company="">
//   
// </copyright>
// <summary>
//   Implements parsing of entities
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Majestic12
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Linq;

    /// <summary>
    /// Implements parsing of entities
    /// </summary>
    public static class HtmlEntities
    {
        #region Entities
        /// <summary>
        /// Supported HTML entities.
        /// </summary>
        public static readonly BiDictionary<string, int> Entities = new BiDictionary<string, int>{
                // FIXIT: we will treat non-breakable space... as space!?!
                // perhaps it would be better to have separate return types for entities?
                { "nbsp", 32 },// allEntities.Add("nbsp",160);
                { "iexcl", 161 },
                { "cent", 162 },
                { "pound", 163 },
                { "curren", 164 },
                { "yen", 165 },
                { "brvbar", 166 },
                { "sect", 167 },
                { "uml", 168 },
                { "copy", 169 },
                { "ordf", 170 },
                { "laquo", 171 },
                { "not", 172 },
                { "shy", 173 },
                { "reg", 174 },
                { "macr", 175 },
                { "deg", 176 },
                { "plusmn", 177 },
                { "sup2", 178 },
                { "sup3", 179 },
                { "acute", 180 },
                { "micro", 181 },
                { "para", 182 },
                { "middot", 183 },
                { "cedil", 184 },
                { "sup1", 185 },
                { "ordm", 186 },
                { "raquo", 187 },
                { "frac14", 188 },
                { "frac12", 189 },
                { "frac34", 190 },
                { "iquest", 191 },
                { "Agrave", 192 },
                { "Aacute", 193 },
                { "Acirc", 194 },
                { "Atilde", 195 },
                { "Auml", 196 },
                { "Aring", 197 },
                { "AElig", 198 },
                { "Ccedil", 199 },
                { "Egrave", 200 },
                { "Eacute", 201 },
                { "Ecirc", 202 },
                { "Euml", 203 },
                { "Igrave", 204 },
                { "Iacute", 205 },
                { "Icirc", 206 },
                { "Iuml", 207 },
                { "ETH", 208 },
                { "Ntilde", 209 },
                { "Ograve", 210 },
                { "Oacute", 211 },
                { "Ocirc", 212 },
                { "Otilde", 213 },
                { "Ouml", 214 },
                { "times", 215 },
                { "Oslash", 216 },
                { "Ugrave", 217 },
                { "Uacute", 218 },
                { "Ucirc", 219 },
                { "Uuml", 220 },
                { "Yacute", 221 },
                { "THORN", 222 },
                { "szlig", 223 },
                { "agrave", 224 },
                { "aacute", 225 },
                { "acirc", 226 },
                { "atilde", 227 },
                { "auml", 228 },
                { "aring", 229 },
                { "aelig", 230 },
                { "ccedil", 231 },
                { "egrave", 232 },
                { "eacute", 233 },
                { "ecirc", 234 },
                { "euml", 235 },
                { "igrave", 236 },
                { "iacute", 237 },
                { "icirc", 238 },
                { "iuml", 239 },
                { "eth", 240 },
                { "ntilde", 241 },
                { "ograve", 242 },
                { "oacute", 243 },
                { "ocirc", 244 },
                { "otilde", 245 },
                { "ouml", 246 },
                { "divide", 247 },
                { "oslash", 248 },
                { "ugrave", 249 },
                { "uacute", 250 },
                { "ucirc", 251 },
                { "uuml", 252 },
                { "yacute", 253 },
                { "thorn", 254 },
                { "yuml", 255 },
                { "quot", 34 },
                // NOTE: this is a not a proper entity but a fairly common mistake - & is important symbol
                // and we don't want to lose it even if webmaster used upper case instead of lower
                { "AMP", 38 },
                { "REG", 174 },
                { "amp", 38 },
                { "reg", 174 },
                { "lt", 60 },
                { "gt", 62 },
                // ' - apparently does not work in IE
                { "apos", 39 },
                // unicode
                { "OElig", 338 },
                { "oelig", 339 },
                { "Scaron", 352 },
                { "scaron", 353 },
                { "Yuml", 376 },
                { "circ", 710 },
                { "tilde", 732 },
                { "ensp", 8194 },
                { "emsp", 8195 },
                { "thinsp", 8201 },
                { "zwnj", 8204 },
                { "zwj", 8205 },
                { "lrm", 8206 },
                { "rlm", 8207 },
                { "ndash", 8211 },
                { "mdash", 8212 },
                { "lsquo", 8216 },
                { "rsquo", 8217 },
                { "sbquo", 8218 },
                { "ldquo", 8220 },
                { "rdquo", 8221 },
                { "bdquo", 8222 },
                { "dagger", 8224 },
                { "Dagger", 8225 },
                { "permil", 8240 },
                { "lsaquo", 8249 },
                { "rsaquo", 8250 },
                { "euro", 8364 },
                { "fnof", 402 },
                { "Alpha", 913 },
                { "Beta", 914 },
                { "Gamma", 915 },
                { "Delta", 916 },
                { "Epsilon", 917 },
                { "Zeta", 918 },
                { "Eta", 919 },
                { "Theta", 920 },
                { "Iota", 921 },
                { "Kappa", 922 },
                { "Lambda", 923 },
                { "Mu", 924 },
                { "Nu", 925 },
                { "Xi", 926 },
                { "Omicron", 927 },
                { "Pi", 928 },
                { "Rho", 929 },
                { "Sigma", 931 },
                { "Tau", 932 },
                { "Upsilon", 933 },
                { "Phi", 934 },
                { "Chi", 935 },
                { "Psi", 936 },
                { "Omega", 937 },
                { "alpha", 945 },
                { "beta", 946 },
                { "gamma", 947 },
                { "delta", 948 },
                { "epsilon", 949 },
                { "zeta", 950 },
                { "eta", 951 },
                { "theta", 952 },
                { "iota", 953 },
                { "kappa", 954 },
                { "lambda", 955 },
                { "mu", 956 },
                { "nu", 957 },
                { "xi", 958 },
                { "omicron", 959 },
                { "pi", 960 },
                { "rho", 961 },
                { "sigmaf", 962 },
                { "sigma", 963 },
                { "tau", 964 },
                { "upsilon", 965 },
                { "phi", 966 },
                { "chi", 967 },
                { "psi", 968 },
                { "omega", 969 },
                { "thetasym", 977 },
                { "upsih", 978 },
                { "piv", 982 },
                { "bull", 8226 },
                { "hellip", 8230 },
                { "prime", 8242 },
                { "Prime", 8243 },
                { "oline", 8254 },
                { "frasl", 8260 },
                { "weierp", 8472 },
                { "image", 8465 },
                { "real", 8476 },
                { "trade", 8482 },
                { "alefsym", 8501 },
                { "larr", 8592 },
                { "uarr", 8593 },
                { "rarr", 8594 },
                { "darr", 8595 },
                { "harr", 8596 },
                { "crarr", 8629 },
                { "lArr", 8656 },
                { "uArr", 8657 },
                { "rArr", 8658 },
                { "dArr", 8659 },
                { "hArr", 8660 },
                { "forall", 8704 },
                { "part", 8706 },
                { "exist", 8707 },
                { "empty", 8709 },
                { "nabla", 8711 },
                { "isin", 8712 },
                { "notin", 8713 },
                { "ni", 8715 },
                { "prod", 8719 },
                { "sum", 8721 },
                { "minus", 8722 },
                { "lowast", 8727 },
                { "radic", 8730 },
                { "prop", 8733 },
                { "infin", 8734 },
                { "ang", 8736 },
                { "and", 8743 },
                { "or", 8744 },
                { "cap", 8745 },
                { "cup", 8746 },
                { "int", 8747 },
                { "there4", 8756 },
                { "sim", 8764 },
                { "cong", 8773 },
                { "asymp", 8776 },
                { "ne", 8800 },
                { "equiv", 8801 },
                { "le", 8804 },
                { "ge", 8805 },
                { "sub", 8834 },
                { "sup", 8835 },
                { "nsub", 8836 },
                { "sube", 8838 },
                { "supe", 8839 },
                { "oplus", 8853 },
                { "otimes", 8855 },
                { "perp", 8869 },
                { "sdot", 8901 },
                { "lceil", 8968 },
                { "rceil", 8969 },
                { "lfloor", 8970 },
                { "rfloor", 8971 },
                { "lang", 9001 },
                { "rang", 9002 },
                { "loz", 9674 },
                { "spades", 9824 },
                { "clubs", 9827 },
                { "hearts", 9829 },
                { "diams", 9830 },
            };
        #endregion

        private static readonly int MaxEntityLength = Entities.Max(kvp => kvp.Key.Length);

        /// <summary>
        /// If false then HTML entities (like "nbsp") will not be decoded
        /// </summary>
        public static bool DoDecodeEntities { get; set; }

        /// <summary>
        /// The check for entity.
        /// </summary>
        internal static char CheckForEntity(byte[] htmlBytes, ref int curPos, int dataLength)
        {
            if (!DoDecodeEntities)
            {
                return '\0';
            }

            int chars = 0;

            // if true it means we are getting hex or decimal value of the byte
            bool bCharCode = false;
            bool isCharCodeHex = false;

            int iEntLen = 0;

            int iFrom = curPos;

            try
            {
                while (curPos < dataLength)
                {
                    byte charByte = htmlBytes[curPos++];

                    // 21/10/05: not necessary
                    // if(cChar==0)
                    // 	break;
                    if (++chars <= 2)
                    {
                        // the first byte for numbers should be #
                        if (chars == 1)
                        {
                            if (charByte == '#')
                            {
                                iFrom++;
                                bCharCode = true;
                                continue;
                            }
                        }
                        else
                        {
                            if (bCharCode && charByte == 'x')
                            {
                                iFrom++;
                                iEntLen--;
                                isCharCodeHex = true;
                            }
                        }
                    }

                    // Console.WriteLine("Got entity end: {0}",sEntity);
                    // Break on:
                    // 1) ; - proper end of entity
                    // 2) number 10-based entity but current byte is not a number
                    // if(cChar==';' || (bCharCode && !bCharCodeHex && !char.IsNumber((char)cChar)))

                    // TODO: browsers appear to be lax about ; requirement for end of entity 
                    // we should really do the same and treat whitespace as termination of entity
                    if (charByte == ';' || (bCharCode && !isCharCodeHex && !(charByte >= '0' && charByte <= '9')))
                    {
                        string sEntity = Encoding.Default.GetString(htmlBytes, iFrom, iEntLen);

                        if (bCharCode)
                        {
                            // NOTE: this may fail due to wrong data format,
                            // in which case we will return 0, and entity will be
                            // ignored
                            if (iEntLen > 0)
                            {
                                return isCharCodeHex ?
                                    (char)int.Parse(sEntity, NumberStyles.HexNumber) :
                                    (char)int.Parse(sEntity);
                            }
                        }

                        if (Entities.ContainsKey(sEntity))
                        {
                            return (char)Entities[sEntity];
                        }
                    }

                    // break;

                    // as soon as entity length exceed max length of entity known to us
                    // we break up the loop and return nothing found

                    if (iEntLen > MaxEntityLength)
                    {
                        break;
                    }

                    iEntLen++;
                }
            }
            catch
            {
                // (Exception oEx)
                // Console.WriteLine("Entity parsing exception: "+oEx.ToString());
            }

            // if we have not found squat, then we will need to put point back
            // to where it was before this function was called
            if (chars > 0 && curPos - chars >= 0) curPos -= chars;

            return (char)0;
        }

        /// <summary>
        /// Parses an unsigned integer number from byte buffer from offset to offset + length.
        /// Starts from end.
        /// </summary>
        private static uint ParseUInt(byte[] bytes, int offset, int length)
        {
            return uint.Parse(
                bytes.
                Skip(offset).Take(length).
                Reverse().
                TakeWhile(b => char.IsDigit((char)b)).
                Reverse().
                Aggregate(b => (char)(b - '0')));
        }

        /// <summary>
        /// Parses line and changes known entiry characters into proper HTML entiries
        /// </summary>
        internal static string ChangeToEntities(string line, int offset)
        {
            var sb = new StringBuilder(line, 0, offset, line.Length);

            foreach (char ch in line.Skip(offset))
            {
                // yeah I know its lame but its 3:30am and I had v.long debugging session :-/
                if (ch < 32 || ch == 39 || ch.InRange((char)145, (char)148))
                {
                    sb.Append("&#").Append((int)ch).Append(';');
                }
                else if (Entities.ContainsValue(ch))
                {
                    // 14/05/08 we use numeric entities above ASCII level
                    // this is safer way - PHP XML parser was dieing on proper entities
                    sb.Append("&");
                    sb.Append(Entities.GetKey(ch));
                    sb.Append(";");
                }
                else
                {
                    sb.Append(ch);
                }

            }

            return sb.ToString();
        }
    }
}