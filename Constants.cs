namespace ActivitySimulator
{
    public static class Constants
    {
        public const string structurePath = @"C:\Users\35989\Desktop\ActivitySimulatorStructure.txt";
    }

    public class OlxConstants
    {
        public const string olxMainLogInPattern = @"olx_log_in_main\((.+),(.+)\)";

        public const string olxUrl = "https://www.olx.bg/";
        public const string olxLogInUrl = "https://www.olx.bg/account/?ref%5B0%5D%5Baction%5D=myaccount&ref%5B0%5D%5Bmethod%5D=index";
        public const string baseSearchUrl = "https://www.olx.bg/ads/q-"; // optional is to add '/' after the search Term

        public const string defaultLegoSearch = "lego";
        public const string defaultHotWheelsSearch = "hot wheels";
    }
}
