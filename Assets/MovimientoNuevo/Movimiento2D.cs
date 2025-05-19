using UnityEngine;
using UnityEngine.Rendering;

public class Movimiento2D : MonoBehaviour
{
    public Controles controles;

    public Vector2 direccion;

    public Rigidbody2D rb2d;

    public float velocidadMovimiento;

    public bool mirandoDerecha = true;

    public float fuerzaSalto;

    public LayerMask queEsSuelo;
    public Transform controladorSuelo;
    public Vector3 dimensionesCaja;
    public bool enSuelo;
    [Header("Animacion")]
    private Animator animator;
    private bool puedeMoverse = true;
    private bool estaAtacando = false;


    private void Awake()
    {
        controles = new();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        controles.Enable();
        controles.Movimiento.Saltar.started += _ => Saltar();
        controles.Movimiento.Atacar.started += _ => Atacar();
    }

    private void OnDisable()
    {
        controles.Disable();
        controles.Movimiento.Saltar.started -= _ => Saltar();
        controles.Movimiento.Atacar.started -= _ => Atacar();

    }

    private void Update()
    {
        direccion = controles.Movimiento.Mover.ReadValue<Vector2>();

        if (!estaAtacando)
        {
            AjustarRotacion(direccion.x);
        }

        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);

        animator.SetFloat("Horizontal", Mathf.Abs(direccion.x));
        animator.SetFloat("VelocidadY", rb2d.linearVelocityY);
    }

    private void FixedUpdate()
    {
        if (puedeMoverse)
        {
            rb2d.linearVelocity = new Vector2(direccion.x * velocidadMovimiento, rb2d.linearVelocity.y);
        }
        else
        {
            rb2d.linearVelocity = new Vector2(0, rb2d.linearVelocity.y);
        }

        animator.SetBool("enSuelo", enSuelo);
    }


    private void AjustarRotacion(float direccionX)
    {
        if (direccionX > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (direccionX < 0 && mirandoDerecha)
        {
            Girar();
        }
    }


    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void Saltar()
    {
        if(enSuelo)
        {
            rb2d.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
        }
    }

    private void Atacar()
    {
        if (estaAtacando || !enSuelo)
            return;

        estaAtacando = true;
        puedeMoverse = false;
        animator.SetTrigger("Atacar");

        // Esperar duración del ataque (ajústalo al tiempo real de la animación)
        Invoke(nameof(FinAtaque), 0.5f);
    }
    private void FinAtaque()
    {
        estaAtacando = false;
        puedeMoverse = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
}
