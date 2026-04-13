using UnityEngine;
using System.Collections;

public class MoveOnPlayerDistance : MonoBehaviour
{
    [Header("Настройки движения")]
    public float moveDistance = 1f;
    public float moveDuration = 0.1f;
    public float triggerDistance = 5f;

    [Header("Направление движения")]
    public Vector3 moveDirection = Vector3.right;

    [Header("Ссылки")]
    public Transform player;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool isMoving = false;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + moveDirection.normalized * moveDistance;
    }

    void Update()
    {
        if (!isMoving && player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= triggerDistance)
            {
                StartCoroutine(MoveRoutine());
            }
        }
    }

    private IEnumerator MoveRoutine()
    {
        isMoving = true;
        float elapsed = 0f;
        Vector3 initialPos = transform.position;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            transform.position = Vector3.Lerp(initialPos, targetPos, smoothT);
            yield return null;
        }

        transform.position = targetPos;
    }
}