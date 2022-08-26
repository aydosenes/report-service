namespace Application.Results
{
    public interface IBaseResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
