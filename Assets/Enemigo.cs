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

    public void TomarDa�o(float da�o)
    {
        vida -= da�o;
        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        animator.SetTrigger("DeathTrigger");
        // No destruimos aqu� para esperar al evento de la animaci�n
    }

    // Este m�todo ser� llamado desde el Animation Event en el �ltimo frame de la animaci�n DeathRat
    public void DestruirObjeto()
    {
        Destroy(gameObject);
    }
}
