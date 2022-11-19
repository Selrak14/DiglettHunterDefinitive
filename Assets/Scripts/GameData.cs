 using System;
 using System.Collections.Generic;

[System.Serializable]
public class PlayerGameData
{
    public string _username;
    public List<Partidas> PartidasClasicas;
    public List<Partidas> PartidasContraReloj;
    public List<Partidas> PartidasBatalla;
    public int _dinero = 0;

    // CONFIGURACION DEL USUARIO
    public string _MapaSkinCustom;
    public string _PointerCustom = "Maza";
    

    // CONSTRCTOR
}

[System.Serializable]
public class Partidas
{
    public string jugador;
    public int puntuacion;
    public float tiempo;
    public int dinero;
    

    public Partidas(string _jugador, int _puntuacion, float _tiempo, int _dinero)
    {
        this.jugador = _jugador;
        this.puntuacion = _puntuacion;
        this.tiempo = _tiempo;
        this.dinero = _dinero;
        
    }

}

//  Name1 [...]
//  Name2 [...]