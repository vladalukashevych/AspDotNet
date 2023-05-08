using AspDotNetLab2.Interfaces;
using AspDotNetLab2.Interfaces;
using System.Text;

namespace AspDotNetLab2.Services
{
    public class TimerService : ITimerService
    {
        public string GetDateAndTime()
        {            
            return DateTime.Now.ToString();
        }
    }
}
