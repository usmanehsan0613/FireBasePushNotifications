importScripts("https://www.gstatic.com/firebasejs/7.17.1/firebase-app.js");
importScripts("https://www.gstatic.com/firebasejs/7.17.1/firebase-messaging.js");
// For an optimal experience using Cloud Messaging, also add the Firebase SDK for Analytics.
importScripts("https://www.gstatic.com/firebasejs/7.17.1/firebase-analytics.js");

// Initialize the Firebase app in the service worker by passing in the
// messagingSenderId.
firebase.initializeApp({
    messagingSenderId: "MESSAGINGSenderID",
    apiKey: " apiKey ",
    projectId: "zupushnotif",
    appId: "x:xxxxxxxx:web:xxxxxxxxxxxxxxxx",
});

// Retrieve an instance of Firebase Messaging so that it can handle background
// messages.
const messaging = firebase.messaging();

console.log('firebase-messaging-sw.js'); 

  
self.addEventListener('notificationclick', function(event) {
    event.notification.close();
    console.log(event.notification.data);
    event.waitUntil(self.clients.openWindow(event.notification.data));
});

messaging.setBackgroundMessageHandler(function(payload) {
    
    console.log(
        "[zu-fb-service-worker.js] Received background message ",
        payload);

    // Customize notification here
    const title = payload.notification.title; //"Background Message Title";
    const options = {
        body: payload.notification.body,
        icon: payload.notification.image,
        click_action: payload.notification.click_action,
        data: payload.notification.click_action
                   
    };
  
    return self.registration.showNotification(
        title,options
        );

});



self.addEventListener('push', function (event) {
});

self.addEventListener('notificationclick', function(event) {
    event.notification.close();
    console.log(event);
    event.waitUntil(self.clients.openWindow(event.notification.data));
});

self.addEventListener('click', function(event) {
    console.log('click');
    console.log(event);
});    

self.addEventListener('show', function(event) {
    console.log('show');
    console.log(event);
});    

 
