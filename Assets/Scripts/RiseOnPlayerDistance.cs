using UnityEngine;
using System.Collections;

public class RiseOnPlayerDistance : MonoBehaviour
{
    [Header("Настройки подъёма")]
    public float riseDistance = 3f;      
    public float riseDuration = 2f;     
    public float triggerDistance = 5f;    

    [Header("Ссылки")]
    public Transform player;              

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool isRising = false;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.up * riseDistance;
    }

    void Update()
    {
        if (!isRising && player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= triggerDistance)
            {
                StartCoroutine(RiseRoutine());
            }
        }
    }

    private IEnumerator RiseRoutine()
    {
        isRising = true;
        float elapsed = 0f;
        Vector3 initialPos = transform.position;

        while (elapsed < riseDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / riseDuration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            transform.position = Vector3.Lerp(initialPos, targetPos, smoothT);
            yield return null;
        }

        transform.position = targetPos;
    }
}