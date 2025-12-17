using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static void SavePlayerData(PlayerController player)
    {
        PlayerData playerData = new PlayerData(player);
        string dataPath = Application.persistentDataPath + "/player.save";//Ruta del archivo de guardado
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);//Crear el archivo
        BinaryFormatter binaryFormatter = new BinaryFormatter();//Formateador binario
        binaryFormatter.Serialize(fileStream, playerData);
        fileStream.Close();
    }

    public static PlayerData LoadPlayerData()
    {
        string dataPath = Application.persistentDataPath + "/player.save";//Ruta del archivo de guardado
        if (File.Exists(dataPath))
        {//Existe el archivo
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);//Abrir el archivo
            BinaryFormatter binaryFormatter = new BinaryFormatter();//Formateador binario
            PlayerData playerData = (PlayerData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return playerData;
        }
        else
        {
            Debug.Log("Error: No se encontro el archivo de guardado");
            return null;
        }
    }
}
