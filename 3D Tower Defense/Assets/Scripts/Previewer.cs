using UnityEngine;
using System.Collections.Generic;
public class Previewer : MonoBehaviour {

    public static Previewer instance;

    public float touchDist = 10f;
    public float previewAlpha;
    public Camera mainCamera;
    public bool overlapping;

    private bool previewing;
    private GameManager gameManager;
    private BuildManager buildManager;
    private Shop shop;
    private Plane myPlane;

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
        myPlane = new Plane(Vector3.up, new Vector3(0,0.2f,0));
	}

    private void Start()
    {
        buildManager = BuildManager.instance;
        gameManager = GameManager.instance;
        shop = Shop.instance;
    }

    private void Update()
    {
        if (Previewing)
        {
            if (Input.touchCount == 1)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);

                float rayLength = 0f;
                if (myPlane.Raycast(ray, out rayLength))
                {
                    Debug.Log("Plane Raycast hit at distance: " + rayLength);
                    Vector3 hitPoint = ray.GetPoint(rayLength);

                    MovePreview(hitPoint);
                }
            }
        } else if (gameManager.selectedTower != null)
        {
            ResetTowerMaterials(gameManager.selectedTower);
            gameManager.selectedTower = null;
        }
    }

    public void ExitPreview()
    {
        Destroy(gameManager.selectedTower);
        gameManager.selectedTower = null;
        Previewing = false;
    }

    public void MovePreview(Vector3 pos)
    {
        if(Previewing && gameManager.selectedTower)
        {
            gameManager.selectedTower.transform.position = pos;
        }    
    }

    public void PreviewTower(GameObject tower)
    {
        if (gameManager.selectedTower == null || gameManager.selectedTower.GetComponent<Tower>().title != tower.GetComponent<Tower>().title)
        {
            Destroy(gameManager.selectedTower);
            gameManager.selectedTower = buildManager.BuildTower(tower);
            MakeTowerTransparent(gameManager.selectedTower);
        } else if (gameManager.selectedTower.GetComponent<Tower>().title == tower.GetComponent<Tower>().title)
        {
            Destroy(gameManager.selectedTower);
            Previewing = false;
            gameManager.selectedTower = null;
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

    private void ResetTowerMaterials(GameObject tower)
    {
        List<Material> currentMaterials = new List<Material>();

        foreach (Renderer renderer in tower.GetComponentsInChildren<Renderer>())
        {
            foreach (Material mat in renderer.materials)
            {
                currentMaterials.Add(mat);
            }
        }

        for (int i = 0; i < currentMaterials.Count; i++)
        {
            // Opaque
            currentMaterials[i].SetOverrideTag("RenderType", "");
            currentMaterials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            currentMaterials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            currentMaterials[i].SetInt("_ZWrite", 1);
            currentMaterials[i].DisableKeyword("_ALPHATEST_ON");
            currentMaterials[i].DisableKeyword("_ALPHABLEND_ON");
            currentMaterials[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
            currentMaterials[i].renderQueue = -1;

            // Set Alpha for materals
            Color c = currentMaterials[i].color;
            c.a = 1;
            currentMaterials[i].color = c;
        }
    }
}
