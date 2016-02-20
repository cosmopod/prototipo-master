using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Combatiente))]

public class Dummy : MonoBehaviour {

    public GameObject dialogBoxPrefab;
    public GameObject damageTextPrefab;
    public Transform dialogPoint;

    private GameObject dialogBoxInstance;
    private Combatiente combatController;


    void Start()
    {
        combatController = GetComponent<Combatiente>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ataque")
        {
            //Destruir instancia previa de diálogo
            Destroy(dialogBoxInstance);
            //Crear diálogo
            dialogBoxInstance = Instantiate(dialogBoxPrefab, dialogPoint.position, dialogPoint.rotation) as GameObject;

            //Aplicar daño
            Damage damage_received = collision.gameObject.GetComponent<IAtaque>().damage;
            ApplyDamage(damage_received);
        }


    }

    void ApplyDamage(Damage dmg)
    {
        combatController.damageReceived(dmg);

        //Crear popup de daño
        GameObject damageTextInstance = Instantiate(damageTextPrefab, dialogPoint.position, Quaternion.identity) as GameObject;
        damageTextInstance.GetComponent<DmgText>().LaunchText(dmg.cantidad.ToString(), Color.red);
    }

}
