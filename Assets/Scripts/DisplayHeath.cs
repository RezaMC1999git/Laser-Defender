using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHeath : MonoBehaviour
{
    TextMeshProUGUI health;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            health.text = 0.ToString();
            return;
        }
        health.text = player.GetPlayerHealth().ToString();
    }
}
