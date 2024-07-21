namespace EndpointProtector.Extensions
{
    internal static class UintExtensions
    {
        public static string ToHumanizedMemorySpeed(this uint speed) => $"{speed} Hz";
    }
}
