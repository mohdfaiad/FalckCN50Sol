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
                return LeidoVigilante(v, tag);
            }
            // comprobamos si es una ronda
            TRonda r = CntCN50.GetRondaFromTag(tag, conn);
            if (r != null)
            {
                CntCN50.TClose(conn);
                return LeidaRonda(r, tag);
            }
            // comprobamos si es punto
            TPunto p = CntCN50.GetPuntoFromTag(tag, conn);
            if (p != null)
            {
                CntCN50.TClose(conn);
                return LeidoPunto(p, tag);
            }
            // desconocido
            CntCN50.TClose(conn);
            return TagDesconocido(tag);
        }
        public static Lectura LeidoVigilante(TVigilante v, string tag)
        {

            // este es fácil, simplemente cambiamos el estado y ponemos al vigilante
            Estado.Vigilante = v;
            Lectura l = new Lectura();
            l.InAuto = "CORRECTO";
            l.Leido = v.nombre;
            l.ObsAuto = "A partir de este momento las lecturas se asignarán a este vigilante";
            // no hay dudas y podemos montar la descarga a grabar;
            //--- Montar descarga asociada
            SqlCeConnection conn = CntCN50.TSqlConnection();
            CntCN50.TOpen(conn); 
            l.DescargaLinea = new TDescargaLinea();
            l.DescargaLinea.descargaLineaId = CntCN50.GetSiguienteDescargaLineaId(conn);
            l.DescargaLinea.tag = tag;
            l.DescargaLinea.tipo = "VIGILANTE";
            l.DescargaLinea.tipoId = v.vigilanteId;
            l.DescargaLinea.nombre = v.nombre;
            l.DescargaLinea.fechaHora = DateTime.Now;
            l.DescargaLinea.observaciones = "";
            //---------
            // Este hay que grabarlo porque ahora si no no lo haría nadie
            CntCN50.SetDescargaLinea(l.DescargaLinea, conn);
            CntCN50.TClose(conn);
            return l;
        }
        public static Lectura LeidaRonda(TRonda r, string tag)
        {

            Lectura l = new Lectura();
            l.Status = 0; // por defecto no hay tratamiento especial
            // Debería haberse leido previamente un vigilante 
            l.Leido = r.nombre;
            //--- Montar descarga asociada
            SqlCeConnection conn = CntCN50.TSqlConnection();
            CntCN50.TOpen(conn);
            l.DescargaLinea = new TDescargaLinea();
            l.DescargaLinea.descargaLineaId = CntCN50.GetSiguienteDescargaLineaId(conn);
            l.DescargaLinea.tag = tag;
            l.DescargaLinea.tipo = "RONDA";
            l.DescargaLinea.tipoId = r.rondaId;
            l.DescargaLinea.nombre = r.nombre;
            l.DescargaLinea.fechaHora = DateTime.Now;
            //---------
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
                l.ObsAuto = "No se había cerrado la ronda anterior. Si pulsa 'Volver' no se tendrá en cuenta esta lectura y podrá seguir con la ronda anterior. Si pulsa 'Continuar' se cerrará la ronda quedando puntos sin controlar. ";
                l.Status = 2; // ronda no cerrada
            }
            // salvamos datos de ronda anterior en previsión de cancelar
            Estado.Ronda2 = Estado.Ronda;
            Estado.RondaPuntoEsperado2 = Estado.RondaPuntoEsperado;
            Estado.Orden2 = Estado.Orden;
            // cambiamos en el estado la ronda
            Estado.Ronda = r;
            Estado.RondaPuntoEsperado = r.RondasPuntos[0];
            Estado.Orden = 0;
            return l;
        }
        public static Lectura LeidoPunto(TPunto p, string tag)
        {
            Lectura l = new Lectura();
            l.Status = 0;
            //--- Montar descarga asociada
            SqlCeConnection conn = CntCN50.TSqlConnection();
            CntCN50.TOpen(conn);
            l.DescargaLinea = new TDescargaLinea();
            l.DescargaLinea.descargaLineaId = CntCN50.GetSiguienteDescargaLineaId(conn);
            l.DescargaLinea.tag = tag;
            l.DescargaLinea.tipo = "PUNTO";
            l.DescargaLinea.tipoId = p.puntoId;
            l.DescargaLinea.nombre = p.nombre;
            l.DescargaLinea.fechaHora = DateTime.Now;
            //---------
            // comprobar si hay una ronda activa
            if (Estado.Ronda == null)
            {
                l.InAuto = "INCIDENCIA";
                l.Leido = p.nombre;
                l.ObsAuto = "No hay ninguna ronda activa, debería haber leido la etiqueta de inicio de ronda";
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
                l.ObsAuto = "El punto pertenece a la ronda pero no se ha leido en el orden correcto. Si pulsa 'Volver' el punto siguiente continuará siendo " + Estado.RondaPuntoEsperado.Punto.nombre + ". Si pulsa 'Continuar' el punto esperado será el siguiente del leido.";
                l.Status = 1; // punto no en orden.
            }
            else
            {
                l.InAuto = "INCIDENCIA";
                l.Leido = p.nombre;
                l.ObsAuto = "El punto leido no pertenece a esta ronda";
            }
            return l;
        }
        public static Lectura TagDesconocido(string tag)
        {
            Lectura l = new Lectura();
            l.Status = 0;
            //--- Montar descarga asociada
            SqlCeConnection conn = CntCN50.TSqlConnection();
            CntCN50.TOpen(conn);
            l.DescargaLinea = new TDescargaLinea();
            l.DescargaLinea.descargaLineaId = CntCN50.GetSiguienteDescargaLineaId(conn);
            l.DescargaLinea.tag = tag;
            l.DescargaLinea.tipo = null;
            l.DescargaLinea.tipoId = 0;
            l.DescargaLinea.nombre = null;
            l.DescargaLinea.fechaHora = DateTime.Now;
            //---------
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
                l.ObsAuto = "FINAL DE RONDA." + l.ObsAuto;
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
