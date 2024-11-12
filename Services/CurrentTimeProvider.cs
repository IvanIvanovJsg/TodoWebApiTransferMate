namespace TodoWebApiTransferMate.Services;

public class CurrentTimeProvider : ICurrentTimeProvider
{
    public DateTime GetCurrentTime()
    {
        return DateTime.UtcNow;
    }
}