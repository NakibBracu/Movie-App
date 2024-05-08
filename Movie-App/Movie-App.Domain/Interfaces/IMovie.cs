namespace Movie_App.Domain.Interfaces
{
    public interface IMovie
    {
        string Title { get; }
        IEnumerable<string> Cast { get; }
        string Category { get; }
        DateTime ReleaseDate { get; }
        double Budget { get; }
    }
}