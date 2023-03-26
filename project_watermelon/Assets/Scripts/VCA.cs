using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class VCA : MonoBehaviour
{
    public string vcaType = "vca:/MASVCA";
    private Slider slider;
    [Range(-80f, 10f)]
    public float VCAvolume;

    private FMOD.Studio.VCA VCAController;

    private void Start()
    {
        VCAController = RuntimeManager.GetVCA(vcaType);
        slider = GetComponent<Slider>();
    }
    public void SetVolume()
    {
        VCAController.setVolume(slider.value);
    }

}
