using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AtributosDefensa))]
public class Combatiente : MonoBehaviour {

    public float vidaBase = 0;
    public float energiaBase = 0;
    public Arma prefabArmaEquipada;

    private EstadoActual estado;
    private AtributosDefensa atributosDefensa;

    private Arma armaEquipada;

	// Use this for initialization
	void Start () {
        atributosDefensa = GetComponent<AtributosDefensa>();

        estado = new EstadoActual(vidaBase, energiaBase);

        if (prefabArmaEquipada)
        {
            armaEquipada = Instantiate(prefabArmaEquipada, transform.position, transform.rotation) as Arma;
            armaEquipada.transform.parent = transform;
        }
        else Debug.LogWarning("El Combatiente " + this.gameObject.name + " no tiene arma. Esto podría provocar inconsistencias en la ejecución.");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void normalAttack()
    {
        if (armaEquipada) armaEquipada.normalAttack();
        else Debug.LogWarning("Combatiente sin arma equipada intentando atacar!");
    }

    public void specialAttack()
    {
        if(armaEquipada) armaEquipada.specialAttack();
        else Debug.LogWarning("Combatiente sin arma equipada intentando atacar!");
    }

    public void damageReceived(Damage dmg)
    {
        if (dmg != null)
        {
            if (estado.vidaActual - dmg.cantidad < 0) estado.vidaActual = 0;
            else estado.vidaActual -= dmg.cantidad;
        }
        else Debug.LogWarning("NULL recibido. Se esperaba 'Damage'!");
    }

    public float getMaxHp()
    {
        return vidaBase;
    }

    public float getHp()
    {
        return estado.vidaActual;
    }
}
