using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Fundamentals
{
    public class HappyNumberGenerator
    {
        //public IEnumerable<int> Generate()
        //{
        //    IEnumerable<int> numbers = new List<int>() { 10, 5, 7, 8, 2, 1, 9 };

        //    return numbers;
        //}


        // Leniwa kolekcja (Lazy Collection)
        public IEnumerable<int> Generate()
        {
            yield return 2;
            yield return 1;
            yield return 9;
            yield return 10; 
            yield return 5; // <--
            yield return 7;
            yield return 8;
            
        }
    }
}
