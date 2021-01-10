using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetFramework461Console.Algorithms.MicrosoftInterview
{
    public class BalanceItem
    {
        public const int INT_MIN = -Int32.MaxValue;
        public const int INT_MAX = Int32.MaxValue;

        private List<char> _lowerCases = new List<char>();
        private List<char> _upperCases = new List<char>();
        public int LowerCasePos { get; set; } = INT_MIN;
        public int UpperCasePos { get; set; } = INT_MIN;
        public int StartPos { get; set; } = INT_MIN;
        public int EndPos { get; set; } = INT_MAX;

        public string LowerCases => new string(_lowerCases.ToArray());
        public string UpperCases => new string(_upperCases.ToArray());

        public void AddLowerCase(char value)
        {
            _lowerCases.Add(value);
            _upperCases.Add(char.ToUpper(value));
        }

        public BalanceItem()
        {
        }
        public BalanceItem(char lowerCase)
        {
            _lowerCases.Add(lowerCase);
            _upperCases.Add(char.ToUpper(lowerCase));
        }

        public BalanceItem(char lowerCase, int startPos, int endPos)
        {
            _lowerCases.Add(lowerCase);
            _upperCases.Add(char.ToUpper(lowerCase));
            StartPos = startPos;
            EndPos = endPos;
        }

        public BalanceItem(string lowerCase, int startPos, int endPos)
        {
            _lowerCases.AddRange(lowerCase.ToCharArray());
            _upperCases.AddRange(lowerCase.ToUpper().ToCharArray());
            StartPos = startPos;
            EndPos = endPos;
        }

        public bool IsBalance(string s)
        {
            if (!IsUpperLowerSet())
            {
                return false;
            }

            string substring = s.Substring(StartPos, EndPos - StartPos + 1);

            return substring.All(c => LowerCases.Union(UpperCases).Contains(c));
        }

        public bool IsUpperLowerSet()
        {
            return StartPos != INT_MIN && EndPos != INT_MAX;
        }

        public void AddLowerCase(string itemLowerCases)
        {
            _lowerCases.AddRange(itemLowerCases.ToCharArray());
            _upperCases.AddRange(itemLowerCases.ToUpper().ToCharArray());
        }

        public bool IsShortedBalance()
        {
            return EndPos - StartPos == 1;
        }

        public int Distance()
        {
            if (!IsUpperLowerSet())
            {
                return INT_MAX;
            }

            return EndPos - StartPos + 1;
        }

        public BalanceItem Clone()
        {
            return new BalanceItem(LowerCases, StartPos, EndPos);
        }
    }
}
