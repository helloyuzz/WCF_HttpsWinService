using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;

namespace WCF_HttpsClient {
    public partial class Form_WCFHttpsClient:Form {
        public Form_WCFHttpsClient() {
            InitializeComponent();
        }

        private void btn_Call_Click(object sender,EventArgs e) {
            // Create the binding.  
            var myBinding = new WSHttpBinding();
            myBinding.Security.Mode = SecurityMode.Transport;
            myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

            // Create the endpoint address. Note that the machine name   
            // must match the subject or DNS field of the X.509 certificate  
            // used to authenticate the service.   
            var ea = new EndpointAddress("https://localhost:10443/WCF_HttpsWinService/service1?singleWsdl");

            // Create the client. The code for the calculator ,client is not shown here. See the sample applications  
            // for examples :
            var cc = new WCF_HttpWinService.Service1Client(myBinding,ea);
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;

            // The client must specify a certificate trusted by the server.  
            cc.ClientCredentials.ClientCertificate.SetCertificate(
                StoreLocation.LocalMachine,
                StoreName.My,
                X509FindType.FindBySerialNumber,
                "e50ac104bd00779e4bbd03e0724056fe");

            // Begin using the client.  
            string result = cc.GetData(100);

            cc.Close();

            MessageBox.Show(result);
        }

        private bool RemoteCertificateValidate(object sender,X509Certificate certificate,X509Chain chain,SslPolicyErrors sslPolicyErrors) {
            return true;
        }
    }
}
