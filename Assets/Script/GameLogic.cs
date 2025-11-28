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
    private HashSet<string> objetosEncontrados = new HashSet<string>();

    // Orden correcto obligatorio
    private string[] ordenObjetos = { "hamburguesa", "waffle", "tartachocolate" };

    // Indica cuál se debe detectar ahora
    private int indiceActual = 0;


    private void Start()
    {
        textoPista.gameObject.SetActive(false);
        imagenPieza.gameObject.SetActive(false);
        victoria.SetActive(false);
    }

    public bool EsElObjetoCorrecto(string nombreObjeto)
    {
        return nombreObjeto == ordenObjetos[indiceActual];
    }

    //public void MostrarPista(string nombreObjeto)
    //{
    //    if (nombreObjeto != ordenObjetos[indiceActual]) //  Si no es el objeto correcto
    //    {
    //        textoPista.text = "Este no es el objeto correcto todavía.";
    //        textoPista.gameObject.SetActive(true);
    //        return;
    //    }

    //    if (objetosEncontrados.Contains(nombreObjeto)) // Si ya se encontró antes
    //    {
    //        textoPista.text = "Ya encontraste este objeto.";
    //        textoPista.gameObject.SetActive(true);
    //        return;
    //    }

    //    objetosEncontrados.Add(nombreObjeto); // Marcar como encontrado

    //    if (nombreObjeto == "hamburguesa") // Pistas específicas
    //        textoPista.text = "PISTA: la hamburguesa...";
    //    else if (nombreObjeto == "waffle")
    //        textoPista.text = "PISTA: su forma...";
    //    else if (nombreObjeto == "tartachocolate")
    //        textoPista.text = "PISTA: lo dulce...";
    //    else
    //        textoPista.text = "Has encontrado: " + nombreObjeto;

    //    textoPista.gameObject.SetActive(true);
    //    imagenPieza.gameObject.SetActive(true);

    //    indiceActual++;

    //    if (indiceActual >= ordenObjetos.Length)
    //    {  // Juego completado
    //        indiceActual = ordenObjetos.Length - 1; // Mantener en el último índice
    //        victoria.SetActive(true);
    //        textoPista.gameObject.SetActive(false);
    //        imagenPieza.gameObject.SetActive(false);
    //    }
    //}
    public void MostrarPista(string nombreObjeto)
    {
        if (nombreObjeto != ordenObjetos[indiceActual])
        {
            textoPista.text = "Este no es el objeto correcto todavía.";
            textoPista.gameObject.SetActive(true);
            return;
        }

        if (objetosEncontrados.Contains(nombreObjeto))
        {
            textoPista.text = "Ya encontraste este objeto.";
            textoPista.gameObject.SetActive(true);
            return;
        }

        objetosEncontrados.Add(nombreObjeto);

        // 1. DAR UNA HERRAMIENTA SEGÚN EL OBJETO
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

        // 2. Mostrar UI
        textoPista.gameObject.SetActive(true);
        imagenPieza.gameObject.SetActive(true);

        // 3. Avanzar en orden
        indiceActual++;

        if (indiceActual >= ordenObjetos.Length)
        {
            victoria.SetActive(true);
            textoPista.gameObject.SetActive(false);
            imagenPieza.gameObject.SetActive(false);
        }
    }

}
