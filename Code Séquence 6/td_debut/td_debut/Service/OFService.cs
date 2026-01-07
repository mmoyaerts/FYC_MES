using td_debut.Models;
using td_debut.Repository;
using System.Collections.Generic;
using System;
using Dll_OPC;

namespace td_debut.Service
{
    public class OFService
    {
        private readonly OFRepository _ofRepository;
        private readonly OPCUAService _opCUAService;

        // CONSTRUCTEUR
        public OFService()
        {
            _ofRepository = new OFRepository();
            _opCUAService = new OPCUAService();
        }

        // ************************************************************
        // * Méthode pour récupérer l'OF en cours                     *
        // ************************************************************
        public OfEnCours GetOFActuel()
        {
            // Relais simple vers la couche Repository
            return _ofRepository.GetOFEnProduction();
        }


        /// Récupère tous les Ordres de Fabrication définis (table 'Of') via le Repository.
        public List<OF> GetAllOF()
        {
            // Correction : Appel du Repository
            return _ofRepository.GetAllOFs();
        }

        // ************************************************************
        // * Ex. 2 : Méthode Service (Validation & Relais)   *
        // * OPC UA : Écriture du nouveau programme         *
        // ************************************************************
        public void UpdateOFQuantite(OfEnCours ofToUpdate, int newQuantite,
                                     int newProgChargement, int newProgDechargement,
                                     OpcUaClientManager? opcUaClient) // <<< NOUVEAU PARAMÈTRE
        {
            // Validation (Logique métier)
            if (ofToUpdate == null || !ofToUpdate.IsValid)
            {
                throw new InvalidOperationException("L'OF n'est pas en production ou n'est pas valide.");
            }
            if (newQuantite <= 0)
            {
                throw new ArgumentException("La nouvelle quantité doit être supérieure à zéro.");
            }

            // 1. Relais vers la couche Repository (Exécution de la Transaction BDD)
            _ofRepository.UpdateOFEnProductionQuantite(ofToUpdate, newQuantite, newProgChargement, newProgDechargement);

            // 2. : Écriture du nouveau programme vers l'automate
            // TODO Exercice 2 (Extension OPC UA) : Implémenter la connexion et l'écriture de la consigne (newProgChargement)
            // vers le Node ID "ns=3;i=1008" sur le serveur OPC UA, en utilisant _opCUAService.ecrire.


        }
    }
}