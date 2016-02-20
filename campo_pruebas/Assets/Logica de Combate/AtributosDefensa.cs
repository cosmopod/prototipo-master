using UnityEngine;
using System.Collections;
using System;

public class AtributosDefensa : MonoBehaviour{

    [Range (0,1)]
    public float resistenciaGeneral;

    [Range(0, 1)]
    public float resistenciaFisico;

    [Range(0, 1)]
    public float resistenciaSolar;

    [Range(0, 1)]
    public float resistenciaElectrico;

    [Range(0, 1)]
    public float resistenciaOscuro;

    [Range(0, 1)]
    public float resistenciaBiologico;

    [Range(0, 1)]
    public float resistenciaLaser;


}
