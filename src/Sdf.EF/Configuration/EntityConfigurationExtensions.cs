using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sdf.Domain.Entities;

namespace Sdf.EF.Configuration
{
    public static class EntityConfigurationExtensions
    {
        public static EntityTypeBuilder<TEntity> ConfigureSofeDelete<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, ISoftDelete
        {
            builder.HasQueryFilter(m => m.IsDeleted == false);
            builder.HasIndex(m => m.IsDeleted, "fx_is_sofe_delete");

            return builder;
        }
    }
}
