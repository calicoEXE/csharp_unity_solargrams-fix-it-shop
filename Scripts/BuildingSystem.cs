using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem current;

    public GridLayout gridlayout;
    Grid grid;

    [SerializeField] Tilemap mainTileMap;
    [SerializeField] TileBase whiteTile;

    public GameObject prefab0;
    public GameObject prefab1;

    //private PlaceableObject objectToPlace;

    #region Unity methods

    private void Awake()
    {
        current = this;
        grid = gridlayout.gameObject.GetComponent<Grid>();
    }
    #endregion

    #region Utils

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetSelectWorldPosition();

        }

        //  //Instaniates the object where it need to go
        //  if (Input.GetKeyDown(KeyCode.F))
        //  {
        //      InitializeObject(prefab1);
        //  }
    }
    //find mouse position using raycast
    public static Vector3 GetSelectWorldPosition()
    {
        // //send raycast from mouse
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // //onhit return position of raycast
        // if(Physics.Raycast(ray, out RaycastHit raycastHit))
        // {
        //     return raycastHit.point;
        // }
        // else
        // {
        //     return Vector3.zero;
        // }
        GameObject playerGO = GameObject.Find("Player");
        Vector3 direction = playerGO.transform.forward; // The forward direction of the GameObject
        float rayDistance = 1f; // You can adjust the raycast distance as needed

        Ray ray = new Ray(playerGO.transform.position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            Debug.Log(hit.collider.name);
            return hit.point;
        }
        else
        {
            return playerGO.transform.position + direction * rayDistance; // Fallback position
        }

    }
    //snaps coordinates
    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        //gets cell from gridlayout
        Vector3Int cellPos = gridlayout.WorldToCell(position);
        //gets the centre of the cell and returns it
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    #endregion

 //
 //  #region Building Placement
 //  public void InitializeObject(GameObject prefab)
 //  {
 //      Vector3 position = SnapCoordinateToGrid(Vector3.zero);
 //      GameObject obj = Instantiate(prefab, position, Quaternion.identity );
 //      objectToPlace = obj.GetComponent<PlaceableObject>();
 //      obj.AddComponent<ObjectDrag>();
 //
 //  }
 //
 //  #endregion
}