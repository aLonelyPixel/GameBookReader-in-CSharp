using System;
using System.Collections.Generic;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<int> myList = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };

            myList.RemoveAt(myList.Count-1);

            Console.WriteLine(String.Join(",", myList));
        }
    }
}
