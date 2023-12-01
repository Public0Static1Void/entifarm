using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject shop_parent;
    private bool set_shop = false;
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            set_shop = !set_shop;
            OpenShop(set_shop);
        }
    }

    public void OpenShop(bool action)
    {
        if (action)
            shop_parent.SetActive(true);
        else
            shop_parent.SetActive(false);
    }
}
