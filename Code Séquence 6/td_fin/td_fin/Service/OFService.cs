using td_fin.Models;
using td_fin.Repository;
using System.Collections.Generic;
using System;
using Dll_OPC;

namespace td_fin.Service
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
        // * Solution : Méthode pour récupérer l'OF en cours (Relais) *
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
        // * Solution Ex. 2 : Méthode Service (Validation & Relais)   *
        // * EXTENSION OPC UA : Écriture du nouveau programme         *
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

            // 2. EXTENSION OPC UA : Écriture du nouveau programme vers l'automate
            if (opcUaClient != null && opcUaClient.IsConnected)
            {
                try
                {
                    // Node ID pour le Programme Chargement (à créer dans Prosys : ns=3;i=1008 Type Integer)
                    const string NODE_ID_PROG_CHARGEMENT = "ns=3;i=1008";

                    // Appel du service pour écrire la nouvelle valeur
                    _opCUAService.ecrire(NODE_ID_PROG_CHARGEMENT, opcUaClient, newProgChargement);
                    Console.WriteLine($"[OPC UA] Écriture réussie : Program Chargement {newProgChargement} sur {NODE_ID_PROG_CHARGEMENT}.");
                }
                catch (Exception ex)
                {
                    // L'écriture OPC UA est critique, mais la BDD est déjà à jour. 
                    Console.WriteLine($"[OPC UA] Avertissement: Échec de l'écriture du programme de chargement. {ex.Message}");
                }
            }
        }
    }
}