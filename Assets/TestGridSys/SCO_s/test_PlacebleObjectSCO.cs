using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class test_PlacebleObjectSCO : ScriptableObject
{
    public string nameString;
    public Image uiImage;
    public Transform prefab;
    public Transform visual;
    public int x_size;
    public int y_size;
    public int z_size;

    public bool isSquare = false;
    public Vector3 minPlaceIndexs;
    public Vector3 maxPlaceIndexs = new Vector3(99,99,99);

    [Header("BuildingCost")]
    public float lifeSupportMat = 0;
    public float structureMat = 0;
    public float energyMat = 0;
    public float conductiveMat = 0;
    public ConnectionPoint[] connectionPoints;
    public enum Produced { LifeSupportMat, StructureMat, EnergyMat, ConductiveMat, ResearchPoint, Morale, MatCapacity, nullProduce}
    public Produced produced = Produced.nullProduce;
    public float productionValue = 1;
    public float productionTime = 12;



    public void Produce()
    {
        GameManager Gm = GameManager.GetGameManagerInstance;
        switch (produced)
        {
            case Produced.LifeSupportMat:
                Gm.lifeSupportMat += Gm.lifeSupportMat < Gm.matCapacity ? productionValue : 0;
                break;
            case Produced.StructureMat:
                Gm.structureMat += Gm.structureMat< Gm.matCapacity ? productionValue : 0;
                break;
            case Produced.EnergyMat:
                Gm.energyMat += Gm.energyMat < Gm.matCapacity ? productionValue : 0;
                break;
            case Produced.ConductiveMat:
                Gm.conductiveMat += Gm.conductiveMat < Gm.matCapacity ? productionValue : 0;
                break;
            case Produced.ResearchPoint:
                Gm.researchPoint += productionValue;
                break;
            case Produced.Morale:
                Gm.settlersMorale += Gm.settlersMorale < 100 ? productionValue : 0;
                break;
            case Produced.MatCapacity:
                Gm.matCapacity += productionValue;
                break;
            case Produced.nullProduce:
                Debug.Log("Bina bir bok yapmýyor");
                break;
            default:
                break;
        }
    }

    public void ProduceBack()
    {
        if (produced == Produced.MatCapacity)
        {
            GameManager Gm = GameManager.GetGameManagerInstance;
            Gm.matCapacity -= productionValue;
        }
    }

    public void ConnectionPoints()
    {

    }
}

[System.Serializable]
public struct ConnectionPoint
{
    public Vector3[] connectionPointsPosition;
    public int[] connectionPointsRotation;
}
