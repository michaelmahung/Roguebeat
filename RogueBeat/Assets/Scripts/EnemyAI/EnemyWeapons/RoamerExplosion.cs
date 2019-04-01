using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamerExplosion : EnemyProjectile
{
    [SerializeField] private float startScale = 5;
    [SerializeField] private float endScale = 10;
    [SerializeField] private float scaleTime = 0.5f;
    [SerializeField] private float stayTime = 0.5f;

    Collider myCol;
    Vector3 startScaleValues;
    Vector3 currentScale;
    Vector3 endScaleValues;
    float scaleTimer;

    override protected void OnEnable()
    {
        myCol.enabled = true;
        base.OnEnable();
        transform.localScale = new Vector3(startScale, startScale, startScale);
    }

    override protected void Awake()
    {
        myCol = GetComponent<Collider>();
        startScaleValues = new Vector3(startScale, startScale, startScale);
        endScaleValues = new Vector3(endScale, endScale, endScale);
        transform.localScale = new Vector3(startScale, startScale, startScale);

        base.Awake();
    }

    void Update()
    {
        if (scaleTimer <= scaleTime)
        {
            scaleTimer += Time.deltaTime / scaleTime;
        } else
        {
            Invoke("DestroyMe", stayTime);
        }

        currentScale = Vector3.Lerp(startScaleValues, endScaleValues, scaleTimer);
        transform.localScale = currentScale;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PlayerTag))
        {
            otherDamageable = other.GetComponent<IDamageable<float>>();

            otherDamageable.Damage(Damage);
            myCol.enabled = false;
            Invoke("DestroyMe", stayTime);
        }

        /*if (other.CompareTag(Tags.WallTag))
        {
            Destroy(this.gameObject);
        }*/
    }

    void DestroyMe()
    {
        if (this.gameObject != null)
        {
            DisableObject();
        }
    }
}
