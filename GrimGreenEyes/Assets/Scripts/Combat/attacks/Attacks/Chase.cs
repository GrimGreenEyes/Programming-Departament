using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : Attack
{
    private GameObject tile;
    public override void Effect(GameObject enemy, GameObject player)
    {
        float speed = 8;
        int distanceX, distanceY;
        distanceX = player.GetComponent<Plants>().gridX - enemy.GetComponent<Mosquitoes>().gridX;
        distanceY = player.GetComponent<Plants>().gridY - enemy.GetComponent<Mosquitoes>().gridY;
        
        CalculatePosition(distanceX, distanceY, enemy, player);
        player.transform.position = Vector3.MoveTowards(player.transform.position, tile.transform.position + new Vector3(0, 0.25f, 0), speed * Time.deltaTime);

    }
    private void DoDamage(GameObject enemy, GameObject player)
    {
        enemy.GetComponent<Mosquitoes>().livePoints -= DamageCalculator(enemy.GetComponent<Mosquitoes>(), player.GetComponent<Plants>());
        CheckDead(enemy.GetComponent<Mosquitoes>());
        player.GetComponent<Plants>().actualState = Entity.EntityState.IDLE;

    }
    private void CalculatePosition(int distanceX, int distanceY, GameObject enemy, GameObject player)
    {
        if (((distanceX == 1 && distanceY == 0) || (distanceX == -1 && distanceY == 0)) ^ ((distanceY == 1 && distanceX == 0) || (distanceX == 0 && distanceY == -1)) || (Mathf.Abs(distanceX) == 1 && Mathf.Abs(distanceY) == 1))
        {
            player.transform.position = tile.transform.position + new Vector3(0, 0.25f, 0);
            Debug.Log(tile);
            DoDamage(enemy, player);
            return; 
        }
        if (distanceX + distanceY > 0)
        {
            if (distanceX < distanceY)
            {
                tile = GridCreator.instance.GetTile(enemy.GetComponent<Mosquitoes>().gridX, enemy.GetComponent<Mosquitoes>().gridY + 1);
            }
            else if (distanceX > distanceY)
            {
                tile = GridCreator.instance.GetTile(enemy.GetComponent<Mosquitoes>().gridX + 1, enemy.GetComponent<Mosquitoes>().gridY);
            }
            else
            {
                tile = GridCreator.instance.GetTile(enemy.GetComponent<Mosquitoes>().gridX + 1, enemy.GetComponent<Mosquitoes>().gridY + 1);
            }
        }
        else if (distanceX + distanceY < 0)
        {
            if (distanceX < distanceY)
            {
                tile = GridCreator.instance.GetTile(enemy.GetComponent<Mosquitoes>().gridX - 1, enemy.GetComponent<Mosquitoes>().gridY);
            }
            else if (distanceX > distanceY)
            {
                tile = GridCreator.instance.GetTile(enemy.GetComponent<Mosquitoes>().gridX, enemy.GetComponent<Mosquitoes>().gridY - 1);
            }
            else
            {
                tile = GridCreator.instance.GetTile(enemy.GetComponent<Mosquitoes>().gridX - 1, enemy.GetComponent<Mosquitoes>().gridY - 1);
            }
        }
        else
        {
            if (distanceX < 0)
            {
                tile = GridCreator.instance.GetTile(enemy.GetComponent<Mosquitoes>().gridX + 1, enemy.GetComponent<Mosquitoes>().gridY - 1);
            }
            else if (distanceX > 0)
            {
                tile = GridCreator.instance.GetTile(enemy.GetComponent<Mosquitoes>().gridX - 1, enemy.GetComponent<Mosquitoes>().gridY + 1);
            }
        }
        
    }
}
