using UnityEngine;
using System.Collections;


[RequireComponent(typeof (Rigidbody2D))]

public class AtaqueProyectil : IAtaque {

    public float shootForce;

	// Acción de ataque de la instancia en concreto
	void Start () {
        //Lanzar proyectil
        Rigidbody2D bulletBody = GetComponent<Rigidbody2D>();
        bulletBody.AddForce(this.transform.right * shootForce, ForceMode2D.Force);

	}
	
	// Update is called once per frame
	void Update () {
	}


    //Destruir proyectil al colisionar
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
        if (collision.gameObject.tag == "Targeteable")
        {
            //Debug.Log("BALA: He tocao en duro!");

        }
    }
}
