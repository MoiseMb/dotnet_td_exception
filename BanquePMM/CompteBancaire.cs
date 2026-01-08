namespace BanquePMM;
    public class CompteBancaire
    {
        private readonly string? _nomClient;
        private bool _bloqué = false;

        private CompteBancaire() { }

        public CompteBancaire(string nomClient, double solde)
        {
            _nomClient = nomClient;
            Solde = solde;
        }

        public double Solde { get; private set; }

        public void Débiter(double montant)
        {
            if (_bloqué)
            {
                throw new Exception("Compte Bloqué");
            }

            ArgumentOutOfRangeException.ThrowIfGreaterThan(montant, other: Solde);
            
            if (montant <= 0)
            {
                throw new ApplicationException("Le montant retiré doit être Positif");
            }

            Solde -= montant; 
        }

        public void Créditer(double montant)
        {
            if (_bloqué)
            {
                throw new Exception("Compte Bloqué");
            }

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(montant);
            Solde += montant;
        }

        private void BloquerCompte() { _bloqué = true; }

        private void DéBloquerCompte() { _bloqué = false; }

        public static void Main()
        {
            var cb = new CompteBancaire(nomClient: "Pr Mamadou Samba Camara", solde: 500000);
            cb.Créditer(montant: 1000000);
            cb.Débiter(montant: 500000);
            Console.WriteLine($"Solde disponible : {cb.Solde}");
        }
    }
