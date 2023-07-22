using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState{
    walk,
    attack,
    interact,
    stagger,
    idle
}


public class PlayerController : MonoBehaviour, IDataPersistence
{
    public float speed = 4f;
    private Rigidbody2D characterBody;
    private Vector2 inputMovement;
    private Animator animator;
    public LayerMask solidObjectsLayer;
    public VariableJoystick joystick;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public Button attackButton;
    private DialogueTrigger currentDialogueTrigger;
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;
    public PlayerState currentState;


    public CharacterDatabase characterDB;

    private GameObject selectedCharacter;
    private SpriteRenderer artworkSprite;

    private int selectedOption;

    public GameObject maleCharacter;
    public GameObject femaleCharacter;

    [Header("Button")]
    [SerializeField] private Button dialogueButton;

    private bool isAttacking = false;
    public VectorValue startingPosition;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterBody = GetComponent<Rigidbody2D>();
        selectedCharacter = GameObject.FindGameObjectWithTag("SelectedCharacter");
        artworkSprite = selectedCharacter.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Load();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
        attackButton.onClick.AddListener(OnAttackButtonClicked);
        dialogueButton.onClick.AddListener(onDialogueButtonClicked);

        dialogueButton.interactable = false;
    }

    private void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void MyInput()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            inputMovement = Vector2.zero;
            joystick.gameObject.SetActive(false);
            if (dialogueButton != null)
                dialogueButton.gameObject.SetActive(false);
            if (attackButton != null)
                attackButton.gameObject.SetActive(false);
        }
        else
        {
            float inputX = joystick.Horizontal;
            float inputY = joystick.Vertical;

            if (inputX != 0 && inputY != 0)
            {
                inputX *= 0.7f;
                inputY *= 0.7f;
            }

            inputMovement = new Vector2(inputX, inputY);

            if (inputMovement.magnitude > 0)
            {
                animator.SetFloat("Horizontal", inputX);
                animator.SetFloat("Vertical", inputY);

                if (Mathf.Abs(inputX) > Mathf.Abs(inputY))
                {
                    animator.SetFloat("Horizontal", inputX * (1 / Mathf.Abs(inputX)));
                    animator.SetFloat("Vertical", 0);
                }
                else
                {
                    animator.SetFloat("Horizontal", 0);
                    animator.SetFloat("Vertical", inputY * (1 / Mathf.Abs(inputY)));
                }
            }

            if (dialogueButton != null && !dialogueButton.gameObject.activeSelf)
            {
                dialogueButton.gameObject.SetActive(true);
            }

            if (attackButton != null && !attackButton.gameObject.activeSelf)
            {
                attackButton.gameObject.SetActive(true);
            }

            if (!joystick.gameObject.activeSelf)
            {
                joystick.gameObject.SetActive(true);
            }

            if (!dialogueButton.gameObject.activeSelf)
            {
                dialogueButton.gameObject.SetActive(true);
            }


            if (!attackButton.gameObject.activeSelf)
            {
                attackButton.gameObject.SetActive(true);
            }
        }

        animator.SetFloat("Speed", inputMovement.magnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    void Movement()
    {
        if (isAttacking)
        {
            return;
        }

        Vector2 delta = inputMovement * speed * Time.fixedDeltaTime;
        Vector2 newPosition = characterBody.position + delta;

        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, 0.2f, solidObjectsLayer);

        if (colliders.Length > 0)
        {
            return;
        }

        characterBody.MovePosition(newPosition);
    }

    


    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receive item", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("receive item", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }


    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("swordAttack");
        currentState = PlayerState.attack;
        yield return null;
        animator.SetTrigger("swordAttack");
        isAttacking = false;
        yield return new WaitForSeconds(.3f);
    }
   
    private void OnAttackButtonClicked()
    {
        if (!isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("NPC"))
        {
            currentDialogueTrigger = collider.GetComponent<DialogueTrigger>();
            dialogueButton.interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("NPC"))
        {
            currentDialogueTrigger = null;

            if (dialogueButton != null)
            {
                dialogueButton.interactable = false;
            }
        }
    }


    private void onDialogueButtonClicked()
    {
        if (currentDialogueTrigger != null)
        {
            currentDialogueTrigger.TriggerDialogue();
        }
    }

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
    }

    private void Load()
    {
        int selectedOption = PlayerPrefs.GetInt("selectedOption");

        if (selectedOption == 0)
        {
            maleCharacter.SetActive(true);
            femaleCharacter.SetActive(false);
            string characterName = PlayerPrefs.GetString("CharacterName", "MaleCharacter");
        }
        else if (selectedOption == 1)
        {
            maleCharacter.SetActive(false);
            femaleCharacter.SetActive(true);
            string characterName = PlayerPrefs.GetString("CharacterName", "FemaleCharacter");
        }
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
    }

}