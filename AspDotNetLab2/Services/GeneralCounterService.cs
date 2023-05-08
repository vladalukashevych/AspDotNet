using AspDotNetLab2.Interfaces;
using System.Diagnostics.Metrics;
using System.Text;

namespace AspDotNetLab2.Services
{
    public class GeneralCounterService : IGeneralCounterService
    {
        private int count = 0;

        private Dictionary<string, int> countUrl = new Dictionary<string, int>();

        public void IncreaseValue(string url)
        {
            if (countUrl.ContainsKey(url))
            {
                countUrl[url]++;
            }
            else
            {
                countUrl[url] = 1;
            }

            count++;
        }
        public Dictionary<string, int> GetGeneralCountUrl()
        {
            return countUrl;
        }
        public int GetGeneralCount()
        {
            return count;
        }
    }
}
