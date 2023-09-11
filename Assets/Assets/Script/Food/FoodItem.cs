using System;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    [SerializeField]
    public FoodType FoodType;

    public Action<FoodItem> OnConsume;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (OnConsume != null)
            {
                OnConsume(this);
            }
            Destroy(gameObject);
        }
    }
}
