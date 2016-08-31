using UnityEngine;
using System.Collections.Generic;

public class Previewer : MonoBehaviour {

    public static Previewer instance;

    public float touchDist = 10f;
    public float previewAlpha;
    public Camera mainCamera;

    private bool previewing;
    private GameObject currentTower;
    private BuildManager buildManager;

    public bool Previewing
    {
        get
        {
            return previewing;
        }

        set
        {
            previewing = value;
        }
    }

    // Use this for initialization
    void Awake ()
    {
        instance = this;
	}

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

	// Update is called once per frame
	void Update ()
    {
         if (Previewing && Input.touchCount > 0)
        {
            Vector3 point = mainCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, touchDist));
            currentTower.transform.position = point;
        }
    }

    public void PreviewTower(GameObject tower)
    {
        if (currentTower != tower)
        {
            currentTower = buildManager.BuildTower(tower);
            MakeTowerTransparent(currentTower);
        } else if (currentTower == tower)
        {
            previewing = false;
            currentTower = null;
        }

        Previewing = true;
    }

    private void MakeTowerTransparent(GameObject tower)
    {
        List<Material> defaultMaterials = new List<Material>();
        List<Material> previewMaterials = new List<Material>();

        foreach (Renderer renderer in tower.GetComponentsInChildren<Renderer>())
        {
            foreach (Material mat in renderer.materials)
            {
                defaultMaterials.Add(mat);
            }
        }

        previewMaterials.AddRange(defaultMaterials);

        for (int i = 0; i < defaultMaterials.Count; i++)
        {
            // Set Material mode to Fade
            #region Opaque Code
            // Opaque
            //material.SetOverrideTag("RenderType", "");
            //material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            //material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            //material.SetInt("_ZWrite", 1);
            //material.DisableKeyword("_ALPHATEST_ON");
            //material.DisableKeyword("_ALPHABLEND_ON");
            //material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            //material.renderQueue = -1;
            #endregion
            #region Cutout Code
            // Cutout 
            //material.SetOverrideTag("RenderType", "TransparentCutout");
            //material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            //material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            //material.SetInt("_ZWrite", 1);
            //material.EnableKeyword("_ALPHATEST_ON");
            //material.DisableKeyword("_ALPHABLEND_ON");
            //material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            //material.renderQueue = 2450;
            #endregion
            #region Fade Code
            // Fade 

            previewMaterials[i].SetOverrideTag("RenderType", "Transparent");
            previewMaterials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            previewMaterials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            previewMaterials[i].SetInt("_ZWrite", 0);
            previewMaterials[i].DisableKeyword("_ALPHATEST_ON");
            previewMaterials[i].EnableKeyword("_ALPHABLEND_ON");
            previewMaterials[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
            previewMaterials[i].renderQueue = 3000;
            #endregion
            #region Transparency Code
            // transparent

            //material.SetOverrideTag("RenderType", "Transparent");
            //material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            //material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            //material.SetInt("_ZWrite", 0);
            //material.DisableKeyword("_ALPHATEST_ON");
            //material.DisableKeyword("_ALPHABLEND_ON");
            //material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            //material.renderQueue = 3000;
            #endregion

            // Set Alpha for materals
            Color c = previewMaterials[i].color;
            c.a = previewAlpha;
            previewMaterials[i].color = c;
        }
    }
}
