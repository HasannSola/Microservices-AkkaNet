start cmd /k Call "C:\Projeler\Microservices-AkkaNet\MSA.Core\Bats\1-lighthouse.bat"
timeout 5
start cmd /k Call "C:\Projeler\Microservices-AkkaNet\MSA.Core\Bats\2-router.bat"
start cmd /k Call "C:\Projeler\Microservices-AkkaNet\MSA.Core\Bats\3-worker.bat"
start cmd /k Call "C:\Projeler\Microservices-AkkaNet\MSA.Core\Bats\4-Api.bat"