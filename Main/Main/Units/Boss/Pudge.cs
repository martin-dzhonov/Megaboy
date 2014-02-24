namespace Main
{
    public class Pudge : Enemy
    {
        public Pudge(int positonX, int positionY, int rectangleWidth = 130, int rectangleHeight = 130)
            : base(positonX, positionY, rectangleWidth, rectangleHeight)
        {
            this.Health = 100;
            this.spriteName = "Pudge";
        }
    }
}
