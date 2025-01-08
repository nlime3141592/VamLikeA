using System;
using UnityEngine;

namespace Unchord
{
    public class ScreenCaptureComponent : MonoBehaviour
    {
        public bool useScreenCapture;

        private void Update()
        {
            if (!useScreenCapture)
                return;

            if (Input.GetKeyDown(KeyCode.Print))
            {
                string filename = DateTime.Now.ToString("Screenshot yyyyMMddHHmmssffffff");
                string path = Application.persistentDataPath + "/" + filename + ".png";
                ScreenCapture.CaptureScreenshot(path);
                Debug.LogFormat("Screenshot saved. ({0})", path);
            }
        }
    }
}