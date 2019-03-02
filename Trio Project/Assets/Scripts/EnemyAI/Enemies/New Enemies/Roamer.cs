using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Roamer : DamageableEnvironmentItemParent {

    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private int rotationSpeed = 100;
    [SerializeField] private int baseMineDelay = 2;
    [SerializeField] private int minimumDistance = 10;
    [SerializeField] private GameObject minePrefab;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Animator[] animators;

    private float mineDelay { get { return baseMineDelay / GameManager.Instance.Difficulty; } }
    private bool chasing;
    private bool canDropMine;
    private bool engagedPlayer;
    private float timer;
    private int pingCount;

    new void Start ()
    {
        base.Start();
        timer = 0;
        ItemType = myItemType.Wood;
        Armor = 2;
        KillPoints = 100;
        //maxHealth = 75 * GameManager.Instance.Difficulty;
        RoomSetter.UpdatePlayerRoom += CheckPlayerRoom;
        healthBarImage.fillAmount = HealthPercent;
	}

    protected override void SetColors()
    {
        //Override and dont let the default colors to be assigned (leave empty)
        //We will need to assign colors for this object in the inspector
    }

    public override void Kill()
    {
        RoomManager.Instance.AddToDoor(GameManager.Instance.PlayerRoom, RoomManager.RoomType.MiniBoss); //We want to tell every door in the room the player is in that a miniboss died
        RoomSetter.UpdatePlayerRoom -= CheckPlayerRoom;
        base.Kill();
    }

    public void CheckPlayerRoom()
    {
        if (GameManager.Instance.PlayerRoom == MyRoom && MyRoom != null)
        {
            EngagePlayer();
            return;
        }

        StopAllCoroutines();
        chasing = false;
        return;
    }

    void EngagePlayer()
    {
        if (!engagedPlayer)
        {
            engagedPlayer = true;

            foreach (Animator anim in animators)
            {
                anim.SetBool("EngagingPlayer", true);
            }

            StartCoroutine(EngageTimer());
            return;

        } else
        {
            canDropMine = false;
            chasing = true;
            return;
        }
    }

    IEnumerator EngageTimer()
    {
        chasing = false;
        yield return new WaitForSeconds(2f);

        if (GameManager.Instance.PlayerRoom == MyRoom)
        {
            canDropMine = false;
            chasing = true;
        }

        yield break;
    }

    void PingPlayer()
    {
        float distance = Vector3.Distance(transform.position, GameManager.Instance.PlayerObject.transform.position);

        if (distance <= minimumDistance)
        {
            pingCount += 1;
        } else
        {
            pingCount = 0;
        }

        if (pingCount >= 3)
        {
            Debug.Log("Selfdestruct " + Time.time);
        }
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);

        if (damageTaken > 0)
        {
            healthBarImage.fillAmount = HealthPercent;
            GameManager.Instance.CameraShaker.ShakeMe(8, .15f);
            //System.Threading.Thread.Sleep(15);
        }
    }

    new void Update()
    {
        base.Update();

        if (chasing == true)
        {
            timer += Time.deltaTime;
        }

        if (timer > mineDelay)
        {
            timer = 0;
            canDropMine = true;
            DropMine();
            PingPlayer();
        }
    }

    void DropMine()
    {
        if (canDropMine)
        {
            canDropMine = false;
            Instantiate(minePrefab, transform.position, Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        if (chasing == true)
        {
            float step = moveSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up * (Time.deltaTime * rotationSpeed), Space.World);

            Transform player = GameManager.Instance.PlayerObject.transform;
            Vector3 newPos = new Vector3(player.position.x, transform.position.y, player.position.z);

            transform.position = Vector3.MoveTowards(transform.position, newPos, step);
        }
    }

    public void IncrementRotationSpeed(int speed)
    {
        rotationSpeed += speed;
    }

    public void IncrementMoveSpeed(float speed)
    {
        moveSpeed += speed;
    }
}
