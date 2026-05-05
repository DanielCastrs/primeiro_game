using UnityEngine;

public class PlayerMovimento : MonoBehaviour
{
   



    public float velocidade = 5f;
    private Rigidbody2D rb;
    private float movimento;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Captura input do teclado (A/D ou setas)
        movimento = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        // Aplica o movimento no eixo X
        rb.linearVelocity = new Vector2(movimento * velocidade, rb.linearVelocity.y);
    }
}

