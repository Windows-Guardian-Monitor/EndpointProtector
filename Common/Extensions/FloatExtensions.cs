namespace Common.Extensions
{
    public static class FloatExtensions
    {
        public static string ToTwoPointString(this float value) => $"{value:0.00}%";
    }
}
