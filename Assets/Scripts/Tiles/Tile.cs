using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public string TileName;
    public SpriteRenderer _renderer;
    [SerializeField] private SpriteRenderer _highlight;
    [SerializeField] private bool _isWalkable;

    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;

    public virtual void Init(int x, int y)
    {
        
    }

    private void OnMouseEnter()
    {
        _highlight.gameObject.SetActive(true);
        InterfaceManager.Instance.SetTileInfo(this);
    }

    private void OnMouseExit()
    {
        _highlight.gameObject.SetActive(false);
        InterfaceManager.Instance.SetTileInfo(null);
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.GameState != GameState.HeroesTurn) 
            return;

        if (OccupiedUnit != null)
        {
            if (OccupiedUnit.faction == Faction.Hero)
                UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
            else if (OccupiedUnit.faction == Faction.Enemy)
            {
                if (UnitManager.Instance.SelectedHero != null)
                {
                    UnitManager.Instance.SetSelectedEnemy((BaseEnemy)OccupiedUnit);
                    int randomMoney = Random.Range(5, 10);
                    Destroy(UnitManager.Instance.SelectedEnemy.gameObject);
                    SetUnit(UnitManager.Instance.SelectedHero);
                    InterfaceManager.Instance.SetTileInfo(this);
                    UnitManager.Instance.SetSelectedHero(null);
                }
            }
        }
        else
        {
            if (UnitManager.Instance.SelectedHero != null && _isWalkable)
            {
                SetUnit(UnitManager.Instance.SelectedHero);
                InterfaceManager.Instance.SetTileInfo(this);
                UnitManager.Instance.SetSelectedHero(null);
            }
        }
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null)
            unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
}
