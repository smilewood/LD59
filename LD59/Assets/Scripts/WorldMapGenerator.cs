using System.Collections.Generic;
using UnityEngine;

public class WorldMapGenerator : MonoBehaviour
{
   private HashSet<(int, int)> filledTiles;

   public int tileSize;
   public GameObject[] tilePrefabs;

   private Transform player;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      
      filledTiles = new HashSet<(int, int)>();
      player = GameObject.Find("PlayerRoot").transform;
   }

   // Update is called once per frame
   void Update()
   {
      //TODO this should really only run when the player crosses a boundry
      AddTilesAround((int)player.position.x / tileSize, (int)player.position.y / tileSize);
   }

   private void AddTilesAround(int x, int y)
   {
      for (int i = x-2; i <= x+2; ++i)
      {
         for (int j = y-2; j <= y+2; ++j)
         {
            TryAddTile((i, j));
         }
      }
   }

   private bool TryAddTile((int, int) location)
   {
      if (!filledTiles.Contains(location))
      {
         GameObject newTile = Instantiate(tilePrefabs[Random.Range(0, tilePrefabs.Length)], new Vector3(tileSize * location.Item1, tileSize * location.Item2, 0), Quaternion.identity, this.transform);
         newTile.name = $"({location.Item1}, {location.Item2})";
         filledTiles.Add(location);
         return true;
      }
      return false;
   }
}
