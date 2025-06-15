using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] public GameObject boss;  // Drag your Player GameObject here

    [Header("Obiectul barrieră pe care vrei să-l reactivezi")]
    public GameObject[] barriersToEnable;

    [Header("Alte obiecte pe care vrei să le dezactivezi")]
    public GameObject[] objectsToDisable;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1) Reactivează barrier-ul principal
            foreach (var obj in barriersToEnable)
                obj.SetActive(true);

            // 2) Dezactivează fiecare din lista de obiecte
            foreach (var obj in objectsToDisable)
                if (obj != null)
                    obj.SetActive(false);

            // 3) Dezactivează-te pe tine însuți
            gameObject.SetActive(false);
            boss.SetActive(true);
        }
    }
}
