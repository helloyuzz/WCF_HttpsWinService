using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Web;

namespace WCFSSL_Project {
    public class WCFWinService:ServiceBase {
        public ServiceHost serviceHost = null;
        public WCFWinService() {
            // Name the Windows Service
            ServiceName = "WCF_HttpsWinService";
        }

        public static void Main() {
            ServiceBase.Run(new WCFWinService());
        }// Start the Windows service.
        protected override void OnStart(string[] args) {
            if(serviceHost != null) {
                serviceHost.Close();
            }
            try {
                string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\system_tracelog.txt";
                Trace.Listeners.Add(new TextWriterTraceListener(fileName));
                Trace.AutoFlush = true;

                //string addressHttp = String.Format("http://{0}:10443/WCFWinServiceSamples/",System.Net.Dns.GetHostEntry("").HostName);
                //Uri a = new Uri(addressHttp);
                //Uri[] baseAddresses = new Uri[] { a };
                Trace.WriteLine("a");


                //serviceHost = new ServiceHost(typeof(Service1),baseAddresses);
                serviceHost = new ServiceHost(typeof(Service1));


                //WSHttpBinding b = new WSHttpBinding();
                //b.Security.Mode = SecurityMode.Transport;
                //b.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                //Trace.WriteLine("aa");
                //Type c = typeof(IService1);
                //serviceHost.AddServiceEndpoint(c,b,"Service1");                
                //Trace.WriteLine("aaa");
                //serviceHost.Credentials.ServiceCertificate.SetCertificate(StoreLocation.LocalMachine,StoreName.My,X509FindType.FindBySerialNumber,"4cf5e9eae99ae59f4d8bfc3896c3fdc9");
                //Trace.WriteLine("aaa1");

                // Create a ServiceHost for the CalculatorService type and 
                // provide the base address.

                // Open the ServiceHostBase to create listeners and start 
                // listening for messages.
                serviceHost.Open();

                //string address = serviceHost.Description.Endpoints[0].ListenUri.AbsoluteUri;
                foreach(ServiceEndpoint endpoint in serviceHost.Description.Endpoints) {
                    Trace.WriteLine("AbsoluteUri:" + endpoint.ListenUri.AbsoluteUri);
                }
            } catch(Exception exc) {
                Trace.WriteLine(exc.ToString());
                serviceHost.Close();
            }
            Trace.Close();
        }
        protected override void OnStop() {
            if(serviceHost != null) {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}