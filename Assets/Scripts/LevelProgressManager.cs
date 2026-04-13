using UnityEngine;
using System;

public class LevelProgressManager : MonoBehaviour
{
    public static LevelProgressManager Instance;

    public int UnlockedLevel { get; private set; }
    public GameObject lockIcon;

    public event Action OnProgressChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadProgress();
    }


    void LoadProgress()
    {
        if (!PlayerPrefs.HasKey("UnlockedLevel"))
        {
            PlayerPrefs.SetInt("UnlockedLevel", 1);
            PlayerPrefs.Save();
        }
        UnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
    }
    

    public void SaveGems(int levelIndex, int collected, int total)
    {
        string collectedKey = "Level_" + levelIndex + "_Collected";
        string totalKey = "Level_" + levelIndex + "_Total";

        int bestCollected = PlayerPrefs.GetInt(collectedKey, 0);

        // сохранение только лучшего результата
        if (collected > bestCollected)
        {
            PlayerPrefs.SetInt(collectedKey, collected);
        }

        PlayerPrefs.SetInt(totalKey, total);
        PlayerPrefs.Save();

        OnProgressChanged?.Invoke();
    }
        public int GetCollectedGems(int levelIndex)
    {
        return PlayerPrefs.GetInt("Level_" + levelIndex + "_Collected", 0);
    }

    public int GetTotalGems(int levelIndex)
    {
        return PlayerPrefs.GetInt("Level_" + levelIndex + "_Total", 0);
    }
    public void UnlockNextLevel(int currentLevel)
    {
        if (currentLevel >= UnlockedLevel)
        {
            UnlockedLevel = currentLevel + 1;

            PlayerPrefs.SetInt("UnlockedLevel", UnlockedLevel);
            PlayerPrefs.Save();

            OnProgressChanged?.Invoke();
        }
    }
   public void ResetProgress()
    {
        UnlockedLevel = 1;

        PlayerPrefs.SetInt("UnlockedLevel", 1);

        for (int i = 1; i <= maxLevel; i++)
        {
            PlayerPrefs.DeleteKey("Level_" + i + "_Gems");
            PlayerPrefs.DeleteKey("Level_" + i + "_Collected");
            PlayerPrefs.DeleteKey("Level_" + i + "_Total");
        }

        PlayerPrefs.Save();

        OnProgressChanged?.Invoke();
    }
    public void UnlockAllLevels(int maxLevel)
    {
        UnlockedLevel = maxLevel;

        PlayerPrefs.SetInt("UnlockedLevel", UnlockedLevel);
        PlayerPrefs.Save();

        OnProgressChanged?.Invoke();
    }
    public int maxLevel = 6;

    public void UnlockAllLevels()
    {
        UnlockedLevel = maxLevel;

        PlayerPrefs.SetInt("UnlockedLevel", UnlockedLevel);
        PlayerPrefs.Save();

        OnProgressChanged?.Invoke();
    }
}