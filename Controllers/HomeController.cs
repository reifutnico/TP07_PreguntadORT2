using Microsoft.AspNetCore.Mvc;

namespace PreguntadORT.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ConfigurarJuego()
    {
        Juego.InicializarJuego();
        ViewBag.Categoria = BD.ObtenerCategorias();
        ViewBag.Dificultad= BD.ObtenerDificultades();
        return View();
    }
    
    public IActionResult Comenzar(string username, int dificultad, int categoria)
    {
        Juego.CargarPartida(username, dificultad, categoria);
        if(Juego.preguntas.Count > 0 /*...*/) 
        {
        return RedirectToAction("Jugar");
        }
        else
        {
        return RedirectToAction("ConfigurarJuego");
        }

    }
    public IActionResult Jugar(int IdPregunta)
    {
        ViewBag.Pregunta = Juego.ObtenerProximaPregunta();
        if(Juego.preguntas.Count > 0)
        {
        ViewBag.Respuesta = Juego.ObtenerProximaRespuesta(IdPregunta);
        return View("Juego");
        }
        else
        {
        return View("Fin");
        }
    }
    [HttpPost] public IActionResult VerificarRespuesta(int IdPregunta, int IdRespuesta)
    {
        Juego.VerificarRespuesta(IdPregunta,IdRespuesta);
        ViewBag.IdRespuesta = Juego.VerificarRespuesta(IdPregunta, IdRespuesta);
        return View("Respuesta");
    }
}
