using UnityEngine;

public class LoadingBehaviour : MonoBehaviour
{
    [Header("Script Variables")]
    [SerializeField] private GameObject loadingPanel;
    private bool isShowing;

    public void ShowingLoading()
    {
        isShowing = !isShowing;
        loadingPanel.SetActive(isShowing);
    }
}