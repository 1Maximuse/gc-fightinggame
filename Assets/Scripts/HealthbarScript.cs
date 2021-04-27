using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarScript : MonoBehaviour
{
    [SerializeField]
    private PlayerScript player;

    [SerializeField]
    private RectTransform redBar;

    private float maxWidth;
    private float height;

    private void Start()
    {
        maxWidth = redBar.rect.width;
        height = redBar.rect.height;
    }

    void Update()
    {
        int hp = player.GetHP();
        redBar.sizeDelta = new Vector2((float)hp / 100 * maxWidth, height);
    }
}
