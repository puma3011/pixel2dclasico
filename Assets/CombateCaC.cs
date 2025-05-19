using System;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;

    [SerializeField] private float radioGolpe;

    [SerializeField] private float dañoGolpe;

    [SerializeField] private float tiempoEntreAtaques;

    [SerializeField] private float tiempoSiguienteAtaque;

    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (tiempoSiguienteAtaque > 0) { 
            tiempoSiguienteAtaque -= Time.deltaTime;
        }
            
            

        if (Input.GetButtonDown("Fire1") && tiempoSiguienteAtaque <= 0)
        {
            Golpe();
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }
    }
    private void Golpe()
    {
        animator.SetTrigger("Golpe");
        
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
        foreach (Collider2D colisionador in objetos)
        {
            Debug.Log("Buscando Enemigo");
            if (colisionador.CompareTag("Enemigo"))
            {
                Debug.Log("Detectando Enemigo");
                colisionador.transform.GetComponent<Enemigo>().TomarDaño(dañoGolpe);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }

}
