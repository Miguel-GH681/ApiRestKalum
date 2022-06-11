namespace WebApiKalum.Entities{
    public class InversionCarreraTecnica{
        public string InversionId {get;set;}
        public string MontoInscripcion {get;set;}
        public string NumeroPagos {get;set;}
        public string MontoPagos {get;set;}
        public string CarreraId {get;set;}
        public virtual CarreraTecnica CarreraTecnica {get;set;}
    }
}