using UnityEngine;

public class UIManager : MonoBehaviour
{
    private bool isPaused = false;
    public bool IsPaused { get { return isPaused; } }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused) Pause();
        else Resume();
    }

    public void EschapPerformed()
    {
        // TODO: si inv, close inv au lieu de pause
        TogglePause();
    }
}
