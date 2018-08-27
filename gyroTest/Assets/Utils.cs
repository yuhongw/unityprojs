using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils  {

    private const double EARTH_RADIUS = 6378137;
    private static double oriLat = 0;
    private static double oriLng = 0;

    private static double rad(double d)
    {
        return d * Math.PI / 180.0;
    }

    public static double DistanceOfTwoPoints(double lat1, double lng1,double lat2, double lng2)
    {
        double radLat1 = rad(lat1);
        double radLat2 = rad(lat2);
        double a = radLat1 - radLat2;
        double b = rad(lng1) - rad(lng2);
        double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2)
        + Math.Cos(radLat1) * Math.Cos(radLat2)
        * Math.Pow(Math.Sin(b / 2), 2)));
        s = s * EARTH_RADIUS;
        s = Math.Round(s * 10000) / 10000;
        return s;
        //double ss = s * 1.0936132983377;
        //System.out.println("两点间的距离是：" + s + "米" + "," + (int)ss + "码");
    }

    /// <summary>
    /// 将当前位置设为原点
    /// </summary>
    internal static void SetasOri(double lat, double lng)
    {
        oriLat = lat;
        oriLng = lng;
        PlayerPrefs.SetString("lat", lat.ToString());
        PlayerPrefs.SetString("lng", lng.ToString());
    }

    
    

    public static Vector2 DistanceV2(double lat1, double lng1, double lat2, double lng2)
    {
        return new Vector2((lng2>=lng1?1:-1) * (float)DistanceOfTwoPoints(lat1, lng1, lat1, lng2), (lat2 >= lat1 ? 1 : -1) * (float)DistanceOfTwoPoints(lat1, lng1, lat2, lng1));
    }


    

    public static Vector2 DistanceOri(double lat1, double lng1)
    {

        const double oriLatDefault = 39.97728;
        const double oriLngDefault = 116.4917;

        if (oriLat == 0 || oriLng == 0)
        {

            if (PlayerPrefs.HasKey("lat"))
            {
                oriLat = Convert.ToDouble(PlayerPrefs.GetString("lat"));
            }
            else
            {
                oriLat = oriLatDefault;
                PlayerPrefs.SetString("lat", oriLat.ToString());
            }

            if (PlayerPrefs.HasKey("lng"))
            {
                oriLng = Convert.ToDouble(PlayerPrefs.GetString("lng"));
            }
            else
            {
                oriLng = oriLngDefault;
                PlayerPrefs.SetString("lng", oriLng.ToString());
            }
        }
        return DistanceV2(lat1,lng1,oriLat,oriLng);
    }
}
