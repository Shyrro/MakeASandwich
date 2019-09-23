public class Tomato : Collectible
{
    protected override void Start()
    {
        base.Start();
        ingType = "tomato";
        value = 7;
        id = 2;
    }
}
