using UnityEngine;

public static class AbilitiesSaver
{
    

    public static void Load(int money, float speed, float jumpForce, int health)
    {
        money = PlayerPrefs.GetInt("Money");
        speed = PlayerPrefs.GetInt("Speed");
        jumpForce = PlayerPrefs.GetInt("JumpForce");
        health = PlayerPrefs.GetInt("Health");
    }

    public static void ResetAbilities()
    {
        PlayerPrefs.DeleteAll();
    }
}
