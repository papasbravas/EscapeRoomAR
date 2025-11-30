using System.Collections.Generic;
using UnityEngine;

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

    public void AddTools(string nombre, Sprite icono) // Añadir herramienta al inventario
    {
        if (herramientas.Contains(nombre))  // Evitar duplicados
        {
            Debug.Log("Ya tienes esta herramienta.");
            return;
        }
        herramientas.Add(nombre);

        // Crear el icono del objeto en la interfaz
        GameObject iconoJuego = Instantiate(iconPrefab, iconSlot); // Instanciar el prefab del icono
        iconoJuego.GetComponent<UnityEngine.UI.Image>().sprite = icono; // Asignar el sprite del icono
        Debug.Log("Herramienta " + nombre + " añadida al inventario."); // Mensaje de confirmación
    }

    public bool HasTool(string nombre) // Verificar si el inventario contiene una herramienta
    {
        return herramientas.Contains(nombre); // Retorna true si la herramienta está en el inventario
    }
}
