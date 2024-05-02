using CodeBase.Boosts.Effects;
using CodeBase.Data.Static;

namespace CodeBase.Boosts
{
    public class ImmunityBoost : Boost
    {
        public override void Construct(BoostsStaticData boostsStaticData)
        {
            Effect = new ImmunityEffect(boostsStaticData.ImmunityBoostDuration);
        }
    }
}