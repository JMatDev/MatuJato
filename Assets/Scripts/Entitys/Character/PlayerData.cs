[System.Serializable]

public class PlayerData
{
    public float speed;
    public float[] position = new float[3];//(x,y,z)

    public PlayerData(PlayerController player)
    {
        speed = player.speed;
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }

}
