using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hud : MonoBehaviour
{

    public Sprite[] HeartSprites;

    public Image HeartUI;

    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        HeartUI.sprite = HeartSprites[player.curHealth];
    }
}
