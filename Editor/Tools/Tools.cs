using System.IO;
using UnityEditor;
using UnityEngine;

namespace UniTools.Editor.Tools
{
    public static class Tools
    {
        private const string ScreenshotsFileFormat = "yyyy-MM-ddTHH-mm-ss.ffff";
        private const string ScreenshotsDirectoryName = "Screenshots";
        
        [MenuItem("Tools/Take a Screenshot &#s")]
        public static void TakeScreenshot()
        {
            var filename = $"{System.DateTime.Now.ToString(ScreenshotsFileFormat)}.png";
            var pathToScreenshot = Path.Join(ScreenshotsDirectoryName, filename);

            if (!Directory.Exists(ScreenshotsDirectoryName))
                Directory.CreateDirectory(ScreenshotsDirectoryName);
            
            ScreenCapture.CaptureScreenshot(pathToScreenshot);
            Debug.LogWarning($"Screenshot captured at {pathToScreenshot}");
        }
    }
}