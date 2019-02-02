using System.Collections;
using System.Collections.Generic;
using jkGenerator;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class LevelBuilder : MonoBehaviour
{
    
    public Image gridConstructorImage;
    public Sprite gridConstructorSprite;
    public float size;
    public int divisions;
    [HideInInspector]
    public bool allowDisplay;
    [HideInInspector]
    public JKGrid grid;

    public void Construct()
    {
        if (gridConstructorImage)
            grid = new JKGrid(gridConstructorImage, divisions, gameObject.transform.position, gameObject);
        else if (gridConstructorSprite)
            grid = new JKGrid(gridConstructorSprite, divisions, gameObject.transform.position, gameObject);
        else
            grid = new JKGrid(size, divisions, gameObject.transform.position, gameObject);
        allowDisplay = true;
        Debug.Log(grid);
    }

    public void Deconstruct()
    {
        grid?.RemoveAll();
        foreach(Transform child in gameObject.transform)
            DestroyImmediate(child.gameObject);
    }

    public void Place(int x, int y, GameObject g)
    {
        if(g)
            grid?.Set(x, y, g);
        else
        {
            grid?.Remove(x, y);
        }
    }

    public void Place(GameObject g)
    {
        if(g)    
            grid.Insert(g);
        else
            Debug.Log("No object set to insert.");
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (allowDisplay)
        {
            foreach (var element in grid.GetIndexes())
            {
                if(!element.Item1)
                    Gizmos.DrawSphere(element.Item2, grid.GetCellSize().x * .1f);
            }

            Gizmos.color = Color.magenta;
            //Horizontal
            Gizmos.DrawLine(
                new Vector3(grid.GetWorldPos().x - .5f * grid.GetSize().x, grid.GetWorldPos().y + .5f * grid.GetSize().y),
                new Vector3(grid.GetWorldPos().x + .5f * grid.GetSize().x, grid.GetWorldPos().y + .5f * grid.GetSize().y));
            Gizmos.DrawLine(
                new Vector3(grid.GetWorldPos().x - .5f * grid.GetSize().x, grid.GetWorldPos().y - .5f * grid.GetSize().y),
                new Vector3(grid.GetWorldPos().x + .5f * grid.GetSize().x, grid.GetWorldPos().y - .5f * grid.GetSize().y));
            //Vertical
            Gizmos.DrawLine(
                new Vector3(grid.GetWorldPos().x - .5f * grid.GetSize().x, grid.GetWorldPos().y + .5f * grid.GetSize().y),
                new Vector3(grid.GetWorldPos().x - .5f * grid.GetSize().x, grid.GetWorldPos().y - .5f * grid.GetSize().y));
            Gizmos.DrawLine(
                new Vector3(grid.GetWorldPos().x + .5f * grid.GetSize().x, grid.GetWorldPos().y + .5f * grid.GetSize().y),
                new Vector3(grid.GetWorldPos().x + .5f * grid.GetSize().x, grid.GetWorldPos().y - .5f * grid.GetSize().y));
        }
    }
}
