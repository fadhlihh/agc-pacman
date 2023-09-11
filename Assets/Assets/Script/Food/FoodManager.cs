using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private GameManager _gameManager;

    private List<FoodItem> _foodItemList = new List<FoodItem>();

    private void Start()
    {
        InitFoodItemList();
    }

    private void InitFoodItemList()
    {
        FoodItem[] foodItemArray = GameObject.FindObjectsOfType<FoodItem>();
        for (int i = 0; i < foodItemArray.Length; i++)
        {
            foodItemArray[i].OnConsume += OnConsumeFoodItem;
            _foodItemList.Add(foodItemArray[i]);
        }
    }

    private void OnConsumeFoodItem(FoodItem foodItem)
    {
        if (foodItem.FoodType == FoodType.Big)
        {
            _player.ConsumePowerUp();
        }
        _foodItemList.Remove(foodItem);
        if (_foodItemList.Count <= 0)
        {
            _gameManager?.Win();
        }
    }
}
