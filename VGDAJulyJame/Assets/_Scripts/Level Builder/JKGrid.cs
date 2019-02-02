using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Linq;
using UnityEngine.UI;

#if UNITY_EDITOR
namespace jkGenerator
{
    //TODO: replace 2d grid object array in patternbuilder, have it use this instead.
    [ExecuteInEditMode]
    [System.Serializable]
    public class JKGrid
    {
        private Vector2 gridSize, cellSize;
        private int subdivisions;
        private (GameObject, Vector2)[,] index;
        private Vector2 worldSpacePos;
        private GameObject parentObj;
        
        //Square Grid Constructor
        public JKGrid(float size, int subdivisions, Vector2 centerPos, GameObject attachedObj)
        {
            gridSize = new Vector2(size, size);
            //Debug.Log("Grid Size: " + gridSize);
            this.subdivisions = subdivisions;
            cellSize = new Vector2(gridSize.x/subdivisions, gridSize.y/subdivisions);
            index = new (GameObject, Vector2)[subdivisions, subdivisions];
            worldSpacePos = centerPos;
            parentObj = attachedObj;
            Initialize();
        }
        
        //Image-based constructor
        public JKGrid(Image img, int subdivisions, Vector2 centerPos, GameObject attachedObj)
        {
            Rect imgRect = img.sprite.rect;
            gridSize.x = imgRect.width;
            gridSize.y = imgRect.height;
            //Debug.Log("Grid Size: " + gridSize);
            this.subdivisions = subdivisions;
            cellSize = new Vector2(gridSize.x / subdivisions, gridSize.y/subdivisions);
            index = new (GameObject, Vector2)[subdivisions, subdivisions];
            worldSpacePos = centerPos;
            parentObj = attachedObj;
            Initialize();
        }
        
        //Sprite-based constructor
        public JKGrid(Sprite sprite, int subdivisions, Vector2 centerPos, GameObject attachedObj)
        {
            
            gridSize = sprite.bounds.size;
            //Debug.Log("Grid Size: " + gridSize);
            this.subdivisions = subdivisions;
            cellSize = new Vector2(gridSize.x / subdivisions, gridSize.y/subdivisions);
            index = new (GameObject, Vector2)[subdivisions, subdivisions];
            worldSpacePos = centerPos;
            parentObj = attachedObj;
            Initialize();
        }
        
        /// <summary>
        /// Creates grid in world space based on constructor args
        /// </summary>
        private void Initialize()
        {
            //First "Cell" position
            float initialX = (worldSpacePos.x - .5f * gridSize.x) + (.5f * cellSize.x);
            float initialY = (worldSpacePos.y + .5f *gridSize.y) - (.5f * cellSize.y);
            Vector2 position = new Vector2(initialX, initialY);
            //initialize all grid positions
            for (int i = 0; i < index.GetLength(0); i++){
                for (int j = 0; j < index.GetLength(1); j++){
                    index[i, j].Item2 = new Vector2(position.x + (cellSize.x * i), position.y - (cellSize.y * j));
                }
            }

        }
        /// <summary>
        /// Insert item into first empty grid position, starting at upper left-hand corner
        /// </summary>
        /// <param name="g"></param>
        public void Insert(GameObject g)
        {
            for(int i = 0;i < index.GetLength(0); i++){
                for (int j = 0; j < index.GetLength(1); j++){
                    if (index[i, j].Item1 == null){
                        index[i, j].Item1 = Object.Instantiate(g, index[i, j].Item2, Quaternion.identity, parentObj.transform);
                        return;
                    }
                }
            }
            Debug.Log("Could not insert! No empty space available.");
        }

        /// <summary>
        /// Set grid position based on int x and int y coordinates, to GameObject g
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="g"></param>
        public void Set(int x, int y, GameObject g)
        {
            if(index[x, y].Item1 != null)
                Object.DestroyImmediate(index[x, y].Item1);
            if (g == null)
                return;
            index[x, y].Item1 = Object.Instantiate(g, index[x, y].Item2, Quaternion.identity, parentObj.transform);
            index[x, y].Item1.transform.localScale = cellSize;
        }

        /// <summary>
        /// Set grid position based on World Space coordinates, to GameObject g - only works if the position at the
        /// param location is APPROXIMATELY on the grid
        /// </summary>
        /// <param name="location"></param>
        /// <param name="g"></param>
        public void Set(Vector2 location, GameObject g)
        {
            for(int i = 0;i < index.GetLength(0); i++){
                for (int j = 0; j < index.GetLength(1); j++){
                    if (index[i, j].Item2 == location){
                        if (index[i, j].Item1 != null) Object.DestroyImmediate(index[i, j].Item1);
                        if (g == null)
                            return;
                        index[i, j].Item1 = Object.Instantiate(g, index[i, j].Item2, Quaternion.identity, parentObj.transform);
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// Destroy object at grid position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Remove(int x, int y)
        {
            if(index[x, y].Item1 != null)
                Object.DestroyImmediate(index[x, y].Item1);
            else
                Debug.Log("Nothing to remove at location given.");
        }
        
        /// <summary>
        /// Destroy object at Vector2 location
        /// </summary>
        /// <param name="location"></param>
        public void Remove(Vector2 location)
        {
            for(int i = 0;i < index.GetLength(0); i++){
                for (int j = 0; j < index.GetLength(1); j++){
                    if (index[i, j].Item2 == location){
                        if (index[i, j].Item1 != null) Object.DestroyImmediate(index[i, j].Item1);
                        return;
                    }
                }
            }
            Debug.Log("Either location does not exist, or there was nothing to remove.");
        }
        
        /// <summary>
        /// Destroy all grid objects
        /// </summary>
        public void RemoveAll()
        {
            if (index != null){
                foreach (var element in index) {
                    if (element.Item1 != null)
                        Object.DestroyImmediate(element.Item1);
                    element.Item2.Set(0f, 0f);
                }
            }
        }
        
        /// <summary>
        /// Get position of the entire grid in world space
        /// </summary>
        /// <returns></returns>
        public Vector2 GetWorldPos(){ return worldSpacePos; }

        /// <summary>
        /// Manually set grid size
        /// </summary>
        /// <param name="s"></param>
        public void SetSize(Vector2 s){ gridSize = s; }
        
        /// <summary>
        /// Get grid's size
        /// </summary>
        /// <returns></returns>
        public Vector2 GetSize(){ return gridSize; }

        /// <summary>
        /// Get size of individual cell with Vector2 dimensions
        /// </summary>
        /// <returns></returns>
        public Vector2 GetCellSize(){ return cellSize; }
        
        /// <summary>
        /// Return all objects in the grid as a List of GameObjects
        /// </summary>
        /// <returns></returns>
        public List<GameObject> GetObjects()
        {
            List<GameObject> gameObjects = new List<GameObject>();
            foreach (var element in index)
            {
                if(element.Item1 != null)
                    gameObjects.Add(element.Item1);
            }
            return gameObjects;
        }
        
        /// <summary>
        /// Return each worldspace position and object associated with it in the grid
        /// </summary>
        /// <returns></returns>
        public (GameObject, Vector2)[,] GetIndexes(){ return index; }
        
        /// <summary>
        /// Print each position and object in the grid
        /// </summary>
        /// <returns></returns>
        string PrintGrid()
        {
            string g = "";
            for (int i = 0; i < index.GetLength(0); i++){
                for (int j = 0; j < index.GetLength(1); j++){
                    g += "( " + i + " , " + j + " ): "+ index[i, j].Item1 + " at location " + index[i, j].Item2;
                }
            }
            return g;
        }
        
        /// <summary>
        /// Return all grid information
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Grid Size: " + gridSize + "\n" +
                   "Cell Size: " + cellSize + "\n" +
                   PrintGrid();
        }
        
    }
}
#endif