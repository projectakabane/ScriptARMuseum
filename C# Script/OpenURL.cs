using UnityEngine;

public class OpenURL : MonoBehaviour
{
    [SerializeField] private string url = "https://minilemon.id/";

    public void OpenWebURL()
    {
        Application.OpenURL(url);
    }
}
