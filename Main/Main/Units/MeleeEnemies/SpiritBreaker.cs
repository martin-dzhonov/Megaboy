namespace Main
{
    public class SpiritBreaker : Melee
    {
        public SpiritBreaker(int positonX, int positionY)
            : base(positonX, positionY)
        {
            this.Health = 2;
            this.spriteName = "SpiritBreaker";
        }
    }
}
