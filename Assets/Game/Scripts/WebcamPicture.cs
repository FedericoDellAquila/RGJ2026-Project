// Source - https://stackoverflow.com/a/24497723
// Posted by Bart, modified by community. See post 'Timeline' for change history
// Retrieved 2026-11-07, License - CC BY-SA 4.0

using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class WebCamPhotoCamera : MonoBehaviour
{
    public RawImage preview;      // optional: a UI RawImage to show the live feed
    private WebCamTexture webcamTexture;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            Debug.LogError("No webcam found.");
            return;
        }

        // Use the first camera (or pick one by name: devices[i].name)
        webcamTexture = new WebCamTexture(devices[0].name, 1280, 720, 30);

        if (preview != null)
            preview.texture = webcamTexture;

        webcamTexture.Play();
    }

    // void Update()
    // { 
    //     TakePicture();
    // }

    public void TakePicture()
    {
        if (webcamTexture == null || !webcamTexture.isPlaying) return;

        // Copy the current frame into a Texture2D
        Texture2D photo = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGB24, false);
        photo.SetPixels32(webcamTexture.GetPixels32());
        photo.Apply();

        // Save as PNG
        byte[] bytes = photo.EncodeToPNG();
        string path = Path.Combine(Application.persistentDataPath, "rgj2026_photo_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png");
        File.WriteAllBytes(path, bytes);
        Debug.Log("Saved to: " + path);

        Destroy(photo); // or keep it if you want to display/use it
    }
    
    /// <summary>
    /// Saves the current webcam frame as a PNG right now.
    /// Returns the file path, or null if it failed.
    /// </summary>
    public string SavePng(string fileName = null)
    {
        if (webcamTexture == null || !webcamTexture.isPlaying || webcamTexture.width <= 16)
        {
            Debug.LogWarning("Webcam isn't ready yet.");
            return null;
        }

        if (string.IsNullOrEmpty(fileName))
            fileName = "photo_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");

        if (!fileName.EndsWith(".png")) fileName += ".png";

        Texture2D photo = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGB24, false);
        photo.SetPixels32(webcamTexture.GetPixels32());
        photo.Apply();

        string path = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllBytes(path, photo.EncodeToPNG());
        Destroy(photo);

        Debug.Log("Saved PNG to: " + path);
        return path;
    }

    /// <summary>
    /// Waits the given number of seconds, then saves a PNG.
    /// </summary>
    public void SavePngAfterDelay(float seconds, string fileName = null)
    {
        StartCoroutine(SavePngRoutine(seconds, fileName));
    }

    private IEnumerator SavePngRoutine(float seconds, string fileName)
    {
        yield return new WaitForSeconds(seconds);
        yield return new WaitForEndOfFrame(); // make sure the frame is fully updated
        SavePng(fileName);
    }

    void OnDestroy()
    {
        if (webcamTexture != null && webcamTexture.isPlaying)
            webcamTexture.Stop();
    }
}