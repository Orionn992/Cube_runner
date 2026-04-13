using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel(int index)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(index);
    }
public void LoadMainMenu()
{
    Time.timeScale = 1f; 
    SceneManager.LoadScene("MainMenu");
}
}