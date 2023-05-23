using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _goldReward = 25;
    [SerializeField] private int _goldPenalty = 25;
    [SerializeField] private GameObject _rewardText;
    [SerializeField] private GameObject _stealText;

    private Bank _bank;
    // Start is called before the first frame update
    void Start()
    {
        _bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if(_bank == null)
        {
            return;
        }
        _bank.Deposit(_goldReward);
        Instantiate(_rewardText, new Vector3(transform.position.x, transform.position.y + 6, transform.position.z), _rewardText.transform.rotation);
    }

    public void StealGold()
    {
        if (_bank == null)
        {
            return;
        }

        Instantiate(_stealText, new Vector3(transform.position.x, transform.position.y + 6, transform.position.z), _stealText.transform.rotation);
        _bank.Withdraw(_goldPenalty);
    }
}
