using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.Collections;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class SplatterController : MonoBehaviour
{
    // Start is called before the first frame update
    private int[,] splatterStruct; 
    private List<(int,int)> structUpdate;
    public Tilemap splatterMap;
    public TileBase[] colors;
    public GameObject crowd;

    
    void Start()
    {
        splatterStruct = new int[128,52];       
        structUpdate =  new List<(int,int)>();
        
       
        
    }


    float[] ArrayCount()
    {
        float[] newD = new float[4];
        int[] tempStruct = splatterStruct.Cast<int>().ToArray();
        for (int i = 0; i < 4; i++)
        {
            newD[i] = Array.FindAll<int>(tempStruct,elem => elem == i+1).Length;
        }
        return newD;
    }
    // Update is called once per frame
    void Update()
    {
        if (structUpdate.Count > 0) //Has there been any new splatters
        {
            //Remove the duplicate entries as the newest splatter overwrites the old one
            var structUpdateSet = structUpdate.Distinct().ToList(); 
            foreach (var dataP in structUpdateSet)
            {
                (int,int) pos = DataToCell(dataP);
                
                splatterMap.SetTile(new Vector3Int(pos.Item1,pos.Item2,0),colors[splatterStruct[dataP.Item1,dataP.Item2]]); //set the splatter
            }
            crowd.GetComponent<CrowdController>().updateCrowd(ArrayCount());
            structUpdate.Clear();
        }
    }

    

   

    public void Propagate(Vector3Int cellPos, float stren,int color) //Splatter Propagation Code
    {
        
        //randomly check if splatter strenght is enough and checks if splatter is in bounds
        if ((stren >= UnityEngine.Random.Range(0.0f,1.0f)) 
            && cellPos.y <= 11 && cellPos.y >= -40 && cellPos.x <= 63 && cellPos.x >= -64) 
        {

            (int, int) dataP = CellToData((cellPos.x,cellPos.y));
           
            splatterStruct[dataP.Item1,dataP.Item2] = color;    //update the splatter data structure
            structUpdate.Add(dataP);                            //add the tile position that needs to be updated            
            
            //Propagate at the cardinal directions            
            cellPos.y += 1;
            PropagateUp(cellPos,stren - 0.1f, color);
            cellPos.y -= 2;
            PropagateDown(cellPos,stren - 0.1f, color);
            cellPos.y +=1;
            cellPos.x +=1;
            PropagateRight(cellPos,stren - 0.1f, color);
            cellPos.x -= 2;
            PropagateLeft(cellPos,stren - 0.1f, color);           

        }
    }

    public void PropagateUp(Vector3Int cellPos, float stren,int color)
     {
        //randomly check if splatter strenght is enough and checks if splatter is in bounds
        if ((stren >= UnityEngine.Random.Range(0.0f,1.0f)) 
            && cellPos.y <= 11 && cellPos.y >= -40 && cellPos.x <= 63 && cellPos.x >= -64) 
        {

            (int, int) dataP = CellToData((cellPos.x,cellPos.y));
           
            splatterStruct[dataP.Item1,dataP.Item2] = color;    //update the splatter data structure
            structUpdate.Add(dataP);                            //add the tile position that needs to be updated            
            
            //Propagate at the cardinal directions            
            cellPos.y += 1;
            PropagateUp(cellPos,stren - 0.1f, color);
            cellPos.y -=1;
            cellPos.x +=1;
            PropagateRight(cellPos,stren - 0.1f, color);
            cellPos.x -= 2;
            PropagateLeft(cellPos,stren - 0.1f, color);           

        }
    }
    public void PropagateDown(Vector3Int cellPos, float stren,int color)
    {
        //randomly check if splatter strenght is enough and checks if splatter is in bounds
        if ((stren >= UnityEngine.Random.Range(0.0f,1.0f)) 
            && cellPos.y <= 11 && cellPos.y >= -40 && cellPos.x <= 63 && cellPos.x >= -64) 
        {

            (int, int) dataP = CellToData((cellPos.x,cellPos.y));
           
            splatterStruct[dataP.Item1,dataP.Item2] = color;    //update the splatter data structure
            structUpdate.Add(dataP);                            //add the tile position that needs to be updated            
            
                    
         
            
            cellPos.y -=1;
            PropagateDown(cellPos,stren - 0.1f, color);
            cellPos.y +=1;
            cellPos.x +=1;
            PropagateRight(cellPos,stren - 0.1f, color);
            cellPos.x -=2;
            PropagateLeft(cellPos,stren - 0.1f, color);
           
                      

        }
    }
    public void PropagateRight(Vector3Int cellPos, float stren,int color)
    {
        //randomly check if splatter strenght is enough and checks if splatter is in bounds
        if ((stren >= UnityEngine.Random.Range(0.0f,1.0f)) 
            && cellPos.y <= 11 && cellPos.y >= -40 && cellPos.x <= 63 && cellPos.x >= -64) 
        {

            (int, int) dataP = CellToData((cellPos.x,cellPos.y));
           
            splatterStruct[dataP.Item1,dataP.Item2] = color;    //update the splatter data structure
            structUpdate.Add(dataP);                            //add the tile position that needs to be updated            
            
            //Propagate at the cardinal directions            
            cellPos.y += 1;
            PropagateUp(cellPos,stren - 0.1f, color);
            cellPos.y -= 2;
            PropagateDown(cellPos,stren - 0.1f, color);
            cellPos.y +=1;
            cellPos.x +=1;
            PropagateRight(cellPos,stren - 0.1f, color);
           
                      

        }
    }
    public void PropagateLeft(Vector3Int cellPos, float stren,int color)
    {
        //randomly check if splatter strenght is enough and checks if splatter is in bounds
        if ((stren >=UnityEngine. Random.Range(0.0f,1.0f)) 
            && cellPos.y <= 11 && cellPos.y >= -40 && cellPos.x <= 63 && cellPos.x >= -64) 
        {

            (int, int) dataP = CellToData((cellPos.x,cellPos.y));
           
            splatterStruct[dataP.Item1,dataP.Item2] = color;    //update the splatter data structure
            structUpdate.Add(dataP);                            //add the tile position that needs to be updated            
            
            //Propagate at the cardinal directions            
            cellPos.y += 1;
            PropagateUp(cellPos,stren - 0.1f, color);
            cellPos.y -= 2;
            PropagateDown(cellPos,stren - 0.1f, color);
            cellPos.y +=1;
            cellPos.x -=1;
            PropagateLeft(cellPos,stren - 0.1f, color);
           
                      

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