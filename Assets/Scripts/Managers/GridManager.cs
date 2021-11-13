using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private int _width;
    [SerializeField] private int _height;

    [SerializeField] private Transform tilesParent;
    [SerializeField] private Tile _grassTile, _mountainTile;

    [SerializeField] private Transform _cam;

    private Dictionary<Vector2, Tile> _tiles;

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var randomMountainTile = Random.Range(0, 10) == 3 ? _mountainTile : _grassTile;
                var tileSpawn = Instantiate(randomMountainTile, new Vector2(x, y), Quaternion.identity, tilesParent);
                tileSpawn.name = $"Tile {x} {y}";

                tileSpawn.Init(x, y);

                _tiles[new Vector2(x, y)] = tileSpawn;
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

        InterfaceManager.Instance.GetMoneyOnStart();

        GameManager.Instance.ChangeState(GameState.SpawnHeroes);
    }

    public Tile GetHeroSpawnedTile()
    {
        return _tiles.Where(t => t.Key.x < _width && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetEnemySpawnedTile()
    {
        return _tiles.Where(t => t.Key.x < _width && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }
}
