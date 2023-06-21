using Mapster;
using shop.Core.Commons;
using shop.Service.DTOs.CommonsCommand;

namespace shop.Service.Extension.Util
{
    public static class GenericMapping
    {
        public static TDTO ToDTO<TDTO>(this BaseEntity entity)
        {
            var dto = entity.Adapt<TDTO>();
            return dto;
        }
        public static TEntity ToEntity<TEntity>(this BaseDTO baseDto)
        {
            var entity = baseDto.Adapt<TEntity>();

            return entity;
        }
    }
}
