# WCF_WinHttpsService

```doskey  
Usage ssl port :10443
```

**Makecert**
```shell
makecert -in "WCF_HttpsWinService"   // Common name
         -a "sha256"                 // The signature's digest algorithm. <md5|sha1|sha256|sha384|sha512>. 
                                     // Default to 'sha1'
         -sk "WCF_HttpsWinService"   // Subject's key container name;
         -pe                         // Mark private key as exportable
         -ss My                      // Subject's certificate store name
         -sr "CurrentUser"           // Subject's certificate store location.
                                     // CurrentUser|LocalMachine.  Default to 'CurrentUser'
         -n "CN=WCF_HttpsWinService" // Certificate subject X500 name
```

```shell
netsh http delete sslcert 0.0.0.0:10443
```
