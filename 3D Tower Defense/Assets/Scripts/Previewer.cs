using UnityEngine;
using System.Collections.Generic;
public class Previewer : MonoBehaviour {

    public static Previewer instance;

    public float touchDist = 10f;
    public float previewAlpha;
    public Camera mainCamera;
    public GameObject exitPreviewButton;
    public GameObject comfirmPurchaseButton;

    private bool previewing;
    private GameManager gameManager;
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
        gameManager = GameManager.instance;
    }

    private void Update()
    {
        if (Previewing)
        {
            exitPreviewButton.SetActive(true);
            comfirmPurchaseButton.SetActive(true);
            if (Input.touchCount == 1)
            {
                //Debug.Log(mainCamera.ScreenPointToRay(Input.GetTouch(0).position));
                MovePreview(mainCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, touchDist)));
            }
        } else
        {
            exitPreviewButton.SetActive(false);
            comfirmPurchaseButton.SetActive(false);
        }
    }

    public void ComfirmPlacement()
    {
        // Tell Shop to purchase tower, and activate tower place animation
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
}
