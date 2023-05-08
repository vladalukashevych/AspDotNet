namespace AspDotNetLab2.Interfaces
{
    public interface IGeneralCounterService
    {
        public Dictionary<string, int> GetGeneralCountUrl();
        public void IncreaseValue(string url);
        public int GetGeneralCount();
    }
}
