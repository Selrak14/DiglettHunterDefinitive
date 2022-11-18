 using System;
 using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    
    public List<Partidas> PartidasClasicas;
    public List<Partidas> PartidasContraReloj;
    
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
