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
            // cierre manual de ronda
            if (tag == "*99*")
            {
                return FinRondaManual(tag);
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
            l.ObsAuto = "Usuario " + v.nombre + " leido correctamente";
            // no hay dudas y podemos montar la descarga a grabar;
            //--- Montar descarga asociada
            //SqlCeConnection conn = CntCN50.TSqlConnection();
            //CntCN50.TOpen(conn); 
            l.DescargaLinea = new TDescargaLinea();
            // l.DescargaLinea.descargaLineaId = CntCN50.GetSiguienteDescargaLineaId(conn);
            l.DescargaLinea.descargaLineaId = 0;
            l.DescargaLinea.tag = tag;
            l.DescargaLinea.tipo = "VIGILANTE";
            l.DescargaLinea.tipoId = v.vigilanteId;
            l.DescargaLinea.nombre = v.nombre;
            l.DescargaLinea.fechaHora = DateTime.Now;
            l.DescargaLinea.observaciones = "";
            //---------
            // Este hay que grabarlo porque ahora si no no lo haría nadie
            //CntCN50.SetDescargaLinea(l.DescargaLinea, conn);
            //CntCN50.TClose(conn);
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
                l.ObsAuto = "Usuario NO IDENTIFICADO. Realizar lectura de codigo personal.";
            }
            else
            {
                l.InAuto = "CORRECTO";
                l.ObsAuto = "Inicio de ronda " + r.nombre + ".";
            }
            if (Estado.Ronda != null)
            {
                l.InAuto = "INCIDENCIA";
                l.ObsAuto = "Ronda anterior SIN CERRAR. Pulsa <Volver> para seguir con la ronda anterior. Pulsa <Continuar> para cerrar la ronda anterior SIN FINALIZAR.";
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
            // marcar el punto como controlado en la ronda activa
            CntCN50.SetPointLastControl(p.puntoId,conn);
            //---------
            // comprobar si hay una ronda activa
            if (Estado.Ronda == null)
            {
                l.InAuto = "INCIDENCIA";
                l.Leido = p.nombre;
                l.ObsAuto = "No hay ninguna ronda activa. Realizar la lectura del Codigo de la ronda deseada.";
                MarcarControlado(Estado.Ronda, p);
                return l;
            }
            // comprobar si el punto está en secuencia
            TRondaPunto rp = Estado.RondaPuntoEsperado;
            if (p.puntoId == rp.Punto.puntoId)
            {
                l.InAuto = "CORRECTO";
                l.Leido = p.nombre;
                l.ObsAuto = "Punto control [" + p.nombre + "] leido en secuencia correcta.";
                MarcarControlado(Estado.Ronda, p);
                // Verificar si es el último punto de la ronda
                return UltimoEnRonda(l, p);
            }
            // comprobar si pertence a esa ronda
            bool enRonda = false;
            bool repetido = false;
            for (int i = 0; i < Estado.Ronda.RondasPuntos.Count; i++)
            {
                rp = Estado.Ronda.RondasPuntos[i];
                if (rp.Punto.puntoId == p.puntoId)
                {
                    enRonda = true;
                    if (rp.Controlado) repetido = true;
                }
            }
            MarcarControlado(Estado.Ronda, p);
            if (enRonda)
            {
                l.InAuto = "INCIDENCIA";
                l.Leido = p.nombre;
                if (repetido)
                {
                    l.ObsAuto = "Punto repetido, ya ha leido este punto en la ronda. Pulsa <Volver> para realizar la lectura del Punto Control esperado [" + Estado.RondaPuntoEsperado.Punto.nombre + "]. Pulsar <Continuar> para seguir con la ronda desde el Punto Control leido.";
                    l.Status = 4; // punto repetido
                }
                else
                {
                    l.ObsAuto = "Punto control leido pertenece a la ronda pero no se ha leido en el orden esperado. Pulsa <Volver> para realizar la lectura del Punto Control esperado [" + Estado.RondaPuntoEsperado.Punto.nombre + "]. Pulsar <Continuar> para seguir con la ronda desde el Punto Control leido.";
                    l.Status = 1; // punto no en orden.
                }
                // puede que sea el último
                l = UltimoEnRonda(l, p);
            }
            else
            {
                l.InAuto = "INCIDENCIA";
                l.Leido = p.nombre;
                l.ObsAuto = "Punto control [" + p.nombre + "] leido NO pertenece a esta ronda.";
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
            l.ObsAuto = "El codigo leido no figura en la base de datos.";
            return l;
        }

        public static Lectura Observacion()
        {
            if (Estado.RondaPuntoEsperado == null)
            {
                return null;
            }
            SqlCeConnection conn = CntCN50.TSqlConnection();
            CntCN50.TOpen(conn);
            Lectura l = new Lectura();
            l.Status = 0;
            l.DescargaLinea = new TDescargaLinea();
            l.DescargaLinea.descargaLineaId = CntCN50.GetSiguienteDescargaLineaId(conn);
            l.DescargaLinea.tag = "OBSERVACION";
            l.DescargaLinea.tipo = "OBSERVACION";
            TRondaPunto rp = Estado.RondaPuntoEsperado;
            l.DescargaLinea.tipoId = 0;
            l.DescargaLinea.nombre = rp.Punto.nombre;
            l.DescargaLinea.fechaHora = DateTime.Now;
            CntCN50.TClose(conn);
            //---------
            l.InAuto = "OBSERVACION";
            l.Leido = rp.Punto.nombre;
            l.ObsAuto = "Introduzca incidencia y/o comentario";
            return l;
        }


        public static Lectura FinRondaManual(string tag)
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
            l.Leido = "FIN RONDA MANUAL";
            l.ObsAuto = "Ha finalizado la ronda manualmente, pulse <CONTINUAR> para seguir con las lecturas.";
            // cambiamos en el estado la ronda
            Estado.Ronda = null;
            Estado.RondaPuntoEsperado = null;
            Estado.Orden = 0;
            return l;
        }
        public static Lectura UltimoEnRonda(Lectura l, TPunto p)
        {
            // comprobamos si la ronda esta completa
            PuntosSinControl psc = PuntosNoControlados(Estado.Ronda);
            // Cogemos el último punto de verdad
            int ultindex = Estado.Ronda.RondasPuntos.Count - 1;
            TRondaPunto urp = Estado.Ronda.RondasPuntos[ultindex];
            if (urp.Punto.puntoId == p.puntoId)
            {
                // es el útimo punto
                l.ObsAuto = "FINAL DE RONDA." + l.ObsAuto;
                if (psc.pos >= 0)
                {
                    // hay puntos sin controlar i el siguiente viene en pos
                    l.InAuto = "INCIDENCIA";
                    l.ObsAuto = "Ronda sin completar, faltan los puntos de control " + psc.lista + ". Pulsa <Volver> para realizar las lecturas pendientes Pulsar <Continuar> para forzar el cierre de la Ronda sin completar.";
                    Estado.RondaPuntoEsperado = Estado.Ronda.RondasPuntos[psc.pos];
                    l.Status = 3;
                }
                else
                {
                    Estado.Ronda = null;
                    Estado.RondaPuntoEsperado = null;
                    Estado.Orden = 0;
                }
            }
            else
            {
                if (psc.pos >= 0)
                {
                    if (l.Status == 0)
                    {
                        // no es el útimo (ponemos como siguiente el no controlado)
                        Estado.Orden = psc.pos;
                        Estado.RondaPuntoEsperado = Estado.Ronda.RondasPuntos[psc.pos];
                    }
                }
                else
                {
                    // la ronda en realidad está completa
                    l.ObsAuto = "FINAL DE RONDA. " + "Ha controlado todos los puntos de la ronda activa";
                    Estado.Ronda = null;
                    Estado.RondaPuntoEsperado = null;
                    Estado.Orden = 0;
                }
            }
            return l;
        }

        public static void MarcarControlado(TRonda r, TPunto p)
        {
            if (r == null) return;
            for (int i = 0; i < r.RondasPuntos.Count; i++)
            {
                TRondaPunto rp = r.RondasPuntos[i];
                if (rp.Punto.puntoId == p.puntoId)
                {
                    // lo marcamos como controlado
                    rp.Controlado = true;
                }
            }
        }

        public static void DesmarcarControlado(TRonda r, TPunto p)
        {
            if (r == null) return;
            for (int i = 0; i < r.RondasPuntos.Count; i++)
            {
                TRondaPunto rp = r.RondasPuntos[i];
                if (rp.Punto.puntoId == p.puntoId)
                {
                    // lo desmarcamos como controlado
                    rp.Controlado = false;
                }
            }
        }


        public static PuntosSinControl PuntosNoControlados(TRonda r)
        {
            PuntosSinControl psc = new PuntosSinControl();
            int pos = -1;
            string lista = "";
            for (int i = 0; i < r.RondasPuntos.Count; i++)
            {
                TRondaPunto rp = r.RondasPuntos[i];
                if (!rp.Controlado)
                {
                    if (pos == -1) pos = i;
                    lista += "[" + rp.Punto.nombre + "]";
                }
            }
            psc.pos = pos;
            psc.lista = lista;
            return psc;
        }
    }
}
