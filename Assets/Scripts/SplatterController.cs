using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class SplatterController : MonoBehaviour
{
    // Start is called before the first frame update
    private int[,] splatterStruct; 
    private int[,] splatterStructOld;
    private List<(int,int)> structUpdate;
    public Tilemap splatterMap;
    public TileBase splatter;
    public TileBase clear;
    void Start()
    {
        splatterStruct = new int[128,52];
        splatterStructOld = splatterStruct;
        structUpdate =  new List<(int,int)>();        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (structUpdate.Count > 0) //Has there been any new splatters
        {
            //Remove the duplicate entries as the newest splatter overwrites the old one
            var structUpdateSet = structUpdate.Distinct().ToList(); 
            foreach (var item in structUpdateSet)
            {
                (int,int) pos = DataToCell(item);
                splatterMap.SetTile(new Vector3Int(pos.Item1,pos.Item2,0),splatter); //set the splatter
            }
            structUpdate.Clear();
        }
    }

    

   

    public void Propagate(Vector3Int cellPos, float stren,int color) //Splatter Propagation Code
    {
        
        //randomly check if splatter strenght is enough and checks if splatter is in bounds
        if ((stren >= Random.Range(0.0f,1.0f)) 
            && cellPos.y <= 11 && cellPos.y >= -40 && cellPos.x <= 63 && cellPos.x >= -64) 
        {

            (int, int) dataP = CellToData((cellPos.x,cellPos.y));
           
            splatterStruct[dataP.Item1,dataP.Item2] = color;    //update the splatter data structure
            structUpdate.Add(dataP);                            //add the tile position that needs to be updated            
            
            //Propagate at the cardinal directions            
            cellPos.y += 1;
            Propagate(cellPos,stren - 0.1f, color);
            cellPos.y -= 2;
            Propagate(cellPos,stren - 0.1f, color);
            cellPos.y +=1;
            cellPos.x +=1;
            Propagate(cellPos,stren - 0.1f, color);
            cellPos.x -= 2;
            Propagate(cellPos,stren - 0.1f, color);           

        }
    }

    private (int,int) CellToData((int,int) pos) //Converts cell position to the data position.
    {
        return (pos.Item1 +64,-pos.Item2+11);
    }

     private (int,int) DataToCell((int,int) pos ) //Converts data position to the cell position.
    {
        return (pos.Item1 -64,-pos.Item2+11);
    }
}
