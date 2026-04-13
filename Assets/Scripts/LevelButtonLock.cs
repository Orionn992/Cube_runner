using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;


public class LevelButtonLock : MonoBehaviour{
    public int levelIndex;
    public Button button;
    public GameObject lockIcon;
    public Image buttonImage;


    private void Start()
    {
        UpdateState();

        LevelProgressManager.Instance.OnProgressChanged += UpdateState;
    }

    private void OnDestroy()
    {
        if (LevelProgressManager.Instance != null)
            LevelProgressManager.Instance.OnProgressChanged -= UpdateState;
    }



    void UpdateState()
    {
        int unlocked = LevelProgressManager.Instance.UnlockedLevel;

        bool isUnlocked = levelIndex <= unlocked;

        button.interactable = isUnlocked;

        lockIcon.SetActive(!isUnlocked);

        if (buttonImage != null)
        {
            buttonImage.color = isUnlocked
                ? Color.white
                : new Color(1f, 1f, 1f, 0.5f);
        }
    }


}