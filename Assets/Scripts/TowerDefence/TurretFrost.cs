using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

public class TurretFrost : MonoBehaviour
{
    
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TMP_Text upgradeCostText;
    //[SerializeField] private GameObject rangeIndicator;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 150f; //Speed of tower rotation
    [SerializeField] private float aps = 1f; //attacks per second
    [SerializeField] private float freezeTime = 1f;
    [SerializeField] private int baseUpgradeCost = 150;

    private float apsBase;
    private float targetingRangeBase;

    private float freezeTimeBase;
    private Transform target;
    private float timeUntilFire;

    private int level = 1;

    private void Start()
    {
        targetingRangeBase = targetingRange;
        apsBase = aps;
        freezeTimeBase = freezeTime;
        //UIManager.main.RegisterTurret(this);

        //upgradeButton.onClick.AddListener(Upgrade);
        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(Upgrade);
            UpdateUpgradeCostText();
        }
    }

    private void Update() 
    {
        if (target == null)
        {
            FindTarget();
            return;
        }
        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / aps)
            {
                Shoot();
                FreezeEnemies();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void FreezeEnemies()
    {
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

            foreach (var hit in hits)
            {
                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                if (em != null)
                {
                    em.UpdateSpeed(0.2f); 
                    StartCoroutine(ResetEnemySpeed(em));
                }
            }
        }
    }


    private IEnumerator ResetEnemySpeed(EnemyMovement em) {
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
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

    private void OnDrawGizmosSelected() {

        Handles.color = Color.magenta;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange); //Draws arc showing range of tower

    }


}
