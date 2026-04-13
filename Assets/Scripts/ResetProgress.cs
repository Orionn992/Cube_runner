using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    public void ResetLevels()
    {
        LevelProgressManager.Instance.ResetProgress();
    }
}