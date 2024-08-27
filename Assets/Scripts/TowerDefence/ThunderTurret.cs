using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

public class ThunderTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TMP_Text upgradeCostText;
    [SerializeField] private GameObject shockEffectPrefab;
    //[SerializeField] private GameObject rangeIndicator;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float aps = 2f; // Attacks per second
    [SerializeField] private float shockDuration = 1f; // Duration of the shock effect
    [SerializeField] private float shockDamage = 10f; // Damage dealt by shock
    [SerializeField] private int baseUpgradeCost = 150;

    private float apsBase;
    private float targetingRangeBase;
    private float shockDamageBase;
    private float shockDurationBase;
    private float timeUntilNextShock;

    private int level = 1;

    private void Start()
    {
        targetingRangeBase = targetingRange;
        apsBase = aps;
        shockDamageBase = shockDamage;
        shockDurationBase = shockDuration;
        //UIManager.main.RegisterTurret(this);


        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(Upgrade);
            UpdateUpgradeCostText();
        }
    }

    private void Update()
    {
        timeUntilNextShock += Time.deltaTime;

        if (timeUntilNextShock >= 1f / aps)
        {
            if (AreEnemiesInRange())
            {
                ApplyShockEffect();
            }
            timeUntilNextShock = 0f;
        }
    }

    private bool AreEnemiesInRange()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, targetingRange, LayerMask.GetMask("Enemy"));
        return enemies.Length > 0;

    }

    private void ApplyShockEffect()
    {
        // Create and manage the shock effect
        GameObject shockEffect = Instantiate(shockEffectPrefab, transform.position, Quaternion.identity);
        ShockEffect shockEffectScript = shockEffect.GetComponent<ShockEffect>();
        shockEffectScript.Initialize(targetingRange, shockDamage, shockDuration);

        Destroy(shockEffect, shockDuration); // Destroy the shock effect after its duration
    }

    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    public void Upgrade()
    {
        if (CalculateCost() > LevelManager.main.currency) return;

        LevelManager.main.SpendCurrency(CalculateCost());

        level++;

        aps = CalculateAPS();
        targetingRange = CalculateRange();
        shockDamage = CalculateShockDamage();
        shockDuration = CalculateShockDuration();

        UpdateUpgradeCostText();
        CloseUpgradeUI();
    }

    private int CalculateCost()
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 0.8f));
    }

    private float CalculateAPS()
    {
        return apsBase * Mathf.Pow(level, 0.2f);
    }

    private float CalculateRange()
    {
        return targetingRangeBase * Mathf.Pow(level, 0.2f);
    }

    private float CalculateShockDamage()
    {
        return shockDamageBase * Mathf.Pow(level, 0.2f);
    }

    private float CalculateShockDuration()
    {
        return shockDurationBase * Mathf.Pow(level, 0.1f);
    }

    private void UpdateUpgradeCostText()
    {
        if (upgradeCostText != null)
        {
            upgradeCostText.text = $"Upgrade Cost: {CalculateCost()}";
        }
    }

    //public void ToggleRangeVisibility(bool visible)
    //{
    //    if (rangeIndicator != null)
    //    {
    //        rangeIndicator.SetActive(visible);
    //    }
    //}

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.yellow; // Different color for thunder turret range indicator
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange); // Draws arc showing range of turret
    }
}
