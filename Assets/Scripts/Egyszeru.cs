using UnityEngine;
using System.Collections;

public class Egyszeru : ScriptedController {

    public GameObject kidobottObjektum;

    override public void Run()
    {
        Varakozik(2);
        Felirat("Felszáll");
        Motor(true);
        Elmozdul(0, 0, 0);

        Felirat("Lebeg");
        Varakozik(2);

        Felirat("Leszáll");
        Motor(false);
    }

}
