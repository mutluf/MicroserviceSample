namespace AggregatorService
{
    public class Fatura : IUserEnrolledCommand
    {
        public string Username { get; set ; }
        public string Email { get ; set; }
    }
}
