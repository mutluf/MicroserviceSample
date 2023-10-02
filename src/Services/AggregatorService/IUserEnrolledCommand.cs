namespace AggregatorService
{
    public interface IUserEnrolledCommand
    {
        string Username { get; set; }
        string Email { get; set; }
    }
}
