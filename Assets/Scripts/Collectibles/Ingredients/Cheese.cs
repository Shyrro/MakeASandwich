public class Cheese : Collectible
{

    protected override void Start()
    {
        base.Start();
        ingType = "cheese";
        value = 8;
        id = 5;
    }

}
