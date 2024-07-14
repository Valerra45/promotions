namespace Infrastructure.Environment
{
    public class PicturesDatabaseSettings : IPicturesDatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;

        public string DatabaseName { get; set; } = string.Empty;
    }

    public interface IPicturesDatabaseSettings
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}
