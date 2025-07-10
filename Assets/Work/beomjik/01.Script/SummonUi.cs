using UnityEngine;
using UnityEngine.InputSystem;

public class SummonUi : MonoBehaviour
{
    public void SetActive()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void SetDisable()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            SetDisable();
    }
}
