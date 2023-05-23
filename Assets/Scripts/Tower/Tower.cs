using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int cost = 75;
    [SerializeField] private GameObject _costText;
   
    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        if(bank == null)
        {
            return false;
        }

        if(bank.CurrentBalance >= cost)
        {
            Instantiate(tower, position, Quaternion.identity);
            Instantiate(_costText,new Vector3(position.x, position.y + 6, position.z), _costText.transform.rotation);

            bank.Withdraw(cost);
            return true;
        }

        return false;
        
    }
}
