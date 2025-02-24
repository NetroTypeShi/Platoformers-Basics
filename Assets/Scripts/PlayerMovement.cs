using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float maxAirAcceleration = 8;
    [SerializeField] float maxGroundAcceleration = 5;
    [SerializeField] float newSpeedX = 8;
    [SerializeField] float objetiveSpeed = 10;
    [SerializeField] float pressingTime = 0f;
    [SerializeField] ParticleSystem jumpParticles;
    [SerializeField] ParticleSystem bloodParticles;
    [SerializeField] ParticleSystem landParticlesRight;
    [SerializeField] ParticleSystem landParticlesLeft;
    [SerializeField] ParticleSystem followParticles;
    [SerializeField] bool landing;
    MovementStats stats;
    ParticleSystem.MainModule particleSystemMain;
    SpriteRenderer rend;
    public Vector2 movementImput;
    [SerializeField] Gradient colorGrd;
    Color newColor;
    Rigidbody2D rb;
    [SerializeField] public bool isOnFloor = false;
    // Variable para almacenar el estado del suelo del frame anterior.
    bool previousOnFloor = false;
    bool wasOnFloor = false;
    [SerializeField] LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = movementImput;        
        rend = GetComponent<SpriteRenderer>();
        particleSystemMain = followParticles.main;
        particleSystemMain.startColor = Color.white;
    }

    void Update()
    {
        print(pressingTime);                     
        Movement();
        RestartFloats();
        ColorChange();
        
    }

    private void FixedUpdate()
    {
        GravityScale();
        ParticleColorChange();
        
        rb.velocity += movementImput * Time.deltaTime * stats.acceleration;

        if (isOnFloor == false) { LandParticlesOff(); }
        previousOnFloor = wasOnFloor;
        
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);
        isOnFloor = (hit);

       
        if (!previousOnFloor && isOnFloor && rb.velocity.y < -1f)
        {
            LandParticlesOn();
            
            Invoke("LandParticlesOff", 1f);
        }
        
        
        wasOnFloor = isOnFloor;

        
        if (isOnFloor)
        {
            stats.onAirJumps = stats.defaultJumpsNumber;
            rb.gravityScale = stats.defaultGravity;
            stats.acceleration = stats.groundAcceleration;
            
            if (movementImput.x == 0)
            {
                newSpeedX = Mathf.MoveTowards(rb.velocity.x, 0, stats.groundFriction * Time.fixedDeltaTime);
            }
            else
            {
                newSpeedX = Mathf.MoveTowards(rb.velocity.x, objetiveSpeed * movementImput.x, stats.acceleration * Time.fixedDeltaTime);
            }

            rb.velocity = new Vector2(newSpeedX, rb.velocity.y);

            newSpeedX = Mathf.MoveTowards(rb.velocity.x, objetiveSpeed * movementImput.x, stats.groundAcceleration * Time.fixedDeltaTime);
            if (stats.groundAcceleration >= maxGroundAcceleration)
            {
                if (Mathf.Abs(rb.velocity.x) > maxAirAcceleration)
                {
                    rb.velocity = new Vector2((rb.velocity.x < 0 ? -1 : 1) * maxAirAcceleration, rb.velocity.y);
                }
            }
        }
    }

    void Movement()
    {
        movementImput = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
            movementImput += Vector2.left;

        if (Input.GetKey(KeyCode.D))
            movementImput += Vector2.right;

        if (stats.onAirJumps > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pressingTime = 0f;
                stats.onAirJumps--;
                ColorChange();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (pressingTime < stats.maxJumpTime)
                {
                    pressingTime += Time.deltaTime;
                    rb.velocity = new Vector2(rb.velocity.x, stats.jumpStrength);
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
                bloodParticles.Play();
        }
    }

    void RestartFloats()
    {
        if (isOnFloor)
        {
            stats.jumpStrength = 5f;
            particleSystemMain.startColor = Color.white;

            if (Input.GetKeyDown(KeyCode.Space))
                jumpParticles.Play();
        }
    }

    void ColorChange()
    {
        rend.color = colorGrd.Evaluate(1f * stats.onAirJumps / stats.defaultJumpsNumber);
        print(stats.onAirJumps + " -> " + (1f * stats.onAirJumps / stats.defaultJumpsNumber));
    }

    
    void LandParticlesOn()
    {
        landParticlesLeft.Play();
        landParticlesRight.Play();
        print("Landing particles activated");
    }

    
    void LandParticlesOff()
    {
        landParticlesLeft.Stop();
        landParticlesRight.Stop();
        print("Landing particles deactivated");
    }

    public void SetStats(MovementStats stats)
    {
        this.stats = stats;
        print("player received stats " + stats.name);
    }

    void GravityScale()
    {
        if (isOnFloor)
        {
            rb.gravityScale = stats.defaultGravity;
        }
        else if (rb.velocity.y >= -1f && rb.velocity.y < 1f)
        {
            rb.gravityScale = stats.peakGravity;
        }
        else if (rb.velocity.y >= -1f)
        {
            rb.gravityScale = stats.defaultGravity;
        }
        else
        {
            rb.gravityScale = stats.fallingGravity;
            if (isOnFloor)
            {
                jumpParticles.Play();
            }
        }
    }

    void ParticleColorChange()
    {
        if (rb.gravityScale == stats.peakGravity)
        {
            particleSystemMain.startColor = Color.green;
        }
        if (rb.gravityScale == stats.defaultGravity)
        {
            particleSystemMain.startColor = Color.white;
        }
        if (rb.gravityScale == stats.fallingGravity)
        {
            particleSystemMain.startColor = Color.blue;
        }
    }
}
