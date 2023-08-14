using System.Data.SqlClient;
using Dapper;

public class BD
{
    private static string _connectionString = @"Server = localhost;DataBase=PreguntadOrt;Trusted_Connections=true;";
    public static List<Categoria> ObtenerCategorias()
    {
        List<Categoria> ListaCategorias;
        string SQL = "SELECT * FROM Categorias";
        using(SqlConnection BD = new SqlConnection(_connectionString))
        {
            ListaCategorias = BD.Query<Categoria>(SQL).ToList();
        }
        return ListaCategorias;
    }

    public static List<Dificultad> ObtenerDificultades()
    {
        List<Dificultad> ListaDificultades;
        string SQL = "SELECT * FROM Dificultades";
        using(SqlConnection BD = new SqlConnection(_connectionString))
        {
            ListaDificultades = BD.Query<Dificultad>(SQL).ToList();
        }
        return ListaDificultades;
    }

    public static List<Pregunta> ObtenerPreguntas(int IdDificultad, int IdCategoria)
    {
        List <Pregunta> ListaPreguntas;
        using(SqlConnection BD = new SqlConnection(_connectionString))
        {
        string SQL = "SELECT*FROM Preguntas";
        if(IdDificultad != -1 && IdCategoria == -1)
        {
         SQL += "WHERE IdDificultad = @pIdDificultad";
        }
        else if(IdDificultad == -1 && IdCategoria != -1)
        {
         SQL += "WHERE IdCategoria = @pIdCategoria";
        }
        else if(IdDificultad != -1 && IdCategoria != -1)
        {
         SQL += "WHERE IdDificultad = @pIdDificultad AND IdCategoria = @pIdCategoria";
        }
        return BD.Query<Pregunta>(SQL,new{ pIdDificultad = IdDificultad, pIdCategoria = IdCategoria}).ToList();
      }
    }

    public static List<Respuesta> ObtenerRespuestas(List<Pregunta> ListaPreguntas)
    {
        List<Respuesta> ListaRespuestas = new List<Respuesta>();
         using(SqlConnection BD = new SqlConnection(_connectionString))
        {
        string SQL = "SELECT*FROM Respuestas WHERE IdPregunta = @pIdPregunta ";
        foreach (Pregunta preg in ListaPreguntas)
        {
            ListaRespuestas.AddRange(BD.Query<Respuesta>(SQL,new{pIdPregunta=preg.IdPregunta}).ToList());
        }
        }
        return ListaRespuestas;
    }
}