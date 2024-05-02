namespace CodeBase.Hero.Speed
{
    public class HeroSpeed
    {
        public float Value => _speed.Value; 
        
        private readonly Speed _speed;
        
        public HeroSpeed(float value)
        {
            _speed = new Speed(value);
        }

        public void AddModifier(ISpeedModifier speedModifier) => 
            speedModifier.Modify(_speed);
    }
}