namespace Projeto_2biPOO.Entities
{
    internal class Mesa
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public bool Status { get; set; } = false;

        public Mesa()
        {
            Numero = 0;
            Status = false;
        }

        public Mesa(int numero, bool status)
        {
            Numero = numero;
            Status = status;
        }

        public bool AlocarMesa()
        {
            if (!Status)
            {
                Status = true;
                return true;
            }
            return false;
        }

        public void LiberarMesa()
        {
            Status = false;
        }
    }
}