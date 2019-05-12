# SoCWS-2019-Labs

## Etudiants

Calvin AUDIER & Mawuyram Frido Cédric SOEDJEDE

## Extensions

- Graphical User Interface for the client
- Security Extension
- Deployment and Virtualization
- Monitoring
- Cache Management

## Architecture du projet

- IWS : Projet qui contient le contrat et les configurations de la requête SOAP et communiquant avec l'API de JC Decaux au moyen d'une requête REST.
- IWS-Client : Projet console qui contient le code Client pour envoyer les requêtes SOAP au serveur.
- IWS-Monitoring : Projet bibliothèque qui contient les méthodes pour sauvegarder les requêtes envoyées par le client en enregistrant les paramètres, la méthode appelée et l'heure et date d'appel
- IWS-Caching : Projet bibliothèque qui contient 2 systèmes de gestion de caches différents, un pour le client et l'autre pour l'administrateur (Dans notre cas l'utilisateur VIP). De base il est mis 30secondes de cache pour faciliter les tests mais ce temps est modifiable grâce à une méthode