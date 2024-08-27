using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager main;

    //private List<Plot> plots = new List<Plot>();

    //private List<TurretRange> turrets = new List<TurretRange>();

    private bool isHoveringUI;

    private void Awake() {
        main = this;
    }

    public void SetHoveringState(bool state) {
        isHoveringUI = state;
    }

    public bool IsHoveringUI() {
        return isHoveringUI;
    }

    //public void RegisterPlot(Plot plot)
    //{
    //    plots.Add(plot);
    //}

    //public void RegisterTurret(Turret turret)
    //{
    //    if (!turrets.Contains(turret))
    //    {
    //        turrets.Add(turret);
    //    }
    //}

    //public void ToggleAllTurretRanges(bool visible)
    //{
    //    foreach (var turret in turrets)
    //    {
    //        turret.ToggleRangeVisibility(visible);
    //    }
    //}
}
