using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    
    [Header("References")]
    [SerializeField] TextMeshProUGUI waveCounterUI;
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] Animator anim;
    //[SerializeField] private Plot[] plots;
    //[SerializeField] private Turret[] turrets;

    private bool isMenuOpen = true;
    //private bool arePlotsAndRangesVisible = true;

    [SerializeField] private EnemySpawner enemySpawner;

    public void ToggleMenu() {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    private void Update()
    {
        UpdateWaveCounter();
    }

    private void OnGUI() {
        currencyUI.text = LevelManager.main.currency.ToString();
    }

    private void UpdateWaveCounter()
    {
        if (enemySpawner != null && waveCounterUI != null)
        {
            waveCounterUI.text = "Wave: " + enemySpawner.CurrentWave.ToString();
        }
    }

    //private void TogglePlotsAndRanges()
    //{
    //    arePlotsAndRangesVisible = !arePlotsandRangesVisible;

    //    foreach (Plot plot in plots)
    //    {
    //        plot.gameObject.setActive(arePlotsAndRangesVisible);
    //    }

    //    foreach (Turret turret in turrets)
    //    {
    //        turret.ToggleRangeVisibility(arePlotsAndRangesVisible);
    //    }
    //}

    public void SetSelected() {

    }

}
