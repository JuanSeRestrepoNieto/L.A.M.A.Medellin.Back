
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class InternalBaseController : ControllerBase
{
  protected const string API_SCOPE_MIEMBROS_LECTURA = "Miembros.Read";
  protected const string API_SCOPE_MIEMBROS_ACTUALIZACION = "Miembros.UpdateById";
  protected const string API_SCOPE_MIEMBROS_LEER_ID = "Miembros.GetById";
  protected const string API_SCOPE_MIEMBROS_CREACION = "Miembros.Create";
  protected const string API_SCOPE_MIEMBROS_ELIMINACION = "Miembros.Delete";
  protected const string API_SCOPE_MIEMBROS_TODOS = "Miembros.ReadAll";
}