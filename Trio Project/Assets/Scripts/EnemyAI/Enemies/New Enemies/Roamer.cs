using UnityEngine.UI;
using UnityEngine;

public class Roamer : DamageableEnvironmentItemParent {

    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private int rotationSpeed = 100;
    [SerializeField] private int baseMineDelay = 2;
    [SerializeField] private GameObject minePrefab;
    [SerializeField] private Image healthBarImage;
    private float mineDelay { get { return baseMineDelay / GameManager.Instance.Difficulty; } }
    private bool chasing;
    private bool canDropMine;
    private float timer;

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

    public override void Kill()
    {
        RoomManager.Instance.AddToDoor(GameManager.Instance.PlayerRoom, RoomManager.KillType.MiniBoss); //We want to tell every door in the room the player is in that a miniboss died
        RoomSetter.UpdatePlayerRoom -= CheckPlayerRoom;
        base.Kill();
    }

    public void CheckPlayerRoom()
    {
        if (GameManager.Instance.PlayerRoom == MyRoom && MyRoom != null)
        {
            canDropMine = false;
            chasing = true;
            return;
        }

        StopAllCoroutines();
        chasing = false;
        return;
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
        healthBarImage.fillAmount = HealthPercent;
    }

    new void Update()
    {
        base.Update();

        if (chasing)
        {
            timer += Time.deltaTime;
        }

        if (timer > mineDelay)
        {
            timer = 0;
            canDropMine = true;
            DropMine();
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
        if (chasing)
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
