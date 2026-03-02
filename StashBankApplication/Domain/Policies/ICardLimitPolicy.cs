using StashBankApplication.Domain.Enums;

namespace StashBankApplication.Domain.Policies
{
    public interface ICardLimitPolicy
    {
        decimal GetLimit(CardTier tier);
    }
}
