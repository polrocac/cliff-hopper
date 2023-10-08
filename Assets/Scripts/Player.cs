using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Aplastable
{
    // Start is called before the first frame update

    private int direction = 1;
    public float jumpVel;
    private float velY;

    [System.NonSerialized]
    public float velHorizontal;
    private float normalVel;

    public float gravity;
    public int maxJumps;
    private int jumps;
    private bool bJumping = false;

    private bool inCorner = false;

    private float correctionVel = 0.1f;

    Rigidbody rg;

    private Platform lastCorner;

    private float time;
    public float rotationTime;
    private int initialDirection;

    public Animator animator;

    public UnityEvent OnCornerLit;

    private bool muelto = false;

    private bool god = false;

    private readonly float jumpCD = 0.1f;
    private float jumpRCD;

    private int platPisadas = 0;

    void Start()
    {
        jumps = maxJumps;
        rg = GetComponent<Rigidbody>();
        OnCornerLit.AddListener(GameManager.Instance.IncreaseCorners);
        OnCornerLit.AddListener(UIManager.Instance.UpdateCornerText);
        time = 0f;
        initialDirection = direction;
        normalVel = velHorizontal;

        jumpRCD = 0;
    }

    void Update()
    {

        transform.localEulerAngles = new Vector3(0, Mathf.LerpAngle(90 * (1 - initialDirection), 90 * (1 - direction), time/rotationTime), 0);
        time += Time.deltaTime;

        if (muelto) return;

        //Debug.Log(CoordManager.toCHCoords(transform.position));
        if (Input.GetButtonDown("Jump"))
        {
            if (inCorner)
            {
                Girar();
            }
            else if (jumps > 0)
            {
                Salto();
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            god = !god;
            if (god) GetComponent<MeshRenderer>().material.color = Color.yellow;
            else GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }

    void FixedUpdate()
    {
        jumpRCD -= Time.fixedDeltaTime;
        if (god) velHorizontal = normalVel;
        if (bJumping)
        {
            velY += gravity * Time.fixedDeltaTime;
            transform.position += new Vector3(0, velY, 0) * Time.fixedDeltaTime;
        }
        transform.position += new Vector3(1 - direction, 0, direction) * velHorizontal * Time.fixedDeltaTime;

        if (muelto) return;

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 2;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;

        // Hay que mirar antes este porque si estas encima de escalera con bJumping=false se hace snap, y no estas cayendo aun que estes flotando un poco
        // RAYO 2: MIRAR SI ESTAS BAJANDO ESCALERA
        if (!bJumping && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask) && hit.collider.CompareTag("Rampa"))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.green);
            //GetComponent<MeshRenderer>().material.color = Color.blue;
            // transform.localScale.y porque el collider mide 2 veces eso
            transform.position += Vector3.down * (hit.distance - transform.localScale.y);
        }
        // RAYO 1: MIRAR SI ESTAS TOCANDO SUELO
        // Does the ray intersect any objects excluding the player layer
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, transform.localScale.y + 0.001f, layerMask) && (hit.collider.CompareTag("Platform") || hit.collider.CompareTag("Rampa") || hit.collider.CompareTag("Meta")))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
            //Debug.Log("Did Hit");
            //GetComponent<MeshRenderer>().material.color = Color.red;
            // transform.localScale.y porque el collider mide 2 veces eso
            transform.position += Vector3.down * (hit.distance - transform.localScale.y);
            if (bJumping)
                Suelo();

        }
        else
        {
            //Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask);
            //Debug.Log("Did not Hit");
            //GetComponent<MeshRenderer>().material.color = Color.green;
            if (!bJumping)
                Caer();
        }

        if (god)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1, layerMask))
            {
                if (hit.collider.CompareTag("Fog"))
                {
                    Salto();
                }
            }
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.CompareTag("Platform"))
                {
                    Platform p = hit.collider.transform.parent.GetComponent<Platform>();

                    //Esto es para cuando estas en el aire
                    if (p.Trampa == Trampas.CORNER && lastCorner != p)
                    {
                        inCorner = true;
                        lastCorner = hit.collider.transform.parent.GetComponent<Platform>();
                    }
                    else if (p.Trampa == Trampas.LENTO && hit.distance < transform.localScale.y + 0.05f)
                    {
                        Salto();
                    }
                }
                else if (hit.collider.CompareTag("Trap") && hit.distance < transform.localScale.y + 0.05f)
                {
                    Salto();
                }
            }

            Debug.DrawRay(transform.position + Vector3.down * 0.4f, transform.forward * 1f, Color.red);
            //Rayo desde parte baja hacia delante
            if (Physics.Raycast(transform.position + Vector3.down * 0.4f, transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                if ((hit.collider.CompareTag("Trap") || hit.collider.CompareTag("Pinguin")) && hit.distance < 1.5f || (hit.collider.CompareTag("Fuego") && hit.distance < 3f))
                {
                    Salto();
                }
            }

        }


        //// RAYO 3: MIRAR SI CHOCAS (TRUCAZO CHAVAL) DE FRENTE
        //Debug.DrawRay(transform.position, transform.forward * 0.5f, Color.cyan);
        //if (Physics.Raycast(transform.position, transform.forward, out hit, 0.5f, layerMask))
        //{
        //    if (hit.collider.tag == "Rampa")
        //    {
        //        GetComponent<MeshRenderer>().material.color = Color.blue;
        //        transform.position += Vector3.down * (hit.distance - transform.localScale.y);
        //    }
        //}

        int targetX = Mathf.RoundToInt(transform.position.x);
        int targetZ = Mathf.RoundToInt(transform.position.z);
        if (direction == 0) // direccion +X
        {
            if (transform.position.z < targetZ)
            {
                float newZPos = Mathf.Min(transform.position.z + correctionVel, targetZ);
                transform.position = new Vector3(transform.position.x, transform.position.y, newZPos);
            }
            else if (transform.position.z > targetZ)
            {
                float newZPos = Mathf.Max(transform.position.z - correctionVel, targetZ);
                transform.position = new Vector3(transform.position.x, transform.position.y, newZPos);
            }

            //Giro automatico
            if (god && inCorner && transform.position.x > targetX)
            {
                Girar();
            }
        }
        else // direccion +Z
        {
            int target = Mathf.RoundToInt(transform.position.x);
            if (transform.position.x < target)
            {
                float newXPos = Mathf.Min(transform.position.x + correctionVel, target);
                transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > target)
            {
                float newXPos = Mathf.Max(transform.position.x - correctionVel, target);
                transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);
            }

            //Giro automatico
            if (god && inCorner && transform.position.z > targetZ)
            {
                Girar();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (muelto) return;

        //if (other.tag == "Platform")
        //{
        //    Suelo();
        //}

        //if (other.tag == "Corner")
        //{
        //    EnterCorner(other.gameObject.transform.parent.GetComponent<Platform>());
        //}

        if (other.CompareTag("Trap") || other.CompareTag("Fog"))
        {
            Muelto(other.CompareTag("Trap"));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (muelto) return;

        //if (other.tag == "Corner" && inCorner)
        //{
        //    ExitCorner(other.GetComponent<Platform>());
        //}
    }

    public void ExitCorner(Platform corner)
    {
        inCorner = false;
        bJumping = true;
        lastCorner = corner;
    }
    public void EnterCorner(Platform corner)
    {
        if (lastCorner != corner)
        {
            inCorner = true;
            lastCorner = corner;
        }
    }

    private void Salto()
    {
        if (jumpRCD > 0) return;

        velY = jumpVel;
        //rg.velocity = new Vector3(0, jumpVel, 0);
        jumps--;
        bJumping = true;
        //inCorner = false;

        jumpRCD = jumpCD;

        if (muelto)
        {
            jumps = 0;
            return;
        }
        float rand = Random.value;
        if(rand <= 0.5) SoundManager.Instance.SelectAudio(0, 0.5f);
        else SoundManager.Instance.SelectAudio(1, 0.5f);
    }

    private void Suelo()
    {
        bJumping = false;
        velY = 0;

        if (!muelto) jumps = maxJumps;
    }

    private void Caer()
    {
        inCorner = false;
        bJumping = true;
    }

    public void Muelto(bool salto = true)
    {
        if (god) return;
        Debug.Log("Muelto");
        muelto = true;
        if (salto)
        {
            Salto();
            Girar();
        }
        SoundManager.Instance.SelectAudio(6, 0.5f);
        UpdateHighScore();
        UIManager.Instance.ActivarMenuMuerte();
    }

    public void UpdateHighScore()
    {
        GameManager.Instance.setHighscore(platPisadas);
    }

    private void Girar()
    {
        initialDirection = direction;
        direction = 1 - direction;
        time = 0f;
        inCorner = false;
        if (muelto) return;
        ++platPisadas;
        lastCorner.setGlow(true);
        LevelGenerator.Instance.ChangeBioma(lastCorner.Bioma, 1f);
        OnCornerLit?.Invoke();
        SoundManager.Instance.SelectAudio(2, 0.5f);

    }

    public float getJumpVel()
    {
        return jumpVel;
    }

    public void setJumpVel(float jumpVel)
    {
        this.jumpVel = jumpVel;
    }

    public override void Aplastar(float scale)
    {
        base.Aplastar(scale);
        velHorizontal = 0;
        Muelto(false);
        animator.Play("Aplastao");
    }
}
