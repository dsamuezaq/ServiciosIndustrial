using DaemonIndustialMolinera.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DaemonIndustialMolinera.Base.Tablas;
using DaemonIndustialMolinera.baseChariot;
using System.Threading.Tasks;
using DaemonIndustialMolinera.Modelos;
using System.Globalization;

namespace DaemonIndustialMolinera.Negocio
{
   public   class ExtracionDatoNegocio
    {
         TestContext Context;
        EngineV2Context EngV2Context;
        protected HelpersHttpClientBussiness _helpersHttpClientBussiness = new HelpersHttpClientBussiness();

        public ExtracionDatoNegocio(TestContext _chariotContext , EngineV2Context _EngV2Context)
        {
            Context = _chariotContext;
            EngV2Context = _EngV2Context;


        }


#pragma warning disable CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        public async  Task<int> GuardarfacturaPendientes()
#pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        {
            try
            {
                var DatosFacturaIndustrialMolinera = Task.Factory.StartNew(() => {
                    return _helpersHttpClientBussiness.GetApi<FacturasApi>("CoberturaFacturaRuta/obtener");
                });
           
              //   var a=     EngV2Context.Orders.ToList();
              
                List<FacturasApi>  DatosFactura = Context.FacturasApiTabla.Where(x=>x.estadoentrega=="PENDIENTE" ).ToList();
                DatosFacturaIndustrialMolinera.Wait();
                List<FacturasApi> SetDatoFacturaAPI = DatosFacturaIndustrialMolinera.Result.Result.ToList();
     
                foreach (FacturasApi Factura in DatosFactura) {

                 
                    var  ConsultaFactura = SetDatoFacturaAPI.Where(x => x.factura == Factura.factura);
                    if (ConsultaFactura.Count() > 0) {

                        SetDatoFacturaAPI.Remove(ConsultaFactura.FirstOrDefault());
                    }
              
                  
                }
                foreach (FacturasApi Factura in DatosFacturaIndustrialMolinera.Result.Result.ToList())
                {
                    var ConsultaFactura = DatosFactura.Where(x => x.factura == Factura.factura);
                    if (ConsultaFactura.Count() > 0)
                    {

                        DatosFactura.Remove(ConsultaFactura.FirstOrDefault());
                    }
                   
                }
              
                SetDatoFacturaAPI.AsParallel()
                   .ForAll(
                   s =>
                   {
                       s.estadoentrega="PENDIENTE";

                       s.fechaMardis = DateTime.ParseExact(s.fecha.ToString(),
                                                       "yyyyMMdd",
                                                       CultureInfo.InvariantCulture,
                                                       DateTimeStyles.None);
                   }
                   );
                DatosFactura.AsParallel()
               .ForAll(
               s =>
               {
                   s.estadoentrega = "ENTREGADO"; 
                 

               }
               );
                if (SetDatoFacturaAPI.Count() > 0) {
                    Context.FacturasApiTabla.AddRange(SetDatoFacturaAPI);
                    Context.SaveChanges();
                }
                if (DatosFactura.Count() > 0)
                {
                    Context.FacturasApiTabla.UpdateRange(DatosFactura);
                    Context.SaveChanges();

                }

                return SetDatoFacturaAPI.Count();

            }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {

                return 0; ;
            }
        }



        public async Task<int> GuardarfacturaPendientesindividuales(String CodigoFactura)
#pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        {
            try
            {
                var DatosFacturaIndustrialMolinera = Task.Factory.StartNew(() => {
                    return _helpersHttpClientBussiness.GetApi<FacturasApi>("CoberturaFactura/obtenerxfactura?factura=" + CodigoFactura);
                });

                //   var a=     EngV2Context.Orders.ToList();

                List<FacturasApi> DatosFactura = Context.FacturasApiTabla.Where(x => x.estadoentrega == "PENDIENTE").ToList();
                DatosFacturaIndustrialMolinera.Wait();
                List<FacturasApi> SetDatoFacturaAPI = DatosFacturaIndustrialMolinera.Result.Result.ToList();

                foreach (FacturasApi Factura in DatosFactura)
                {


                    var ConsultaFactura = SetDatoFacturaAPI.Where(x => x.factura == Factura.factura);
                    if (ConsultaFactura.Count() > 0)
                    {

                        SetDatoFacturaAPI.Remove(ConsultaFactura.FirstOrDefault());
                    }


                }
                foreach (FacturasApi Factura in DatosFacturaIndustrialMolinera.Result.Result.ToList())
                {
                    var ConsultaFactura = DatosFactura.Where(x => x.factura == Factura.factura);
                    if (ConsultaFactura.Count() > 0)
                    {

                        DatosFactura.Remove(ConsultaFactura.FirstOrDefault());
                    }

                }

                SetDatoFacturaAPI.AsParallel()
                   .ForAll(
                   s =>
                   {
                       s.estadoentrega = "PENDIENTE";

                       s.fechaMardis = DateTime.ParseExact(s.fecha.ToString(),
                                                       "yyyyMMdd",
                                                       CultureInfo.InvariantCulture,
                                                       DateTimeStyles.None);
                   }
                   );
                DatosFactura.AsParallel()
               .ForAll(
               s =>
               {
                   s.estadoentrega = "ENTREGADO";


               }
               );
                if (SetDatoFacturaAPI.Count() > 0)
                {
                    Context.FacturasApiTabla.AddRange(SetDatoFacturaAPI);
                    Context.SaveChanges();
                }
                if (DatosFactura.Count() > 0)
                {
                    Context.FacturasApiTabla.UpdateRange(DatosFactura);
                    Context.SaveChanges();

                }

                return SetDatoFacturaAPI.Count();

            }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {

                return 0; ;
            }
        }

#pragma warning disable CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        public async Task<int> GuardarPedidosPendientes()
#pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        {
            try
            {
                var DatosPedidoIndustrialMolinera = Task.Factory.StartNew(() => {
                    return _helpersHttpClientBussiness.GetApi<PedidosAPI>("pedidocobertura/obtener");
                });

                //   var a=     EngV2Context.Orders.ToList();

                List<string> DatosPedidos = Context.ResumenPedidos_APIs.Select(x=>x.p_PEDIDO_MARDIS.Trim()).ToList();
                DatosPedidoIndustrialMolinera.Wait();
                List<PedidosAPI> SetDatoPedidosAPI = DatosPedidoIndustrialMolinera.Result.Result.ToList();


                var InsertarPedidos = SetDatoPedidosAPI.Where(x => !DatosPedidos.Contains(x.p_PEDIDO_MARDIS.Trim()))
                    .Select(c => new ResumenPedidos_API
                    {
                        id = c.p_PEDIDO,
                        codCliente = c.p_CLIENTE.ToString(),
                        fecha = c.p_FECHA.ToString(),
                        idVendedor = c.p_VENDEDOR.ToString(),
                        // totalNeto = c.,
                        // totalFinal = s.totalFinal,
                        idArticulo = c.p_PRODUCTO,
                        importeUnitario = c.p_PRECIO,
                        cantidad = c.p_CANTIDAD,
                          total = c.p_CANTIDAD *c.p_PRECIO,
                        //  formapago = s.formapago,
                        // unidad = s.unidad,
                        FechaPedido = DateTime.ParseExact(c.p_FECHA.ToString(),
                                                       "yyyyMMdd",
                                                       CultureInfo.InvariantCulture,
                                                       DateTimeStyles.None),
                        p_PEDIDO_MARDIS = c.p_PEDIDO_MARDIS.Trim()
                    })
;
            
                if (InsertarPedidos.Count() > 0)
                {
                    Context.ResumenPedidos_APIs.UpdateRange(InsertarPedidos);
                    Context.SaveChanges();
                }

                    return InsertarPedidos.Count();

            }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
                {

                return 0; ;
            }
        }

#pragma warning disable CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        public async Task<int> GuardarProductoNuevo()
#pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        {
            try
            {
                var ProductoCartera = Task.Factory.StartNew(() => {
                    return _helpersHttpClientBussiness.GetApi<GetCoberturaProductoViewModel>("CoberturaProducto/obtener");
                });

                //   var a=     EngV2Context.Orders.ToList();
         
                List<Product> RegistrosProductos = Context.MaestroProductos.ToList();
                ProductoCartera.Wait();
                List<GetCoberturaProductoViewModel> SetDatoProductoAPI = ProductoCartera.Result.Result.ToList();
                foreach (Product Producto in RegistrosProductos)
                {

                    var ConsultaProducto= SetDatoProductoAPI.Where(x => x.codigoprod.ToString().Trim() == Producto.IdArticulo.Trim());
                    if (ConsultaProducto.Count() > 0)
                    {

                        SetDatoProductoAPI.Remove(ConsultaProducto.FirstOrDefault());
                    }


                }
             
                List < Product > DatoProducto = SetDatoProductoAPI.Select(_datoProducto => new Product
                {

                    IdArticulo = _datoProducto.codigoprod.Trim(),
                    Descripcion = _datoProducto.nombreprod.Trim(),
                    Id_Línea= CodigoLinea(_datoProducto.linea.Trim()),
                    Línea = _datoProducto.linea.Trim(),
                    Iva = _datoProducto.pagaiva.Trim() == "N" ? 0 : 1,
                    Precio1 = _datoProducto.precioprod,
                    Precio10 = _datoProducto.stock

                }).ToList();



                if (DatoProducto.Count() > 0)
                {
                    Context.MaestroProductos.AddRange(DatoProducto);
                    Context.SaveChanges();
                }
              

                return DatoProducto.Count();

            }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {

                return 0; ;
            }
        }

#pragma warning disable CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        public async Task<int> ExtraerPedidosRealizados()
#pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        {
            try
            {
                var DatosVistaPedido = EngV2Context.VistaDetallePedidos;

                if (DatosVistaPedido.Count() > 0)
                {

                    try
                    {
                        var DatosResumenPedidos = DatosVistaPedido.Select(s => new ResumenPedidos
                        {
                            id = s.id,
                            codCliente = s.codCliente,
                            fecha = s.fecha,
                            idVendedor = s.idVendedor,
                            totalNeto = s.totalNeto,
                            totalFinal = s.totalFinal,
                            idArticulo = s.idArticulo,
                            importeUnitario = s.importeUnitario,
                            cantidad = s.cantidad,
                            total = s.total,
                            formapago = s.formapago,
                            unidad = s.unidad,
                            FechaPedido = s.FechaPedido
                           ,p_PEDIDO_MARDIS=s.p_PEDIDO_MARDIS
                        }).ToList();



                        Context.ResumenPedidos.AddRange(DatosResumenPedidos);
                        Context.SaveChanges();

                   
                    var ListaIDPedidos = DatosVistaPedido.Select(s => s.id).ToList();




                    var DatosActualizar = EngV2Context.Orders.Where(q => ListaIDPedidos.Contains(q.id)).ToList();
                    DatosActualizar.AsParallel()
                                    .ForAll(
                                    s =>
                                    {
                                        s.transferido = 1;
                                    }
                                    );
                    EngV2Context.Orders.UpdateRange(DatosActualizar);
                    EngV2Context.SaveChanges();
                    }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
                    catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
                    {

                        throw;
                    }



                }


                return DatosVistaPedido.Count();
            }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {

                return 0;
            }
        }

#pragma warning disable CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        public async Task<int> ExtraerVisistaRealizados()
#pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        {
            try
            {
                var DatosVistaVisita = EngV2Context.VistaVisitalocales;

                if (DatosVistaVisita.Count() > 0)
                {

                    try
                    {
                        var DatosResumenVisita = DatosVistaVisita.Select(s => new Visita
                        {
                            //id = s.id,
                            codcliente = s.codcliente,
                            codvendedor = s.codvendedor,
                            fechavisita = s.fechavisita,
                            Latitud = s.Latitud,
                            Longitud = s.Longitud,
                            Linkfotoexterior = s.Linkfotoexterior,
                            Compro = s.Compro,
                            Observacion = s.Observacion,
                            estado = s.estado


                        }).ToList();



                        Context.Visitalocales.AddRange(DatosResumenVisita);
                        Context.SaveChanges();


                        var ListaIDVisita = DatosVistaVisita.Select(s => s.id).ToList();




                        var DatosActualizar = EngV2Context.VisitaUIOs.Where(q => ListaIDVisita.Contains(q.id)).ToList();
                        DatosActualizar.AsParallel()
                                        .ForAll(
                                        s =>
                                        {
                                            s.migrado = 1;
                                        }
                                        );
                        EngV2Context.VisitaUIOs.UpdateRange(DatosActualizar);
                        EngV2Context.SaveChanges();
                    }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
                    catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
                    {

                        throw;
                    }



                }


                return DatosVistaVisita.Count();
            }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {

                return 0;
            }
        }


        public int ExtraerVisistaNuevos()
        {
            try
            {
                var DatosVistaNUevos = EngV2Context.VistaLocalesNuevos;
               
                //  var localesPedidos = EngV2Context.VistaLocalesNuevos2();
                if (DatosVistaNUevos.Count() > 0)
                {
                    var d = DatosVistaNUevos.ToList();
                    try
                    {
                        var DatosResumenNuevos = DatosVistaNUevos.Select(s => new ClienteNuevo
                        {
                            //id = s.id,
                            CodigoCliente = s.CodigoCliente,
                            Code = s.Code,
                            Documento = s.Documento,
                            nombreLocal = s.nombreLocal,
                            Direcion = s.Direcion,
                            negocio = s.negocio,
                            dia = s.dia,
                            ESTADO = s.ESTADO,
                            ruta = s.ruta,
                            cov = s.cov,
                            vendedor = s.vendedor,
                            lat = s.lat,
                            lng = s.lng,
                            CodigoMardis = s.CodigoMardis



                        }).ToList();
            
                        foreach (ClienteNuevo item in DatosResumenNuevos)
                        {

                            var pedido = Context.ResumenPedidos.Where(x => x.codCliente == item.CodigoCliente.ToString());
                            if (pedido.Count() > 0) {
                                var Factura = Context.FacturasApiTabla.Where(x => x.pedidomardis.Trim() == pedido.FirstOrDefault().p_PEDIDO_MARDIS.Trim());
                                if (Factura.Count() > 0)
                                {
                                    try
                                    {
                                        item.Code = Factura.FirstOrDefault().codigocliente.ToString();
                                        Context.ClienteNuevos.Add(item);
                                        Context.SaveChanges();

                                        var Branch = EngV2Context.Branchess.Where(x => x.Id == item.CodigoCliente);
                                        Branch.FirstOrDefault().Code = item.Code;
                                        Branch.FirstOrDefault().ExternalCode = item.Code;
                                        Branch.FirstOrDefault().CommentBranch = "Nuevo Actualizado";
                                        EngV2Context.Branchess.Update(Branch.FirstOrDefault());
                                        EngV2Context.SaveChanges();
                                    }
                                    catch (Exception)
                                    {
                                        throw;
                                    }
                
                                }
                            }
    
                        
                        
                        }
                 
                    }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
                    catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
{
                        return 0;
                        throw;
                    }



                }


                return 1;
            }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {

                return 0;
            }
        }
#pragma warning disable CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        public async Task<bool> ExecutarSPResumen()
#pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        {
            try
            {
                var tasks = Context.Query<List<int>>($@"EXEC IndustrialMolinera.Sp_procesar_Resumen_ventas_auto").ToList();
                return true;
            }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {

                return false;
            }
        }
        public async Task<int> ExtraerVisistaDevoluciones()
#pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        {
            try
            {
                var iddevolucion = Context.DevolucionFacturaAPIs.Select(x => x.id_DEVOLUCION).ToList();
                var CoberturaDevolucion = Task.Factory.StartNew(() => {
                    return _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaDevolucion>("CoberturaFacturaDevolucion/obtener");
                });
                if (CoberturaDevolucion.Result.Result.Count() > 0)
                {

                    try
                    {
                        if (iddevolucion.Count() == 0)
                        {
                            var _DevolucionFactura = CoberturaDevolucion.Result.Result.Select(s => new DevolucionFacturaAPI
                            {
                                //id = s.id,
                                id_DEVOLUCION = s.dF_DEVOLUCION,
                                d_DEVOLUCION = s.dF_DEVOLUCION,
                                d_ORDEN = s.dF_ORDEN,
                                d_FECHA = s.dF_FECHA.ToString().Trim(),
                                d_FACTURA = s.dF_FACTURA,
                                d_CLIENTE = s.dF_CLIENTE,
                                d_PRODUCTO = s.dF_PRODUCTO.ToString(),
                                d_PRECIO = s.dF_PRECIO,
                                d_CANTIDAD = s.dF_CANTIDAD,
                                d_VENDEDOR = s.dF_VENDEDOR,
                                d_ESTADO = s.dF_ESTADO
                                //  d_PEDIDO_MARDIS = s.d_PEDIDO_MARDIS
                                ,
                                FechaDevolucion = DateTime.ParseExact(s.dF_FECHA.ToString().Trim(),
                                                           "yyyyMMdd",
                                                           CultureInfo.InvariantCulture,
                                                           DateTimeStyles.None),


                            }).ToList();
                            Context.DevolucionFacturaAPIs.AddRange(_DevolucionFactura);
                            Context.SaveChanges();
                            return _DevolucionFactura.Count();
                        }
                        else
                        {
                            var _DevolucionFactura = CoberturaDevolucion.Result.Result.Where(x => !iddevolucion.Contains(x.dF_DEVOLUCION)).Select(s => new DevolucionFacturaAPI
                            {
                                //id = s.id,
                                id_DEVOLUCION = s.dF_DEVOLUCION,
                                d_DEVOLUCION = s.dF_DEVOLUCION,
                                d_ORDEN = s.dF_ORDEN,
                                // d_FECHA = s.dF_FECHA.ToString().Trim(),
                                d_FACTURA = s.dF_FACTURA,
                                d_CLIENTE = s.dF_CLIENTE,
                                d_PRODUCTO = s.dF_PRODUCTO.ToString(),
                                d_PRECIO = s.dF_PRECIO,
                                d_CANTIDAD = s.dF_CANTIDAD,
                                d_VENDEDOR = s.dF_VENDEDOR,
                                d_ESTADO = s.dF_ESTADO
                                //  d_PEDIDO_MARDIS = s.d_PEDIDO_MARDIS
                                //,
                                //  FechaDevolucion = DateTime.ParseExact(s.dF_FECHA.ToString().Trim(),
                                //                           "yyyyMMdd",
                                //                           CultureInfo.InvariantCulture,
                                //                           DateTimeStyles.None),


                            }).ToList();
                            Context.DevolucionFacturaAPIs.AddRange(_DevolucionFactura);
                            Context.SaveChanges();
                            return _DevolucionFactura.Count();
                        }




                    }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
                    catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
                    {
                        return 0;
                        throw;
                    }



                }

           


                return 0;
            }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {

                return 0;
            }
        }

        public async Task<int> ExtraerVisistaDevolucionesAPI()
#pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        {
            try
            {
                var iddevolucion = Context.DevolucionFacturaAPIs.Select(x=>x.id_DEVOLUCION).ToList();
                var CoberturaDevolucion = Task.Factory.StartNew(() => {
                    return _helpersHttpClientBussiness.GetApi<GetCoberturaFacturaDevolucion>("CoberturaFacturaDevolucion/obtener");
                });
                if (CoberturaDevolucion.Result.Result.Count() > 0)
                {

                    try
                    {
                        if (iddevolucion.Count() == 0)
                        {
                            var _DevolucionFactura = CoberturaDevolucion.Result.Result.Select(s => new DevolucionFacturaAPI
                            {
                                //id = s.id,
                                id_DEVOLUCION = s.dF_DEVOLUCION,
                                d_DEVOLUCION = s.dF_DEVOLUCION,
                                d_ORDEN = s.dF_ORDEN,
                                d_FECHA = s.dF_FECHA.ToString().Trim(),
                                d_FACTURA = s.dF_FACTURA,
                                d_CLIENTE = s.dF_CLIENTE,
                                d_PRODUCTO = s.dF_PRODUCTO.ToString(),
                                d_PRECIO = s.dF_PRECIO,
                                d_CANTIDAD = s.dF_CANTIDAD,
                                d_VENDEDOR = s.dF_VENDEDOR,
                                d_ESTADO = s.dF_ESTADO
                                //  d_PEDIDO_MARDIS = s.d_PEDIDO_MARDIS
                                ,
                                FechaDevolucion = DateTime.ParseExact(s.dF_FECHA.ToString().Trim(),
                                                           "yyyyMMdd",
                                                           CultureInfo.InvariantCulture,
                                                           DateTimeStyles.None),


                            }).ToList();
                            Context.DevolucionFacturaAPIs.AddRange(_DevolucionFactura);
                            Context.SaveChanges();
                            return _DevolucionFactura.Count();
                        }
                        else {
                            var _DevolucionFactura = CoberturaDevolucion.Result.Result.Where(x=>!iddevolucion.Contains(x.dF_DEVOLUCION)).Select(s => new DevolucionFacturaAPI
                            {
                                //id = s.id,
                                id_DEVOLUCION = s.dF_DEVOLUCION,
                                d_DEVOLUCION = s.dF_DEVOLUCION,
                                d_ORDEN = s.dF_ORDEN,
                               // d_FECHA = s.dF_FECHA.ToString().Trim(),
                                d_FACTURA = s.dF_FACTURA,
                                d_CLIENTE = s.dF_CLIENTE,
                                d_PRODUCTO = s.dF_PRODUCTO.ToString(),
                                d_PRECIO = s.dF_PRECIO,
                                d_CANTIDAD = s.dF_CANTIDAD,
                                d_VENDEDOR = s.dF_VENDEDOR,
                                d_ESTADO = s.dF_ESTADO
                              //  d_PEDIDO_MARDIS = s.d_PEDIDO_MARDIS
                              //,
                              //  FechaDevolucion = DateTime.ParseExact(s.dF_FECHA.ToString().Trim(),
                              //                           "yyyyMMdd",
                              //                           CultureInfo.InvariantCulture,
                              //                           DateTimeStyles.None),


                            }).ToList();
                            Context.DevolucionFacturaAPIs.AddRange(_DevolucionFactura);
                            Context.SaveChanges();
                            return _DevolucionFactura.Count();
                        }


                     

                    }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
                    catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
                    {
                        return 0;
                        throw;
                    }



                }

                return 0;

            }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {

                return 0;
            }
        }
        int CodigoLinea(string Nombrelinea) {

            int Codigolinea = 0;
            switch (Nombrelinea)
            {
                case "AVENA SABORIZADA":
                    Codigolinea = 1;
                    break;
                case "AVENA":
                    Codigolinea = 2;
                    break;
                case "HARINA":
                    Codigolinea = 3;
                    break;
                case "MAIZABROSA":
                    Codigolinea = 4;
                    break;
                case "MANTECA":
                    Codigolinea = 5;
                    break;
                case "CAFE":
                    Codigolinea = 6;
                    break;
                case "TRONCAL AZUCAR":
                    Codigolinea = 7;
                    break;
                default:
                    break;
            }
            return Codigolinea;

        }

    }
}
