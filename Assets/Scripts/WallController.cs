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
    private string[] colors = {"blue","yellow","green","pink"};
    private int winningScore;


    // Start is called before the first frame update
    private void Start() {
        gamePoints = new int[4];
        
    }
    public void setWinningScore(int winning)
    {
        winningScore = winning;
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
        if (gamePoints[player] == winningScore)
        {
            GetComponentInParent<GameManagerBattle>().EndGame(player);
        }
        
    }
}
