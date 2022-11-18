 using System;
 using System.Collections.Generic;

[System.Serializable]
public class PlayerGameData
{
    public string _username;
    public List<Partidas> PartidasClasicas;
    public List<Partidas> PartidasContraReloj;
    public List<Partidas> PartidasBatalla;

    // CONFIGURACION DEL USUARIO
    public string _MapaSkinCustom;
    

    // CONSTRCTOR
}

[System.Serializable]
public class Partidas
{
    public string jugador;
    public int puntuacion;
    public int tiempo;
    

    public Partidas(string _jugador, int _puntuacion, int _tiempo)
    {
        this.jugador = _jugador;
        this.puntuacion = _puntuacion;
        this.tiempo = _tiempo;
        
    }

}

//  Name1 [...]
//  Name2 [...]