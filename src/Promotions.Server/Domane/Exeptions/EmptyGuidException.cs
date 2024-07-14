namespace Domain.Exeptions
{
    public class EmptyGuidException : Exception
    {
        public EmptyGuidException(string message)
            : base(message)
        {

        }
    }
}
