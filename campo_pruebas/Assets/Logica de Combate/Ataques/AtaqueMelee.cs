using UnityEngine;
using System.Collections;

public class AtaqueMelee : IAtaque {

    public float range = 1;
    public float width = 1;
    public float duration = 0.1f;

    public bool debugMode;

    private BoxCollider2D attackCollider;
    private Bounds colliderBounds;

    //DEBUG
    private float lifeTime = 0;
    private float maxLifeTime = 1;

    void Start()
    {
        attackCollider = this.gameObject.AddComponent<BoxCollider2D>();
        attackCollider.size = new Vector2(range, width);
        attackCollider.offset = new Vector2(range/2, 0);
        colliderBounds = attackCollider.bounds;

        //DEBUG
        //Debug.DrawLine(colliderBounds.min, colliderBounds.max);
       
    }

    void Update()
    {
        lifeTime += Time.deltaTime;

        //Destruir collider al siguiente frame de su creación
        if (attackCollider && lifeTime > duration) Destroy(attackCollider);

        //Destruir objeto (TO-DO: Esperar a finalizar animación)
        //Destroy(this.gameObject);

        if (debugMode)
        {
            if (lifeTime > maxLifeTime) Destroy(this.gameObject);
            Vector2 ur = new Vector2(colliderBounds.max.x, colliderBounds.min.y);
            Vector2 dl = new Vector2(colliderBounds.min.x, colliderBounds.max.y);
            Debug.DrawLine(colliderBounds.min, ur);
            Debug.DrawLine(ur, colliderBounds.max);
            Debug.DrawLine(colliderBounds.max, dl);
            Debug.DrawLine(dl, colliderBounds.min);
            Debug.Log("Collider Bounds: " + colliderBounds.min + ", " + colliderBounds.max);
        }
        else
        {
            Destroy(this.gameObject);
        }


    }
}
