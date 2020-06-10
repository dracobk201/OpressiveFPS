using UnityEngine;

public class MainMenuActions : MonoBehaviour
{
    [Header("Data Variables")]
    [SerializeField] private StringReference sceneToChange;
    [SerializeField] private GameEvent changeSceneEvent;

    public void StartLevel()
    {
        sceneToChange.Value = Global.FirstLevelScene;
        changeSceneEvent.Raise();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}