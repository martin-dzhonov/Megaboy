namespace Main
{
    class DrowRanger : Ranged
    {
         public DrowRanger(int positonX, int positionY, int rectangleWidth = 50, int rectangleHeight = 50)
            : base(positonX, positionY, rectangleWidth, rectangleHeight)
        {
            this.Health = 2;
            this.spriteName = "DrowRanger";
        }
    }
}
