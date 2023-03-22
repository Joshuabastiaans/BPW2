using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public int m_RequiredFrequency;
    public GameObject m_OpenDoorObject;
    public GameObject m_ClosedDoorObject;
    public DoorManager m_Doormanager;

    [SerializeField] private bool m_MultipleDoors;
    [SerializeField] private int m_WhichDoor;

    public void OpenDoor(Flower flower)
    {

        if (flower.m_SignalHitBy.m_Frequency == m_RequiredFrequency)
        {

            if (!m_MultipleDoors)
            {
                m_OpenDoorObject.SetActive(true);
                m_ClosedDoorObject.SetActive(false);

            }

            else
            {
                switch (m_WhichDoor)
                {
                    case 1:
                        m_Doormanager.Door1 = true;
                        break;
                    case 2:
                        m_Doormanager.Door2 = true;
                        break;
                    case 3:
                        m_Doormanager.Door3 = true;
                        break;
                    default:
                        Debug.Log("This flower doesn't corralate with the Doormanager");
                        break;
                }
            }
        }
    }
}