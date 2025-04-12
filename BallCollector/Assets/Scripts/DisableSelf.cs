using UnityEngine;

public class DisableSelf : MonoBehaviour
{
    public float t = 5f;
    private void OnEnable()
    {
        Invoke(nameof(Disable), t);
    }
    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
