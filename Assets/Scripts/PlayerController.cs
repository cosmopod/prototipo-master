using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Combatiente))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{

    //Parámetros de configuración
    public float jumpForce = 5f;        //  m/s
    public float maxSpeedOnGround = 5f; //  m/s
    public float maxSpeedOnAir = 3f;    //  m/s
    public float maxSpeedDucked = 3f;   //  m/s
    public float acceleration = 1f;     //  +m/s
    public KeyCode normalAttackKey;         //Key to launch simple attack
    public KeyCode specialAttackKey;        //Key to launch powered attack
                                            //public Arma equipedWeaponPrefab;


    //Estado de collisiones con el entorno
    private bool onGroundCollision = false;
    private bool onTopCollision = false;
    private bool onRightCollision = false;
    private bool onLeftCollision = false;

    //Estado del input del usuario
    private bool pressingDown = false;
    private bool pressingUp = false;
    private bool pressingRight = false;
    private bool pressingLeft = false;

    private Rigidbody2D rigidb;
    private Combatiente combatController;
    //private Arma equipedWeaponInstance;

    public enum MovementState
    {
        Stand, Walking, Duck, DuckNmoving, AimUpVertical, AimUpDiagonal, AirStand, AirMove, AirAimDownVertical, AirAimDownDiagonal,
        AirAimUpVertical, AirAimUpDiagonal
    };

    private MovementState movementState;
    private Vector2 actualSpeed;

    private bool lookRight = true;      //Dirección a la que mira


    //======================================================================================================================================================================
    // FUNCIONES HEREDADAS
    //======================================================================================================================================================================
    void Start()
    {
        rigidb = GetComponent<Rigidbody2D>();
        combatController = GetComponent<Combatiente>();

        //Estado inicial
        movementState = MovementState.Stand;

        //equipedWeaponInstance = Instantiate(equipedWeaponPrefab, transform.position, transform.rotation) as Arma;
        //equipedWeaponInstance.transform.parent = transform;
    }


    void Update()
    {
        actualSpeed = rigidb.velocity;

    }

    void FixedUpdate()
    {

        capturarInputMovimiento();
        capturarInputSalto();
        orientarSprite();
        movementState = calcularEstado();
        capturarInputAtaque();

        //Aplicar velocidad
        rigidb.velocity += new Vector2(calcularIncrementoVelocidad(), 0);

    }


    //======================================================================================================================================================================
    // FUNCIONES INTERNAS
    //======================================================================================================================================================================

    private void capturarInputMovimiento()
    {

        //Direccion horizontal
        if (Input.GetKey(KeyCode.A))
        {
            //rigidb.velocity += new Vector2(-acceleration, 0);

            pressingLeft = true;
            pressingRight = false;
            lookRight = false;
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                //rigidb.velocity += new Vector2(acceleration, 0);
                pressingLeft = false;
                pressingRight = true;
                lookRight = true;
            }
            else
            {
                pressingLeft = false;
                pressingRight = false;
            }

        }

        //Direccion vertical
        if (Input.GetKey(KeyCode.W))
        {
            pressingUp = true;
            pressingDown = false;
        }
        else
        {
            if (Input.GetKey(KeyCode.S))
            {
                pressingUp = false;
                pressingDown = true;
            }
            else
            {
                pressingUp = false;
                pressingDown = false;
            }
        }
    }

    private void capturarInputAtaque()
    {
        //Ataques
        if (Input.GetKey(normalAttackKey) || Input.GetKeyDown(normalAttackKey))
        {
            //equipedWeaponInstance.normalAttack();
            combatController.normalAttack();
        }
        else if (Input.GetKey(specialAttackKey) || Input.GetKeyDown(specialAttackKey))
        {
            //equipedWeaponInstance.specialAttack();
            combatController.specialAttack();
        }

    }

    private void capturarInputSalto()
    {
        //Salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGroundCollision)
                //rigidb.velocity += new Vector2(0, jumpForce);
                rigidb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private MovementState calcularEstado()
    {
        MovementState ms;
        //Calcular estado final (6 estados on ground, 6 estados on air)
        if (onGroundCollision)
        {
            if (pressingRight || pressingLeft)
            {
                ms = MovementState.Walking;
                if (pressingDown) ms = MovementState.DuckNmoving;
                else if (pressingUp) ms = MovementState.AimUpDiagonal;

            }
            else
            {
                ms = MovementState.Stand;
                if (pressingDown) ms = MovementState.Duck;
                else if (pressingUp) ms = MovementState.AimUpVertical;
            }

        }
        else
        {
            if (pressingRight || pressingLeft)
            {
                ms = MovementState.AirMove;
                if (pressingDown) ms = MovementState.AirAimDownDiagonal;
                else if (pressingUp) ms = MovementState.AirAimUpDiagonal;
            }
            else
            {
                ms = MovementState.AirStand;
                if (pressingDown) ms = MovementState.AirAimDownVertical;
                else if (pressingUp) ms = MovementState.AirAimUpVertical;
            }
        }

        return ms;
    }

    private void orientarSprite()
    {
        //Girar izquierda/derecha
        if (lookRight)
        {
            transform.rotation = Quaternion.identity;
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(-Vector3.forward);
        }
    }

    private float calcularIncrementoVelocidad()
    {
        //Calcular incremento de velocidad
        float actual_speed = rigidb.velocity.x;

        float factor_escala = 4;

        float target_speed = 0;
        if (movementState == MovementState.Walking) target_speed = maxSpeedOnGround;
        if (movementState == MovementState.DuckNmoving) target_speed = maxSpeedDucked;
        if (movementState == MovementState.AirMove) target_speed = maxSpeedOnAir;
        if (!lookRight) target_speed = -target_speed;


        float diferencia = target_speed - actual_speed;
        float incremento = diferencia / factor_escala;
        if (incremento < 0.0001 && incremento > -0.0001) incremento = 0;

        return incremento;


    }

    //Deprecated
    private void normalizeSpeed()
    {
        float max_speed;

        if (!onGroundCollision) max_speed = maxSpeedOnAir;
        else if (!pressingDown) max_speed = maxSpeedOnGround;
        else max_speed = maxSpeedDucked;

        float actual_speed = rigidb.velocity.x;

        if (Mathf.Abs(actual_speed) > max_speed)
        {
            if (pressingRight) rigidb.velocity = new Vector2(max_speed, rigidb.velocity.y);
            else if (pressingLeft) rigidb.velocity = new Vector2(-max_speed, rigidb.velocity.y);
        }

    }

    //======================================================================================================================================================================
    // FUNCIONES EXTERNAS
    //======================================================================================================================================================================

    //Informe de los checker para nuevas colisiones
    public void CollisionChange(CheckerController.Place collisionPlace, Collider2D collider, bool collisionIn)
    {
        switch (collisionPlace)
        {
            case CheckerController.Place.Bottom:
                onGroundCollision = collisionIn;
                break;

            case CheckerController.Place.Top:
                onTopCollision = collisionIn;
                break;

            case CheckerController.Place.Right:
                onRightCollision = collisionIn;
                break;

            case CheckerController.Place.Left:
                onLeftCollision = collisionIn;
                break;
        }
    }

    //======================================================================================================================================================================
    // DEBUG E INFO
    //======================================================================================================================================================================

    public bool[] getCollisions()
    {
        bool[] v_collisions = { onGroundCollision, onTopCollision, onRightCollision, onLeftCollision };
        return v_collisions;
    }

    public bool[] getKeysPressed()
    {
        bool[] v_keys = { pressingDown, pressingUp, pressingRight, pressingLeft };
        return v_keys;
    }

    public MovementState getMovementState()
    {
        return movementState;
    }

    public Vector2 getSpeed()
    {
        return actualSpeed;
    }



}
