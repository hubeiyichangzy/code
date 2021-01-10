using System;
using System.Collections.Generic;
using System.Text;
using DotNetFramework461Console.Algorithms.MicrosoftInterview;
using Xunit;

namespace DotNetCoreXUnitTest
{
    public class MicrosoftInterviewFacts
    {
        /*
A string is considered balanced when every letter in the string appears both in uppercase and lowercase
For example, CATattac is balanced (a, c, t occur in both cases). Madam is not (a, d only appear in lowercase).
Write a function that given a string returns the shortest balanced substring of that string.
Can this be solved with a sliding window approach?
Update:
More examples
“azABaabza” returns “ABaab”
“TacoCat” returns -1 (not balanced)
“AcZCbaBz” returns the entire string
        */

        #region TestBalanceSubString

        [Fact]
        public void ShortestBalanceItemFact()
        {
            Assert.Equal("aaa", new String(new List<char>{'a', 'a', 'a'}.ToArray()));

            var s = "aA";
            var item = new BalanceItem('a', 0, 1);
            Assert.Equal(2, item.Distance());
            Assert.True(item.IsBalance(s));
            Assert.True(item.IsShortedBalance());
            Assert.True(item.IsUpperLowerSet());
        }

        [Fact]
        public void BalanceItemFact()
        {
            var s = "abAB";
            var item = new BalanceItem("ab", 0, 3);
            Assert.Equal(4, item.Distance());
            Assert.True(item.IsBalance(s));
            Assert.False(item.IsShortedBalance());
            Assert.True(item.IsUpperLowerSet());

            item.AddLowerCase('c');
            Assert.Equal("ABC", item.UpperCases);
        }
        
        [Fact]
        public void NotBalanceItemFact()
        {
            var s = "abA";
            var item = new BalanceItem("a", 0, 2);
            Assert.Equal(3, item.Distance());
            Assert.False(item.IsBalance(s));
            Assert.False(item.IsShortedBalance());
            Assert.True(item.IsUpperLowerSet());
        }

        [Fact]
        public void SubStringFacts()
        {
            // int a = 1;
            // int b = -1;
            //
            // Assert.Equal(2, a-b);
            // Assert.Equal(2, 1-(-1));
            // Assert.Equal(-1, new BalanceSubString().SubString("Ab"));
            // Assert.Equal(2, new BalanceSubString().SubString("Aa"));
            // Assert.Equal(4, new BalanceSubString().SubString("AbaB"));
            Assert.Equal(-1, new BalanceSubString().SubString("Aba"));
            // Assert.Equal(2, new BalanceSubString().SubString("AaABAbA"));
            // Assert.Equal(5, new BalanceSubString().SubString("azABaabza"));
        }
        
        [Fact]
        public void SubString1Facts()
        {
            // Assert.Equal(-1, new BalanceSubString().SubString1("Ab"));
            // Assert.Equal(2, new BalanceSubString().SubString1("Aa"));
            // Assert.Equal(4, new BalanceSubString().SubString1("AbaB"));
            Assert.Equal(-1, new BalanceSubString().SubString1("AbacB")); // case to check if you check IsBalanceString or not
            // Assert.Equal(2, new BalanceSubString().SubString1("AaABAbA"));
            // Assert.Equal(5, new BalanceSubString().SubString1("azABaabza"));
        }
        #endregion

    }
}
