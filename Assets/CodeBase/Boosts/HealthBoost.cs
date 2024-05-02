using CodeBase.Boosts.Effects;
using CodeBase.Data.Static;

namespace CodeBase.Boosts
{
    public class HealthBoost : Boost
    {
        public override void Construct(BoostsStaticData boostsStaticData)
        {
            Effect = new HealthEffect(boostsStaticData.HealthBoostValue);
        }
    }
}