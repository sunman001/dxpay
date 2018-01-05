namespace JmPay.PayChannelMonitor.Util.LogManager
{
    public class LogWritterFactory
    {
        public static void Write(string summary, string content)
        {
            new DbLogger().Write(summary,content);
        }
    }
}
