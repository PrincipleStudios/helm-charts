## Usage

[Helm](https://helm.sh) must be installed to use the charts.  Please refer to
Helm's [documentation](https://helm.sh/docs) to get started.

To install the <chart-name> chart:

    helm install my-<release-name> --repo https://principlestudios.github.io/helm-charts <chart-name>

To uninstall the chart:

    helm delete my-<release-name>


### Single-Container

A chart that runs a single container and exposes it as a service and ingress. Supports TLS. Also supports HorizontalPodAutoscaler, as the default helm template.

To install this chart (Powershell Core):

    $fullImageName = 'alexwhen/docker-2048'
    $imageTag = 'latest'
    $k8sNamespace = 'my-site-ns'
    $releaseName = 'my-site'
    $sslClusterIssuer = 'letsencrypt'
    $domain = '2048.example.com'

    helm install -n $k8sNamespace $releaseName --create-namespace --repo https://principlestudios.github.io/helm-charts single-container `
        --set-string "image.repository=$($fullImageName)" `
        --set-string "image.tag=$imageTag" `
        --set-string "ingress.annotations.cert-manager\.io/cluster-issuer=$sslClusterIssuer" `
        --set-string "ingress.hosts[0].host=$domain"
