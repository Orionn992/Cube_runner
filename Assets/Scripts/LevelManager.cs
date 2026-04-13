using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int collectedGems = 0;
    public int totalGems = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void CollectGem(int amount)
    {
        collectedGems += amount;
    }

    public void RegisterGem()
    {
        totalGems++;
    }
}