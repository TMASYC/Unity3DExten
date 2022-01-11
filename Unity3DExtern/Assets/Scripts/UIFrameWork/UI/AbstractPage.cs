using System.Collections;
using System.Collections.Generic;
using TMPro.SpriteAssetUtilities;
using UnityEngine;

public class AbstractPage : AbstractUIElement
{
    [SerializeField] protected AbstractPage m_ParentPage;

    ///[SerializeField] private TexturePacker.SpriteData[] m_SpriteData;
    ///

    private int m_PanelID = -1;

    private bool m_HasInitUI = false;
    private bool m_HasOpen = false;
    
    protected int m_UIID;

    public int MUiid
    {
        get => m_UIID;
        set => m_UIID = value;
    }

    public string uiName
    {
        get
        {
            if (m_UIID < 0)
            {
                return "";
            }

            var data = "";//UIDataMoudle.Get(m_UIID);
            if (data == null)
            {
                return "";
            }

            return "";
        }
    }

    public bool hasOpen
    {
        get { return m_HasOpen; }
    }

    public AbstractPage parentPage
    {
        get { return m_ParentPage; }
        set { m_ParentPage = value; }
    }

    public int panelID
    {
        get
        {
            return m_PanelID;
        }
        set { m_PanelID = value; }
    }
}
