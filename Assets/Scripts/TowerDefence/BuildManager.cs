using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
     public static BuildManager main;

     [Header("References")]
     [SerializeField] private Tower[] towers;
     [SerializeField] private Button[] towerButtons;

    private int selectedTower = 0;
    private Button currentSelectedButton;

     private void Awake() {
        main = this;
     }

    private void Start()
    {
        SetSelectedTower(0);
    }

    public Tower GetSelectedTower() {
        return towers[selectedTower];
    }

    public void SetSelectedTower(int _selectedTower) 
    {
        if (currentSelectedButton != null)
        {
            currentSelectedButton.GetComponent<Image>().color = Color.white;
        }
        selectedTower = _selectedTower;
        currentSelectedButton = towerButtons[selectedTower];

        currentSelectedButton.GetComponent<Image>().color = Color.green;
    }

}
