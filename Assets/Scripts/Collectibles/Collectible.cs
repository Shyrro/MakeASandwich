using UnityEngine;

public class Collectible : MonoBehaviour
{

    [HideInInspector]
    public int id;
    [HideInInspector]
    public string ingType;
    [HideInInspector]
    public int value;

    private readonly float _damp = 0.9f;
    private float _deltaFall = 0.5f;

    public static float fallingSpeed = 180f;
    
    private Rigidbody _rb;
    private Collider _collectibleCollider;
    private GameObject _collectibleContainer;
    private GameObject _plate;

    private static readonly string _tagAfterCollision = "IngredientOnPlate";
    private static readonly string _ingredientsContainerName = "IngredientsContainer";
    private static readonly string _plateTag = "DetectionZone";
    private static readonly string _playerLayer = "Player";
    private static readonly string _ingredientLayer = "Ingredient";

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collectibleCollider = GetComponent<Collider>();
        _collectibleContainer = GameObject.Find(_ingredientsContainerName);
        _plate = GameObject.Find("Plate");

        int playerLayerIndex = LayerMask.NameToLayer(_playerLayer);
        int ingredientLayerIndex = LayerMask.NameToLayer(_ingredientLayer);

        Physics.IgnoreLayerCollision(playerLayerIndex, ingredientLayerIndex);
    }

    private void Update()
    {
        if (_collectibleCollider.tag.Equals(_tagAfterCollision))
        {
            transform.position = new Vector3(_plate.transform.position.x, transform.position.y, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        if (!_collectibleCollider.tag.Equals(_tagAfterCollision))
            _deltaFall = -fallingSpeed * Time.fixedDeltaTime;
        if (_rb != null)
            _rb.velocity = new Vector3(0, _deltaFall, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(_plateTag))
        {
            AddIngredientOnPlate(other, false);
        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {        
        if ((other.collider.tag.Equals(_tagAfterCollision)))
        {
            Physics.IgnoreCollision(_collectibleCollider, other.collider, true);
        }
        
        if (other.collider.tag.Equals("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void AddIngredientOnPlate(Collider collisionWith, bool collision)
    {
        _rb.drag = 5f;
        _collectibleCollider.tag = _tagAfterCollision;
        //The main purpose of the damp is to push the object a little bit upward so that they don't bounce of each other
        _deltaFall *= 1 - _damp;

        gameObject.transform.parent = _collectibleContainer.transform;

        //This is not working as expected
        //Try raycasts                
        transform.position = new Vector3(_plate.transform.position.x, transform.position.y, transform.position.z);

        FixedJoint fj = gameObject.AddComponent<FixedJoint>();
        fj.connectedBody = collisionWith.gameObject.GetComponent<Rigidbody>();
    }

}