using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism.Core.Helper.DTO
{
    public static class RandomGenerator
    {
        private static Random _random = new Random();
        public static int Generate(int min , int max)
        {
            return _random.Next(min, max);
        }

    }
}
