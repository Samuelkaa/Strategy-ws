using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager Instance;

    [SerializeField] private GameObject _heroInfoObject, _tileInfoObject, _tileUnitObject;

    [SerializeField] private Image _tileSprite, _heroSprite, _tileUnitSprite;

    private void Awake()
    {
        Instance = this;
    }

    public void SetTileInfo(Tile tile)
    {
        if (tile == null)
        {
            _tileInfoObject.SetActive(false);
            _tileUnitObject.SetActive(false);
            return;
        }

        _tileInfoObject.SetActive(true);
        _tileInfoObject.GetComponentInChildren<Text>().text = tile.TileName;
        _tileSprite.color = tile._renderer.color;

        if (tile.OccupiedUnit)
        {
            _tileUnitObject.SetActive(true);
            _tileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName;
            _tileUnitSprite.sprite = tile.OccupiedUnit.UnitSprite;
        }
    }

    public void SetHeroInfo(BaseHero hero)
    {
        if (hero == null)
        {
            _heroInfoObject.SetActive(false);
            return;
        }

        _heroInfoObject.GetComponentInChildren<Text>().text = hero.UnitName;
        _heroSprite.sprite = hero.UnitSprite;
        _heroInfoObject.SetActive(true);
    }
}
