using AspDotNetLab2.Interfaces;

namespace AspDotNetLab2.Services
{
    public class RandomService : IRandomService
    {
        private int value;

        static Random rand = new Random();

        public RandomService()
        {
            value = rand.Next(0, 1000);
        }

        public int GetNumber()
        {
            return value;
        }
    }
}