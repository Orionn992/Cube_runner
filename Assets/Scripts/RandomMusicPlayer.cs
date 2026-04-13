using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomMusicPlayer : MonoBehaviour
{
    public static RandomMusicPlayer Instance;

    public AudioClip[] playlist;
    private AudioSource audioSource;
    private int lastIndex = -1;

    void Awake()
    {
        // Singleton: если уже есть объект музыки, уничтожаем новый
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return; // прекратить выполнение Awake для нового объекта
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        PlayRandomTrack();
    }

    void Update()
    {
        if (!Application.isFocused) return;

        if (audioSource.clip != null &&
            audioSource.time >= audioSource.clip.length - 0.1f)
        {
            PlayRandomTrack();
        }
    }

    void PlayRandomTrack()
    {
        if (playlist.Length == 0) return;

        int newIndex;
        do
        {
            newIndex = Random.Range(0, playlist.Length);
        }
        while (playlist.Length > 1 && newIndex == lastIndex);

        lastIndex = newIndex;
        audioSource.clip = playlist[newIndex];
        audioSource.Play();
    }
    
}