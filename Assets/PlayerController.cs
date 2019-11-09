using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float tiempoPoderSalto;

    private Rigidbody2D RB;

    bool isGrounded;
    bool isSpecialGrounded;
    bool isLookRight;
    bool isLookLeft;

    public bool isEnabledPowerAttack;
    public bool isEnabledPowerShot;

    [SerializeField]
    Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RB.gravityScale = 1;

        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("SpecialGround")) && !isSpecialGrounded)
        {
            isSpecialGrounded = true;
            StartCoroutine(CuentaRegresivaPoder(tiempoPoderSalto));
        }
        else if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("SpecialGround")))
        {
            isSpecialGrounded = true;
        }
        else
        {
            isSpecialGrounded = false;
        }

        movimiento();


    }


    //Funcion para los movimientos del personaje
    //Derecha, izquierda, salto
    void movimiento()
    {
        //Tiene el poder de salto largo
        if (Input.GetKeyDown(KeyCode.Space) && ((!isSpecialGrounded && tiempoPoder < tiempoPoderSalto && tiempoPoder > 0) || (isSpecialGrounded)))
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpForce * 2);
        }
        //Salto normal
        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpForce);
        }

        //Mover Izquierda
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RB.velocity = new Vector2(-2, RB.velocity.y);
        }
        //Mover Derecha
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            RB.velocity = new Vector2(2, RB.velocity.y);
        }

        //Atacar Caer si tiene la habilidad
        if (Input.GetKey(KeyCode.DownArrow) && isEnabledPowerAttack)
        {
            RB.velocity = new Vector2(RB.velocity.x, -3);
            RB.gravityScale = 100;
        }

    }
    //Funcion para determinar el tiempo restante del poder de salto
    float tiempoPoder;
    public IEnumerator CuentaRegresivaPoder(float tiempoCuentaAtras)
    {
        tiempoPoder = tiempoCuentaAtras;
        while (tiempoPoder > 0)
        {
            Debug.Log("CuentaAtras: " + tiempoPoder);
            yield return new WaitForSeconds(1.0f);
            tiempoPoder--;
        }
    }
}

