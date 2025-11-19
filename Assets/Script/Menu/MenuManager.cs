using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Info()
    {
        
    }

    public void Return()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
