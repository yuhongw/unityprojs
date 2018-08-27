using UnityEngine;
using System.Collections;
 
public class GetGPS : MonoBehaviour
{
    public GameObject tubes;
    public string gps_info = "";
    public int flash_num = 1;
    private string distStr = "";
    // Use this for initialization
    Vector2 testDist;
    AndroidJavaClass gpsActivity;

    private double lat=40;
    private double lng=116;

    void Start()
    {
        testDist = new Vector2(0, 0);
#if UNITY_ANDROID
        gpsActivity = new AndroidJavaClass("com.cn.gyro.gps");
#endif
        InvokeRepeating("RefreshGPS", 2.0f, 2.0f);
    }

    void OnGUI()
    {

        GUI.skin.button.fontSize = 20;
        if (GUI.Button(new Rect(Screen.width / 2 - 110, 0, 200, 85), "设当前位置为原点"))
        {
            Utils.SetasOri(this.lat,this.lng);
        }

#if UNITY_STANDALONE_WIN
        if (GUI.Button(new Rect(Screen.width / 2 - 110+200, 0, 100, 85), "Lat+0.1"))
        {
            this.lat += 0.05;
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 110+300, 0, 100, 85), "Lat-0.1"))
        {
            this.lat -= 0.05;
        }


        if (GUI.Button(new Rect(Screen.width / 2 - 110+400, 0, 100, 85), "Lng+0.1"))
        {
            this.lng += 0.1;
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 110+500, 0, 100, 85), "Lng-0.1"))
        {
            this.lng -= 0.1;
        }
#endif

        //if (GUI.Button(new Rect(Screen.width / 2 - 110, 300, 220, 85), "刷新GPS"))
        //{
        //    RefreshGPS();
        //}

        //if(GUI.Button(new Rect(Screen.width / 2 - 110+220, 400, 220, 85),"X++"))
        //{
        //    testDist.x += 5;
        //    tubes.transform.position = new Vector3(-testDist.x, -testDist.y);
        //}

        //if (GUI.Button(new Rect(Screen.width / 2 - 110, 400, 220, 85), "X--"))
        //{
        //    testDist.x -= 5;
        //    tubes.transform.position = new Vector3(-testDist.x, -testDist.y);
        //}

        GUI.skin.label.fontSize = 28;
        GUI.Label(new Rect(20, 20, 600, 200), this.gps_info);
        GUI.Label(new Rect(20, 50, 600, 48), this.flash_num.ToString());
        GUI.Label(new Rect(20, 90, 600, 48), this.distStr);
    }

    void RefreshGPS()
    {
        //this.gps_info = "N:" + Input.location.lastData.latitude.ToString("R") + " E:" + Input.location.lastData.longitude.ToString("R");
        //this.gps_info =  "N:" + (double)Input.location.lastData.latitude + " E:" + (double)Input.location.lastData.longitude;
#if UNITY_ANDROID
        this.lat = gpsActivity.CallStatic<double>("getLat");
        this.lng = gpsActivity.CallStatic<double>("getLng");
#endif
        this.gps_info = "N:" + lat.ToString()  + " E:" + lng.ToString();
        this.gps_info = this.gps_info + " Time:" + Input.location.lastData.timestamp;
        this.flash_num += 1;

        Vector2 dist = Utils.DistanceOri(this.lat, this.lng);
        this.distStr = "X:" + dist.x.ToString() + " Y:" + dist.y.ToString();
        tubes.transform.position = new Vector3(-dist.x , -5.0f, -dist.y );
    }

    // Input.location = LocationService
    // LocationService.lastData = LocationInfo 

    //void StopGPS()
    //{
    //    Input.location.Stop();
    //}

    /*
    IEnumerator StartGPS()
    {
        // Input.location 用于访问设备的位置属性（手持设备）, 静态的LocationService位置
        // LocationService.isEnabledByUser 用户设置里的定位服务是否启用
        if (!Input.location.isEnabledByUser)
        {
            this.gps_info = "isEnabledByUser value is:" + Input.location.isEnabledByUser.ToString() + " Please turn on the GPS";
            yield return new WaitForEndOfFrame();
        }

        // LocationService.Start() 启动位置服务的更新,最后一个位置坐标会被使用
        Input.location.Start(5.0f, 5.0f);

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            // 暂停协同程序的执行(1秒)
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            this.gps_info = "Init GPS service time out";
            yield return new WaitForEndOfFrame();
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            this.gps_info = "Unable to determine device location";
            yield return new WaitForEndOfFrame();
        }
        else
        {
            InvokeRepeating("RefreshGPS", 2.0f, 2.0f);
            this.gps_info = "N:" + Input.location.lastData.latitude + " E:" + Input.location.lastData.longitude;
            this.gps_info = this.gps_info + " Time:" + Input.location.lastData.timestamp;
            yield return new WaitForSeconds(100);
        }
    }
    */
}

