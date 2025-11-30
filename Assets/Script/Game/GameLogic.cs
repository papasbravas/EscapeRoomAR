using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI textoPista;
    public Image imagenPieza;
    public GameObject victoria;

    [Header("Herramientas por objeto")]
    public Sprite iconoHamburguesa;
    public Sprite iconoWaffle;
    public Sprite iconoTarta;
    private HashSet<string> objetosEncontrados = new HashSet<string>(); // Objetos ya encontrados

    // Orden correcto obligatorio
    private string[] ordenObjetos = { "hamburguesa", "waffle", "tartachocolate" };

    // Indica cuál se debe detectar ahora
    private int indiceActual = 0;


    private void Start() // Inicialización
    {
        textoPista.gameObject.SetActive(false);
        imagenPieza.gameObject.SetActive(false);
        victoria.SetActive(false);
    }

    public bool EsElObjetoCorrecto(string nombreObjeto) // Verifica si el objeto es el correcto en el orden
    {
        return nombreObjeto == ordenObjetos[indiceActual];
    }

    public void MostrarPista(string nombreObjeto) // Versión con entrega de herramientas
    {
        if (nombreObjeto != ordenObjetos[indiceActual]) //  Si no es el objeto correcto
        {
            textoPista.text = "Este no es el objeto correcto todavía.";
            textoPista.gameObject.SetActive(true);
            return;
        }

        if (objetosEncontrados.Contains(nombreObjeto)) // Si ya se encontró antes
        {
            textoPista.text = "Ya encontraste este objeto.";
            textoPista.gameObject.SetActive(true);
            return;
        }

        objetosEncontrados.Add(nombreObjeto);

        // 1. ENTREGAR HERRAMIENTA CORRESPONDIENTE
        if (nombreObjeto == "hamburguesa")
        {
            textoPista.text = "PISTA: has obtenido...";
            InventoryManager.Instance.AddTools("llave", iconoHamburguesa);
        }
        else if (nombreObjeto == "waffle")
        {
            textoPista.text = "PISTA: has obtenido...";
            InventoryManager.Instance.AddTools("lupa", iconoWaffle);
        }
        else if (nombreObjeto == "tartachocolate")
        {
            textoPista.text = "PISTA: has obtenido...";
            InventoryManager.Instance.AddTools("linterna", iconoTarta);
        }

        // 2. MOSTRAR PISTA
        textoPista.gameObject.SetActive(true);
        imagenPieza.gameObject.SetActive(true);

        // 3. AVANZAR AL SIGUIENTE OBJETO
        indiceActual++;

        if (indiceActual >= ordenObjetos.Length)
        {
            victoria.SetActive(true);
            textoPista.gameObject.SetActive(false);
            imagenPieza.gameObject.SetActive(false);
        }
    }

}
