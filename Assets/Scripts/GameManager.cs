using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GameManager : MonoBehaviour
{
    public List<GameObject> nivele;
    private int nivelCurent = 0;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        ActivareNivel(nivelCurent);
        MutaPlayerLaSpawn(nivelCurent);
    }

    public void TreciLaUrmatorulNivel()
    {
        DezactivareNivel(nivelCurent);
        nivelCurent++;

        if (nivelCurent < nivele.Count)
        {
            ActivareNivel(nivelCurent);
            MutaPlayerLaSpawn(nivelCurent);
        }
        else
        {
            Debug.Log("Toate nivelele terminate!");
        }
    }

    private void ActivareNivel(int index) => nivele[index].SetActive(true);

    private void DezactivareNivel(int index) => nivele[index].SetActive(false);

    private void MutaPlayerLaSpawn(int nivelIndex)
    {
        Transform spawn = nivele[nivelIndex].transform.Find("Level/PlayerSpawn");
        if (spawn != null && player != null)
        {
            player.transform.position = spawn.position;
        }
        else
        {
            Debug.LogWarning("Spawn point sau playerul lipsesc!");
        }
    }
}
