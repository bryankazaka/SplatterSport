using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallController : MonoBehaviour
{
    private int[] gamePoints;
    public TileBase[] banners;
    public Tilemap wall;
    private int[] positions = {-15,-7,2,10};
    
    // Start is called before the first frame update
    private void Start() {
        gamePoints = new int[4];
        
    }
    public void UpdateWall(int[] players)
    {
        for (int i = 0; i < gamePoints.Length; i++)
        {
            for (int j = 0; j < gamePoints[i]; j++)
            {
                Vector3Int tilePos = new(positions[i]+j,3,0);
                wall.SetTile(tilePos,banners[players[i]]);
            
            
            }
        }
    }
    public void winPoint(int player)
    {
        
        gamePoints[player] +=1;
    }
}
