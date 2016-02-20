using UnityEngine;
using System.Collections;

public class Arma : MonoBehaviour{

    public float fireRate = 1;              //Secs. between attacks

    public Transform shootPoint;            //Initial point of the attack
    public GameObject normalAttackPrefab;   //Normal attack prefab to apply
    public GameObject specialAttackPrefab;  //Special attack prefab to apply


    //Variables internas
    private float timeElapsed = 0;          //Time elapsed since last attack
    private bool attackEnabled = true;

    void Start()
    {
        if (shootPoint == null)
        {
            Debug.LogWarning("No se ha proporcionado un punto de ataque para el arma => Se calculará automáticamente.");
            shootPoint = transform;
        }
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > fireRate)
        {
            attackEnabled = true;
            
        }

    }

    public void normalAttack()
    {
        if (attackEnabled)
        {
            if (normalAttackPrefab)
            {
                GameObject attackInstance = Instantiate(normalAttackPrefab, shootPoint.position, shootPoint.rotation) as GameObject;
                attackEnabled = false;
                timeElapsed = 0;
            }
            else Debug.LogWarning("No se ha proporcionado prefab para ataque normal. El ataque no se ejecutará");
            
        }
        
    }

    public void specialAttack()
    {
        if (attackEnabled)
        {
            if (specialAttackPrefab)
            {
                GameObject attackInstance = Instantiate(specialAttackPrefab, shootPoint.position, shootPoint.rotation) as GameObject;
                attackEnabled = false;
                timeElapsed = 0;
            }
            else Debug.LogWarning("No se ha proporcionado prefab para ataque especial. El ataque no se ejecutará");
        }
    }
}
