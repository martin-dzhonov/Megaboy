namespace Main
{
    public class UrsaWarrior : Melee
    {
        public UrsaWarrior(int positonX, int positionY)
            : base(positonX, positionY)
        {
            this.Health = 4;
            this.spriteName = "UrsaWarrior";
        }
    }
}
