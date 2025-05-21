using System;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        animator.SetTrigger("DeathTrigger");
        // No destruimos aquí para esperar al evento de la animación
    }

    // Este método será llamado desde el Animation Event en el último frame de la animación DeathRat
    public void DestruirObjeto()
    {
        Destroy(gameObject);
    }
}
