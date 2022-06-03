using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> neSprites;
    public List<Sprite> nwSprites;
    public List<Sprite> eSprites;
    public List<Sprite> wSprites;
    [SerializeField] private GameObject pauseMenu;

    public float walkSpeed;
    public float frameRate;

    float idleTime;

    Vector2 direction;

    // Update is called once per frame
    void Update()
    {
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