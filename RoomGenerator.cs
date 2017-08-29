using System.Collections;
using UnityEngine;

public class RoomGenerator : MonoBehaviour {
    private int gameloads = 0;
    public bool disableFog;

    public Room roomPrefab;

    Room roomInstance;

    public Player playerPrefab;

    Player playerInstance;

    public FogOfWar fogprefab;

    void Start ()
    {
        ++gameloads;
        StartCoroutine(BeginGame());
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        StopAllCoroutines();
        Destroy(roomInstance.gameObject);
        if (playerInstance != null)
        {
            Destroy(playerInstance.gameObject);
            if (!disableFog)
            {
                foreach (var item in playerInstance.fogs)
                {
                    Destroy(item.gameObject);

                }

                Destroy(playerInstance.Fog.gameObject);
            }
        }
        StartCoroutine(BeginGame());
    }

    private IEnumerator BeginGame()
    {
        roomInstance = Instantiate(roomPrefab) as Room;
        yield return StartCoroutine(roomInstance.Generate());

        playerInstance = Instantiate(playerPrefab) as Player;     
        if(!disableFog)
        playerInstance.CreateRoomFog(roomInstance.size,fogprefab);
        playerInstance.SetLocation(roomInstance.GetCell(roomInstance.exit));
        playerInstance.size = roomInstance.size;
    }

}
