using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api
{
    public class EndpointHelper
    {

        //CREAR USUARIO CONTRASEÑA ADMINISTRADOR
        public const string CrearLoginPrefix = "api/CrearLogin";
        public const string AdministradirLoginCrear = "CreateAdminLoin";
        public const string PersonalLoginCrear = "CreateEempresaLogin";
        public const string EmpresaLoginCrear = "CreatePersonalLogin";
        public const string AdministradorLoginLista = "List";
        public const string AdministradorLoginUpdate = "Update";
        public const string ComparatorAdministradorLoginId = "Read";
        ////CREAR USUARIO CONTRASEÑA EMPRESA
        //public const string EmpresaLoginPrefix = "api/EmpresaLogin";
        //public const string EmpresaLoginCrear = "Create";
        //public const string EmpresaLoginLista = "List";
        //public const string EmpresaLoginUpdate = "Update";
        //public const string ComparatorEmpresaLoginId = "Read";
        ////CREAR USUARIO CONTRASEÑA PERSONAL
        //public const string PersonalLoginPrefix = "api/PersonalLogin";
        //public const string PersonalLoginCrear = "Create";
        //public const string EmpresaPersonalLoginLista = "List";
        //public const string EmpresaPersonalLoginUpdate = "Update";
        //public const string ComparatorPersonalLoginId = "Read";
        //Administrador
        public const string AdministradorPrefix = "api/Administrador";
        public const string AdministradirCrear= "Create";
        public const string AdministradorLista = "List";
        public const string AdministradorUpdate = "Update";
        public const string ComparatorAdministradorId = "Read";
        //Empresa
        public const string EmpresaPrefix = "api/Empresa";
        public const string EmpresaCrear = "Create";
        public const string EmpresaLista = "List";
        public const string EmpresaUpdate = "Update";
        public const string EmpresaUpdateEstado = "UpdateEstado";
        public const string ComparatorEmpresaId = "Read";
        //Personal
        public const string PersonalPrefix = "api/Personal";
        public const string PersonalCrear = "Create";
        public const string PersonalLista = "List";
        public const string PersonalUpdate = "Update";
        //public const string PersonalUpdateEstado = "Update";
        public const string ComparatorPersonalId = "Read";

        //CostoPorInfraccion
        public const string CostoInfraccionPrefix = "api/CostoInfraccion";
        //public const string FirDigCrear = "Create";
        public const string CostoInfraccionCrear = "Create";
        public const string CostoInfraccionLista = "List";
        public const string CostoInfraccionUpdate = "Update";
        public const string ComparatorostoInfraccionId = "Read";
        //public const string FirDigSearch = "Search";
        //public const string FirDigUpdate = "Update";

        //FERIADO
        public const string HorarioPrefix = "api/Horario";
        public const string HorarioCrear = "Create";
        public const string HorarioLista = "List";
        public const string HorarioUpdate = "Update";
        public const string ComparatorHorarioId = "Read";
        //FERIADO
        public const string FeriadoPrefix = "api/DiaFeriado";
        public const string FeriadoCrear = "Create";
        public const string FeriadoLista = "List";
        public const string FeriadoUpdate = "Update";
        public const string ComparatorFeriadoId = "Read";
        //TARIFARIO
        public const string TarifarioPrefix = "api/Tarifario";
        public const string TarifarioCrear = "Create";
        public const string TarifarioLista = "List";
        public const string TarifarioUpdate = "Update";
        public const string ComparatorTarifarioId = "Read";


        //PARQUEO
        public const string ParqueoPrefix = "api/Parqueo";
        public const string ParqueoCrear = "Create";
        public const string ParqueoLista = "List";
        public const string ParqueoUpdate = "Update";
        public const string ComparatorParqueoId = "Read";

        //TIPO PAGO
        public const string TipoPagoServicioPrefix = "api/TipoPagoServicio";
        public const string TipoPagoCrear = "Create";
        public const string TipoPagoLista = "List";
        public const string TipoPagoUpdate = "Update";
        public const string ComparatorTipoPagoId = "Read";

        //DETALLE_PARQUEO 1
        public const string DParqueoPrefix = "api/UsoParqueo";
        public const string DParqueoCrear = "Create";  // crea nuevo uso de parqueo
        public const string DPruebaParqueoCrear = "CreateUso"; // crea nuevo uso de parqueo codigo via , tipo Vehi, placa
        
        public const string DPruebacargaTiempoCrear = "CreateTiempoUso";

        public const string DpTiempoExtensionCrear = "CreateTiempoExtension";

        public const string DPParqueoCrear = "PagoCreate";

        public const string DPParqueoBuscarDeuda = "ReadDeuda";

        public const string DPParqueoListVigente = "listarPorEstadoVigente";
        //cambiio de estado a Concluido vIGENTE A CONCLUIDO
        public const string DPParqueoConcluidoCrear = "UpdateConcluido";
        //Listado por tipo de estado VehiculoConcluido
        public const string DPParqueoListConcluido = "listarPorEstadoConcluido";

        //aun fatan
        public const string DParqueoLista = "List";
        public const string DParqueoUpdate = "Update";
        public const string ComparatorDParqueoId = "Read";


        //EXTENSION_PARQUEO 2
        public const string EParqueoPrefix = "api/ExtensionParqueo";
        public const string EParqueoCrear = "Create";  // cambia estado de vigente a vigente extension
        public const string EPagoParqueoCrear = "PagoCreate"; 
                         //cambiio de estado a Concluido VIGENTE EXTENSION A CONCLUIDO
        public const string DExParqueoConcluidoCrear = "UpdateConcluido";

        public const string ExtensionBuscarDeuda = "ReadExtension";

        //INMOVILIZADO 3
        public const string InmovilizadoPrefix = "api/Inmovilizado";
        public const string InmovilizadoCrear = "Create"; //ok  cambia estado a VIGENTE O  VIGENTE -EXTENSION a  INMOVILIZADO
        public const string InmovilizadoPagoCrear = "PagoCreate"; //CAMBIA DE ESTAD INMOVILIZADO A LIBERADO
                       //cambiio de estado a Concluido VIGENTE LIBERADO A CONCLUIDO
        public const string InmovilizadoConcluidoCrear = "UpdateConcluido";
       
        //REMOLCADO 4
        public const string RemolcadoPrefix = "api/Remolcado";
        public const string RemolcadoCrear = "Create";  //ok  CAMBIA ESTADO DE INMOVILIZADO A REMOLCADO
        public const string RemolcadoPagoCrear = "PagoCreate"; // CAMBIA ESTADO DE  REMOLCADO A LIBERADO-INM

        //cambiio de estado a LIBERADO-INM A LIBERADO
        public const string RemolcadoLiberadoCrear = "UpdateLiberado"; // CAMBIA ESTADO DE  LIBERADO-INM A LIBERADO
                            //cambiio de estado a Concluido VIGENTE LIBERADO A CONCLUIDO
        public const string InmovilizadoInmConcluidoCrear = "UpdateConcluido";


        //REMOLCADO 4
        public const string DeudaUsoParqueoPrefix = "api/DeudaUso";
        public const string DeudaUsoPArquepCrear = "Create";  //ok  CAMBIA ESTADO DE INMOVILIZADO A REMOLCADO
                                                              //public const string RemolcadoPagoCrear = "PagoCreate"; // 

        //USO nuevo PArqueo
        public const string DeudasPagosParqueoPrefix = "api/DeudasPagos";    
        public const string ConusltarDeuda = "QueryDeuda";  //ok  CAMBIA ESTADO DE INMOVILIZADO A REMOLCADO
    
       
        //Pago de deuda por uso de via
        public const string PagosParqueoPrefix = "api/PagosDeuda";
        public const string PagarDeuda = "PayDeuda";


        //USO nuevo PArqueo
        public const string DeudaExtensionParqueoPrefix = "api/DeudasExtension";
        public const string ConusltarDeudaextension = "QueryDeudaextension";  //ok  CAMBIA ESTADO DE INMOVILIZADO A REMOLCADO

        //Pago de deuda por uso de via
        public const string PagosextensionParqueoPrefix = "api/Pagosextensio";
        public const string PagarDeudaExtension = "PayDeudaExtension";

        //INM
        public const string DeudaInmovilizadoParqueoPrefix = "api/Deudainmovilizado";
        public const string ConusltarDeudaInmovilizado = "QueryDeudaInmovilizado";

        //Pago de inmovilizado por uso de via
        public const string PagosInmovilizadoParqueoPrefix = "api/PagoInmovilizado";
        public const string PagarDeudaInmovilizado = "PayDeudaInmovilizado";

        //REM
        public const string DeudaRemolcadoParqueoPrefix = "api/DeudaRemolcado";
        public const string ConusltarDeudaRemolcado = "QueryDeudaRemolcado";

        //Pago de inmovilizado por uso de via
        public const string PagosRemolcadoParqueoPrefix = "api/PagoRemolque";
        public const string PagarDeudaRemolcado = "PayDeudaRemolque";


        //Extrono de pago
        public const string PagosExtornoPrefix = "api/Extorno";
        public const string RealizaExtono = "Ejecutar";
    }

}

