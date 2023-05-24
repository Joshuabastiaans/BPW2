using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public int m_RequiredFrequency;
    public GameObject m_OpenDoorObject;
    public GameObject m_ClosedDoorObject;
    public DoorManager m_Doormanager;

    [SerializeField] private bool m_MultipleConnections;
    [SerializeField] private int m_WhichFlower;
    [SerializeField] private bool m_ResetDoors = true;

    public void OpenDoor(Flower flower)
    {

        if (flower.m_SignalHitBy.m_Frequency == m_RequiredFrequency)
        {

            if (!m_MultipleConnections)
            {
                m_OpenDoorObject.SetActive(true);
                m_ClosedDoorObject.SetActive(false);

            }
            else
            {

                if (m_WhichFlower == 1)
                {
                    m_Doormanager.Door1 = true;
                }
                if (m_WhichFlower == 2)
                {
                    m_Doormanager.Door2 = true;
                }
                if (m_WhichFlower == 3)
                {
                    m_Doormanager.Door3 = true;
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (m_ResetDoors)
        {
            //reset doors
            m_Doormanager.Door1 = false;
            m_Doormanager.Door2 = false;
            m_Doormanager.Door3 = false;
        }
    }
}