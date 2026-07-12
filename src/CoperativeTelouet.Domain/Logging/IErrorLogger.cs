namespace CoperativeTelouet.Domain.Logging;

public interface IErrorLogger
{
    void LogError(Exception exception, AppLayer layer, string context);
}
