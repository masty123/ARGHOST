using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{

    [SerializeField] private float fillAmount;
    [SerializeField] private Image hp;
    private PlayerManager player;

    void Start()
    {
        player = PlayerManager.instance;
    }


    // Update is called once per frame
    void Update()
    {
        HandleHPBar();
    }

    private void HandleHPBar()
    {
        Debug.Log("HP = " + player.GetHealth());
        hp.fillAmount = Map(player.GetHealth(), 0, player.GetMaxHealth(), 0, 1);
    }

    // Turn stat into fillAmount for content
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
