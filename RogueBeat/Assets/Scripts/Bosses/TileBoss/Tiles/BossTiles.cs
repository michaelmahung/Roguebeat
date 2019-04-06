using UnityEngine;

enum BossTileStates
{
    Idle,
    Activating,
    Holding,
    Deactivating,
}

public class BossTiles : MonoBehaviour
{
    public TilePosition Position;
    public float Damage = 0.25f;
    [SerializeField] float timeToStayActive = 1;
    [SerializeField] float glowColorBrightness = 1.25f;

    Renderer myRend;
    Material myMat;
    Material brightMat;
    [SerializeField] Material startMat;
    [SerializeField] Material endMat;
    [SerializeField] Color glowColor;

    BossTileStates currentState;
    IDamageable<float> playerDamage;

    bool tileActive;

    float timer;
    float activeTimer;
    float timeActive;

    private void Awake()
    {
        playerDamage = GameManager.Instance.PlayerHealthRef.GetComponent < IDamageable<float>>();
        myRend = GetComponent<Renderer>();
        myMat = myRend.material;
        myRend.material = startMat;
    }

    public void Activate(float time)
    {
        currentState = BossTileStates.Activating;
        activeTimer = time;
    }

    public void Deactivate(float time)
    {
        currentState = BossTileStates.Deactivating;
        activeTimer = time;
    }

    private void Update()
    {
        switch (currentState)
        {
            case BossTileStates.Idle:
                break;
            case BossTileStates.Activating:
                StartActivating();
                break;
            case BossTileStates.Holding:
                StayActive();
                break;
            case BossTileStates.Deactivating:
                StartDeactivating();
                break;
            default:
                StartDeactivating();
                break;
        }
    }

    void StartActivating()
    {
        timer += Time.deltaTime / activeTimer;
        myRend.material.Lerp(startMat, endMat, timer);

        if (timer > activeTimer)
        {
            tileActive = true;
            currentState = BossTileStates.Holding;
            myRend.material.SetColor("_EmissionColor", glowColor * glowColorBrightness);
        }
    }

    void StayActive()
    {
        timeActive += Time.deltaTime;

        if (timeActive > timeToStayActive)
        {
            tileActive = false;
            timeActive = 0;
            currentState = BossTileStates.Deactivating;
        }
    }

    void StartDeactivating()
    {
        timer -= Time.deltaTime / activeTimer;
        myRend.material.Lerp(startMat, endMat, timer);

        if (timer <= 0)
        {
            currentState = BossTileStates.Idle;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!tileActive)
            return;

        if (other.CompareTag("Player"))
        {
            playerDamage.Damage(Damage);
        }
    }
}
