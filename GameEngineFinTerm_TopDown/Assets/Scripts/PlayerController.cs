using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float verticalSpeed = 4f;

    public Sprite[] walkSprites;
    public Sprite jumpSprite;

    public float dashDistance = 1f;
    public float dashPower = 3f;

    public float frameTime = 0.15f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private float verticalInput;
    private Vector2 velocity;

    private int frameIndex = 0;
    private float timer = 0f;

    private bool canMove = true;
    private bool isJumping = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

       if (walkSprites.Length > 0)
        {
            sr.sprite = walkSprites[0];
        }
    }

    public void OnMove(InputValue value)
    {
        if (!canMove)
        {
            verticalInput = 0f;
            return;
        }

        Vector2 input = value.Get<Vector2>();
        verticalInput = input.y;
    }

    private void Update()
    {
        if (!canMove)
            return;

        if (isJumping)
        {
            if (jumpSprite != null)
                sr.sprite = jumpSprite;

            return;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            UseSkill();
        }

        PlayWalkAnimation();
    }

    private void FixedUpdate()
    {
        if (!canMove)
            return;

        velocity = new Vector2(forwardSpeed, verticalInput * verticalSpeed);
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void PlayWalkAnimation()
    {
        if (walkSprites.Length == 0)
            return;

        timer += Time.deltaTime;

        if (timer >= frameTime)
        {
            timer = 0f;
            frameIndex++;

            if (frameIndex >= walkSprites.Length)
                frameIndex = 0;

            sr.sprite = walkSprites[frameIndex];
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;

        if (!canMove)
        {
            verticalInput = 0f;
            velocity = Vector2.zero;
        }
    }

    public void SetJumping(bool value)
    {
        isJumping = value;

        if (isJumping && jumpSprite != null)
        {
            sr.sprite = jumpSprite;
        }
    }

    public void Dash()
    {
        transform.position += Vector3.right * dashDistance;
    }

    public void OnSkill()
    {
        if (SkillManager.Instance != null)
        {
            SkillManager.Instance.UseDash(this);
        }
    }

    private void UseSkill()
    {
        if (SkillManager.Instance == null)
            return;

        SkillManager.Instance.UseDash(this);
    }
}
