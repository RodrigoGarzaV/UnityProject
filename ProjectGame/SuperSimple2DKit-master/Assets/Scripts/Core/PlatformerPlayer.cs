using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Adds player functionality to a physics object*/

[RequireComponent(typeof(RecoveryCounter))]

public class PlatformerPlayer : PhysicsObject
{
    [Header ("Reference")]
    [SerializeField] private Animator animator;
    private AnimatorFunctions animatorFunctions;
    private CapsuleCollider2D capsuleCollider;
    public CameraEffects cameraEffects;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private AudioSource flameParticlesAudioSource;
    [SerializeField] private GameObject graphic;
    [SerializeField] private Component[] graphicSprites;
    [SerializeField] private ParticleSystem jumpParticles;
    [SerializeField] private GameObject pauseMenu;
    public RecoveryCounter recoveryCounter;

    // Singleton instantiation
    private static PlatformerPlayer instance;
    public static PlatformerPlayer Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<PlatformerPlayer>();
            return instance;
        }
    }

    [Header("Properties")]
    public bool dead = false;
    public bool frozen = false;
    private float fallForgivenessCounter; //Counts how long the player has fallen off a ledge
    [SerializeField] private float fallForgiveness = .2f; //How long the player can fall from a ledge and still jump
    [System.NonSerialized] public RaycastHit2D ground; 
    [SerializeField] Vector2 hurtLaunchPower; //How much force should be applied to the player when getting hurt?
    private float launch; //The float added to x and y moveSpeed. This is set with hurtLaunchPower, and is always brought back to zero
    [SerializeField] private float launchRecovery; //How slow should recovering from the launch be? (Higher the number, the longer the launch will last)
    public float maxSpeed = 7; //Max move speed
    public float jumpPower = 17;
    private bool jumping;
    private Vector3 origLocalScale;
    [System.NonSerialized] public bool pounded;
    [System.NonSerialized] public bool pounding;
    [System.NonSerialized] public bool shooting = false;

    [Header ("Inventory")]
    public int bugs;
    public int health;
    public int maxHealth;

    [Header ("Isometric References")]
    public bool Isometric = false;
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> neSprites;
    public List<Sprite> nwSprites;
    public List<Sprite> eSprites;
    public List<Sprite> wSprites;

    public float walkSpeed;
    public float frameRate;

    float idleTime;

    Vector2 direction;

    void Start()
    {
        Cursor.visible = false;
        health = maxHealth;
        origLocalScale = transform.localScale;
        recoveryCounter = GetComponent<RecoveryCounter>();
        
        //Find all sprites so we can hide them when the player dies.
        graphicSprites = GetComponentsInChildren<SpriteRenderer>();
        if (!Isometric){
            animatorFunctions = GetComponent<AnimatorFunctions>();
        }
        

    }

    private void Update()
    {

        if (Isometric){
            direction = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")).normalized;
            body.velocity = direction * walkSpeed;
            HandleSpriteFlip();
            List<Sprite> directionSprites = GetSpriteDirection();
            if (directionSprites != null){
                float playTime = Time.time - idleTime;
                int totalFrames = (int)(playTime * frameRate);
                int frame = totalFrames % directionSprites.Count;
                spriteRenderer.sprite = directionSprites[frame];
            } 
            else 
            {
                idleTime = Time.time;
            }

            if (Input.GetButtonDown("Cancel"))
            {
                pauseMenu.SetActive(true);
            }
        }

        else
        {
            ComputeVelocity();
        }

    }

    protected void ComputeVelocity()
    {
        //Player movement & attack
        Vector2 move = Vector2.zero;
        ground = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), -Vector2.up);

        //Lerp launch back to zero at all times
        launch += (0 - launch) * Time.deltaTime * launchRecovery;

        if (Input.GetButtonDown("Cancel"))
        {
            pauseMenu.SetActive(true);
        }

        //Movement, jumping, and attacking!
        if (!frozen)
        {
            move.x = Input.GetAxis("Horizontal") + launch;

            if (Input.GetButtonDown("Jump") && animator.GetBool("grounded") == true && !jumping)
            {
                animator.SetBool("pounded", false);
                Jump(1f);
            }

            //Flip the graphic's localScale
            if (move.x > 0.01f)
            {
               graphic.transform.localScale = new Vector3(origLocalScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if (move.x < -0.01f)
            {
               graphic.transform.localScale = new Vector3(-origLocalScale.x, transform.localScale.y, transform.localScale.z);
            }



            //Allow the player to jump even if they have just fallen off an edge ("fall forgiveness")
            if (!grounded)
            {
                if (fallForgivenessCounter < fallForgiveness && !jumping)
                {
                    fallForgivenessCounter += Time.deltaTime;
                }
                else
                {
                    animator.SetBool("grounded", false);
                }
            }
            else
            {
                fallForgivenessCounter = 0;
                animator.SetBool("grounded", true);
            }

            //Set each animator float, bool, and trigger to it knows which animation to fire
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
            animator.SetFloat("velocityY", velocity.y);
            animator.SetInteger("moveDirection", (int)Input.GetAxis("HorizontalDirection"));
            targetVelocity = move * maxSpeed;

        }
        else
        {
            //If the player is set to frozen, his launch should be zeroed out!
            launch = 0;
        }
    }


    public void Freeze(bool freeze)
    {
        //Set all animator params to ensure the player stops running, jumping, etc and simply stands
        if (freeze && !Isometric)
        {
            animator.SetInteger("moveDirection", 0);
            animator.SetBool("grounded", true);
            animator.SetFloat("velocityX", 0f);
            animator.SetFloat("velocityY", 0f);
            GetComponent<PhysicsObject>().targetVelocity = Vector2.zero;
        }

        frozen = freeze;
        shooting = false;
        launch = 0;
    }


    public void GetHurt(int hurtDirection, int hitPower)
    {
        //If the player is not frozen (ie talking, spawning, etc), recovering, and pounding, get hurt!
        if (!frozen && !recoveryCounter.recovering && !pounding)
        {
            HurtEffect();
            cameraEffects.Shake(100, 1);
            animator.SetTrigger("hurt");
            velocity.y = hurtLaunchPower.y;
            launch = hurtDirection * (hurtLaunchPower.x);
            recoveryCounter.counter = 0;

            if (health <= 0)
            {
                StartCoroutine(Die());
            }
            else
            {
                health -= hitPower;
            }

            GameManager.Instance.hud.HealthBarHurt();
        }
    }

    private void HurtEffect()
    {
        StartCoroutine(FreezeFrameEffect());

        cameraEffects.Shake(100, 1f);
    }

    public IEnumerator FreezeFrameEffect(float length = .007f)
    {
        Time.timeScale = .1f;
        yield return new WaitForSeconds(length);
        Time.timeScale = 1f;
    }


    public IEnumerator Die()
    {
        if (!frozen)
        {
            dead = true;
            deathParticles.Emit(10);
            Hide(true);
            Time.timeScale = .6f;
            yield return new WaitForSeconds(5f);
            GameManager.Instance.hud.animator.SetTrigger("coverScreen");
            GameManager.Instance.hud.loadSceneName = SceneManager.GetActiveScene().name;
            Time.timeScale = 1f;
        }
    }

    public void ResetLevel()
    {
        Freeze(true);
        dead = false;
        health = maxHealth;
    }

    public void Jump(float jumpMultiplier)
    {
        if (velocity.y != jumpPower)
        {
            velocity.y = jumpPower * jumpMultiplier; //The jumpMultiplier allows us to use the Jump function to also launch the player from bounce platforms
            JumpEffect();
            jumping = true;
        }
    }


    public void JumpEffect()
    {
        jumpParticles.Emit(1);
    }

    public void LandEffect()
    {
        if (jumping)
        {
            jumpParticles.Emit(1);
            jumping = false;
        }
    }


    public void FlashEffect()
    {
        //Flash the player quickly
        animator.SetTrigger("flash");
    }

    public void Hide(bool hide)
    {
        Freeze(hide);
        foreach (SpriteRenderer sprite in graphicSprites)
            sprite.gameObject.SetActive(!hide);
    }



    // ISOMETRIC FUNCTIONS

    void HandleSpriteFlip() {
        if(!spriteRenderer.flipX && direction.x > 0){
            spriteRenderer.flipX = true;
        } else if(spriteRenderer.flipX && direction.x < 0){
            spriteRenderer.flipX = false;
        }
    }

    List<Sprite> GetSpriteDirection() {

        List<Sprite> selectedSprites = null;

        if(direction.y > 0){
            if(Mathf.Abs(direction.x) < 0)
            {
                selectedSprites = neSprites;
            }
            else
            {
                selectedSprites = nwSprites;
            }
        }
        else if (direction.y < 0)
        {
            if (Mathf.Abs(direction.x) < 0)
            {
                selectedSprites = eSprites;
            } 
            else 
            {
                selectedSprites = wSprites;
            }
        }
        else{
            if(Mathf.Abs(direction.x) < 0)
            {
                selectedSprites = eSprites;
            }
            else
            {
                selectedSprites = wSprites;
            }
        }

        return selectedSprites;
    }
}
