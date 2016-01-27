using UnityEngine;
using System.Collections;

public class Logo : ScriptedController
{

    public GameObject kidobottObjektum;

    override public void Run()
    {
        Varakozik(2);
        Felirat("Felszáll");
        Motor(true);
        
        for (;;)
        {
            Fordul(60);
            Elore(10);
            Kidob(kidobottObjektum);
        }
      
    }

}
