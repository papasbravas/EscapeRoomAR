using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI textoPista;
    public Image imagenPieza;

    private HashSet<string> objetosEncontrados = new HashSet<string>();

    // Orden correcto obligatorio
    private string[] ordenObjetos = { "hamburguesa", "waffle", "tartachocolate" };

    // Indica cuál se debe detectar ahora
    private int indiceActual = 0;

    public void MostrarPista(string nombreObjeto)
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

        objetosEncontrados.Add(nombreObjeto); // Marcar como encontrado

        if (nombreObjeto == "hamburguesa") // Pistas específicas
            textoPista.text = "PISTA: la hamburguesa...";
        else if (nombreObjeto == "waffle")
            textoPista.text = "PISTA: su forma...";
        else if (nombreObjeto == "tartachocolate")
            textoPista.text = "PISTA: lo dulce...";
        else
            textoPista.text = "Has encontrado: " + nombreObjeto;

        textoPista.gameObject.SetActive(true);
        imagenPieza.gameObject.SetActive(true);

        indiceActual++;

        if (indiceActual >= ordenObjetos.Length)
            indiceActual = ordenObjetos.Length - 1;
    }
}
