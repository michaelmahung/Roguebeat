using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(Rigidbody), (typeof(Collider)))]
public abstract class DamageableEnvironmentItemParent : MonoBehaviour, IDamageable<float>, IKillable
{
    private float _damageTaken { get; set; }
    protected float damageTaken
    {
        get { return _damageTaken; }
        set
        {
            if (value < 0f)
            {
                value = 0f;
            }
            _damageTaken = value;
        }
    }

    private float _reactDuration;
    protected float reactDuration
    {
        get { return _reactDuration; }
        set
        {
            if (value < 0f)
            {
                value = 0f;
            }
            _reactDuration = value;
        }
    }

    private float _duration;
    protected float duration
    {
        get { return _duration; }
        set
        {
            if (value > 2f)
            {
                value = 2f;
            }
            _duration = value;

        }
    }

    [SerializeField]
    protected float Health;

    [SerializeField]
    protected int Armor;

    protected Color startColor;
    protected Color currentColor;
    protected Renderer objectRenderer;

    public virtual void Start()
    {
        reactDuration = 1;
        if (gameObject.tag == null)
        {
            gameObject.tag = "Damageable";
        }
        objectRenderer = gameObject.GetComponent<Renderer>();
        startColor = objectRenderer.material.color;
    }

    public virtual void Update()
    {
        if (reactDuration < 1)
        {
            ReactToDamage();
        }
    }

    public void Damage(float damage)
    {
        damageTaken = (damage - Armor);

        if (damageTaken > 0)
        {
            Health -= damageTaken;
            objectRenderer.material.color = Color.red;
            reactDuration = 0;
            duration = damageTaken;
        }

        if (Health <= 0)
        {
            Kill();
        }
    }

    public virtual void ReactToDamage()
    {
        currentColor = Color.Lerp(Color.red, startColor, reactDuration);
        objectRenderer.material.color = currentColor;
        reactDuration += Time.deltaTime / (duration * 0.5f);
    }

    public virtual void Kill()
    {
        Destroy(gameObject);
    }
}
