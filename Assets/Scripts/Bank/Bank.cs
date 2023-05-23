using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Bank : MonoBehaviour
{
    [SerializeField] private int _startBalance = 150;
    [SerializeField] private int _currentBalance;
    [SerializeField] private TextMeshProUGUI _displayBalance;
    public int CurrentBalance {get { return _currentBalance; } }

    private void Awake()
    {
        _currentBalance = _startBalance;
        UpdateDisplayBalance();
    }

    public void Deposit(int amount)
    {
        _currentBalance += Mathf.Abs(amount); // remove the negative sign infront of the number
        UpdateDisplayBalance();
    }

    public void Withdraw(int amount)
    {
        _currentBalance -= Mathf.Abs(amount);
        UpdateDisplayBalance();

        if (_currentBalance < 0)
        {
            ReloadScene();
        }
    }

    private void UpdateDisplayBalance()
    {
        _displayBalance.text = "Gold: " + _currentBalance;
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

}
