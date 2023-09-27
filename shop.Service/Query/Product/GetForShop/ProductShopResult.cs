﻿using shop.Service.Extension.Util;

namespace shop.Service.Query.Product.GetForShop;

public class ProductShopResult : BaseFilter<ProductShopDto, ProductShopFilterParam>
{
    public CategoryQueryDto? CategoryDto { get; set; }
}

public class ProductShopDto : BaseDto
{
    public string Name { get; set; }
    public int InventoryId { get; set; }
    public int Price { get; set; }
    public int DiscountPercentage { get; set; }
    public string ImageName { get; set; }

    public int TotalPrice
    {
        get
        {
            var discount = Price * DiscountPercentage / 100;
            return Price - discount;
        }
    }
}
public class ProductShopFilterParam : BaseFilterParam
{
    public string? Category { get; set; } = "";
    public string? Search { get; set; } = "";
    public bool OnlyAvailableProducts { get; set; } = true;
    public bool JustHasDiscount { get; set; } = false;
    public ProductSearchOrderBy SearchOrderBy { get; set; } = ProductSearchOrderBy.Cheapest;
}

public enum ProductSearchOrderBy
{
    Latest,
    Expensive,
    Cheapest,
}