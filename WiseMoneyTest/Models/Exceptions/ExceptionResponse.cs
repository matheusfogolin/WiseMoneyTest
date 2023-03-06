namespace WiseMoneyTest.Models.Exceptions
{
    public class ExceptionResponse
    {
        public ExceptionResponse(string description)
        {
            Description = description;
        }

        public string Description { get; set; }
    }
}
