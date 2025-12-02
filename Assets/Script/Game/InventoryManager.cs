using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Interfaz de Inventario")]
    public Transform iconSlot; // Contenedor para los iconos de los objetos
    public GameObject iconPrefab; // Prefab del icono del objeto

    private List<string> herramientas = new List<string>(); // Lista de nombres de herramientas en el inventario

    private void Awake()
    {
        Instance = this;
    }

    public void AddTools(string nombre, Sprite icono)
    {
        if (herramientas.Contains(nombre))
        {
            Debug.Log("Ya tienes esta herramienta.");
            return;
        }

        herramientas.Add(nombre);

        GameObject iconoJuego = Instantiate(iconPrefab, iconSlot);
        iconoJuego.GetComponent<Image>().sprite = icono;

        Debug.Log("Herramienta " + nombre + " añadida al inventario.");
    }


    public bool HasTool(string nombre) // Verificar si el inventario contiene una herramienta
    {
        return herramientas.Contains(nombre); // Retorna true si la herramienta está en el inventario
    }
}
