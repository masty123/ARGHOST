
public class HealthBar : BarAbstract
{
    private PlayerManager player;

    void Start()
    {
        player = PlayerManager.instance;
        statType = "HP";
    }

    // override GetValue() to get HP value
    protected override void GetValue()
    {
        current = player.GetHealth();
        max = player.GetMaxHealth();
    }
}
