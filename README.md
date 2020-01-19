# WCF_HttpsWinService

```doskey  
Usage ssl port :10443
```

**Makecert**
```shell
makecert -in "WCF_HttpsWinService"    // Common name
         -a "sha256"                  // The signature's digest algorithm. <md5|sha1|sha256|sha384|sha512>. 
                                      // Default to 'sha1'
         -sk "WCF_HttpsWinService"    // Subject's key container name;
         -pe                          // Mark private key as exportable
         -ss My                       // Subject's certificate store name
         -sr "CurrentUser"            // Subject's certificate store location.
                                      // CurrentUser|LocalMachine.  Default to 'CurrentUser'
         -n "CN=WCF_HttpsWinService,E=yusais_life@qq.com" // Certificate subject X500 name
```
<img src="https://github.com/helloyuzz/WCF_HttpsWinService/blob/master/screentshot/cert_hash.png" width="600px" height="800px">
**App.config**
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <system.serviceModel>
    <bindings>
      <wsHttpBinding>
        <binding name="WCFSSLBinding">
          <security mode="Transport">
            <transport clientCredentialType="Certificate"></transport> <!--important-->
            <message clientCredentialType="None"/>
          </security>
        </binding>
      </wsHttpBinding>
    </bindings>
    <services>
      <service behaviorConfiguration="WCFWinServiceBehavior" name="WCF_HttpsWinService.Service1">
        <endpoint address="" binding="mexHttpsBinding" contract="WCF_HttpsWinService.IService1" />
        <endpoint address="mex" binding="mexHttpsBinding" contract="IMetadataExchange" />
        <endpoint address="" binding="mexHttpBinding" contract="WCF_HttpsWinService.IService1" />
        <endpoint address="mex" binding="mexHttpBinding" contract="IMetadataExchange" />
        <host>
          <baseAddresses>
            <add baseAddress="https://localhost:10443/WCF_HttpsWinService/service1" />
            <add baseAddress="http://localhost:10010/WCF_HttpsWinService/service1" />
          </baseAddresses>
        </host>
      </service>
    </services>
    <behaviors>
      <serviceBehaviors>
        <behavior name="WCFWinServiceBehavior">
          <serviceMetadata httpGetEnabled="true" httpsGetEnabled="true"/> <!--important-->
          <serviceDebug includeExceptionDetailInFaults="False"/>
          <serviceCredentials>
            <serviceCertificate storeLocation="LocalMachine"
                                storeName="My" 
                                x509FindType="FindBySerialNumber" 
                                findValue="e50ac104bd00779e4bbd03e0724056fe"/> <!--important-->
          </serviceCredentials>
        </behavior>
      </serviceBehaviors>
    </behaviors>
  </system.serviceModel>
</configuration>
```

**VS2017 Command line**
```shell
cd D:\GitHub\WCF_HttpsWinService\bin

installutil.exe WCF_HttpsWinService.exe

net start WCF_HttpsWinService
```
<img src="https://github.com/helloyuzz/WCF_HttpsWinService/blob/master/screentshot/screenshot_1.png" width="800px" height="600px">  


**IE or Chrome**
```url
http://localhost:10010/WCF_HttpsWinService/service1
or 
https://localhost:10443/WCF_HttpsWinService/service1
```

<img src="https://github.com/helloyuzz/WCF_HttpsWinService/blob/master/screentshot/screenshot_2.png" width="800px" height="600px">  

**Client demo**  

<img src="https://github.com/helloyuzz/WCF_HttpsWinService/blob/master/screentshot/screenshot_3.png" width="600px">

**See also**  
[How to: Configure a Port with an SSL Certificate](https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/how-to-configure-a-port-with-an-ssl-certificate)  

[netsh](http://www.colorconsole.de/cmd/en/Windows_7/netsh.htm)

[WcfTestClient.exe](https://docs.microsoft.com/en-us/dotnet/framework/wcf/wcf-test-client-wcftestclient-exe)
