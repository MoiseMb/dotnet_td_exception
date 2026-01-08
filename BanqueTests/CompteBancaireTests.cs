using BanquePMM;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BanqueTests
{
    [TestClass]
    public class CompteBancaireTests
    {
        #region Tests de la méthode Débiter

        [TestMethod]
        public void VérifierDébitCompteCorrect()
        {
            // Arrange
            const double soldeInitial = 500000;
            const double montantDébit = 400000;
            const double soldeAttendu = 100000;
            var cb = new CompteBancaire(nomClient: "Pr Ibrahima Fall", soldeInitial);

            // Act
            cb.Débiter(montantDébit);

            // Assert
            Assert.AreEqual(soldeAttendu, cb.Solde, delta: 0.001, message: "Compte débité incorrectement");
        }

        [TestMethod]
        public void DébiterMontantSupérieurAuSoldeLèveArgumentOutOfRangeException()
        {
            // Arrange
            const double soldeInitial = 100000;
            const double montantDébit = 200000;
            var cb = new CompteBancaire(nomClient: "Pr Ibrahima Fall", soldeInitial);

            // Act
            void Action() => cb.Débiter(montantDébit);

            // Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(Action);
        }

        [TestMethod]
        public void DébiterMontantNégatifSoulèveApplicationException()
        {
            // Arrange
            const double soldeInitial = 500000;
            const double montantDébit = -400000;
            var cb = new CompteBancaire(nomClient: "Pr Ibrahima NGom", soldeInitial);

            // Act
            void Action() => cb.Débiter(montantDébit);

            // Assert
            var ex = Assert.Throws<ApplicationException>(Action);
            Assert.AreEqual("Le montant retiré doit être Positif", ex.Message);
        }

        [TestMethod]
        public void DébiterMontantZéroSoulèveApplicationException()
        {
            // Arrange
            const double soldeInitial = 500000;
            const double montantDébit = 0;
            var cb = new CompteBancaire(nomClient: "Pr Ibrahima Fall", soldeInitial);

            // Act
            void Action() => cb.Débiter(montantDébit);

            // Assert
            var ex = Assert.Throws<ApplicationException>(Action);
            Assert.AreEqual("Le montant retiré doit être Positif", ex.Message);
        }

        #endregion

        #region Tests de la méthode Créditer

        [TestMethod]
        public void VérifierCréditCompteCorrect()
        {
            // Arrange
            const double soldeInitial = 100000;
            const double montantCrédit = 50000;
            const double soldeAttendu = 150000;
            var cb = new CompteBancaire(nomClient: "Pr Ibrahima Fall", soldeInitial);

            // Act
            cb.Créditer(montantCrédit);

            // Assert
            Assert.AreEqual(soldeAttendu, cb.Solde, delta: 0.001, message: "Compte crédité incorrectement");
        }

        [TestMethod]
        public void CréditMontantNégatifSoulèveArgumentOutOfRangeException()
        {
            // Arrange
            const double soldeInitial = 100000;
            const double montantCrédit = -50000;
            var cb = new CompteBancaire(nomClient: "Pr Ibrahima Fall", soldeInitial);

            // Act
            void Action() => cb.Créditer(montantCrédit);

            // Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(Action);
        }

        [TestMethod]
        public void CréditMontantZéroSoulèveArgumentOutOfRangeException()
        {
            // Arrange
            const double soldeInitial = 100000;
            const double montantCrédit = 0;
            var cb = new CompteBancaire(nomClient: "Pr Ibrahima Fall", soldeInitial);

            // Act
            void Action() => cb.Créditer(montantCrédit);

            // Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(Action);
        }

        #endregion

        #region Tests de la méthode Virement

        [TestMethod]
        public void VérifierVirementEntreDeuxComptesCorrect()
        {
            // Arrange
            const double soldeInitialSource = 500000;
            const double soldeInitialDestination = 200000;
            const double montantVirement = 150000;
            const double soldeAttenduSource = 350000;
            const double soldeAttenduDestination = 350000;
            
            var cbSource = new CompteBancaire(nomClient: "M. Dupont", soldeInitialSource);
            var cbDestination = new CompteBancaire(nomClient: "Mme Martin", soldeInitialDestination);

            // Act
            Virement(cbSource, cbDestination, montantVirement);

            // Assert
            Assert.AreEqual(soldeAttenduSource, cbSource.Solde, delta: 0.001, message: "Solde source incorrect");
            Assert.AreEqual(soldeAttenduDestination, cbDestination.Solde, delta: 0.001, message: "Solde destination incorrect");
        }

        [TestMethod]
        public void VirementMontantSupérieurAuSoldeSoulèveArgumentOutOfRangeException()
        {
            // Arrange
            const double soldeInitialSource = 100000;
            const double soldeInitialDestination = 200000;
            const double montantVirement = 150000;
            
            var cbSource = new CompteBancaire(nomClient: "M. Dupont", soldeInitialSource);
            var cbDestination = new CompteBancaire(nomClient: "Mme Martin", soldeInitialDestination);

            // Act
            void Action() => Virement(cbSource, cbDestination, montantVirement);

            // Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(Action);
        }

        [TestMethod]
        public void VirementMontantNégatifSoulèveApplicationException()
        {
            // Arrange
            const double soldeInitialSource = 500000;
            const double soldeInitialDestination = 200000;
            const double montantVirement = -100000;
            
            var cbSource = new CompteBancaire(nomClient: "M. Dupont", soldeInitialSource);
            var cbDestination = new CompteBancaire(nomClient: "Mme Martin", soldeInitialDestination);

            // Act
            void Action() => Virement(cbSource, cbDestination, montantVirement);

            // Assert
            var ex = Assert.Throws<ApplicationException>(Action);
        }

        [TestMethod]
        public void VirementMontantZéroSoulèveApplicationException()
        {
            // Arrange
            const double soldeInitialSource = 500000;
            const double soldeInitialDestination = 200000;
            const double montantVirement = 0;
            
            var cbSource = new CompteBancaire(nomClient: "M. Dupont", soldeInitialSource);
            var cbDestination = new CompteBancaire(nomClient: "Mme Martin", soldeInitialDestination);

            // Act
            void Action() => Virement(cbSource, cbDestination, montantVirement);

            // Assert
            var ex = Assert.Throws<ApplicationException>(Action);
        }

        #endregion

        #region Méthode auxiliaire Virement

        /// <summary>
        /// Effectue un virement d'un compte source vers un compte destination
        /// </summary>
        private void Virement(CompteBancaire compteSource, CompteBancaire compteDestination, double montant)
        {
            compteSource.Débiter(montant);
            compteDestination.Créditer(montant);
        }

        #endregion
    }
}