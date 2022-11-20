 using System;
 using System.Collections.Generic;

[System.Serializable]
public class PlayerGameData
{
    public string _username;
    public List<Partidas> PartidasClasicas = new List<Partidas>();
    public List<Partidas> PartidasContraReloj = new List<Partidas>();
    public List<Partidas> PartidasBatalla = new List<Partidas>();
    public float _dineroP = 0;

    // CONFIGURACION DEL USUARIO
    public string _MapaSkinCustom = "beach";
    public string _PointerCustom = "Maza";

    //  COMPRAS
    public bool _pointer1;
    public bool _pointer2;
    public bool _Map3;
    public bool _Map4;

    public int _NivelTorreta = 0;

    

    // CONSTRCTOR
}

[System.Serializable]
public class Partidas
{
    public string jugador;
    public int puntuacion;
    public float tiempo;
    public float dinero;
    

    public Partidas(string _jugador, int _puntuacion, float _tiempo, float _dinero)
    {
        this.jugador = _jugador;
        this.puntuacion = _puntuacion;
        this.tiempo = _tiempo;
        this.dinero = _dinero;
        
    }

}

//  Name1 [...]
//  Name2 [...]