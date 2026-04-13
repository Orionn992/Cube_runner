using UnityEngine;
using UnityEngine.UI;

public class LevelButtonGems : MonoBehaviour
{
    public int levelIndex;
    public Image[] gems;

    public Sprite fullGemSprite;
    public Sprite darkGemSprite;

    private void Start()
    {
        UpdateGems();
    }

    private void OnEnable()
    {
        UpdateGems();
    }

    void UpdateGems()
    {
        int collected = PlayerPrefs.GetInt("Level_" + levelIndex + "_Gems", 0);

        Debug.Log("Level " + levelIndex + " gems: " + collected);

        for (int i = 0; i < gems.Length; i++)
        {
            if (i < collected)
                gems[i].sprite = fullGemSprite;
            else
                gems[i].sprite = darkGemSprite;
        }
    }
}