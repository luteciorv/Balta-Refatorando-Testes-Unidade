﻿using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories;

public class FakeDiscountRepository : IDiscountRepository
{
    public Discount Get(string code)
    {
        if(code == "12345678") return new(10, DateTime.Now.AddDays(1));

        if (code == "11111111") return new(10, DateTime.Now.AddDays(-1));

        return null;
    }
}
