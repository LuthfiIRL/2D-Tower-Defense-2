using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    private GameObject towerObj;
    public Turret turret;    
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        if (UIManager.main.IsHoveringUI()) return;
        
        if (towerObj != null)
        {
            turret.OpenUpgradeUI();            
            return;
        }

        Tower towerToBuild = BuildManager.main.GetSelectedTower();
        
        if (towerToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("You cant afford this tower");
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);
        
        towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        turret = towerObj.GetComponent<Turret>();        
    }
}
