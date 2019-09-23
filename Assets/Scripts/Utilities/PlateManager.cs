using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateManager : MonoBehaviour
{

    [HideInInspector]
    public static List<Collectible> ingredients;
    [HideInInspector]
    public static string lastIngredientType = "";
    [HideInInspector]
    public static int countSameIngredients = 0;

    GameManager gameManager;
    bool locked = false;
    BoxCollider boxCollider;
    public ParticleSystem effectOnCollect;

    float leap = 1.7f;

    // Use this for initialization
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ingredients = new List<Collectible>();
        boxCollider = GetComponent<BoxCollider>();
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (!locked)
        {

            locked = true;

            UpdateCount(other);

            MoveBoxColliderCenterUp();

            CheckPlate();

            locked = false;
        }
    }

    private void UpdateCount(Collider ingredientCollider)
    {
        Collectible ing = ingredientCollider.gameObject.GetComponent<Collectible>();
        ingredients.Add(ing);

        if (ing.ingType.Contains("bad"))
        {
            countSameIngredients = 0;
            return;
        }

        UpdateScore(ing, false);

        if (lastIngredientType == "")
        {
            countSameIngredients++;
            lastIngredientType = ing.ingType;

        }
        else if (lastIngredientType != "" && lastIngredientType.Equals(ing.ingType))
        {
            countSameIngredients++;
            lastIngredientType = ing.ingType;
        }
        else if (lastIngredientType != "" && !lastIngredientType.Equals(ing.ingType))
        {
            countSameIngredients = 1;
            lastIngredientType = ing.ingType;
        }
    }
    private void UpdateScore(Collectible ingredient, bool multiple)
    {
        if (gameManager == null) return;
        if (!multiple)
            gameManager.Score += ingredient.value * 5;
        else
        {
            gameManager.Score += ingredient.value * 5 * 3;
        }
    }

    private void CheckPlate()
    {
        if (countSameIngredients > 2)
        {
            //Destroying last 3 same ingredients 
            effectOnCollect.transform.position = new Vector3(transform.position.x, 
                                                             ingredients[ingredients.Count - 2].gameObject.transform.position.y, 
                                                             transform.position.z);
            effectOnCollect.GetComponent<Renderer>().material = ingredients[ingredients.Count - 2].gameObject.GetComponent<Renderer>().material;
            effectOnCollect.Play();
            Destroy(ingredients[ingredients.Count - 3].gameObject);
            Destroy(ingredients[ingredients.Count - 2].gameObject);
            Destroy(ingredients[ingredients.Count - 1].gameObject);

            UpdateScore(ingredients[ingredients.Count - 1], true);

            ingredients.RemoveRange(ingredients.Count - 3, 3);

            //Checking if the plate is not empty
            if (ingredients.Count > 0)
            {
                //If the plate is not empty, get the two last elements
                int last = ingredients.Count - 1;
                int beforeLast = ingredients.Count - 2;

                //Save the last element type
                lastIngredientType = ingredients[last].ingType;

                //Compare the two last elements remaining, to see if they are similar
                if (beforeLast >= 0 && last >= 0 && ingredients[beforeLast].ingType.Equals(lastIngredientType))
                    countSameIngredients = 2; //Two similar
                else
                    countSameIngredients = 1; //Only one element of that type

            }
            else
            {
                //If the list is empty, set these options to their default parameters
                lastIngredientType = "";
                countSameIngredients = 0;
            }

            //Since we destroy the three last same ingredients, we move back the detection zone 3 times back
            MoveBoxColliderCenterDown(3);
        }
    }
    public void MoveBoxColliderCenterUp()
    {
        boxCollider.center += Vector3.up * leap;
    }

    private void MoveBoxColliderCenterDown(int leapCounts)
    {
        boxCollider.center += Vector3.down * (leapCounts * leap);
    }

    public static BoxCollider GetBoxCollider(PlateManager pm)
    {
        return pm.boxCollider;
    }
}