using Dapper;
using Microsoft.EntityFrameworkCore;
using shop.Core.Domain.Category;
using shop.Data.ApplicationContext;
using shop.Data.Persistent.Dapper;
using shop.Service.Query;

namespace shop.Service.Query.Product.GetForShop;

public class GetProductsForShopQuery
{
    private readonly DapperContext _dapperContext;
    private readonly IApplicationContext _context;

    public GetProductsForShopQuery(DapperContext dapperContext, IApplicationContext context)
    {
        _dapperContext = dapperContext;
        _context = context;

    }

    public async Task<ProductShopResult> Handle(ProductShopFilterParam request)
    {
        var @params = request;
        string conditions = "";
        string orderBy = "";
        string inventoryOrderBy = "i.Price Asc";
        CategoryQueryDto? selectedCategory = null;

        if (!string.IsNullOrWhiteSpace(@params.Category))
        {
            var category = await _context.Set<Category>().FirstOrDefaultAsync(c => c.Name == @params.Category);

            if (category != null)
            {
                conditions += @$" and (A.CategoryId={category.Id})";
                selectedCategory = category.Map();
            }
        }

        if (!string.IsNullOrWhiteSpace(@params.Search))
        {
            conditions += $" and A.Name Like N'%{@params.Search}%'";
        }

        if (@params.OnlyAvailableProducts)
        {
            conditions += " and A.Count>=1";
        }

        if (@params.JustHasDiscount)
        {
            conditions += " and A.DiscountPercentage>0";
            inventoryOrderBy = "i.DiscountPercentage Desc";
        }
        switch (@params.SearchOrderBy)
        {
            case ProductSearchOrderBy.Cheapest:
                {
                    orderBy = "A.Price Asc";
                    break;
                }
            case ProductSearchOrderBy.Expensive:
                {
                    orderBy = "A.Price Desc";
                    break;
                }
            case ProductSearchOrderBy.Latest:
                {
                    orderBy = "A.Id Desc";
                    break;
                }
            default:
                orderBy = "p.Id";
                break;
        }
        using var sqlConnection = _dapperContext.CreateConnection();

        var skip = (@params.PageId - 1) * @params.Take;
        var sql = @$"SELECT Count(A.Name)
            FROM (Select p.Name , i.Price  , i.Id as InventoryId , i.DiscountPercentage , i.Count
                        , pc.CategoryId, p.Id as Id , s.Status
                            ,ROW_NUMBER() OVER(PARTITION BY p.Id ORDER BY {inventoryOrderBy} ) AS RN
            From {_dapperContext.Products} p
            left join {_dapperContext.Inventories} i on p.Id=i.ProductId
            left join {_dapperContext.Sellers} s on i.SellerId=s.Id
            left join {_dapperContext.ProductCategory} pc on pc.ProductID=p.Id ) as A
            WHERE  A.RN = 1 and A.Status='Accepted'  {conditions}";




        var resultSql = @$"SELECT A.Id ,A.Name,A.Price,A.InventoryId,A.DiscountPercentage,A.ImageName,A.CategoryId
            FROM (Select p.Name , i.Price  , i.Id as InventoryId , i.DiscountPercentage,p.ImageName , i.Count,
                    p.Id as Id , s.Status,pc.CategoryId
                            ,ROW_NUMBER() OVER(PARTITION BY p.Id ORDER BY {inventoryOrderBy}) AS RN
            From {_dapperContext.Products} as p
            left join {_dapperContext.Inventories} as i on p.Id=i.ProductId
            left join {_dapperContext.Sellers} as s on i.SellerId=s.Id
            left join {_dapperContext.ProductCategory} as pc on pc.ProductID = p.Id)A
            WHERE  A.RN = 1 and A.Status='Accepted'  {conditions} order By {orderBy} offset @skip ROWS FETCH NEXT @take ROWS ONLY";




        var count = await sqlConnection.QueryFirstAsync<int>(sql);
        var result = await sqlConnection.QueryAsync<ProductShopDto>(resultSql,
            new { skip, take = @params.Take });
        var model = new ProductShopResult()
        {
            FilterParams = @params,
            Data = result.ToList(),
            CategoryDto = selectedCategory
        };
        model.GeneratePaging(@params.Take, @params.PageId, count);
        return model;
    }
}