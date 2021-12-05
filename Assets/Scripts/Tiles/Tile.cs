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
        if (UnitManager.Instance.SelectedHero != null)
        {
            if (Vector2.Distance(UnitManager.Instance.SelectedHero.transform.position, transform.position) <= 5 && _isWalkable)
            {
                _highlight.gameObject.SetActive(true);
                _highlight.color = new Color(1, 1, 1, 0.35f);
            }
            else
            {
                _highlight.gameObject.SetActive(true);
                _highlight.color = new Color(1, 0, 0, 0.35f);
            }
        }
        else
        {
            _highlight.gameObject.SetActive(true);
            _highlight.color = new Color(1, 1, 1, 0.35f);
        }
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
                    if (Vector2.Distance(UnitManager.Instance.SelectedHero.transform.position, UnitManager.Instance.SelectedEnemy.transform.position) <= 5)
                    {
                        StartCoroutine(MoneyScript.Instance.AddMoneyOnKill());
                        Destroy(UnitManager.Instance.SelectedEnemy.gameObject);
                        SetUnit(UnitManager.Instance.SelectedHero);
                        InterfaceManager.Instance.SetTileInfo(this);
                        UnitManager.Instance.SetSelectedHero(null);
                    }
                }
            }
        }
        else
        {
            if (UnitManager.Instance.SelectedHero != null && _isWalkable)
            {
                if (Vector2.Distance(UnitManager.Instance.SelectedHero.transform.position, transform.position) <= 5)
                {
                    SetUnit(UnitManager.Instance.SelectedHero);
                    InterfaceManager.Instance.SetTileInfo(this);
                    UnitManager.Instance.SetSelectedHero(null);
                }
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

    public void HighlightNearTiles()
    {

    }
}
