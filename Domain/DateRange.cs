namespace Domain;

public class DateRange(DateOnly start, DateOnly end)
{
    public DateOnly Start { get; } = start;
    public DateOnly End { get; } = end;

    public bool Overlap(DateRange range)
    {
        return Start < range.End && End > range.Start;
    }
}