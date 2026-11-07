namespace Twisted.Domain.Models
{
    public static class BnetTokenModel
    {
        public static string? AccessToken { get; set; }
        public static string? TokenType { get; set; }
        public static DateTime? RetrivalTime { get; set; }
        public static int? SecondsUntilExpiration { get; set; }
        public static string? Sub { get; set; }
        public static bool HasTokenExpired()
        {
            if(RetrivalTime.HasValue && SecondsUntilExpiration.HasValue)
            {
                if (DateTime.Now < RetrivalTime.Value.AddSeconds(SecondsUntilExpiration.Value))
                    return false;
            }
            return true;
        }
    }
}
