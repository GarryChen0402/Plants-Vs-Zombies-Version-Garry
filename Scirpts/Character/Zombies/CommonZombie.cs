using UnityEngine;

enum CommonZombieState
{
    walk,
    eat,
    dying,
    dead
}

public class CommonZombie : Zombie
{
    [SerializeField] private CommonZombieState state;

    public Animator animator;

    public GameObject headPrefab;

    [SerializeField] private float move_speed;

    //private float attackDamage = 10f;
    //public float AttackDamage => attackDamage;

    private bool facingRight = true;

    private int walkAnimType;

    private float curHPRatio;

    private bool isAttacking;
    private bool hasHead;
    private Plant currentEatPlant;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        facingRight = false;
        walkAnimType = Random.Range(1, 2);
        state = CommonZombieState.walk;
        maxHP = 100;
        curHP = maxHP;
        curHPRatio = 1;
        isAttacking = false;
        currentEatPlant = null;
        attackDamage = 10;
        targetTag = "Plants";
        attackCD = 2f;
        attackTimer = attackCD;
        hasHead = true;
        move_speed = 0.15f;
        canMove = true;
    }

    private void Start()
    {
        animator.SetInteger("walkSelect", walkAnimType);
        CurHpRatioUpdate();

    }

    private void Update()
    {
        switch (state)
        {
            case CommonZombieState.walk:
                WalkUpdate();
                break;
            case CommonZombieState.eat:
                EatUpdate();
                break;
            case CommonZombieState.dying:
                break;
            case CommonZombieState.dead:
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag.Equals(targetTag))
        {

            //currentEatPlant = collision.gameObject;
            currentEatPlant = collision.gameObject.GetComponent<Plant>();
            SwitchToEat();
        }
        else if (collision.tag.Equals("House"))
        {
            EnterTheHouse();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag.Equals(targetTag))
        {
            currentEatPlant = null;
            SwitchToWalk();
        }
    }


    private void WalkUpdate()
    {
        if (!canMove) return;
        //transform.Translate(Vector3.left)
        if(facingRight)transform.Translate(Vector3.right * move_speed * Time.deltaTime);
        else transform.Translate(Vector3.left * move_speed * Time.deltaTime);
    }

    private void EatUpdate()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer < attackCD) return;
        attackTimer -= attackCD;
        currentEatPlant?.GetDamage(attackDamage);
        AudioManager.Instance?.PlayFx("ZombieEat");
        //currentEatPlant?.GetComponent<Plant>().GetDamage(attackDamage);
    }

    public override void Hurt(float value)
    {
        if (curHP < value) SwitchToDying();
        else
        {
            curHP -= value;
            float curHPRatio = curHP / maxHP;
            CurHpRatioUpdate();
            //animator.SetFloat("HP");
        }
    }


    private void SwitchToDying()
    {
        state = CommonZombieState.dying;
        animator.SetFloat("HP", -1f);
    }

    private void SwitchToEat()
    {
        state = CommonZombieState.eat;
        isAttacking = true;
        animator.SetBool("isEating", isAttacking);
    }

    private void SwitchToWalk()
    {
        state = CommonZombieState.walk;
        isAttacking = false;
        animator.SetBool("isEating", isAttacking);
    }

    private void CurHpRatioUpdate()
    {
        curHPRatio = curHP / maxHP;
        if(curHPRatio <= 0.201)LoseHead();
        animator.SetFloat("HP", curHPRatio);
    }

    public void PostDead()
    {
        ZombieManager.Instance.RemoveDeadZombie(gameObject);
        Destroy(gameObject, 1);
    }

    public void LoseHead()
    {
        if (!hasHead) return;
        hasHead = false;
        //Debug.Log("Common Zombie Lost its head...");
        Vector3 position = transform.position;
        position.x += 0.7f;
        position.y -= 0.35f;
        GameObject head = GameObject.Instantiate(headPrefab, position, Quaternion.identity);
        //headPrefab.SetActive(true);
        Destroy(head, 2f);
        //Destroy(head, 2);
    }
}
