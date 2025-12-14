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

    private HashSet<string> objetosEncontrados = new HashSet<string>();

    // Orden correcto obligatorio
    private string[] ordenObjetos = { "hamburguesa", "waffle", "tartachocolate", "helado" };

    // Indica cuál se debe detectar ahora
    private int indiceActual = 0;

    private void Start()
    {
        textoPista.gameObject.SetActive(false);
        imagenPieza.gameObject.SetActive(false);
        victoria.SetActive(false);
    }

    public bool EsElObjetoCorrecto(string nombreObjeto) // Verifica si es el objeto correcto en el orden
    {
        return nombreObjeto == ordenObjetos[indiceActual];
    }

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
            textoPista.text = "PISTA: dulce y cuadrado";
        else if (nombreObjeto == "waffle")
            textoPista.text = "PISTA: amada en cumpleaños";
        else if (nombreObjeto == "tartachocolate")
            textoPista.text = "PISTA: lo dulce lleva al postre";
        else if (nombreObjeto == "helado")
            textoPista.text = "PISTA: ";
        else
            textoPista.text = "Has encontrado: " + nombreObjeto;

        textoPista.gameObject.SetActive(true);
        imagenPieza.gameObject.SetActive(true);

        indiceActual++;

        if (indiceActual >= ordenObjetos.Length)
        {  // Juego completado
            indiceActual = ordenObjetos.Length - 1; // Mantener en el último índice
            victoria.SetActive(true);
            textoPista.gameObject.SetActive(false);
            imagenPieza.gameObject.SetActive(false);
        }
    }
}

