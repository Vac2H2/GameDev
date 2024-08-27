using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    public GameObject towerObj;
    public Turret turret;
    private Color startColor;

    private void Start() {
        startColor = sr.color;
        //UIManager.main.RegisterPlot(this);
    }

    //public void SetVisibility(bool visible)
    //{
    //    sr.enabled = visible;
    //}

    private void OnMouseEnter() {
        sr.color = hoverColor;
    }

    private void OnMouseExit() {
        sr.color = startColor;
    }

    private void OnMouseDown() {
        if (UIManager.main.IsHoveringUI()) return;
        
        if (towerObj != null) {
            turret.OpenUpgradeUI();
            return;
            }

        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild.cost > LevelManager.main.currency) {
            Debug.Log("Insufficient Currency!");
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);

        Vector3 turretPosition = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);


        towerObj = Instantiate(towerToBuild.prefab, turretPosition, Quaternion.identity);
        turret = towerObj.GetComponent<Turret>();
    }

}
