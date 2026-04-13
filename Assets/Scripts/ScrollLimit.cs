using UnityEngine;
using UnityEngine.UI;

public class ScrollLimit : MonoBehaviour
{
    public RectTransform content;
    public ScrollRect scrollRect;

    void Start()
    {
        // Пример: устанавливаем высоту контента по количеству элементов
        int elements = content.childCount;
        float elementHeight = 100f; // размер элемента
        float spacing = 10f;        // расстояние между элементами

        float newHeight = elements * (elementHeight + spacing);
        content.sizeDelta = new Vector2(content.sizeDelta.x, newHeight);

        scrollRect.content = content;
    }
}