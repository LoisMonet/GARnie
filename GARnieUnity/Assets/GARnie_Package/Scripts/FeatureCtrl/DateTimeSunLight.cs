using UnityEngine;
using System;

/// <summary>
/// Maybe unused later
/// Use on directional light to change angle dynamically according to sunrise and sunset
/// </summary>
public class DateTimeSunLight : MonoBehaviour
{
    
    /// <summary>
    /// sunset in s
    /// </summary>
    public float nscouche = 68400;
    /// <summary>
    /// sunrise in s
    /// </summary>
    public float nsleve = 25200;
    /// <summary>
    /// Number of second for one degree
    /// </summary>
	public float nbrspundef = 0;

    /// <summary>
    /// current time in s
    /// </summary>
    public static float nbrs = 0;

    /// <summary>
    /// current deg according to current time
    /// </summary>
    public static float deg = 0;



    // Update is called once per frame
    void Update()
    {
        nbrspundef = 180 / (nscouche - nsleve); //coeff nbr deg en 1 sec

        DateTime dateTime = DateTime.Now;
        int second = int.Parse(dateTime.Second.ToString());
        int min = int.Parse(dateTime.Minute.ToString());
        int heure = int.Parse(dateTime.Hour.ToString());

        nbrs = (heure * 3600 + ((min * 60) % 3600) + (second % 60));
        if (nbrs >= nsleve && nbrs <= nscouche)
        {
            deg = ((nbrs - nsleve) * nbrspundef);
            transform.eulerAngles = new Vector3(deg, 0, 0); //à chaque update mais pour rotate x le deg
        }
        else
        {
        }

    }
}
