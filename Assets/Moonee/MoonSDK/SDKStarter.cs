using Moonee.MoonSDK.Internal;
using Moonee.MoonSDK.Internal.GDPR;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Moonee.MoonSDK
{
    public class SDKStarter : MonoBehaviour
    {
        [SerializeField] private GDPR gdpr;
        [SerializeField] private GameObject moonSDK;
        [SerializeField] private GameObject intro;

        [Inject]
        private LoadSceneController _loadSceneController;

        private void Start()
        {
            gdpr.gameObject.SetActive(false);
            intro.gameObject.SetActive(false);

            StartCoroutine(Starter());
        }
        private void OnGDPRCompleted()
        {
            InitializeMoonSDK();
        }
        private void InitializeMoonSDK()
        {
            moonSDK.SetActive(true);
            DontDestroyOnLoad(moonSDK); 
            _loadSceneController.StartLoadSaveLevel();
        }
        private IEnumerator Starter()
        {
            intro.SetActive(true);
            yield return new WaitForSeconds(4f);
            intro.SetActive(false);

            MoonSDKSettings settings = MoonSDKSettings.Load();

            if (!InternetCheckerController.isNoInternetConnection && CheckGDPRCountry.CheckCountryForGDPR() && !GDPR.IsAlreadyAsked && settings.GDPR )
            {
                DontDestroyOnLoad(this);
                gdpr.OnCompleted += OnGDPRCompleted;
                gdpr.gameObject.SetActive(true);
            }
            else InitializeMoonSDK();
        }
    }
}
