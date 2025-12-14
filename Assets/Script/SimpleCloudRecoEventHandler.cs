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
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url); //URL del AssetBundle
        yield return www.SendWebRequest(); //Esperar a que se complete la descarga

        if (www.result != UnityWebRequest.Result.Success) //Verificar si hubo un error
        {
            Debug.Log(www.error);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www); //Obtener el AssetBundle descargado
            string[] allAssetNames = bundle.GetAllAssetNames(); //Obtener los nombres de todos los assets en el bundle
            string gameObjectName = Path.GetFileNameWithoutExtension(allAssetNames[0]).ToString(); //Asumir que solo hay un GameObject en el bundle
            GameObject objectFound = bundle.LoadAsset<GameObject>(gameObjectName); //Cargar el GameObject desde el bundle

            if (objetoAR != null)
            {
                Destroy(objetoAR);
            }
            objetoAR = Instantiate(objectFound, transform.position, transform.rotation);
        }
    }

    // Here we handle a cloud target recognition event
    public void OnNewSearchResult(CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult)
    {
        Metadatos datos = Metadatos.CreateFromJSON(cloudRecoSearchResult.MetaData);
        mTargetMetadata = datos.nombre;

        // Validar si el objeto reconocido es el correcto
        if (gameLogic != null)
        {
            if (!gameLogic.EsElObjetoCorrecto(datos.nombre))
            {
                // Mostrar pista de error
                gameLogic.MostrarPista(datos.nombre);
                return;
            }

            // Mostrar pista de acierto
            gameLogic.MostrarPista(datos.nombre);
        }

        // Descargar e instanciar el objeto AR desde el AssetBundle
        StartCoroutine(GetAssetBundle(datos.URL));

        // Desactivar escaneo
        mCloudRecoBehaviour.enabled = false;

        if (ImageTargetTemplate)
        {
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
                    objetoAR.SetActive(false);
                    //Destroy(objetoAR); // Destruir el objeto AR existente
                    objetoAR = null;
                }
            }

        }
    }
}
