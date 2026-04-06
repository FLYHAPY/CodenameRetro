using System;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private float flashtime;
    [SerializeField] private CamaraEffects camaraEffects;
    
    private float _timer;
    private bool _isFlashing = false;

    private void Start()
    {
        healthText.text = "Health:" + health.ToString();
    }

    private void Update()
    {
        if (_isFlashing)
        {
            _timer += Time.deltaTime;
            float learpedAmount = Mathf.Lerp(1f, 0, (_timer / flashtime));
            playerSprite.material.SetFloat("_WhiteAmount", learpedAmount);
        }

        if (_timer >= flashtime)
        {
            _isFlashing = false;
            _timer = 0;
        }
    }
    public void ReduceHealth(float amount)
    {
        health -= amount;
        healthText.text = "Health:" + health;
        camaraEffects.Shake();
        Flash();
    }
    void Flash()
    {
        _isFlashing = true;
        _timer = 0;
        playerSprite.material.SetFloat("_WhiteAmount", 0f);
    }
}
