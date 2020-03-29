using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleQueueManagementConsole
{
    class ManagementHelper
    {
        private NamespaceManager m_NamespaceManager;



        public ManagementHelper()

        {

            // Retrieve the account details from the configuration file.

            string nameSpace = ConfigurationManager.AppSettings["NameSpace"];

            string credentialName = ConfigurationManager.AppSettings["CredentialName"];

            string credentialKey = ConfigurationManager.AppSettings["CredentialKey"];





            // Create a token provider with the relevant credentials.

            TokenProvider credentials =

                TokenProvider.CreateSharedSecretTokenProvider

                (credentialName, credentialKey);





            // create a Uri for the service bus using the specified namespace

            Uri sbUri = ServiceBusEnvironment.CreateServiceUri

                ("sb", nameSpace, string.Empty);



            // Create a ServiceBusNamespaceClient for the specified namespace

            // using the specified credentials.

            m_NamespaceManager = new NamespaceManager(sbUri, credentials);



            // Set the timeout to 15 seconds.

            m_NamespaceManager.Settings.OperationTimeout = TimeSpan.FromSeconds(15);



            // Output the client address.

            Console.WriteLine("Service bus address {0}", m_NamespaceManager.Address);

        }

        public void CreateQueue(string queuePath)

        {

            Console.Write("Creating queue {0}...", queuePath);

            m_NamespaceManager.CreateQueue(queuePath);

            Console.WriteLine("Done!");

        }



        public void DeleteQueue(string queuePath)

        {

            Console.Write("Deleting queue {0}...", queuePath);

            m_NamespaceManager.DeleteQueue(queuePath);

            Console.WriteLine("Done!");

        }



        public void GetQueue(string queuePath)

        {

            QueueDescription queueDescription = m_NamespaceManager.GetQueue(queuePath);



            Console.WriteLine("Queue Path:                                   {0}",

                queueDescription.Path);

            Console.WriteLine("Queue MessageCount:                           {0}",

                queueDescription.MessageCount);

            Console.WriteLine("Queue SizeInBytes:                            {0}",

                queueDescription.SizeInBytes);

            Console.WriteLine("Queue RequiresSession:                        {0}",

                queueDescription.RequiresSession);

            Console.WriteLine("Queue RequiresDuplicateDetection:             {0}",

                queueDescription.RequiresDuplicateDetection);

            Console.WriteLine("Queue DuplicateDetectionHistoryTimeWindow:    {0}",

                queueDescription.DuplicateDetectionHistoryTimeWindow);

            Console.WriteLine("Queue LockDuration:                           {0}",

                queueDescription.LockDuration);

            Console.WriteLine("Queue DefaultMessageTimeToLive:               {0}",

                queueDescription.DefaultMessageTimeToLive);

            Console.WriteLine("Queue EnableDeadLetteringOnMessageExpiration: {0}",

                queueDescription.EnableDeadLetteringOnMessageExpiration);

            Console.WriteLine("Queue EnableBatchedOperations:                {0}",

                queueDescription.EnableBatchedOperations);

            Console.WriteLine("Queue MaxSizeInMegabytes:                     {0}",

                queueDescription.MaxSizeInMegabytes);

            Console.WriteLine("Queue MaxDeliveryCount:                       {0}",

                queueDescription.MaxDeliveryCount);

            Console.WriteLine("Queue IsReadOnly:                             {0}",

                queueDescription.IsReadOnly);

        }



        public void ListQueues()

        {

            IEnumerable<QueueDescription> queueDescriptions = m_NamespaceManager.GetQueues();

            Console.WriteLine("Lisitng queues...");

            foreach (QueueDescription queueDescription in queueDescriptions)

            {

                Console.WriteLine("\t{0}", queueDescription.Path);

            }

            Console.WriteLine("Done!");

        }


    }
}
