using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;

    float maxHP = 100f;
    float hp;
    float maxMana = 100f;
    float mana;

    void Start()
    {
        hp = maxHP;
        mana = maxMana;
    }

    void Update()
    {
        Damage(.1f);
    }

    public void Damage(float dmg)
    {
        hp -= dmg;
        if(hp <= 0) 
        {
            GameOver();
        }
    }

    public void ManaUse(float m)
    {
        if(m <= mana)
        {
            mana -= m;
        }
    }

    public float GetHealth()
    {
        return hp;
    }

    public float GetMaxHealth()
    {
        return maxHP;
    }

    public float GetMana()
    {
        return mana;
    }

    public float GetMaxMana()
    {
        return maxMana;
    }

    void GameOver()
    {
        Debug.Log("Game Over!!");
    }
}
