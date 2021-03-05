using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DaemonIndustialMolinera.Base;
using DaemonIndustialMolinera.baseChariot;
using DaemonIndustialMolinera.Negocio;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DaemonIndustialMolinera
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private  ExtracionDatoNegocio _ExtracionDatoNegocio;
        TestContext Context { get; }
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
        
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {

             
                    var dbContext = scope.ServiceProvider.GetRequiredService<TestContext>();
                    var dbContextEngiv2 = scope.ServiceProvider.GetRequiredService<EngineV2Context>();
                    _ExtracionDatoNegocio = new ExtracionDatoNegocio(dbContext, dbContextEngiv2);
            
                   // var EjecucionExtracionDevoluciones = _ExtracionDatoNegocio.ExtraerVisistaDevoluciones();
                    var EjecucionExtracionFacturasPendientes = await    _ExtracionDatoNegocio.GuardarfacturaPendientes();

                    List<String> facturas = new List<string>();
                    //facturas = FacturasPorEnviar();

                    //foreach(var codigofactura in facturas)
                  //  {
                      //  var EjecucionExtracionFacturasPendientesi = await _ExtracionDatoNegocio.GuardarfacturaPendientesindividuales(codigofactura);

                    //}



                    var EjecucionExtracionProductosNuevos = await _ExtracionDatoNegocio.GuardarProductoNuevo();
                    var EjecucionExtracionPedidosNuevos = await _ExtracionDatoNegocio.ExtraerPedidosRealizados();
                    var EjecucionExtracionVisitasNuevos = await _ExtracionDatoNegocio.ExtraerVisistaRealizados();
                    var EjecucionExtracionPedidosAPINuevos = await _ExtracionDatoNegocio.GuardarPedidosPendientes();
                    var EjecucionExtracionClienteNuevos =  _ExtracionDatoNegocio.ExtraerVisistaNuevos();
                 

                    var _ExecutarSPResumen = await _ExtracionDatoNegocio.ExecutarSPResumen();
                    var EjecucionExtracionAPI = await _ExtracionDatoNegocio.ExtraerVisistaDevolucionesAPI();

                    _logger.LogInformation("Servicio Corrio: {time} ,\n Numero de Facturas Ingresadas {factura} \n Numero de Productos Ingresados {factura} \n Numero de Pedidos Ingresados {factura} \n Numero de devolucion {factura} "
                                                                                    , DateTimeOffset.Now
                                                                                    , EjecucionExtracionFacturasPendientes
                                                                                    ,EjecucionExtracionProductosNuevos
                                                                                    , EjecucionExtracionPedidosNuevos,
                                                                                    EjecucionExtracionAPI);
                    // now do your work
                }
            
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
             
            
            }
        }

        public List<String> FacturasPorEnviar()
        {
            List<String> facturas = new List<string>();
            facturas.Add("461111");
            facturas.Add("465337");
            facturas.Add("465338");
            facturas.Add("465339");
            facturas.Add("465340");
            facturas.Add("465341");
            facturas.Add("465342");
            facturas.Add("465343");
            facturas.Add("465344");
            facturas.Add("465345");
            facturas.Add("465346");
            facturas.Add("465347");
            facturas.Add("465348");
            facturas.Add("465351");
            facturas.Add("465352");
            facturas.Add("465353");
            facturas.Add("465354");
            facturas.Add("465355");
            facturas.Add("465356");
            facturas.Add("465357");
            facturas.Add("465398");
            facturas.Add("465399");
            facturas.Add("465403");
            facturas.Add("465404");
            facturas.Add("465409");
            facturas.Add("465413");
            facturas.Add("465415");
            facturas.Add("465423");
            facturas.Add("465436");
            facturas.Add("465437");
            facturas.Add("465438");
            facturas.Add("465439");
            facturas.Add("465440");
            facturas.Add("465441");
            facturas.Add("465442");
            facturas.Add("465443");
            facturas.Add("465444");
            facturas.Add("465445");
            facturas.Add("465446");
            facturas.Add("465447");
            facturas.Add("465462");
            facturas.Add("465464");
            facturas.Add("465550");
            facturas.Add("465551");
            facturas.Add("465552");
            facturas.Add("465553");
            facturas.Add("465554");
            facturas.Add("465555");
            facturas.Add("465556");
            facturas.Add("465559");
            facturas.Add("465560");
            facturas.Add("465561");
            facturas.Add("465562");
            facturas.Add("465590");
            facturas.Add("465603");
            facturas.Add("465604");
            facturas.Add("465606");
            facturas.Add("466007");

            return facturas;
                
        }
    }
}
