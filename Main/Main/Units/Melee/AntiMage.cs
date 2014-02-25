namespace Main
{
    class AntiMage : Melee
    {
        public AntiMage(int positonX, int positionY)
            : base(positonX, positionY)
        {
            this.Health = 2;
            this.spriteName = "AntiMage";
        }
    }
}
