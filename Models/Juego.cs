class Juego
{
    private static string username{get;set;}
    private static int puntajeActual{get;set;}
    private static int cantidadPreguntasCorrectas{get;set;}
    public static List<Pregunta> preguntas{get;set;}
    public static List<Respuesta> respuestas{get;set;}

    static public void InicializarJuego()
    {
        string username = "";
        int puntajeActual = 0;
        int cantidadPreguntasCorrectas = 0;
    }
    static public List<Categoria> ListaCategorias()
    {
        return BD.ObtenerCategorias();
    } 
    static public List<Dificultad> ListaDificultades()
    {
        return BD.ObtenerDificultades();
    }
    static public void CargarPartida(string username, int IdDificultad, int IdCategoria)
    {
        preguntas = BD.ObtenerPreguntas(IdDificultad, IdCategoria);
        respuestas = BD.ObtenerRespuestas(preguntas);
    }
     static public Pregunta ObtenerProximaPregunta()
    {
        Random rnd = new Random();
        int RNDMNUM = rnd.Next(preguntas.Count);
        Pregunta proximaPregunta = preguntas[RNDMNUM];
        return proximaPregunta;
    }   
    static public List<Respuesta> ObtenerProximaRespuesta(int IdPregunta)
    {
        List<Respuesta> ProxRespuesta = new List<Respuesta>();
        foreach (Respuesta rsp in respuestas)
        {
        if (IdPregunta == rsp.IdPregunta)
        {
        ProxRespuesta.Add(rsp);
        }
        }
        return ProxRespuesta;
    }
    static public bool VerificarRespuesta(int IdPregunta, int IdRespuesta)
    {
        foreach (Pregunta p in preguntas)
        {
            if(IdPregunta == p.IdPregunta)
            {
                preguntas.Remove(p);
            }
        }
        foreach (Respuesta r in respuestas)
        {
            if(IdRespuesta == r.IdRespuesta && r.Correcta == true)
            {
                return true;
            }
        }
        return false;
    }
}