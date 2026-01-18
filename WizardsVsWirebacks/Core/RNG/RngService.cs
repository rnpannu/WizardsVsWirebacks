/// RNG Service Class Stub.

using System;

namespace WizardsVsWirebacks.Core.RNG
{
    public class RngService
    {
        private Random _random;

        public RngService(int seed)
        {
            _random = new Random(seed);
        }

        public int NextInt(int min, int max)
        {
            return _random.Next(min, max);
        }

        public float NextFloat()
        {
            return (float)_random.NextDouble();
        }
    }
}
