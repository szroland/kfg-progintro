using UnityEngine;
using System.Collections;

public class Szallito : ScriptedController {

    public string felvettTag = null;

    public override void Run()
    {

        Varakozik(2);
        Motor(true);
        Varakozik(2);

        for (;;)
        {
            GameObject kocka = KeresEgy("Kocka");

            if (kocka != null) {
                Felirat("Keres");

                GameObject cel = KeresEgy("Cel");
                string name = "";
                Vegrehajt(delegate
                {
                    name = cel.name;
                });
                Felirat("Megyek: "+name);

                Fordul(kocka);
                Menj(kocka, 0, alapMagassag, 0);
                Felirat("Felszed");
                
                if (Felszed(true))
                {
                    Felirat("Viszem");
                    //Menj(cel, 0, alapMagassag, 0);
                    Fordul(cel);
                    Felirat("Dobom");
                    Elenged(10);
                }

            }

        }

    }

}
