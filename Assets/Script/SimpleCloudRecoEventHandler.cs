using JetBrains.Annotations;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;
using static UnityEngine.CullingGroup;
using static Vuforia.CloudRecoBehaviour;

[System.Serializable]
public class Metadatos
{
    public string nombre;
    public string URL;
    public string puntuacion;

    public static Metadatos CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Metadatos>(jsonString);
    }

}
public class SimpleCloudRecoEventHandler : MonoBehaviour
{
    CloudRecoBehaviour mCloudRecoBehaviour;
    bool mIsScanning = false;
    string mTargetMetadata = "";
    public GameLogic gameLogic;
    public ImageTargetBehaviour ImageTargetTemplate;
    private GameObject objetoAR;

    //[SerializeField] TextMeshProUGUI nombreObjeto;

    //public void FoodReconocer()
    //{
     
    //    switch (mTargetMetadata)
    //    {
    //        case "hamburguesa":
    //            ImageTargetTemplate.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    //            break;
    //        case "tartachocolate":
    //            ImageTargetTemplate.gameObject.transform.GetChild(1).gameObject.SetActive(true);
    //            break;
    //        case "waffle":
    //            ImageTargetTemplate.gameObject.transform.GetChild(2).gameObject.SetActive(true);
    //            break;
    //        case "helado":
    //            ImageTargetTemplate.gameObject.transform.GetChild(3).gameObject.SetActive(true);
    //            break;
    //    }
    //}

    //public void FoodGen()
    //{
    //    string[] food = { "hamburguesa", "tartachocolate", "waffle" };

    //    nombreObjeto.text = food[Random.Range(0, food.Length + 1)];
    //}

    // Register cloud reco callbacks
    void Awake()
    {
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        mCloudRecoBehaviour.RegisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.RegisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.RegisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }
    //Unregister cloud reco callbacks when the handler is destroyed
    void OnDestroy()
    {
        mCloudRecoBehaviour.UnregisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.UnregisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.UnregisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.UnregisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.UnregisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }

    public void OnInitialized(CloudRecoBehaviour cloudRecoBehaviour)
    {
        Debug.Log("Cloud Reco initialized");
    }

    public void OnInitError(CloudRecoBehaviour.InitError initError)
    {
        Debug.Log("Cloud Reco init error " + initError.ToString());
    }

    public void OnUpdateError(CloudRecoBehaviour.QueryError updateError)
    {
        Debug.Log("Cloud Reco update error " + updateError.ToString());

    }

    public void OnStateChanged(bool scanning)
    {
        mIsScanning = scanning;

        if (scanning)
        {
            // Clear all known targets
        }
    }
    IEnumerator GetAssetBundle(string url)
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            string[] allAssetNames = bundle.GetAllAssetNames();
            string gameObjectName = Path.GetFileNameWithoutExtension(allAssetNames[0]).ToString();
            GameObject objectFound = bundle.LoadAsset<GameObject>(gameObjectName);

            
            if (objetoAR != null)
            {
                Destroy(objetoAR);
            }

            
            objetoAR = Instantiate(objectFound, transform.position, transform.rotation);
        }
    }

    //IEnumerator GetAssetBundle(string url)
    //{
    //    UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url);
    //    yield return www.SendWebRequest();

    //    if (www.result != UnityWebRequest.Result.Success)
    //    {
    //        Debug.Log(www.error);
    //    }
    //    else
    //    {
    //        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
    //        string[] allAssetNames = bundle.GetAllAssetNames();
    //        string gameObjectName = Path.GetFileNameWithoutExtension(allAssetNames[0]).ToString();
    //        GameObject objectFound = bundle.LoadAsset(gameObjectName) as GameObject;
    //        Instantiate(objectFound, transform.position, transform.rotation);

    //    }
    //}
    // Here we handle a cloud target recognition event
    public void OnNewSearchResult(CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult) {
        Metadatos datos; 
        datos = Metadatos.CreateFromJSON(cloudRecoSearchResult.MetaData); 
        StartCoroutine(GetAssetBundle(datos.URL)); // Store the target metadata 
        //mTargetMetadata = cloudRecoSearchResult.MetaData;
        mTargetMetadata = datos.nombre; 
        //if (mTargetMetadata == nombreObjeto.text) //{
        if (gameLogic != null) // Asegurarse de que gameLogic no sea nulo
        { 
            gameLogic.MostrarPista(datos.nombre); // Pasar el nombre del objeto detectado
        } 
        //} // Stop the scanning by disabling the behaviour
        mCloudRecoBehaviour.enabled = false; 
        //FoodReconocer();
        if (ImageTargetTemplate) 
        { 
            /* Enable the new result with the same ImageTargetBehaviour: */ 
            mCloudRecoBehaviour.EnableObservers(cloudRecoSearchResult, ImageTargetTemplate.gameObject); 
        } 
    }


    void OnGUI()
    {
        // Display current 'scanning' status
        GUI.Box(new Rect(100, 100, 200, 50), mIsScanning ? "Scanning" : "Not scanning");
        // Display metadata of latest detected cloud-target
        GUI.Box(new Rect(100, 200, 200, 50), "Metadata: " + mTargetMetadata);
        // If not scanning, show button
        // so that user can restart cloud scanning
        if (!mIsScanning)
        {
            if (GUI.Button(new Rect(100, 300, 200, 50), "Resetear Escaneo"))
            {
                mCloudRecoBehaviour.enabled = true;
                mTargetMetadata = "";

                if (objetoAR != null)
                {
                    Destroy(objetoAR); // Destruir el objeto AR existente
                    objetoAR = null;
                }
            }

        }
    }
}
