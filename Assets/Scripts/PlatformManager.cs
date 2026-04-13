using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance;

    public enum PlatformType
    {
        Auto = 0,
        PC = 1,
        Mobile = 2
    }

    public PlatformType currentPlatform;

    void Awake()
    {
        Instance = this;

        int saved = PlayerPrefs.GetInt("PlatformPreset", 0);
        currentPlatform = (PlatformType)saved;
    }

    public bool IsMobile()
    {
#if UNITY_ANDROID || UNITY_IOS
        bool detectedMobile = true;
#else
        bool detectedMobile = false;
#endif

        if (currentPlatform == PlatformType.Auto)
            return detectedMobile;

        return currentPlatform == PlatformType.Mobile;
    }

    public void SetPlatform(int value)
    {
        currentPlatform = (PlatformType)value;

        PlayerPrefs.SetInt("PlatformPreset", value);
        PlayerPrefs.Save();

        Debug.Log("Platform set to: " + currentPlatform);

        // просто перезагружаем сцену (самый стабильный вариант)
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}