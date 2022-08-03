﻿using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models;

public class FakeProductRepository /*: IProductRepository */
{
    public IQueryable<Product> Products()
    {
        return new List<Product>
        {
            new Product { Name = "Football", Price = 25 },
            new Product { Name = "Surf board", Price = 179 },
            new Product { Name = "Running shoes", Price = 95 }
        }.AsQueryable<Product>();
    }
}