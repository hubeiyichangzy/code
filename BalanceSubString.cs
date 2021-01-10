using System;
using System.Collections.Generic;
using System.Linq;
using Convert = System.Convert;

namespace DotNetFramework461Console.Algorithms.MicrosoftInterview
{
    public class BalanceSubString
    {
        public int SubString1(string s)
        {
            BalanceItem res = new BalanceItem();
            List<CharPair> pairs = new List<CharPair>();
            List<BalanceItem> m = new List<BalanceItem>();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                char lowerCaseC = char.ToLower(c);
                CharPair existedPair = pairs.SingleOrDefault(p => p.LowerCase == lowerCaseC);
                CharPair pair = existedPair ?? new CharPair(lowerCaseC);
                if (existedPair == null)
                {
                    pairs.Add(pair);
                }

                if (char.IsUpper(c))
                {
                    pair.UpperCasePos = i;
                }
                else
                {
                    pair.LowerCasePos = i;
                }

                foreach (var p in pairs)
                {
                    BalanceItem existedItem = m.SingleOrDefault(item => item.LowerCases.Contains(p.LowerCase));
                    if (existedItem != null)
                    {
                        existedItem.StartPos = Min(p.UpperCasePos, p.LowerCasePos); //Notes: this is fantastic idea
                        existedItem.EndPos = Max(p.UpperCasePos, p.LowerCasePos);
                    }
                    else
                    {
                        var item = new BalanceItem(p.LowerCase, Min(p.UpperCasePos, p.LowerCasePos), Max(p.UpperCasePos, p.LowerCasePos));
                        m.Add(item);
                    }
                }

                BalanceItem currentSmallestInterval = FindSmallestInterval(m, s);
                res = Min(res, currentSmallestInterval);
            }

            return res.IsUpperLowerSet()
                ? res.Distance()
                : -1;

        }

        public int SubString(string s)
        {
            int[] upper = new int[26];
            int[] lower = new int[26];
            for (int i = 0; i < 26; i++)
            {
                upper[i] = BalanceItem.INT_MIN;
                lower[i] = BalanceItem.INT_MIN;
            }

            BalanceItem res = new BalanceItem();
            List<BalanceItem> m = new List<BalanceItem>();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (char.IsUpper(c))
                {
                    upper[c - 'A'] = i;
                }
                else
                {
                    lower[c - 'a'] = i;
                }

                for (int j = 0; j < 26; j++)
                {
                    BalanceItem existedItem = m.FirstOrDefault(item => item.LowerCases.Contains(Convert.ToChar('a' + j)));
                    if (existedItem != null)
                    {
                        existedItem.StartPos = Min(upper[j], lower[j]); //Notes: this is fantastic idea
                        existedItem.EndPos = Max(upper[j], lower[j]);
                    }
                    else
                    {
                        var item = new BalanceItem(Convert.ToChar('a' + j), Min(upper[j], lower[j]), Max(upper[j], lower[j]));
                        m.Add(item);
                    }
                }

                BalanceItem currentSmallestInterval = FindSmallestInterval(m, s);
                res = Min(res, currentSmallestInterval);
            }

            return res.IsUpperLowerSet()
                ? res.Distance()
                : -1;
        }

        private BalanceItem Min(BalanceItem item1, BalanceItem item2)
        {
            return item1.Distance() <= item2.Distance() ? item1 : item2;
        }

        private BalanceItem FindSmallestInterval(List<BalanceItem> m, string s)
        {
            var orderedItems = m.OrderBy(balanceItem => balanceItem.StartPos).ToList();
            List<BalanceItem> merged = new List<BalanceItem>();
            var result = new BalanceItem();

            for (int i = 0; i < m.Count; i++)
            {
                var item = orderedItems[i];
                if (item.IsUpperLowerSet())
                {
                    var mergedItem = item.Clone();
                    if (mergedItem.IsShortedBalance())
                    {
                        return mergedItem;
                    }

                    if (merged.Count > 0)
                    {
                        BalanceItem lastMerged = merged.Last();
                        if (mergedItem.StartPos < lastMerged.EndPos && lastMerged.EndPos < mergedItem.EndPos)
                        {
                            lastMerged.EndPos = mergedItem.EndPos;
                            lastMerged.AddLowerCase(mergedItem.LowerCases);
                            if (lastMerged.IsBalance(s) && lastMerged.Distance() < result.Distance())
                            // if (lastMerged.Distance() < result.Distance())
                            {
                                result = lastMerged;
                            }

                            continue;
                        }
                    }
                    merged.Add(mergedItem);

                }
            }



            return result;
        }

        private int Min(int p1, int p2)
        {
            return p1 <= p2 ? p1 : p2;
        }
        private int Max(int p1, int p2)
        {
            return p1 >= p2 ? p1 : p2;
        }
    }
    
    public class CharPair
    {
        public char LowerCase { get; set; }
        public int UpperCasePos { get; set; }
        public int LowerCasePos { get; set; }

        public CharPair(char v)
        {
            LowerCase = v;
            UpperCasePos = BalanceItem.INT_MIN;
            LowerCasePos = BalanceItem.INT_MIN;
        }
    }
}
