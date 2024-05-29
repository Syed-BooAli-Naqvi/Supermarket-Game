using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllProducts : Singleton<AllProducts>
{
    public List<ProductAmounts> allProducts;
    public static List<ProductAmounts> AllProduct { get { return Instance.allProducts; } }
    public List<Product> allProductObjects;
    public static List<Product> AllProductObjects { get { return Instance.allProductObjects; } }

    public bool test;
    private void OnValidate()
    {
        if (test)
        {
            test = false;
            for (int i = 0; i < allProducts.Count; i++)
            {
                int a = Random.Range(30, 100);
                allProducts[i].deliveryTime = a;
                if (a>29 && a<60)
                {
                    allProducts[i].light = 3;
                }
                else if (a>60)
                {
                    allProducts[i].light = 6;
                }
            }
        }   
    }
}
