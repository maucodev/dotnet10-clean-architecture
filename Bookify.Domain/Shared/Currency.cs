using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookify.Domain.Shared;

public record Currency
{
    public static readonly Currency Eur = new ("EUR");
    public static readonly Currency Usd = new ("USD");
    internal static readonly Currency None = new(string.Empty);

    public string Code { get; init; }

    private Currency(string code)
    {
        Code = code;
    }

    public static Currency FromCode(string code)
    {
        return All
            .FirstOrDefault(item => item.Code == code) 
               ?? throw new ApplicationException("The currency code is invalid");
    }

    public static readonly IReadOnlyCollection<Currency> All = [ Usd, Eur ];
}