namespace Crool.DataAccess
{
    public static class Settings
    {
        static Settings()
        {
            Settings.ConnectionString = @"Server=.\SQLEXPRESS;Database=Crool;Trusted_Connection=true;";
        }

        public static string ConnectionString { get; set; }
    }
}
