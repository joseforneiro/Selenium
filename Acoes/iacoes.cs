namespace Acoes
{
    public class iacoes
    {
        public string Ativo { get; set; }

        public string Data { get; set; }

        public string Ultimo { get; set; }

        public string VarDia { get; set; }

        public string VarSemana { get; set; }

        public string VarMes { get; set; }

        public string VarAno { get; set; }

        public string Var12M { get; set; }

        public string ValMin { get; set; }

        public string ValMax { get; set; }

        public string Volume { get; set; }

        // Método Construtor:
        public iacoes(string ativo, string data, string ultimo, string vardia, string varsemana, string varmes, string varano, string var12m, string valmin, string valmax, string volume)
        {
            this.Ativo = ativo;
            this.Data = data;
            this.Ultimo = ultimo;
            this.VarDia = vardia;
            this.VarSemana = varsemana;
            this.VarMes = varmes;
            this.VarAno = varano;
            this.Var12M = var12m;
            this.ValMin = valmin;
            this.ValMax = valmax;
            this.Volume = volume;
        }

    }

}