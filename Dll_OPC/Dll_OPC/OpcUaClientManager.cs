using System;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;
using System.Threading.Tasks;

namespace Dll_OPC
{
    public class OpcUaClientManager
    {
        private Session _session;
        private ApplicationConfiguration _config;
        private readonly string _endpointUrl;
        private Subscription _subscription;

        public OpcUaClientManager(string endpointUrl)
        {
            _endpointUrl = endpointUrl;
        }

        public async Task ConnectAsync()
        {
            _config = new ApplicationConfiguration
            {
                ApplicationName = "MyOpcUaC#",
                ApplicationType = ApplicationType.Client,
                SecurityConfiguration = new SecurityConfiguration
                {
                    ApplicationCertificate = new CertificateIdentifier
                    {
                        StoreType = "Directory",
                        StorePath = "CertificateStores/MachineDefault",
                        SubjectName = "MyOpcUaC#"
                    },
                    TrustedIssuerCertificates = new CertificateTrustList
                    {
                        StoreType = "Directory",
                        StorePath = "CertificateStores/UA Certificate Authorities"
                    },
                    TrustedPeerCertificates = new CertificateTrustList
                    {
                        StoreType = "Directory",
                        StorePath = "CertificateStores/UA Applications"
                    },
                    RejectedCertificateStore = new CertificateTrustList
                    {
                        StoreType = "Directory",
                        StorePath = "CertificateStores/RejectedCertificates"
                    },
                    AutoAcceptUntrustedCertificates = true,
                    AddAppCertToTrustedStore = true
                },
                TransportConfigurations = new TransportConfigurationCollection(),
                TransportQuotas = new TransportQuotas { OperationTimeout = 15000 },
                ClientConfiguration = new ClientConfiguration { DefaultSessionTimeout = 60000 },
                DisableHiResClock = true
            };

            await _config.Validate(ApplicationType.Client);

            var app = new ApplicationInstance
            {
                ApplicationName = "MyOpcUaClient",
                ApplicationType = ApplicationType.Client,
                ApplicationConfiguration = _config
            };

            bool haveCert = await app.CheckApplicationInstanceCertificate(false, 0);
            if (!haveCert)
                throw new Exception("Certificat non valide.");

            var selectedEndpoint = CoreClientUtils.SelectEndpoint(_endpointUrl, false);
            var endpointConfig = EndpointConfiguration.Create(_config);
            var endpoint = new ConfiguredEndpoint(null, selectedEndpoint, endpointConfig);

            _session = await Session.Create(_config, endpoint, false, "OPC UA Client", 60000, null, null);

            _session.KeepAlive += Session_KeepAlive;
        }

        private void Session_KeepAlive(object sender, KeepAliveEventArgs e)
        {
            if (e.Status != null && ServiceResult.IsBad(e.Status))
            {
                Console.WriteLine("⚠️ Session KeepAlive Failed: " + e.Status);
            }
        }

        public void SubscribeToNodes(string[] nodeIds, Action<string, DataValue> onValueChanged)
        {
            if (_session == null || !_session.Connected)
                throw new Exception("Session non connectée.");

            _subscription = new Subscription(_session.DefaultSubscription)
            {
                PublishingInterval = 1000
            };

            foreach (var nodeIdStr in nodeIds)
            {
                var monitoredItem = new MonitoredItem
                {
                    StartNodeId = new NodeId(nodeIdStr),
                    AttributeId = Attributes.Value,
                    DisplayName = nodeIdStr,
                    SamplingInterval = 0,
                    QueueSize = 10,
                    DiscardOldest = true
                };

                monitoredItem.Notification += (sender, args) =>
                {
                    if (args.NotificationValue is MonitoredItemNotification notification)
                    {
                        onValueChanged?.Invoke(monitoredItem.DisplayName, notification.Value);
                    }
                };

                _subscription.AddItem(monitoredItem);
            }

            _session.AddSubscription(_subscription);
            _subscription.Create();
        }


        public string ReadValue(string nodeIdStr)
        {
            if (_session == null || !_session.Connected)
                throw new Exception("Session non connectée.");

            NodeId nodeId = new NodeId(nodeIdStr);
            DataValue val = _session.ReadValue(nodeId);
            return val.Value?.ToString() ?? "(null)";
        }

        public void Disconnect()
        {
            if (_session != null && _session.Connected)
            {
                _session.Close();
                _session.Dispose();
                _session = null;
            }
        }

        public void WriteNodeValue(string nodeId, object valueToWrite)
        {
            if (_session == null || !_session.Connected)
            {
                throw new InvalidOperationException("La session OPC UA n'est pas connectée.");
            }

            // Crée un NodeId à partir de la chaîne
            NodeId nodeToWrite = new NodeId(nodeId);

            // Crée une structure DataValue avec la valeur à écrire
            WriteValue writeValue = new WriteValue
            {
                NodeId = nodeToWrite,
                AttributeId = Attributes.Value,
                Value = new DataValue(new Variant(valueToWrite))
            };

            // Encapsule dans une collection
            WriteValueCollection valuesToWrite = new WriteValueCollection { writeValue };

            // Résultats et diagnostics
            StatusCodeCollection results;
            DiagnosticInfoCollection diagnosticInfos;

            // Appel du service Write (synchrone)
            ResponseHeader responseHeader = _session.Write(
                null,
                valuesToWrite,
                out results,
                out diagnosticInfos
            );

            // Vérifie les résultats
            if (StatusCode.IsBad(results[0]))
            {
                throw new Exception($"Erreur lors de l'écriture sur {nodeId} : {results[0]}");
            }
        }



        public bool IsConnected => _session != null && _session.Connected;
    }
}