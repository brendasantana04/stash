using StashBankApplication.Domain.Enums;

namespace StashBankApplication.Domain.Policies
{
    public class CardLimitPolicy : ICardLimitPolicy
    {
        public decimal GetLimit(CardTier tier)
        {
            return tier switch
            {
                CardTier.Basic => 10000m,
                CardTier.Intermediate => 50000m,
                CardTier.Advanced => 1000000m,
                CardTier.UltraHighLuxury => 5000000m,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
