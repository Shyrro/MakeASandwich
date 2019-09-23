using UnityEngine;

public class EndGame : MonoBehaviour
{

    string ingredientTag = "Ingredient";
    GameManager gameManager;
    Collider objCollider;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        objCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (PlateManager.ingredients.Count == 20)
        {
            gameManager.ShowGameOverScreen();
        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        // Debug.Log(other.collider.tag);
        if (other.collider.tag.Equals(ingredientTag))
        {
            Physics.IgnoreCollision(other.collider, objCollider, true);
        }

    }

}