using UnityEngine;

public class CollectibleGem : MonoBehaviour
{
    public int value = 1;

    private void Start()
    {
        LevelManager.Instance.RegisterGem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.CollectGem(value);
            Destroy(gameObject);
        }
    }
}