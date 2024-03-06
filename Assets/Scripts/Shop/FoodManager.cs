using UnityEngine;

[System.Serializable]
public class Food
{
    public int id;
    public string name;

    public Food(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}

public class FoodManager : MonoBehaviour
{
    public static FoodManager instance; // Singleton instance

    public Food[] foods; // Array of food items

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to find a food item by its ID
    public Food GetFoodById(int id)
    {
        foreach (Food food in foods)
        {
            if (food.id == id)
            {
                return food;
            }
        }
        return null; // Return null if no food with the given ID is found
    }
}
