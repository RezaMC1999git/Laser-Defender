﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage;
    // Start is called before the first frame update
    public int getDamage() 
    {
        return damage;
    }
    public void Hit() 
    {
        Destroy(gameObject);
    }
}
