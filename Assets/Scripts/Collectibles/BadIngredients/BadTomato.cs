public class BadTomato : Collectible
{

    protected override void Start()
    {
        base.Start();
        ingType = "badTomato";
        value = 7;
        id = 2;
    }
}
