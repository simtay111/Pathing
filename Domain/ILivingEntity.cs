using Domain.Enums;

namespace Domain
{
    public interface ILivingEntity
    {
        LivingEntityTypes GetEntityType();
    }
}