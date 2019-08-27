# Kentico training Adapt Generator

This application generates JSON used by Kentico's training courses (MVC training in particular at the moment) from data stored in Kentico Cloud. Application is deployed to Azure and accessed by **production** training courses. Treat it with care.

## Production & deployment

App URL: [https://kentico-adapt-live.azurewebsites.net](https://kentico-adapt-live.azurewebsites.net)

## Web hooks

Training KC project need to define Web hooks to this application so that training materials are updated accordingly.

Web hooks URL: [https://kentico-adapt-live.azurewebsites.net/api/generate/updatecourse](https://kentico-adapt-live.azurewebsites.net/api/generate/updatecourse)
