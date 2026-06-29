using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionsAsset;
    private InputActionMap playerInputMap;

    private bool isPaused = false;
    public bool IsPaused { get { return isPaused; } }

    private void Start()
    {
        if (inputActionsAsset == null) { Debug.LogError($"[{gameObject.name}] InputActionAsset is not assigned in the inspector."); }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerInputMap = inputActionsAsset.FindActionMap("Player");
    }

    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        AudioListener.volume = 0.5f; // 0.5* volume based on settings
        playerInputMap.Disable();
        isPaused = true;
    }

    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        AudioListener.volume = 1f; // TODO: adjust volume based on settings
        playerInputMap.Enable();
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
