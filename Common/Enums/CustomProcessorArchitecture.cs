namespace Common.Enums
{
    /// <summary>
    /// https://learn.microsoft.com/en-us/uwp/api/windows.system.processorarchitecture?view=winrt-22621
    /// </summary>
    public enum CustomProcessorArchitecture
    {
        Arm = 5,
        //The ARM processor architecture

        Arm64 = 12,
        //The Arm64 processor architecture

        Neutral = 11,
        //A neutral processor architecture.

        Unknown = 65535,
        //An unknown processor architecture.

        X64 = 9,
        //The x64 processor architecture.

        X86 = 0,
        //The x86 processor architecture.

        X86OnArm64 = 14,
        //The Arm64 processor architecture emulating the X86 architecture
    }
}
