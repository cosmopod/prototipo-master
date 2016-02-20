using UnityEngine;
using System.Collections;

public abstract class IAtaque : MonoBehaviour {

    public float damageAmount;
    public Damage.Source damageType;
    
    public Damage damage;

    void Awake()
    {
        damage = new Damage(damageType, damageAmount);
        if (this.gameObject.tag != "Ataque") Debug.LogWarning("El proyectil no está tageado como de ataque. Algunas features podrían no funcionar");
    }


}
