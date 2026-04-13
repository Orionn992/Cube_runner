using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class VictoryTrigger : MonoBehaviour
{
    public TextMeshProUGUI victoryText;
    public int currentLevelIndex = 1;

    // Размеры финишной зоны для проверки через координаты
    public Vector3 zoneSize = new Vector3(16f, 10f, 1f);

    private bool victoryAchieved = false;

    private void Start()
    {
        if (victoryText != null)
            victoryText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (victoryAchieved) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Vector3 center = transform.position; // центр линии
        Vector3 playerPos = player.transform.position;

        // Проверка попадания в прямоугольную зону
        if (Mathf.Abs(playerPos.x - center.x) <= zoneSize.x / 2 &&
            Mathf.Abs(playerPos.y - center.y) <= zoneSize.y / 2 &&
            Mathf.Abs(playerPos.z - center.z) <= zoneSize.z / 2)
        {
            TriggerVictory();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (victoryAchieved) return;

        if (other.CompareTag("Player"))
        {
            TriggerVictory();
        }
    }

private void TriggerVictory()
{
    victoryAchieved = true;

    if (victoryText != null)
        victoryText.gameObject.SetActive(true);

    int collected = LevelManager.Instance.collectedGems;
    collected = Mathf.Clamp(collected, 0, 3);

    string key = "Level_" + currentLevelIndex + "_Gems";
    int old = PlayerPrefs.GetInt(key, 0);

    if (collected > old)
    {
        PlayerPrefs.SetInt(key, collected);
    }
    LevelProgressManager.Instance.UnlockNextLevel(currentLevelIndex);

    PlayerPrefs.Save();

    StartCoroutine(GoToMenu());
}

    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(2f); // эти 2 секунды
        SceneManager.LoadScene("MainMenu");
    }
}