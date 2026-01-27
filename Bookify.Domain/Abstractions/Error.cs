namespace Bookify.Domain.Abstractions;

public record Error(string Code, string Name)
{
    public static Error None = new(Code: string.Empty, Name: string.Empty);

    public static Error NullValue = new(Code: "Error.NullValue", Name: "Null value was provided");
}