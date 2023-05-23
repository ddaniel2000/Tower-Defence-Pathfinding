using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowDamage : MonoBehaviour
{
    [SerializeField] private TextMeshPro _damagetext;

    public void DisaplyDamage(int currentHealth, int maxHealth)
    {
        _damagetext.text = currentHealth + "/" + maxHealth;
    }

}
