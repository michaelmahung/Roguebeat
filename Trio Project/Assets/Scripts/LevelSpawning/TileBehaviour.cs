using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    public bool Overheating { get; private set; }
    public bool HasOverheated { get; private set; }
    public bool TileSelected { get; private set; }
    public TileRoom ParentRoom;

    [SerializeField] Material startMat;
    [SerializeField] Material endMat;
    [SerializeField] float baseDamage = 0.2f;

    float damage
    {
        get
        {
            return baseDamage * GameManager.Instance.Difficulty;
        }
    }

    private Material myMat;
    private Renderer myRend;

    IDamageable<float> damageable;



    void Start()
    {
        TileSelected = false;
        myRend = GetComponent<Renderer>();
        myMat = myRend.material;
    }

    public void ResetTile()
    {
        StopAllCoroutines();
        myRend.material = startMat;
        HasOverheated = false;
        TileSelected = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            damageable = other.GetComponent<IDamageable<float>>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Overheating)
        damageable.Damage(damage);
    }

    public void OverheatRoom(float warmupTime)
    {
        TileSelected = true;

        if (!HasOverheated && !ParentRoom.RoomCleared)
        {
            StartCoroutine(StartOverheating(warmupTime));
        }
    }

    public void CancelOverheats()
    {
        //Debug.Log("Cancelling Heat");
        Overheating = false;

        StopAllCoroutines();

        StartCoroutine(StartCooldown(2));
    }

    IEnumerator CancelHeat()
    {
        float timer = 0;

        while (timer < 2)
        {
            myMat.Lerp(myMat, startMat, timer / 2);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator StartOverheating(float heatTime)
    {
        //Debug.Log("Heating Up");
        float timer = 0;

        while (timer < heatTime)
        {
            myMat.Lerp(startMat, endMat, timer / heatTime);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Overheating = true;
        ParentRoom.TileHeated(this);
        HasOverheated = true;
    }

    IEnumerator StartCooldown(float coolTime)
    {
        //Debug.Log("cooling down");

        Overheating = false;
        float timer = 0;

        while (timer < coolTime)
        {
            myMat.Lerp(endMat, startMat, timer / coolTime);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield break;
    }

}
