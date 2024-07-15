namespace EndpointProtector.Database
{
    internal class DatabaseConfiguringService
    {
        private const string FolderName = "tcc";

        internal static void CreateFolder()
        {
            try
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), FolderName));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
