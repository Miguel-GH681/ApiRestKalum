namespace WebApiKalum.Entities{
    public class Cargo{
        public string CargoId {get;set;}
        public string Descripcion {get;set;}
        public string Prefijo {get;set;}
        public string Monto {get;set;}
        public string GeneraMora {get;set;}
        public string PorcentajeMora {get;set;}
        public virtual List<CuentaPorCobrar> CuentasXCobrar {get;set;}
    }
}