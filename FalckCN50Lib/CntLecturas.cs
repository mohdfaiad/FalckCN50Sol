using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;

namespace FalckCN50Lib
{
    public static class CntLecturas
    {
        public static Lectura ComprobarTag(string tag)
        {
            SqlCeConnection conn = CntCN50.TSqlConnection();
            CntCN50.TOpen(conn);
            // comprobamos si el tag corresponde a un vigilante
            TVigilante v = CntCN50.GetVigilanteFromTag(tag, conn);
            if (v != null)
            {
                CntCN50.TClose(conn);
                return LeidoVigilante(v);
            }
            // comprobamos si es una ronda
            TRonda r = CntCN50.GetRondaFromTag(tag, conn);
            if (r != null)
            {
                CntCN50.TClose(conn);
                return LeidaRonda(r);
            }
            // comprobamos si es punto
            TPunto p = CntCN50.GetPuntoFromTag(tag, conn);
            if (p != null)
            {
                CntCN50.TClose(conn);
                return LeidoPunto(p);
            }
            // desconocido
            CntCN50.TClose(conn);
            return TagDesconocido();
        }
        public static Lectura LeidoVigilante(TVigilante v)
        {
            // este es fácil, simplemente cambiamos el estado y ponemos al vigilante
            Estado.Vigilante = v;
            Lectura l = new Lectura();
            l.InAuto = "CORRECTO";
            l.Leido = v.nombre;
            l.ObsAuto = "A partir de este momento las lecturas se asignarán a este vigilante";
            return l;
        }
        public static Lectura LeidaRonda(TRonda r)
        {

            Lectura l = new Lectura();
            // Debería haberse leido previamente un vigilante 
            l.Leido = r.nombre;
            if (Estado.Vigilante == null)
            {
                l.InAuto = "INCIDENCIA";
                l.ObsAuto = "No hay un vigilante identificado, esta ronda no será asignada a nadie.";
            }
            else
            {
                l.InAuto = "CORRECTO";
                l.ObsAuto = "Comienza esta ronda, en la pantalla de códigos puede consulat el punto siguiente a controlar.";
            }
            if (Estado.Ronda != null)
            {
                l.InAuto = "INCIDENCIA";
                l.ObsAuto = "No se había cerrado la ronda anterior, quedarán puntos sin controlar. " + l.ObsAuto;
            }
            // cambiamos en el estado la ronda
            Estado.Ronda = r;
            Estado.RondaPuntoEsperado = r.RondasPuntos[0];
            Estado.Orden = 0;
            return l;
        }
        public static Lectura LeidoPunto(TPunto p)
        {
            Lectura l = new Lectura();
            // comprobar si hay una ronda activa
            if (Estado.Ronda == null)
            {
                l.InAuto = "INCIDENCIA";
                l.Leido = p.nombre;
                l.ObsAuto = "No hay ninguna ronda activa, debería haber ledio la etiqueta de inicio de ronda";
                return l;
            }
            // comprobar si el punto está en secuencia
            TRondaPunto rp = Estado.RondaPuntoEsperado;
            if (p.puntoId == rp.Punto.puntoId)
            {
                l.InAuto = "CORRECTO";
                l.Leido = p.nombre;
                l.ObsAuto = "Punto leido en secuencia correcta";
                // Verificar si es el último punto de la ronda
                return UltimoEnRonda(l, p);
            }
            // comprobar si pertence a esa ronda
            bool enRonda = false;
            for (int i = 0; i < Estado.Ronda.RondasPuntos.Count; i++)
            {
                TRondaPunto rp2 = Estado.Ronda.RondasPuntos[i];
                if (rp.Punto.puntoId == p.puntoId)
                {
                    enRonda = true;
                }
            }
            if (!enRonda)
            {
                l.InAuto = "INCIDENCIA";
                l.Leido = p.nombre;
                l.ObsAuto = "El punto pertenece a la ronda pero no se ha leido en el orden correcto";
            }
            else
            {
                l.InAuto = "INCIDENCIA";
                l.Leido = p.nombre;
                l.ObsAuto = "El punto leido no pertenece a esta ronda";
            }
            return l;
        }
        public static Lectura TagDesconocido()
        {
            Lectura l = new Lectura();
            l.InAuto = "INCIDENCIA";
            l.Leido = "DESCONOCIDO";
            l.ObsAuto = "Se ha leido una etiqueta que no figura en la base de datos";
            return l;
        }

        public static Lectura UltimoEnRonda(Lectura l, TPunto p)
        {
            // Cogemos el último punto de verdad
            int ultindex = Estado.Ronda.RondasPuntos.Count - 1;
            TRondaPunto urp = Estado.Ronda.RondasPuntos[ultindex];
            if (urp.Punto.puntoId == p.puntoId)
            {
                // es el útimo punto
                l.ObsAuto = "FINAL DE RONDA. " + l.ObsAuto;
                Estado.Ronda = null;
                Estado.RondaPuntoEsperado = null;
                Estado.Orden = 0;
            }
            else
            {
                // no es el útimo
                Estado.Orden = Estado.Orden + 1;
                Estado.RondaPuntoEsperado = Estado.Ronda.RondasPuntos[Estado.Orden];
            }
            return l;
        }
    }
}
