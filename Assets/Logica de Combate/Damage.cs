using UnityEngine;
using System.Collections;

public class Damage{

    public enum Source { Fisico, Fuego, Veneno };

    public Source tipo;
    public float cantidad;

    public Damage(Source tipo, float cantidad)
    {
        this.tipo = tipo;
        this.cantidad = cantidad;
    }

}
