namespace Round.NET.AvaloniaApp.EncodeCalculator.Models.Type;

public class Type
{
    public enum NodeType
    {
        Function, // 函数
        Comparison // 比较
    }

    public enum CompareTypes
    {
        Equals,
        NotEquals,
        GreaterThan,
        SmallerThan,
        GreaterThanOrEqualTo,
        SmallerThanOrEqualTo
    }
}