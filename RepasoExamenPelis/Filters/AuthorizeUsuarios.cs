using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics;

namespace RepasoExamenPelis.Filters
{
    public class AuthorizeUsuarios : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //POR AHORA, NOS DA IGUAL QUIEN SEA EL EMPLEADO
            //SIMPLEMENTE QUE EXISTA
            var user = context.HttpContext.User;
            //NECESITAMOS EL CONTROLLER Y EL ACTION DE DONDE HEMOS
            //PULSADO PREVIAMENTE ANTES DE ENTRAR EN ESTE FILTER
            string controller =
                context.RouteData.Values["controller"].ToString();
            string action =
                context.RouteData.Values["action"].ToString();
            var id = context.RouteData.Values["id"];
            //PARA COMPROBAR SI FUNCIONA, DIBUJAMOS EN CONSOLA
            Debug.WriteLine("Controller: " + controller);
            Debug.WriteLine("Action: " + action);
            Debug.WriteLine("Id: " + id);
            ITempDataProvider provider =
                context.HttpContext.RequestServices
                .GetService<ITempDataProvider>();
            //ESTA CLASE CONTIENE EN SU INTERIOR EL TEMPDATA DE NUESTRA APP
            //RECUPERAMOS EL TEMPDATA DE NUESTRA APP
            var TempData = provider.LoadTempData(context.HttpContext);
            //GUARDAMOS LA INFORMACION EN TEMPDATA
            TempData["controller"] = controller;
            TempData["action"] = action;
            if (id != null)
            {
                TempData["id"] = id.ToString();
            }
            else
            {
                //ELIMINAMOS LA KEY PARA QUE NO APAREZCA EN 
                //NUESTRA RUTA 
                TempData.Remove("id");
            }
            //VOLVEMOS A GUARDAR LOS CAMBIOS DE ESTE TEMPDATA EN LA APP
            provider.SaveTempData(context.HttpContext, TempData);


            if (user.Identity.IsAuthenticated == false)
            {
                //ENVIAMOS A LA VISTA LOGIN
                context.Result = this.GetRoute("Managed", "Login");
            }
        }

        private RedirectToRouteResult GetRoute
        (string controller, string action)
        {
            RouteValueDictionary ruta = new RouteValueDictionary(new
            {
                controller = controller,
                action = action
            });
            RedirectToRouteResult result = new RedirectToRouteResult(ruta);
            return result;
        }

    }

}
