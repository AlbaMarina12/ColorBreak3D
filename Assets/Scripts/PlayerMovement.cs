using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;
    public float limiteX = 5f;
    public bool avanceAutomatico = true;

    private Rigidbody rb;
    private bool enSuelo = true;
    private Renderer playerRenderer;
    private ScoreManager scoreManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerRenderer = GetComponent<Renderer>();
        scoreManager = FindObjectOfType<ScoreManager>();

        CambiarColor(); // color inicial
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        Vector3 velocidad = new Vector3(moveX * speed, rb.linearVelocity.y, speed);

        if (!avanceAutomatico)
        {
            velocidad.z = Input.GetAxis("Vertical") * speed;
        }

        rb.linearVelocity = velocidad;

        // salto
        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            enSuelo = false;
        }

        // límite lateral
        if (transform.position.x > limiteX)
        {
            transform.position = new Vector3(limiteX, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -limiteX)
        {
            transform.position = new Vector3(-limiteX, transform.position.y, transform.position.z);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        ProcesarChoque(collision.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        ProcesarChoque(other.gameObject);
    }

    void ProcesarChoque(GameObject objeto)
    {
        if (objeto.CompareTag("Ground"))
        {
            enSuelo = true;
            return;
        }

        if (objeto.CompareTag("ColorWall"))
        {
            RomperPared(objeto);
        }
    }

    void RomperPared(GameObject pared)
    {
        if (scoreManager != null)
        {
            scoreManager.AddScore(1);
        }

        Destroy(pared);
        CambiarColor();
    }

    void CambiarColor()
    {
        int colorRandom = Random.Range(0, 3);

        if (colorRandom == 0)
        {
            playerRenderer.material.color = Color.red;
        }
        else if (colorRandom == 1)
        {
            playerRenderer.material.color = Color.yellow;
        }
        else
        {
            playerRenderer.material.color = Color.blue;
        }
    }
}