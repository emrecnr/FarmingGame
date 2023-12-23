using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolSelector : MonoBehaviour
{
    public enum Tool {None,Sow,Water,Harvest}
    private Tool _activeTool;

    [Header("----- REFERENCES -----")]
    [SerializeField] private Color _activeToolColor;
    [SerializeField] private Image[] toolImages;

    private int _activeToolIndex;
    public int ActiveToolIndex => _activeToolIndex;

    public event Action OnNoneSelected;
    public event Action OnSowSelected;
    public event Action OnWaterSelected;
    public event Action OnHarvestSelected;

    private void Start()
    {
        SelectTool(0);
    }
   
    public void SelectTool(int toolIndex)
    {
        _activeTool = (Tool)toolIndex;
        for (int i = 0; i < toolImages.Length; i++)
        {
            toolImages[i].color = i == toolIndex ? _activeToolColor : Color.white;
        }

        switch (_activeTool)
        {
            case Tool.Sow:
                OnSowSelected?.Invoke();
            break;

            case Tool.Water:
                OnWaterSelected?.Invoke();
                break;

            case Tool.Harvest:
                OnHarvestSelected?.Invoke();
                break;
            case Tool.None:
                OnNoneSelected?.Invoke();
                break;
        }
    }   
    

}
