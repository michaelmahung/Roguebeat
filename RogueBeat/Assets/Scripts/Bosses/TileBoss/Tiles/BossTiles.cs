using UnityEngine;

enum TileActiveStates
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

    TileActiveStates currentState;
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
        currentState = TileActiveStates.Activating;
        activeTimer = time;
    }

    public void Deactivate(float time)
    {
        currentState = TileActiveStates.Deactivating;
        activeTimer = time;
    }

    private void Update()
    {
        switch (currentState)
        {
            case TileActiveStates.Idle:
                break;
            case TileActiveStates.Activating:
                StartActivating();
                break;
            case TileActiveStates.Holding:
                StayActive();
                break;
            case TileActiveStates.Deactivating:
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
            currentState = TileActiveStates.Holding;
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
            currentState = TileActiveStates.Deactivating;
        }
    }

    void StartDeactivating()
    {
        timer -= Time.deltaTime / activeTimer;
        myRend.material.Lerp(startMat, endMat, timer);

        if (timer <= 0)
        {
            currentState = TileActiveStates.Idle;
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
