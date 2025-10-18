using System;
using System.Collections.Generic;
using Dll_OPC;
using gererOF.Models;
using gererOF.Repository;

namespace gererOF.Service
{
    public class OFService
    {
        private readonly OFRepository _ofRepository;
        private readonly OPCUAService _opCUAService;

        public OFService()
        {
            _ofRepository = new OFRepository();
            _opCUAService = new OPCUAService();
        }

        /// <summary>
        /// Récupère la liste de tous les OF depuis la base.
        /// </summary>
        public List<OF> GetAllOF()
        {
            try
            {
                return _ofRepository.getAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des ordres de fabrication.", ex);
            }
        }

        /// <summary>
        /// Lance un OF en production avec vérification métier.
        /// </summary>
        public void LancerOFEnProduction(OF of, int quantite, OpcUaClientManager opcUaClient)
        {
            if (of == null)
                throw new ArgumentNullException(nameof(of));

            if (quantite <= 0)
                throw new ArgumentException("La quantité doit être supérieure à zéro.");
    
            var ofList = _ofRepository.getAll();
            var existe = ofList.Exists(x => x.Numero == of.Numero);
            if (!existe)
                throw new Exception($"L'OF {of.Numero} n'existe pas dans la base de données.");

            try
            {
                _ofRepository.insertOFEnProduction(of, quantite);
                //num chargement
                _opCUAService.ecrire("ns=3;i=1012", opcUaClient, of.NumRobotChargement);
                //num dechargement
                _opCUAService.ecrire("ns=3;i=1013", opcUaClient, of.NumRobotDechargement);
                //num gonflage
                _opCUAService.ecrire("ns=3;i=1016", opcUaClient, of.NumRobotGonflage);
                //ctrl gonflage
                _opCUAService.ecrire("ns=3;i=1017", opcUaClient, of.ControleGonflage);
                //num ctrl gonflage
                if (of.ControleGonflage)
                {
                    _opCUAService.ecrire("ns=3;i=1018", opcUaClient, of.NumControleGonflage.IdGonflage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la mise en production de l’OF.", ex);
            }
        }

        /// <summary>
        /// Récupère l'OF actuellement en cours de production.
        /// </summary>
        public OfEnCours? GetOFActuel()
        {
            return _ofRepository.GetOFEnProduction();
        }
    }
}
