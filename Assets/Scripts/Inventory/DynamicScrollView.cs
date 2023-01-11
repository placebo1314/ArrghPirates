using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static DataSaver;
using static PlayerStatModel;

public class DynamicScrollView : MonoBehaviour
{
    [SerializeField] private Transform scrollViewContent;
    [SerializeField] private GameObject prefab;
    private List<Image> pirates;

    private void Start()
    {
        PlayerStatModel data = loadData<PlayerStatModel>("stats");
        foreach (PirateModel pirate in data.pirates)
        {
            GameObject newPirate = Instantiate(prefab, scrollViewContent);
            newPirate.name = pirate.pirateName;
  
            //if (newPirate.TryGetComponent<ScrollItemView>(out ScrollItemView item))
            //item.ChangePirate(pirate);
        }
    }

}
