# Microservices-AkkaNet
Akka.Net ile Katmanlı Mimari ile Microservis
<br/>

1-lighthouse ayağa kaldırmak içi CMD ye aşağıdaki komutları yazın. Yazdıkan sonra bir hata olmadığında en son satırda,<br/>
 "- Leader is moving node [akka.tcp://MSA@local Ip Adresi] to [Up]" bir çıktı oluşmalı.<br/>
<br/>
dotnet "_dosya yolu \bin\Debug\netcoreapp2.2\MSA.Lighthouse.dll"  <br/>
  hostname=[local Ip Adresi] publichostname=[local Ip Adresi] port=[Açık bir Port]<br/>
  <br/>
  
 2-Router ayağa kaldırmak için önce lighthouse CMD ile ayağa kaldırılmalı sonra Router başka bir CMD ile aşağıdaki komut yazılmalı.  <br/>
  <br/>
 dotnet  "_dosya yolu \bin\Debug\netcoreapp2.2\MSA.Router.dll" <br/>
 hostname=[local Ip Adresi]  publichostname=[local Ip Adresi]  port=[Açık bir Port] <br/>
 
3-Worker ayağa kaldırmak için önce Router CMD ile ayağa kaldırılmalı sonra Worker başka bir CMD ile aşağıdaki komut yazılmalı.  <br/>
	Role olarak şimdilik veri eklemek için Actor tarafında tanımlanan  AddActor tanımlanmış. <br/>
	<br/>
	dotnet "_dosya yolu \bin\Debug\netcoreapp2.2\MSA.Worker.dll"  <br/>
	hostname=[local Ip Adresi]  publichostname=[local Ip Adresi]  port=[Açık bir Port] roles=AddActor <br/>
 
4-En son olarak Api katmanını ayağa kaldırmak için <br/> 
  <br/>
  dotnet "_dosya yolu \bin\Debug\netcoreapp2.2\MSA.Api.dll" hostname=[local Ip Adresi]  port=[Açık bir Port]
  
 5- Yukarıdaki adımlar sağlıklı bir şekilde çalıştıysa en son adım olarak Postman gibi araçlar ile veritabana kayıt atabiliriz.
   
   localhost:5000/api/product/Add?product={StCode:"45S0012", StName: "Bellona Sandalye " ,InCount:10}
   
 NOT : Yukarıdaki adımları tek tek yapmak yerine Core katmanında Bats klasörü içinde tanımlı olan bat dosyasını çalıştırarak tüm adımları tamamlamış oluruz.  