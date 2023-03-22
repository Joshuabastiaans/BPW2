using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [HideInInspector] public bool Door1;
    [HideInInspector] public bool Door2;
    [HideInInspector] public bool Door3;

    public GameObject m_OpenDoorObject;
    public GameObject m_ClosedDoorObject;

    [SerializeField] private int HowManyDoors;

    private void Update()
    {
        int trueDoors = 0;

        if (Door1)
            trueDoors++;

        if (Door2)
        {
            Debug.Log(trueDoors);
            trueDoors++;
        }

        if (Door3)
            trueDoors++;

        if (trueDoors == HowManyDoors)
        {
            m_OpenDoorObject.SetActive(true);
            m_ClosedDoorObject.SetActive(false);
        }

    }
}
