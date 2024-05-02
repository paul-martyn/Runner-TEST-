using CodeBase.Boosts.Effects;
using CodeBase.Data.Static;
using CodeBase.Infrastructure;

namespace CodeBase.Boosts
{
    public class SpeedBoost : Boost, ICoroutineRunner
    {
        public override void Construct(BoostsStaticData boostsStaticData)
        {
            Effect = new SpeedEffect(boostsStaticData.SpeedBoostValue, boostsStaticData.SpeedBoosDuration, this);
        }
    }
}