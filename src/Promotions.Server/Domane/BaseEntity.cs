namespace Domain
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime? Update { get; set; } = null;

        public BaseEntity()
        {
            Created = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Russian Standard Time");

            Update = Created;
        }
    }
}
