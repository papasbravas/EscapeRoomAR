using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Paneles del Menú")]
    [SerializeField] private GameObject panelPrincipal;
    [SerializeField] private GameObject panelAyuda;

    public void Start()
    {
        if (panelPrincipal != null)
        {
            panelPrincipal.SetActive(true);
        }
        panelAyuda.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Info()
    {
        if (panelPrincipal != null)
        {
            panelPrincipal.SetActive(false);
        }
        panelAyuda.SetActive(true);
    }

    public void Return()
    {
        panelPrincipal.SetActive(true);
        panelAyuda.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Saliendo del juego...");

        // Directiva de preprocesador
        #if UNITY_EDITOR
                // Si estamos en el editor de Unity, usamos el comando para detener el juego.
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                        // Si estamos en un ejecutable (Build), cerramos la aplicación.
                        Application.Quit();
        #endif
    }
}
