using UnityEngine;
using TMPro;

public class VictoryTrigger : MonoBehaviour
{
    public TextMeshProUGUI victoryText; 

    private void Start()
    {
        if (victoryText != null)
            victoryText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter: {other.name} tag={other.tag}");
        if (other.CompareTag("Player"))
        {
            if (victoryText != null)
                victoryText.gameObject.SetActive(true);
        }
    }
}