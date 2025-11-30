using UnityEngine;

public class InteractiveChest : MonoBehaviour
{
//  public Animator animator;  // si quieres animación
    public string herramientaRequerida = "llave";

    private bool abierto = false;

    void OnMouseDown()
    {
        if (abierto) return;

        //string herramientaActual = InventoryManager.Instance.herramientaSeleccionada;

        //if (herramientaActual == herramientaRequerida)
        //{
        //    AbrirCofre();
        //}
        else
        {
            Debug.Log("Necesitas " + herramientaRequerida + " para abrir este cofre.");
        }
    }

    void AbrirCofre()
    {
        abierto = true;

        //if (animator != null)
        //{
        //    animator.SetTrigger("Open");
        //}

        Debug.Log("¡El cofre ha sido abierto!");
    }
}
