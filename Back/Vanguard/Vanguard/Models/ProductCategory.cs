﻿using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class ProductCategory:BaseEntity
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public Product Product { get; set; } = null!;
    public Category Category { get; set; } = null!;
}
