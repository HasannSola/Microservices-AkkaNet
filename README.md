# Microservices-AkkaNet
Akka.Net ile Katmanlı Mimari ile Microservis
<br/>

1-lighthouse ayağa kaldırmak içi CMD ye aşağıdaki komutları yazın. Yazdıkan sonra bir hata olmadığında en son satırda,<br/>
 "- Leader is moving node [akka.tcp://MSA@local Ip Adresi] to [Up]" bir çıktı oluşmalı.<br/>
<br/>
dotnet "C:\Users\hsola\source\repos\MSA\MSA.Lighthouse\bin\Debug\netcoreapp2.2\MSA.Lighthouse.dll"  <br/>
  hostname=[local Ip Adresi] publichostname=[local Ip Adresi] port=[Açık bir Port]<br/>
  <br/>
  
 2-Router ayağa kaldırmak için önce lighthouse CMD ile ayağa kaldırılmalı sonra Router başka bir CMD ile aşağıdaki komut yazılmalı.  <br/>
  <br/>
 dotnet  "C:\Users\hsola\source\repos\MSA\MSA.Router\bin\Debug\netcoreapp2.2\MSA.Router.dll" <br/>
 hostname=[local Ip Adresi]  publichostname=[local Ip Adresi]  seedhostname=[local Ip Adresi] port=[Açık bir Port] <br/>
 