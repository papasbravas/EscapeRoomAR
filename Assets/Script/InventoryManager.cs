using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Interfaz de Inventario")]
    public Transform iconSlot; // Contenedor para los iconos de los objetos
    public GameObject iconPrefab; // Prefab del icono del objeto

    private List<string> herramientas = new List<string>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddTools(string nombre, Sprite icono)
    {
        if(herramientas.Contains(nombre)) 
        {
            Debug.Log("Ya tienes esta herramienta.");
            return;
        }
        herramientas.Add(nombre);

        // Crear el icono del objeto en la interfaz
        GameObject iconoJuego = Instantiate(iconPrefab, iconSlot);
        iconoJuego.GetComponent<UnityEngine.UI.Image>().sprite = icono;
        Debug.Log("Herramienta " + nombre + " añadida al inventario.");
    }
    
    public bool HasTool(string nombre)
    {
        return herramientas.Contains(nombre);
    }
}
