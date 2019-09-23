public class Meat : Collectible
{

    protected override void Start()
    {
        base.Start();
        ingType = "meat";
        value = 10;
        id = 1;
    }
}
